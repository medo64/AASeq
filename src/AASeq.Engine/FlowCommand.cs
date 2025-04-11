namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{CommandName,nq}")]
public sealed class FlowCommand : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="commandName">Canonical command name.</param>
    /// <param name="commandInstance">Command instance.</param>
    /// <param name="templateData">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowCommand(string commandName, CommandInstance commandInstance, AASeqNodes templateData) {
        CommandName = commandName;
        Instance = commandInstance;
        TemplateData = templateData;
    }


    internal string CommandName { get; }
    internal CommandInstance Instance { get; }
    internal AASeqNodes TemplateData { get; }

}
