namespace AASeq;
using System.Text.RegularExpressions;
using System;
using System.Diagnostics.CodeAnalysis;

public sealed partial class AASeqNodes {

    /// <summary>
    /// Tries to validate if current nodes match the given nodes.
    /// </summary>
    /// <param name="matchNodes">Nodes to match.</param>
    public bool TryValidate(AASeqNodes matchNodes) {
        return TryValidate(matchNodes, out _);
    }

    /// <summary>
    /// Tries to validate if current nodes match the given nodes.
    /// </summary>
    /// <param name="matchNodes">Nodes to match.</param>
    /// <param name="failedNode">Node that failed to match.</param>
    public bool TryValidate(AASeqNodes matchNodes, [MaybeNullWhen(true)] out AASeqNode failedNode) {
        ArgumentNullException.ThrowIfNull(matchNodes);
        var nodes = Clone();
        matchNodes = matchNodes.Clone();

        for (var i = matchNodes.Count - 1; i >= 0; i--) {
            var j = GetMatchIndex(nodes, matchNodes[i], 0);
            if (j is not null) {
                nodes.RemoveAt(j.Value);
            } else {
                failedNode = matchNodes[i];
                return false;
            }
        }

        failedNode = null;
        return true;
    }

    private static int? GetMatchIndex(AASeqNodes nodes, AASeqNode match, int level) {
        for (var i = 0; i < nodes.Count; i++) {
            var node = nodes[i];

            // check if name matches
            if (string.Equals(node.Name, match.Name, StringComparison.OrdinalIgnoreCase)) {
                // check if values are the same
                if (match.Value.IsNull && !node.Value.IsNull) {
                    return null;  // missing value
                } else if (!match.Value.IsNull && node.Value.IsNull) {
                    return null;  // missing value
                } else if (!match.Value.IsNull && !node.Value.IsNull) {
                    if (match.Value.Value is Regex regex) {  // special behavior for regex
                        if (!regex.Match(node.Value.AsString("")).Success) { return null; }
                    } else {
                        var matchValue = match.Value.AsString("");
                        if (!string.Equals(node.Value.AsString(""), matchValue, StringComparison.Ordinal)) { return null; }
                    }
                }

                // check if properties are the same
                foreach (var property in match.Properties) {
                    var propValue = node.GetPropertyValue(property.Key);
                    if (propValue is null) { return null; }  // missing property
                    if (!string.Equals(propValue, property.Value, StringComparison.OrdinalIgnoreCase)) { return null; }
                }

                // check if nodes are the same 
                if ((match.Nodes.Count > 0) && (node.Nodes.Count > 0)) {
                    for (var j = 0; j < match.Nodes.Count; j++) {
                        var k = GetMatchIndex(node.Nodes, match.Nodes[j], level + 1);
                        if (k is not null) {
                            return i;  // return parent's ID
                        }
                    }
                    continue;
                } else if ((match.Nodes.Count == 0) && (node.Nodes.Count > 0)) {
                    return null;  // too many nodes
                } else if ((match.Nodes.Count > 0) && (node.Nodes.Count == 0)) {
                    return null;  // not enough nodes
                }

                return i;  // all matched and no children
            }
        }

        return null;  // no match
    }

}
