namespace AASeq;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{CommandName,nq}")]
internal sealed class FlowCommand : IFlowCommandAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="commandName">Canonical command name.</param>
    /// <param name="commandInstance">Command instance.</param>
    /// <param name="templateData">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowCommand(ILogger logger, CommandPlugin plugin, AASeqNodes templateData) {
        Logger = new Logger(logger, plugin.Name);
        Plugin = plugin;
        TemplateData = templateData;
    }


    internal ILogger Logger { get; }
    internal CommandPlugin Plugin { get; }
    internal AASeqNodes TemplateData { get; }

    /// <summary>
    /// Gets command name.
    /// </summary>
    public string CommandName => Plugin.Name;


    AASeqNode IFlowAction.DefinitionNode {
        get {
            var definitionNode = new AASeqNode(CommandName);
            foreach (var dataNode in TemplateData) {
                definitionNode.Nodes.Add(dataNode.Clone());
            }
            return definitionNode;
        }
    }

}
