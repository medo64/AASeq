using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace AASeq;

/// <summary>
/// Collection of endpoints.
/// </summary>
[DebuggerDisplay("{Count} endpoints")]
public sealed class AAEndpointCollection : IList<AAEndpoint> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public AAEndpointCollection() {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="items">Default items of the collection.</param>
    public AAEndpointCollection(ICollection<AAEndpoint> items) {
        if (items != null) {
            foreach (var item in items) {
                Add(item);
            }
        }
    }


    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly List<AAEndpoint> BaseCollection = new();
    internal readonly Dictionary<string, AAEndpoint> NameLookupDictionary = new(AAEndpoint.NameComparer);


    #region Index

    /// <summary>
    /// Gets item based on a name or null if item cannot be found.
    /// </summary>
    /// <param name="name">Name.</param>
    public AAEndpoint? this[string name] {
        get {
            if ((name != null) && NameLookupDictionary.TryGetValue(name, out var item)) {
                return item;
            } else {
                return null;
            }
        }
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="name">Name of the item to locate.</param>
    /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
    public bool Contains(string name) {
        return (name == null) ? false : NameLookupDictionary.ContainsKey(name);
    }

    /// <summary>
    /// Removes the item from the collection.
    /// </summary>
    /// <param name="name">Name of the item to remove.</param>
    /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
    public bool Remove(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }

        if (NameLookupDictionary.TryGetValue(name, out var item)) {
            BaseCollection.Remove(item);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Moves item in the collection
    /// </summary>
    /// <param name="moveFrom">Index of item's source.</param>
    /// <param name="moveTo">Index of item's destination.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Index out of range.</exception>
    public void MoveItem(int moveFrom, int moveTo) {
        if ((moveFrom < 0) || (moveFrom >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveFrom), "Index out of range."); }
        if ((moveTo < 0) || (moveTo >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveTo), "Index out of range."); }

        if (moveFrom == moveTo) { return; }

        var item = BaseCollection[moveFrom];
        BaseCollection.RemoveAt(moveFrom);
        if (moveFrom > moveTo) { //move before
            BaseCollection.Insert(moveTo, item);
        } else { //move after
            BaseCollection.Insert(moveTo, item);
        }
    }

    #endregion


    #region ICollection

    /// <summary>
    /// Adds an item.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Duplicate name in collection.</exception>
    public void Add(AAEndpoint item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        if (this[item.Name] != null) { throw new ArgumentOutOfRangeException(nameof(item), "Duplicate name in collection."); }
        if (NameLookupDictionary.ContainsKey(item.Name)) { throw new ArgumentOutOfRangeException(nameof(item), "Duplicate name in collection."); }

        BaseCollection.Add(item);
        NameLookupDictionary.Add(item.Name, item);
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    public void Clear() {
        if (BaseCollection.Count > 0) {
            BaseCollection.Clear();
            NameLookupDictionary.Clear();
        }
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    public bool Contains(AAEndpoint item) {
        if (item == null) { return false; }
        return BaseCollection.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(AAEndpoint[] array, int arrayIndex) {
        BaseCollection.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Gets the number of items contained in the collection.
    /// </summary>
    public int Count {
        get { return BaseCollection.Count; }
    }

    /// <summary>
    /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire collection.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    public int IndexOf(AAEndpoint item) {
        return BaseCollection.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count. -or- Duplicate name in collection.</exception>
    public void Insert(int index, AAEndpoint item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        if (this[item.Name] != null) { throw new ArgumentOutOfRangeException(nameof(item), "Duplicate name in collection."); }
        if (NameLookupDictionary.ContainsKey(item.Name)) { throw new ArgumentOutOfRangeException(nameof(item), "Duplicate name in collection."); }

        BaseCollection.Insert(index, item);
        NameLookupDictionary.Add(item.Name, item);
    }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly {
        get { return false; }
    }

    /// <summary>
    /// Removes the item from the collection.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Item does not belong to this collection.</exception>
    public bool Remove(AAEndpoint item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }

        if (BaseCollection.Remove(item) && NameLookupDictionary.Remove(item.Name)) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count.</exception>
    public void RemoveAt(int index) {
        var item = this[index];
        BaseCollection.Remove(item);
        NameLookupDictionary.Remove(item.Name);
    }
    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    public IEnumerator<AAEndpoint> GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a non-generic collection.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection.</exception>
    public AAEndpoint this[int index] {
        get { return BaseCollection[index]; }
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }

            var item = this[index];
            if (!value.Name.Equals(item.Name, AAEndpoint.NameComparison) && NameLookupDictionary.ContainsKey(value.Name)) { throw new ArgumentOutOfRangeException(nameof(value), "Duplicate name in collection."); }

            BaseCollection.RemoveAt(index);
            NameLookupDictionary.Remove(item.Name);

            BaseCollection.Insert(index, value);
            NameLookupDictionary.Add(value.Name, item);
        }
    }

    #endregion


    /// <summary>
    /// Returns next unique name.
    /// </summary>
    /// <param name="baseName">Base name to which numeric suffix will be added.</param>
    /// <exception cref="System.ArgumentNullException">Base name cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Base name contains invalid characters.</exception>
    public string GetUniqueName(string baseName) {
        if (baseName == null) { throw new ArgumentNullException(nameof(baseName), "Base name cannot be null."); }
        if (!AAEndpoint.IsNameValid(baseName)) { throw new ArgumentNullException(nameof(baseName), "Base name contains some invalid characters."); }
        var index = 1;
        var name = baseName;
        while (this[name] != null) {
            index++;
            name = string.Format(CultureInfo.InvariantCulture, "{0}{1}", baseName, index);
        }
        return name;
    }

}
