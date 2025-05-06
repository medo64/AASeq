namespace AASeq;
using System;

public sealed partial class AASeqNodes {

    /// <summary>
    /// Finds node at a given path, returns it, and removes it from the collection.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will be returned.
    /// All serach starts at the root of the current node.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    public AASeqNode? ConsumeNode(string nodePath) {
        ArgumentNullException.ThrowIfNull(nodePath);
        var nodeNames = nodePath.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (nodeNames.Length == 0) { throw new ArgumentOutOfRangeException(nameof(nodePath), "Must have node name."); }

        var nodes = this;
        for (var n = 0; n < nodeNames.Length; n++) {
            var nodeName = nodeNames[n];
            var isLast = (n == nodeNames.Length - 1);
            for (var i = nodes.Count - 1; i >= 0; i--) {
                if (nodes[i].Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)) {
                    if (isLast) {
                        var resNode = nodes[i];
                        nodes.RemoveAt(i);
                        return resNode;
                    }
                    nodes = nodes[i].Nodes;
                    break;
                }
            }
        }
        return null;
    }

}
