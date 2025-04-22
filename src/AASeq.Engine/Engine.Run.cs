namespace AASeq;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

public sealed partial class Engine {

    private readonly Thread Thread;
    private readonly ManualResetEvent CancelEvent = new(initialState: false);
    private readonly CountdownEvent StepEvent = new(initialCount: 0);
    private readonly ManualResetEvent CanStopEvent = new(initialState: true);
    private int CurrentIsRunning;  // interlocked
    private int CurrentFlowIndex;  // interlocked
    private int CurrentStepIndex;  // interlocked

    private void Run() {
        if (FlowSequence.Count == 0) { return; }  // cannot really do anything if nothing in flow

        try {
            var currIsRunning = false;
            var executingFlows = new AASeqNode?[FlowSequence.Count];
            var executingGuids = new Guid?[FlowSequence.Count];

            while (!CancelEvent.WaitOne(0, false)) {
                var doStep = !StepEvent.IsSet;
                if (doStep) {
                    try {
                        CanStopEvent.Reset();  // prevent any stop action
                        if (!currIsRunning) {
                            currIsRunning = true;
                            Interlocked.Exchange(ref CurrentIsRunning, 1);
                        }

                        StepEvent.Signal();  // wait to be allowed to step

                        // sort out counters
                        int flowIndex;
                        Interlocked.CompareExchange(ref CurrentStepIndex, 0, FlowSequence.Count);
                        var actionIndex = Interlocked.Increment(ref CurrentStepIndex);
                        if (StepIndex == 1) {
                            Array.Clear(executingFlows);
                            Array.Clear(executingGuids);
                            flowIndex = Interlocked.Increment(ref CurrentFlowIndex);
                            OnFlowBegin(flowIndex);
                        } else {
                            flowIndex = Interlocked.CompareExchange(ref CurrentFlowIndex, 0, 0);
                        }
                        var i = actionIndex - 1;

                        // the action
                        var action = FlowSequence[actionIndex - 1];
                        if (action is FlowCommand commandAction) {

                            Debug.WriteLine($"[AASeq.Engine] {actionIndex}: Executing {commandAction.CommandName}");

                            var actionNode = new AASeqNode(commandAction.CommandName, AASeqValue.Null, commandAction.TemplateData.Clone());
                            executingFlows[i] = actionNode;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = new CancellationTokenSource(CommandTimeout);
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                commandAction.Instance
                                    .ExecuteAsync(actionNode.Nodes, token)  // TODO: process data instead of clone
                                    .Wait(token);
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                OnActionDone(flowIndex, actionIndex, action, actionNode);
                            } catch (OperationCanceledException) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", "Timeout");
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            } catch (Exception ex) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", ex.Message);
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            }

                        } else if (action is FlowMessageOut messageOutAction) {

                            Debug.WriteLine($"[AASeq.Engine] {actionIndex}: Sending {messageOutAction.MessageName}");

                            var actionNode = new AASeqNode(messageOutAction.MessageName, ">" + messageOutAction.DestinationName, messageOutAction.TemplateData.Clone());
                            var id = (messageOutAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageOutAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = new CancellationTokenSource(SendTimeout);
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                messageOutAction.DestinationInstance
                                    .SendAsync(id, messageOutAction.MessageName, actionNode.Nodes, token)  // TODO: process data instead of clone
                                    .Wait(token);
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                OnActionDone(flowIndex, actionIndex, action, actionNode);
                            } catch (OperationCanceledException) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", "Timeout");
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            } catch (Exception ex) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", ex.Message);
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            }

                        } else if (action is FlowMessageIn messageInAction) {

                            Debug.WriteLine($"[AASeq.Engine] {actionIndex}: Receiving {messageInAction.MessageName}");

                            var actionNode = new AASeqNode(messageInAction.MessageName, "<" + messageInAction.SourceName);  // TODO: process data instead of clone
                            var id = (messageInAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageInAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = new CancellationTokenSource(ReceiveTimeout);
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                var messageName = messageInAction.MessageName;
                                var task = messageInAction.SourceInstance
                                    .ReceiveAsync(id, messageName, token);
                                task.Wait(token);
                                (messageName, var nodes) = task.Result;
                                sw.Stop();
                                var responseNode = new AASeqNode(messageName, "<" + messageInAction.SourceName, nodes);
                                responseNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                try {
                                    Validate(nodes, messageInAction.TemplateData);
                                    OnActionDone(flowIndex, actionIndex, action, responseNode);
                                } catch (InvalidOperationException ex2) {
                                    responseNode.Properties.Add("exception", ex2.Message);
                                    OnActionError(flowIndex, actionIndex, action, responseNode);
                                }
                            } catch (OperationCanceledException) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", "Timeout");
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            } catch (Exception ex) {
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                actionNode.Properties.Add("exception", ex.Message);
                                OnActionError(flowIndex, actionIndex, action, actionNode);
                            }

                        } else {
                            throw new ArgumentException($"Unknown action type '{action.GetType().Name}'.");
                        }

                        if (actionIndex == FlowSequence.Count) { OnFlowDone(flowIndex); }
                    } finally {
                        CanStopEvent.Set();  // signal it's a good place to stop
                    }
                } else if (currIsRunning) {  // stop running
                    currIsRunning = false;
                    Interlocked.Exchange(ref CurrentIsRunning, 0);
                } else {  // wait for the next step
                    Thread.Sleep(100);
                }
            }
        } catch (ThreadAbortException) { }
    }


    internal static void Validate(AASeqNodes nodes, AASeqNodes matchNodes) {
        if (!nodes.TryValidate(matchNodes, out var failedNode)) {
            throw new InvalidOperationException($"Cannot find match for node {failedNode.Name}.");
        }
    }

}
