namespace AASeq;
using System.Text.RegularExpressions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

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
        var sw = Stopwatch.StartNew();
        try {

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
        } finally {
            Debug.WriteLine($"[AASeq.Document] Validate: {sw.ElapsedMilliseconds} ms");
            Metrics.NodesValidateMilliseconds.Record(sw.ElapsedMilliseconds);
            Metrics.NodesValidateCount.Add(1);
        }
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
                ComparisonKind.eq => (MatchValue == nodeValue),
                ComparisonKind.ne => (MatchValue != nodeValue),
                ComparisonKind.lt => (MatchValue < nodeValue),
                ComparisonKind.le => (MatchValue <= nodeValue),
                ComparisonKind.gt => (MatchValue > nodeValue),
                ComparisonKind.ge => (MatchValue >= nodeValue),
                _ => false,
            };
        }
    }

}
