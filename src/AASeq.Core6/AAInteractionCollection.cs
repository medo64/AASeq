using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AASeq;

/// <summary>
/// Collection of interactions.
/// </summary>
[DebuggerDisplay("{Count} interactions")]
public sealed class AAInteractionCollection : IList<AAInteraction> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public AAInteractionCollection() {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="defaultItems">Default items of the collection.</param>
    public AAInteractionCollection(ICollection<AAInteraction> defaultItems) {
        if (defaultItems != null) {
            foreach (var item in defaultItems) {
                Add(item);
            }
        }
    }


    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly List<AAInteraction> BaseCollection = new();


    #region Index

    /// <summary>
    /// Gets item based on a name or null if item cannot be found.
    /// </summary>
    /// <param name="name">Name.</param>
    public AAInteraction? this[string name] {
        get {
            foreach (var item in BaseCollection) {
                if (AAInteraction.NameComparer.Equals(item.Name, name)) {
                    return item;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="name">Name of the item to locate.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    public bool Contains(string name) {
        foreach (var item in BaseCollection) {
            if (AAInteraction.NameComparer.Equals(item.Name, name)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes the item from the collection.
    /// </summary>
    /// <param name="name">Name of the item to remove.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    public bool Remove(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }

        bool anyChanged = false;
        for (int i = BaseCollection.Count - 1; i >= 0; i--) {
            if (AAInteraction.NameComparer.Equals(BaseCollection[i].Name, name)) {
                BaseCollection.RemoveAt(i);
                anyChanged = true;
            }
        }
        return anyChanged;
    }

    /// <summary>
    /// Moves item in the collection
    /// </summary>
    /// <param name="moveFrom">Index of item's source.</param>
    /// <param name="moveTo">Index of item's destination.</param>
    /// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
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
    /// <exception cref="ArgumentNullException">Item cannot be null.</exception>
    public void Add(AAInteraction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        BaseCollection.Add(item);
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    public void Clear() {
        BaseCollection.Clear();
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    public bool Contains(AAInteraction item) {
        if (item == null) { return false; }
        return BaseCollection.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(AAInteraction[] array, int arrayIndex) {
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
    public int IndexOf(AAInteraction item) {
        return BaseCollection.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    /// <exception cref="ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count.</exception>
    public void Insert(int index, AAInteraction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        BaseCollection.Insert(index, item);
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
    /// <exception cref="ArgumentNullException">Item cannot be null.</exception>
    public bool Remove(AAInteraction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        return BaseCollection.Remove(item);
    }

    /// <summary>
    /// Removes the element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count.</exception>
    public void RemoveAt(int index) {
        var item = this[index];
        BaseCollection.Remove(item);
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    public IEnumerator<AAInteraction> GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    /// <param name="name">Name of the interaction.</param>
    public IEnumerator<AAInteraction> GetEnumerator(string name) {
        foreach (var item in BaseCollection) {
            if (AAInteraction.NameComparer.Equals(item.Name, name)) {
                yield return item;
            }
        }
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
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection.</exception>
    public AAInteraction this[int index] {
        get { return BaseCollection[index]; }
        set {
            var item = this[index];
            BaseCollection.RemoveAt(index);

            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            if (this[item.Name] != null) { throw new ArgumentOutOfRangeException(nameof(value), "Duplicate name in collection."); }
            BaseCollection.Insert(index, item);
        }
    }

    #endregion


    #region Clone

    /// <summary>
    /// Creates a copy of the collection.
    /// </summary>
    public AAInteractionCollection Clone() {
        var collection = new AAInteractionCollection();
        foreach (var item in this) {
            collection.Add(item.Clone());
        }
        return collection;
    }

    #endregion

}
