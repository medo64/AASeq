namespace AASeq;
using System;

public sealed partial class Engine {

    internal static void Validate(AASeqNodes nodes, AASeqNodes matches) {
        nodes = nodes.Clone();
        matches = matches.Clone();

        for (var i = matches.Count - 1; i >= 0; i--) {
            var j = GetMatchIndex(nodes, matches[i], 0);
            if (j is not null) {
                nodes.RemoveAt(j.Value);
            } else {
                throw new InvalidOperationException($"Cannot find match for node {matches[i].Name}.");
            }
        }
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
                    var matchValue = match.Value.AsString("");
                    if (!string.Equals(node.Value.AsString(""), matchValue, StringComparison.Ordinal)) { return null; }
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
