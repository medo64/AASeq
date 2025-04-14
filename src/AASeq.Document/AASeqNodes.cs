namespace AASeq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// AASeq properties.
/// </summary>
[DebuggerDisplay("{Count == 1 ? \"1 node\" : Count + \" nodes\",nq}")]
public sealed partial class AASeqNodes : IList<AASeqNode> {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public AASeqNodes() { }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    public AASeqNodes(AASeqNodes nodes) {
        ArgumentNullException.ThrowIfNull(nodes);
        BaseList.AddRange(nodes);
    }


    private readonly List<AASeqNode> BaseList = [];


    #region IList<AASeqNode>

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">Index.</param>
    public AASeqNode this[int index] {
        get => BaseList[index];
        set => BaseList[index] = value;
    }

    /// <summary>
    /// Gets the number of items.
    /// </summary>
    public int Count => BaseList.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets the index of the specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public int IndexOf(AASeqNode item) {
        return BaseList.IndexOf(item);
    }

    /// <summary>
    /// Inserts the specified item at the specified index.
    /// </summary>
    /// <param name="index">Index.</param>
    /// <param name="item">Item.</param>
    public void Insert(int index, AASeqNode item) {
        if (item is null) { return; }  // skip over empty items
        BaseList.Insert(index, item);
    }

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">Index.</param>
    public void RemoveAt(int index) {
        BaseList.RemoveAt(index);
    }

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void Add(AASeqNode item) {
        if (item is null) { return; }  // skip over empty items
        BaseList.Add(item);
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    public void Clear() {
        BaseList.Clear();
    }

    /// <summary>
    /// Checks if the item is in the collection.
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns></returns>
    public bool Contains(AASeqNode item) {
        return BaseList.Contains(item);
    }

    /// <summary>
    /// Copies all the items to the specified array.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <param name="arrayIndex">Starting index.</param>
    public void CopyTo(AASeqNode[] array, int arrayIndex) {
        BaseList.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the item.
    /// </summary>
    /// <param name="item">Item.</param>
    public bool Remove(AASeqNode item) {
        return BaseList.Remove(item);
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    public IEnumerator<AASeqNode> GetEnumerator() {
        return ((IList<AASeqNode>)BaseList).GetEnumerator();
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
        return BaseList.GetEnumerator();
    }

    #endregion IList<AASeqNode>


    /// <summary>
    /// Returns the clone of the nodes.
    /// </summary>
    public AASeqNodes Clone() {
        var sw = Stopwatch.StartNew();
        try {
            var clone = new AASeqNodes();
            foreach (var node in this) {
                clone.Add(node.Clone());
            }
            return clone;
        } finally {
            Debug.WriteLine($"[AASeqNodes] Clone: {sw.ElapsedMilliseconds} ms");
        }
    }


    /// <summary>
    /// Gets an empty instance.
    /// </summary>
    public static AASeqNodes Empty => [];


    #region Helpers

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private const int BufferSize = 128 * 1024;  // 128 KB

    #endregion Helpers

}
