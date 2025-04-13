namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq} →{DestinationName,nq} ({DestinationInstance.PluginName,nq}/{DestinationInstance.PluginInstanceIndex,nq})")]
internal sealed class FlowMessageOut : IFlowAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="destinationName">Destination name.</param>
    /// <param name="destinationInstance">Destination endpoint instance.</param>
    /// <param name="templateData">Data nodes.</param>
    /// <param name="matchId">Match ID.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageOut(string messageName, string destinationName, EndpointInstance destinationInstance, AASeqNodes templateData, string? matchId) {
        MessageName = messageName;
        DestinationName = destinationName;
        DestinationInstance = destinationInstance;
        TemplateData = templateData;
        MatchId = matchId ?? string.Empty;
    }


    internal string MessageName { get; }
    internal string DestinationName { get; }
    internal EndpointInstance DestinationInstance { get; }
    internal AASeqNodes TemplateData { get; }
    internal string MatchId { get; }  // used for matching out/in or in/out pairs

    internal int? RequestForActionIndex { get; set; }  // used only internally for matching
    internal int? ResponseToActionIndex { get; set; }  // used only internally for matching

    AASeqNode IFlowAction.DefinitionNode {
        get {
            var definitionNode = new AASeqNode(MessageName, ">" + DestinationName);
            foreach (var dataNode in TemplateData) {
                definitionNode.Nodes.Add(dataNode.Clone());
            }
            return definitionNode;
        }
    }

}
