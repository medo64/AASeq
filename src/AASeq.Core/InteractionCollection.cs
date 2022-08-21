using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AASeq;

/// <summary>
/// Collection of interactions.
/// </summary>
[DebuggerDisplay("{Count} interactions")]
public sealed class InteractionCollection : IList<Interaction> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public InteractionCollection()
        : this(null) {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="defaultItems">Default items of the collection.</param>
    public InteractionCollection(ICollection<Interaction> defaultItems) {
        if (defaultItems != null) {
            foreach (var item in defaultItems) {
                Add(item);
            }
        }
    }


    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly List<Interaction> BaseCollection = new List<Interaction>();


    #region Index

    /// <summary>
    /// Gets item based on a name or null if item cannot be found.
    /// </summary>
    /// <param name="name">Name.</param>
    public Interaction this[string name] {
        get {
            foreach (var item in BaseCollection) {
                if (Interaction.NameComparer.Equals(item.Name, name)) {
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
    /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
    public bool Contains(string name) {
        foreach (var item in BaseCollection) {
            if (Interaction.NameComparer.Equals(item.Name, name)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes the item from the collection.
    /// </summary>
    /// <param name="name">Name of the item to remove.</param>
    /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public bool Remove(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        bool anyChanged = false;
        for (int i = BaseCollection.Count - 1; i >= 0; i--) {
            if (Interaction.NameComparer.Equals(BaseCollection[i].Name, name)) {
                BaseCollection.RemoveAt(i);
                anyChanged = true;
            }
        }
        if (anyChanged) {
            OnChanged(new EventArgs());
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
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public void MoveItem(int moveFrom, int moveTo) {
        if ((moveFrom < 0) || (moveFrom >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveFrom), "Index out of range."); }
        if ((moveTo < 0) || (moveTo >= Count)) { throw new ArgumentOutOfRangeException(nameof(moveTo), "Index out of range."); }
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        if (moveFrom == moveTo) { return; }

        var item = BaseCollection[moveFrom];
        BaseCollection.RemoveAt(moveFrom);
        if (moveFrom > moveTo) { //move before
            BaseCollection.Insert(moveTo, item);
        } else { //move after
            BaseCollection.Insert(moveTo, item);
        }

        OnChanged(new EventArgs());
    }

    #endregion


    #region ICollection

    /// <summary>
    /// Adds an item.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Item cannot be in other collection.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public void Add(Interaction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(item), "Item cannot be in other collection."); }
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        item.OwnerCollection = this;
        BaseCollection.Add(item);
        OnChanged(new EventArgs());
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public void Clear() {
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        if (BaseCollection.Count > 0) {
            foreach (var item in BaseCollection) { item.OwnerCollection = null; }
            BaseCollection.Clear();
        }
        OnChanged(new EventArgs());
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    public bool Contains(Interaction item) {
        if (item == null) { return false; }
        return BaseCollection.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(Interaction[] array, int arrayIndex) {
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
    public int IndexOf(Interaction item) {
        return BaseCollection.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count. -or- Item cannot be in other collection.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public void Insert(int index, Interaction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(item), "Item cannot be in other collection."); }
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        item.OwnerCollection = this;
        BaseCollection.Insert(index, item);
        OnChanged(new EventArgs());
    }

    private bool _isReadOnly;
    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly {
        get { return _isReadOnly; }
        private set { _isReadOnly = value; }
    }

    /// <summary>
    /// Removes the item from the collection.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Item does not belong to this collection.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public bool Remove(Interaction item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        if (item.OwnerCollection != this) { throw new ArgumentOutOfRangeException(nameof(item), "Item does not belong to this collection."); }
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        if (BaseCollection.Remove(item)) {
            item.OwnerCollection = null;
            OnChanged(new EventArgs());
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public void RemoveAt(int index) {
        if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

        var item = this[index];
        item.OwnerCollection = null;
        BaseCollection.Remove(item);
        OnChanged(new EventArgs());
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    public IEnumerator<Interaction> GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    /// <param name="name">Name of the interaction.</param>
    public IEnumerator<Interaction> GetEnumerator(string name) {
        foreach (var item in BaseCollection) {
            if (Interaction.NameComparer.Equals(item.Name, name)) {
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
    /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
    /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
    public Interaction this[int index] {
        get { return BaseCollection[index]; }
        set {
            if (IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            var item = this[index];

            item.OwnerCollection = null;
            BaseCollection.RemoveAt(index);

            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            if (this[item.Name] != null) { throw new ArgumentOutOfRangeException(nameof(value), "Duplicate name in collection."); }
            if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException(nameof(value), "Item cannot be in other collection."); }

            item.OwnerCollection = this;
            BaseCollection.Insert(index, item);
            OnChanged(new EventArgs());
        }
    }

    #endregion


    #region Clone

    /// <summary>
    /// Creates a copy of the collection.
    /// </summary>
    public InteractionCollection Clone() {
        var collection = new InteractionCollection();
        foreach (var item in this) {
            collection.Add(item.Clone());
        }
        return collection;
    }

    /// <summary>
    /// Creates a read-only copy of the collection.
    /// </summary>
    public InteractionCollection AsReadOnly() {
        var collection = new InteractionCollection();
        foreach (var item in this) {
            collection.Add(item.AsReadOnly());
        }
        collection.IsReadOnly = true;
        return collection;
    }

    #endregion


    #region Events

    /// <summary>
    /// Occurs when item changes.
    /// </summary>
    public event EventHandler Changed;

    /// <summary>
    /// Raises Changed event.
    /// </summary>
    /// <param name="e">Event data.</param>
    internal void OnChanged(EventArgs e) {
        var ev = Changed;
        if (ev != null) { ev(this, e); }
    }

    #endregion

}
