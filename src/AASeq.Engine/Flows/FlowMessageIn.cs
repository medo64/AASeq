namespace AASeq;
using System;
using System.Diagnostics;

/// <summary>
/// Flow.
/// </summary>
[DebuggerDisplay("{MessageName,nq} ←{SourceName,nq} ({SourceInstance.PluginName,nq}/{SourceInstance.PluginInstanceIndex,nq})")]
internal sealed class FlowMessageIn : IFlowMessageInAction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="sourceName">Destination name.</param>
    /// <param name="sourceInstance">Source endpoint instance.</param>
    /// <param name="templateData">Data nodes.</param>
    /// <param name="matchId">Match ID.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal FlowMessageIn(string messageName, string sourceName, EndpointInstance sourceInstance, AASeqNodes templateData, string? matchId) {
        MessageName = messageName;
        SourceName = sourceName;
        SourceInstance = sourceInstance;
        TemplateData = templateData;
        MatchId = matchId ?? string.Empty;
    }


    /// <summary>
    /// Gets source name.
    /// </summary>
    public string SourceName { get; }

    /// <summary>
    /// Gets message name.
    /// </summary>
    public string MessageName { get; }

    internal EndpointInstance SourceInstance { get; }
    internal AASeqNodes TemplateData { get; }
    internal string MatchId { get; }  // used for matching out/in or in/out pairs

    internal int? RequestForActionIndex { get; set; }  // used only internally for matching
    internal int? ResponseToActionIndex { get; set; }  // used only internally for matching


    AASeqNode IFlowAction.DefinitionNode {
        get {
            var definitionNode = new AASeqNode(MessageName, "<" + SourceName);
            if (!string.IsNullOrEmpty(MatchId)) { definitionNode.Properties.Add("/match", MatchId); }
            foreach (var dataNode in TemplateData) {
                definitionNode.Nodes.Add(dataNode.Clone());
            }
            return definitionNode;
        }
    }

}
