namespace AASeq;
using System;

public sealed partial class AASeqNodes {

    /// <summary>
    /// Finds node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    public AASeqNode? FindNode(string nodePath) {
        ArgumentNullException.ThrowIfNull(nodePath);
        var nodeNames = nodePath.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (nodeNames.Length == 0) { throw new ArgumentOutOfRangeException(nameof(nodePath), "Must have node name."); }

        var nodes = this;
        for (var n = 0; n < nodeNames.Length; n++) {
            var nodeName = nodeNames[n];
            var isLast = (n == nodeNames.Length - 1);
            for (var i = nodes.Count - 1; i >= 0; i--) {
                if (nodes[i].Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)) {
                    if (isLast) { return nodes[i]; }
                    nodes = nodes[i].Nodes;
                    break;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    public AASeqValue? FindValue(string nodePath) {
        ArgumentNullException.ThrowIfNull(nodePath);
        var nodeNames = nodePath.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (nodeNames.Length == 0) { throw new ArgumentOutOfRangeException(nameof(nodePath), "Must have node name."); }

        var nodes = this;
        for (var n = 0; n < nodeNames.Length; n++) {
            var nodeName = nodeNames[n];
            var isLast = (n == nodeNames.Length - 1);
            for (var i = nodes.Count - 1; i >= 0; i--) {
                if (nodes[i].Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)) {
                    if (isLast) { return nodes[i].Value; }
                    nodes = nodes[i].Nodes;
                    break;
                }
            }
        }
        return null;
    }


    /// <summary>
    /// Gets/sets value based on slash (/) separated path.
    /// Get will not create new nodes.
    /// Set will create new nodes.
    /// If there are any duplicates, along the path, only last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    public AASeqValue this[string nodePath] {
        get {
            ArgumentNullException.ThrowIfNull(nodePath);
            var nodeNames = nodePath.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (nodeNames.Length == 0) { throw new ArgumentOutOfRangeException(nameof(nodePath), "Must have node name."); }

            var nodes = this;
            for (var n = 0; n < nodeNames.Length; n++) {
                var nodeName = nodeNames[n];
                var isLast = (n == nodeNames.Length - 1);
                for (var i = nodes.Count - 1; i >= 0; i--) {
                    if (nodes[i].Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)) {
                        if (isLast) { return nodes[i].Value; }
                        nodes = nodes[i].Nodes;
                        break;
                    }
                }
            }
            return AASeqValue.Null;
        }
        set {
            ArgumentNullException.ThrowIfNull(nodePath);
            var nodeNames = nodePath.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (nodeNames.Length == 0) { throw new ArgumentOutOfRangeException(nameof(nodePath), "Must have node name."); }

            var nodes = this;
            for (var n = 0; n < nodeNames.Length; n++) {
                var nodeName = nodeNames[n];
                var isLast = (n == nodeNames.Length - 1);
                AASeqNode? foundNode = null;
                for (var i = nodes.Count - 1; i >= 0; i--) {
                    if (nodes[i].Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)) {
                        if (foundNode == null) {  // find the last one
                            foundNode = nodes[i];
                        } else {  // remove all except the last one
                            nodes.RemoveAt(i);
                        }
                    }
                }
                if (foundNode == null) {  // create new one if nothing is found
                    foundNode = new AASeqNode(nodeName);
                    nodes.Add(foundNode);
                }
                if (isLast) {  // all traversed
                    foundNode.Value = value;
                } else {  // continue
                    nodes = foundNode.Nodes;
                }
            }
        }
    }

}
