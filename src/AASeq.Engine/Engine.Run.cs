namespace AASeq;
using System;
using System.Diagnostics;
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

                        Interlocked.CompareExchange(ref CurrentStepIndex, 0, FlowSequence.Count);
                        var stepIndex = Interlocked.Increment(ref CurrentStepIndex);
                        if (StepIndex == 1) {
                            Interlocked.Increment(ref CurrentFlowIndex);
                            Array.Clear(executingFlows);
                            Array.Clear(executingGuids);
                        } else {
                            Interlocked.CompareExchange(ref CurrentFlowIndex, 0, 0);
                        }
                        var i = stepIndex - 1;

                        var action = FlowSequence[stepIndex - 1];
                        if (action is FlowCommand commandAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Executing {commandAction.CommandName}");

                            var actionNode = new AASeqNode(commandAction.CommandName, AASeqValue.Null, commandAction.TemplateData.Clone());
                            executingFlows[i] = actionNode;
                            commandAction.Instance.TryExecute(actionNode.Nodes);  // TODO: process data instead of clone

                        } else if (action is FlowMessageOut messageOutAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Sending {messageOutAction.MessageName}");

                            var actionNode = new AASeqNode(messageOutAction.MessageName, ">" + messageOutAction.DestinationName, messageOutAction.TemplateData.Clone());
                            var id = (messageOutAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageOutAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;
                            messageOutAction.DestinationInstance.TrySend(id, messageOutAction.MessageName, actionNode.Nodes);  // TODO: process data instead of clone

                        } else if (action is FlowMessageIn messageInAction) {

                            Debug.WriteLine($"[AASeq.Engine] {stepIndex}: Receiving {messageInAction.MessageName}");

                            var actionNode = new AASeqNode(messageInAction.MessageName, "<" + messageInAction.SourceName, messageInAction.TemplateData.Clone());  // TODO: process data instead of clone
                            var id = (messageInAction.ResponseToActionIndex != null)
                                   ? executingGuids[messageInAction.ResponseToActionIndex.Value]!.Value
                                   : Guid.NewGuid();
                            executingFlows[i] = actionNode;
                            executingGuids[i] = id;
                            messageInAction.SourceInstance.TryReceive(Guid.NewGuid(), out var _, out var _);

                        } else {
                            throw new ArgumentException($"Unknown action type '{action.GetType().Name}'.");
                        }
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
