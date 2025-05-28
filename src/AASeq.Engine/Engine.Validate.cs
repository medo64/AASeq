namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

public sealed partial class Engine {

    internal static void Validate(AASeqNodes inputNodes, AASeqNodes matchNodes) {
        if (!TryValidate(inputNodes, matchNodes, ignoreHidden: true, out var failedNode)) {
            throw new InvalidOperationException($"Cannot validate {failedNode.Name}");
        }
    }


    internal static bool TryValidate(AASeqNodes inputNodes, AASeqNodes matchNodes, bool ignoreHidden, [MaybeNullWhen(true)] out AASeqNode failedNode) {
        var sw = Stopwatch.StartNew();
        try {
            ArgumentNullException.ThrowIfNull(matchNodes);
            var nodes = CloneWithoutHidden(inputNodes);
            matchNodes = CloneWithoutHidden(matchNodes);

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
        } finally {
            Debug.WriteLine($"[AASeq.Document] Validate: {sw.ElapsedMilliseconds} ms");
        }
    }

    internal static AASeqNodes CloneWithoutHidden(AASeqNodes input) {
        var sw = Stopwatch.StartNew();
        try {
            var clone = new AASeqNodes();
            foreach (var node in input) {
                if (node.Name.StartsWith('.')) { continue; }
                clone.Add(CloneWithoutHidden(node));
            }
            return clone;
        } finally {
            Debug.WriteLine($"[AASeq.Document] Clone: {sw.ElapsedMilliseconds} ms");
        }
    }

    internal static AASeqNode CloneWithoutHidden(AASeqNode input) {
        var clone = new AASeqNode(input.Name, input.Value);
        foreach (var property in input.Properties) {
            if (property.Key.StartsWith('.')) { continue; }
            clone.Properties.Add(property.Key, property.Value);
        }
        if (input.Nodes.Count > 0) {
            foreach (var node in input.Nodes) {
                if (node.Name.StartsWith('.')) { continue; }
                clone.Nodes.Add(CloneWithoutHidden(node));
            }
        }
        return clone;
    }

    private static int? GetMatchIndex(AASeqNodes nodes, AASeqNode match, int level) {
        // special matching for /op property
        Regex? regex = null;
        ComparisonOp? comparisonOp = null;
        if (match.Tag is Regex regex2) {
            regex = regex2;
        } else if (match.Tag is ComparisonOp comparisonOp2) {
            comparisonOp = comparisonOp2;
        } else {
            var value = match.GetPropertyValue("/op");
            if (value is not null) {
                if (value.Equals("regex", StringComparison.OrdinalIgnoreCase)) {
                    regex = new Regex(match.Value.AsString(""), RegexOptions.Compiled);
                    match.Tag = regex;
                } else if (value.Equals("eq", StringComparison.OrdinalIgnoreCase) || value.Equals('=')) {
                    comparisonOp = new ComparisonOp(ComparisonKind.eq, match.Value);
                    match.Tag = comparisonOp;
                } else if (value.Equals("ne", StringComparison.OrdinalIgnoreCase) || value.Equals("!=", StringComparison.Ordinal)) {
                    comparisonOp = new ComparisonOp(ComparisonKind.ne, match.Value);
                    match.Tag = comparisonOp;
                } else if (value.Equals("lt", StringComparison.OrdinalIgnoreCase) || value.Equals('<')) {
                    comparisonOp = new ComparisonOp(ComparisonKind.lt, match.Value);
                    match.Tag = comparisonOp;
                } else if (value.Equals("le", StringComparison.OrdinalIgnoreCase) || value.Equals("<=", StringComparison.Ordinal)) {
                    comparisonOp = new ComparisonOp(ComparisonKind.le, match.Value);
                    match.Tag = comparisonOp;
                } else if (value.Equals("gt", StringComparison.OrdinalIgnoreCase) || value.Equals('>')) {
                    comparisonOp = new ComparisonOp(ComparisonKind.gt, match.Value);
                    match.Tag = comparisonOp;
                } else if (value.Equals("ge", StringComparison.OrdinalIgnoreCase) || value.Equals(">=", StringComparison.Ordinal)) {
                    comparisonOp = new ComparisonOp(ComparisonKind.ge, match.Value);
                    match.Tag = comparisonOp;
                }
            }
        }

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
                    if (regex is not null) {  // special behavior for regex
                        if (!regex.Match(node.Value.AsString("")).Success) { return null; }
                    } else if (comparisonOp is not null) {
                        if (!comparisonOp.Match(node.Value)) { return null; }
                    } else if ((match.Value.RawValue is byte[]) || (node.Value.RawValue is byte[])) {  // special handling for byte array
                        var bytesMatch = match.Value.AsByteArray([]);
                        var bytesNode = node.Value.AsByteArray([]);
                        if (bytesMatch.Length != bytesNode.Length) { return null; }
                        for (var j = 0; j < bytesMatch.Length; j++) {
                            if (bytesMatch[j] != bytesNode[j]) { return null; }
                        }
                    } else {
                        var matchValue = match.Value.AsString("");
                        if (!string.Equals(node.Value.AsString(""), matchValue, StringComparison.Ordinal)) { return null; }
                    }
                }

                // check if properties are the same
                foreach (var property in match.Properties) {
                    if (property.Key.StartsWith('/')) { continue; }  // skip any property starting with slash (e.g. /op)
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


    private enum ComparisonKind { eq, ne, lt, le, gt, ge }
    private sealed class ComparisonOp {
        public ComparisonOp(ComparisonKind kind, AASeqValue match) {
            Kind = kind;
            MatchValue = match.AsDouble();
        }

        private readonly ComparisonKind Kind;
        private readonly double? MatchValue;

        public bool Match(AASeqValue value) {
            if (MatchValue is null) { return false; }  // not equal if null

            var nodeValue = value.AsDouble();
            if (nodeValue is null) { return false; }  // not equal if null

            return Kind switch {
                ComparisonKind.eq => (nodeValue == MatchValue),
                ComparisonKind.ne => (nodeValue != MatchValue),
                ComparisonKind.lt => (nodeValue < MatchValue),
                ComparisonKind.le => (nodeValue <= MatchValue),
                ComparisonKind.gt => (nodeValue > MatchValue),
                ComparisonKind.ge => (nodeValue >= MatchValue),
                _ => false,
            };
        }
    }

}
