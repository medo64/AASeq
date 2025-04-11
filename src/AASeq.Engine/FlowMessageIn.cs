namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq} ←{SourceName,nq} ({SourceInstance.PluginName,nq})")]
public sealed class FlowMessageIn : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="sourceName">Destination name.</param>
    /// <param name="sourceInstance">Source endpoint instance.</param>
    /// <param name="blueprintData">Data nodes.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageIn(string messageName, string sourceName, EndpointInstance sourceInstance, AASeqNodes blueprintData) {
        MessageName = messageName;
        SourceName = sourceName;
        SourceInstance = sourceInstance;
        BlueprintData = blueprintData;
    }


    internal string MessageName { get; }
    internal string SourceName { get; }
    internal EndpointInstance SourceInstance { get; }
    internal AASeqNodes BlueprintData { get; }


    AASeqNode IFlowAction.GetDefinitionNode() {
        var definitionNode = new AASeqNode(MessageName, "<" + SourceName);
        foreach (var dataNode in BlueprintData) {
            definitionNode.Nodes.Add(dataNode.Clone());
        }
        return definitionNode;
    }

}
