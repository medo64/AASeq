namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{Instance.PluginName,nq}")]
public sealed class FlowCommand : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="commandInstance">Command instance.</param>
    /// <param name="data">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowCommand(CommandInstance commandInstance, AASeqNodes data) {
        Instance = commandInstance;
        Data = data;
    }


    private CommandInstance Instance { get; }
    private AASeqNodes Data { get; }

    internal void Execute() {
        Instance.Execute(Data);
    }

}
