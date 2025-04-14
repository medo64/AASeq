namespace AASeq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// AASeq properties.
/// </summary>
[DebuggerDisplay("{Count == 1 ? \"1 property\" : Count + \" properties\",nq}")]
public sealed partial class AASeqProperties : IDictionary<string, string> {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public AASeqProperties() { }


    private readonly SortedDictionary<string, string> BaseDict = new(StringComparer.OrdinalIgnoreCase);


    #region IDictionary<string, string>

    /// <summary>
    /// Gets or sets the item with the specified key.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <exception cref="NotImplementedException"></exception>
    public string this[string key] {
        get => BaseDict[key];
        set {
            BaseDict.Remove(key);
            BaseDict[key]= value;
        }
    }

    /// <summary>
    /// Gets an collection containing all keys.
    /// </summary>
    public ICollection<string> Keys => BaseDict.Keys;

    /// <summary>
    /// Gets an collection containing all values.
    /// </summary>
    public ICollection<string> Values => BaseDict.Values;

    /// <summary>
    /// Gets the number of items.
    /// </summary>
    public int Count => BaseDict.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds an item with the provided key and value.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="value">Value.</param>
    public void Add(string key, string value) {
        BaseDict.Add(key, value);
    }

    /// <summary>
    /// Adds an item with the provided key and value.
    /// </summary>
    /// <param name="item">Item.</param>
    public void Add(KeyValuePair<string, string> item) {
        ((ICollection<KeyValuePair<string, string>>)BaseDict).Add(item);
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    public void Clear() {
        BaseDict.Clear();
    }

    /// <summary>
    /// Determines whether the collection contains an item with the specified key.
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Contains(KeyValuePair<string, string> item) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns true if the collection contains the specified key.
    /// </summary>
    /// <param name="key">Key.</param>
    public bool ContainsKey(string key) {
        return BaseDict.ContainsKey(key);
    }

    /// <summary>
    /// Copies the items of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <param name="arrayIndex">Index.</param>
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) {
        BaseDict.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the item with the specified key.
    /// </summary>
    /// <param name="key">Key.</param>
    public bool Remove(string key) {
        return BaseDict.Remove(key);
    }

    /// <summary>
    /// Removes the item with the specified key.
    /// </summary>
    /// <param name="item">Item.</param>
    public bool Remove(KeyValuePair<string, string> item) {
        return ((ICollection<KeyValuePair<string, string>>)BaseDict).Remove(item);
    }

    /// <summary>
    /// Tries to get the value associated with the specified key.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="value">Value.</param>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value) {
        return BaseDict.TryGetValue(key, out value);
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
        return ((ICollection<KeyValuePair<string, string>>)BaseDict).GetEnumerator();
    }

    /// <summary>
    /// Gets an enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
        return BaseDict.GetEnumerator();
    }

    #endregion IDictionary<string, string>

}
