using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace AASeq;

/// <summary>
/// Collection of Fields.
/// </summary>
[DebuggerDisplay("{Count} fields")]
public sealed class AAFieldCollection : AAValue, IList<AAField> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public AAFieldCollection() {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="items">Default items of the collection.</param>
    public AAFieldCollection(ICollection<AAField> items) {
        if (items != null) {
            foreach (var item in items) {
                Add(item);
            }
        }
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="defaultItems">Default items of the collection.</param>
    public AAFieldCollection(params AAField[] items) {
        if (items != null) {
            foreach (var item in items) {
                Add(item);
            }
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly List<AAField> BaseCollection = new();


    #region Index

    /// <summary>
    /// Gets/sets item value based on a path.
    /// Each subfield is separated by forward slash (/).
    /// </summary>
    /// <param name="path">Path.</param>
    /// <exception cref="ArgumentNullException">Path cannot be null.</exception>
    public AAValue? this[string path] {
        get {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
            AAFieldCollection? nextFields;
            for (int i = 0; i < pathParts.Length; i++) {
                nextFields = null;
                var name = pathParts[i];
                foreach (var field in currFields) {
                    if (AAField.NameComparer.Equals(field.Name, name)) {
                        if (i == pathParts.Length - 1) {
                            return field.Value;
                        } else {
                            nextFields = field.Value.AsFieldCollection();
                            break;
                        }
                    }
                }
                if (nextFields != null) { currFields = nextFields; }
            }

            return null;
        }
        set {
            if (path == null) { throw new ArgumentNullException(nameof(path), "Name path cannot be null."); }
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }

            var pathParts = path.Split(new char[] { '\\', '/' });

            var currFields = this;
            AAFieldCollection? nextFields;
            for (int i = 0; i < pathParts.Length; i++) {
                nextFields = null;
                var name = pathParts[i];
                foreach (var field in currFields) {
                    if (AAField.NameComparer.Equals(field.Name, name)) {
                        if (i == pathParts.Length - 1) {
                            field.Value = value;
                            return;
                        } else {
                            if (field.Value is not AAFieldCollection) {
                                field.Value = new AAFieldCollection();
                            }
                            nextFields = field.Value.AsFieldCollection();
                            break;
                        }
                    }
                }
                if (nextFields != null) {
                    currFields = nextFields;
                } else { //create that field
                    if (i == pathParts.Length - 1) {
                        currFields.Add(new AAField(name, value));
                        return;
                    } else {
                        nextFields = new AAFieldCollection();
                        currFields.Add(new AAField(name, nextFields));
                        currFields = nextFields;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds first matching item based on a path.
    /// Each subfield is separated by forward slash (/).
    /// </summary>
    /// <param name="path">Path.</param>
    /// <exception cref="ArgumentNullException">Path cannot be null.</exception>
    public AAField? FindFirst(string path) {
        if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

        var pathParts = path.Split(new char[] { '\\', '/' });

        var currFields = this;
        AAFieldCollection? nextFields;
        for (int i = 0; i < pathParts.Length; i++) {
            nextFields = null;
            var name = pathParts[i];
            foreach (var field in currFields) {
                if (AAField.NameComparer.Equals(field.Name, name)) {
                    if (i == pathParts.Length - 1) {
                        return field;
                    } else {
                        nextFields = field.Value.AsFieldCollection();
                        break;
                    }
                }
            }
            if (nextFields != null) { currFields = nextFields; } else { break; }
        }

        return null;
    }

    /// <summary>
    /// Finds last matching item based on a path.
    /// Each subfield is separated by forward slash (/).
    /// </summary>
    /// <param name="path">Path.</param>
    /// <exception cref="ArgumentNullException">Path cannot be null.</exception>
    public AAField? FindLast(string path) {
        if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

        var pathParts = path.Split(new char[] { '\\', '/' });

        var currFields = this;
        AAField? lastField = null;
        AAFieldCollection? nextFields;
        for (int i = 0; i < pathParts.Length; i++) {
            nextFields = null;
            var name = pathParts[i];
            foreach (var field in currFields) {
                if (AAField.NameComparer.Equals(field.Name, name)) {
                    if (i == pathParts.Length - 1) {
                        lastField = field;
                        nextFields = null;
                    } else {
                        nextFields = field.Value.AsFieldCollection();
                        break;
                    }
                }
            }
            if (lastField != null) { return lastField; }
            if (nextFields != null) { currFields = nextFields; } else { break; }
        }

        return null;
    }

    /// <summary>
    /// Finds all matching items based on a path.
    /// Each subfield is separated by forward slash (/).
    /// </summary>
    /// <param name="path">Path.</param>
    /// <exception cref="ArgumentNullException">Path cannot be null.</exception>
    public IEnumerable<AAField> FindAll(string path) {
        if (path == null) { throw new ArgumentNullException(nameof(path), "Path cannot be null."); }

        var pathParts = path.Split(new char[] { '\\', '/' });

        var currFields = this;
        AAFieldCollection? nextFields;
        for (int i = 0; i < pathParts.Length; i++) {
            nextFields = null;
            var name = pathParts[i];
            foreach (var field in currFields) {
                if (AAField.NameComparer.Equals(field.Name, name)) {
                    if (i == pathParts.Length - 1) {
                        yield return field;
                    } else {
                        nextFields = field.Value.AsFieldCollection();
                        break;
                    }
                }
            }
            if (nextFields != null) { currFields = nextFields; } else { break; }
        }
    }

    /// <summary>
    /// Adds new value field.
    /// </summary>
    /// <param name="path">Path. Components are separated by forward slash (/).</param>
    /// <param name="value">Value.</param>
    public void Add(string path, AAValue value) {
        if (path == null) { throw new ArgumentNullException(nameof(path), "Name path cannot be null."); }

        var pathParts = path.Split(new char[] { '\\', '/' });

        var currFields = this;
        AAFieldCollection? nextFields;
        for (int i = 0; i < pathParts.Length; i++) {
            nextFields = null;
            var name = pathParts[i];
            foreach (var field in currFields) {
                if (AAField.NameComparer.Equals(field.Name, name)) {
                    if (i == pathParts.Length - 1) {
                        field.Value = value;
                        return;
                    } else {
                        nextFields = field.Value.AsFieldCollection();
                        break;
                    }
                }
            }
            if (nextFields != null) {
                currFields = nextFields;
            } else { //create that field
                if (i == pathParts.Length - 1) {
                    currFields.Add(new AAField(name, value));
                    return;
                } else {
                    var newSubfields = new AAFieldCollection();
                    var newField = new AAField(name, newSubfields);
                    currFields.Add(newField);
                    currFields = newSubfields;
                }
            }
        }
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="name">Name of the item to locate.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    public bool Contains(string name) {
        foreach (var item in BaseCollection) {
            if (AAField.NameComparer.Equals(item.Name, name)) {
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
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public bool Remove(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        bool anyChanged = false;
        for (int i = BaseCollection.Count - 1; i >= 0; i--) {
            if (AAField.NameComparer.Equals(BaseCollection[i].Name, name)) {
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
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
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
    /// <exception cref="ArgumentOutOfRangeException">Item cannot be in other collection.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public void Add(AAField item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        BaseCollection.Add(item);
    }

    /// <summary>
    /// Adds multiple items.
    /// </summary>
    /// <param name="items">Item.</param>
    /// <exception cref="ArgumentNullException">Items cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Item cannot be in other collection.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public void AddRange(IEnumerable<AAField> items) {
        if (items == null) { throw new ArgumentNullException(nameof(items), "Item cannot be null."); }
        foreach (var item in items) {
            BaseCollection.Add(item);
        }
    }

    /// <summary>
    /// Removes all items.
    /// </summary>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public void Clear() {
        BaseCollection.Clear();
    }

    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    public bool Contains(AAField item) {
        if (item == null) { return false; }
        return BaseCollection.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(AAField[] array, int arrayIndex) {
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
    public int IndexOf(AAField item) {
        return BaseCollection.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    /// <exception cref="ArgumentNullException">Item cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count. -or- Item cannot be in other collection.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public void Insert(int index, AAField item) {
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
    /// <exception cref="ArgumentOutOfRangeException">Item does not belong to this collection.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public bool Remove(AAField item) {
        if (item == null) { throw new ArgumentNullException(nameof(item), "Item cannot be null."); }
        return BaseCollection.Remove(item);
    }

    /// <summary>
    /// Removes the element at the specified index of the collection.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public void RemoveAt(int index) {
        BaseCollection.RemoveAt(index);
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    public IEnumerator<AAField> GetEnumerator() {
        return BaseCollection.GetEnumerator();
    }

    /// <summary>
    /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
    /// </summary>
    /// <param name="name">Name of the field.</param>
    public IEnumerator<AAField> GetEnumerator(string name) {
        foreach (var item in BaseCollection) {
            if (AAField.NameComparer.Equals(item.Name, name)) {
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
    /// <exception cref="ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
    /// <exception cref="NotSupportedException">Collection is read-only.</exception>
    public AAField this[int index] {
        get { return BaseCollection[index]; }
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            var item = this[index];
            BaseCollection.RemoveAt(index);
            BaseCollection.Insert(index, item);
        }
    }

    #endregion

    #region Paths

    /// <summary>
    /// Exposes the enumerator for iteration over each field.
    /// Nested fields names will be separated by forward slash (/).
    /// </summary>
    public IEnumerable<AAFieldNode> AllPaths {
        get {
            foreach (var item in EnumerateAllPaths(this)) {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Exposes the enumerator for iteration over each value field.
    /// Nested fields names will be separated by forward slash (/).
    /// </summary>
    public IEnumerable<AAFieldNode> PathsWithValue {
        get {
            foreach (var item in AllPaths) {
                if (item.Field.Value is not AAFieldCollection) { yield return item; }
            }
        }
    }

    private static IEnumerable<AAFieldNode> EnumerateAllPaths(AAFieldCollection fields, string? pathPrefix = null) {
        foreach (var field in fields) {
            var path = ((pathPrefix != null) ? pathPrefix + "/" : "") + field.Name;
            yield return new AAFieldNode(path, field);
            if (field.Value is AAFieldCollection subfields) {
                foreach (var innerField in EnumerateAllPaths(subfields, pathPrefix: path)) {
                    yield return innerField;
                }
            }
        }
    }

    #endregion

    #region AAValue

    protected override bool? ConvertToBoolean() => null;
    protected override sbyte? ConvertToInt8() => null;
    protected override short? ConvertToInt16() => null;
    protected override int? ConvertToInt32() => null;
    protected override long? ConvertToInt64() => null;
    protected override byte? ConvertToUInt8() => null;
    protected override ushort? ConvertToUInt16() => null;
    protected override uint? ConvertToUInt32() => null;
    protected override ulong? ConvertToUInt64() => null;
    protected override float? ConvertToFloat32() => null;
    protected override double? ConvertToFloat64() => null;
    protected override ReadOnlyMemory<byte>? ConvertToBinary() => null;
    protected override string? ConvertToString() => null;
    protected override DateTimeOffset? ConvertToDateTime() => null;
    protected override DateOnly? ConvertToDate() => null;
    protected override TimeOnly? ConvertToTime() => null;
    protected override TimeSpan? ConvertToDuration() => null;
    protected override IPAddress? ConvertToIPAddress() => null;
    protected override IPAddress? ConvertToIPv4Address() => null;
    protected override IPAddress? ConvertToIPv6Address() => null;
    protected override ulong? ConvertToSize() => null;
    protected override AAFieldCollection? ConvertToFieldCollection() => this;

    #endregion AAValue

}
