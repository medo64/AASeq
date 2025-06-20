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

                            var data = CurrVariables.GetExpanded(commandAction.TemplateData);
                            var actionNode = new AASeqNode(commandAction.CommandName, AASeqValue.Null, data);
                            executingFlows[i] = actionNode;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = (CommandTimeout < TimeSpan.MaxValue)
                                ? new CancellationTokenSource(CommandTimeout)
                                : new CancellationTokenSource();
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                var responseNode = new AASeqNode(
                                    commandAction.CommandName,
                                    AASeqValue.Null,
                                    commandAction.Plugin.Execute(commandAction.Logger, actionNode.Nodes, token)
                                );
                                responseNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                OnActionDone(flowIndex, actionIndex, action, responseNode);
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

                            var data = CurrVariables.GetExpanded(messageOutAction.TemplateData);
                            var actionNode = new AASeqNode(messageOutAction.MessageName, (messageOutAction.SkipMatching ? ">>" : ">") + messageOutAction.DestinationName, data);
                            var id = (messageOutAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageOutAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = (SendTimeout < TimeSpan.MaxValue)
                                ? new CancellationTokenSource(SendTimeout)
                                : new CancellationTokenSource();
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                var responseNode = new AASeqNode(
                                    messageOutAction.MessageName,
                                    (messageOutAction.SkipMatching ? ">>" : ">") + messageOutAction.DestinationName,
                                    messageOutAction.DestinationInstance.Send(id, messageOutAction.MessageName, actionNode.Nodes, token)
                                );
                                actionNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                OnActionDone(flowIndex, actionIndex, action, responseNode);
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

                            var data = CurrVariables.GetExpanded(messageInAction.TemplateData);
                            var actionNode = new AASeqNode(messageInAction.MessageName, (messageInAction.SkipMatching ? "<<" : "<") + messageInAction.SourceName, data);
                            var id = (messageInAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageInAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;

                            OnActionStart(flowIndex, actionIndex, action, actionNode);
                            using var cts = (ReceiveTimeout < TimeSpan.MaxValue)
                                ? new CancellationTokenSource(ReceiveTimeout)
                                : new CancellationTokenSource();
                            var token = cts.Token;

                            var sw = Stopwatch.StartNew();
                            try {
                                var messageName = messageInAction.MessageName;
                                (messageName, var nodes) = messageInAction.SourceInstance.Receive(id, messageName, actionNode.Nodes, token);
                                sw.Stop();
                                var responseNode = new AASeqNode(messageName, "<" + messageInAction.SourceName, nodes);
                                responseNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                try {
                                    Validate(nodes, messageInAction.TemplateData, Variables);
                                    OnActionDone(flowIndex, actionIndex, action, responseNode);
                                } catch (InvalidOperationException ex2) {
                                    responseNode.Properties.Add("exception", ex2.Message);
                                    OnActionError(flowIndex, actionIndex, action, responseNode);
                                }
                            } catch (OperationCanceledException) {
                                var errNode = new AASeqNode(actionNode.Name, actionNode.Value);
                                errNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                errNode.Properties.Add("exception", "Timeout");
                                OnActionError(flowIndex, actionIndex, action, errNode);
                            } catch (Exception ex) {
                                var errNode = new AASeqNode(actionNode.Name, actionNode.Value);
                                errNode.Properties.Add("elapsed", sw.Elapsed.TotalMilliseconds.ToString("0.0'ms'", CultureInfo.InvariantCulture));
                                errNode.Properties.Add("exception", ex.Message);
                                OnActionError(flowIndex, actionIndex, action, errNode);
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

}
