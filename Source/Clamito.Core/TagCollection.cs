using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Clamito {
    /// <summary>
    /// Collection of tags.
    /// </summary>
    [DebuggerDisplay("{Count} tags")]
    public sealed class TagCollection : IList<Tag> {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public TagCollection()
            : this(null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="defaultItems">Default items of the collection.</param>
        public TagCollection(ICollection<Tag> defaultItems) {
            if (defaultItems != null) {
                foreach (var item in defaultItems) {
                    this.Add(item);
                }
            }
        }


        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<Tag> BaseCollection = new List<Tag>();
        internal readonly Dictionary<String, Tag> NameLookupDictionary = new Dictionary<String, Tag>(Tag.NameComparer);


        #region Index

        /// <summary>
        /// Gets item based on a name or null if item cannot be found.
        /// </summary>
        /// <param name="name">Name.</param>
        public Tag this[string name] {
            get {
                Tag item;
                if ((name != null) && this.NameLookupDictionary.TryGetValue(name, out item)) {
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
            return (name == null) ? false : this.NameLookupDictionary.ContainsKey(name);
        }

        /// <summary>
        /// Removes the item from the collection.
        /// </summary>
        /// <param name="name">Name of the item to remove.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public bool Remove(string name) {
            if (name == null) { throw new ArgumentNullException("name", "Name cannot be null."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            Tag item;
            if (this.NameLookupDictionary.TryGetValue(name, out item)) {
                this.BaseCollection.Remove(item);
                this.OnChanged(new EventArgs());
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
            if ((moveFrom < 0) || (moveFrom >= this.Count)) { throw new ArgumentOutOfRangeException("moveFrom", "Index out of range."); }
            if ((moveTo < 0) || (moveTo >= this.Count)) { throw new ArgumentOutOfRangeException("moveTo", "Index out of range."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            if (moveFrom == moveTo) { return; }

            var item = this.BaseCollection[moveFrom];
            this.BaseCollection.RemoveAt(moveFrom);
            if (moveFrom > moveTo) { //move before
                this.BaseCollection.Insert(moveTo, item);
            } else { //move after
                this.BaseCollection.Insert(moveTo, item);
            }

            this.OnChanged(new EventArgs());
        }

        #endregion


        #region ICollection

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">Item.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Add(Tag item) {
            if (item == null) { throw new ArgumentNullException("item", "Item cannot be null."); }
            if (this[item.Name] != null) { throw new ArgumentOutOfRangeException("item", "Duplicate name in collection."); }
            if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException("item", "Item cannot be in other collection."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            item.OwnerCollection = this;
            this.BaseCollection.Add(item);
            this.NameLookupDictionary.Add(item.Name, item);
            this.OnChanged(new EventArgs());
        }

        /// <summary>
        /// Removes all items.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Clear() {
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            if (this.BaseCollection.Count > 0) {
                foreach (var item in this.BaseCollection) { item.OwnerCollection = null; }
                this.BaseCollection.Clear();
                this.NameLookupDictionary.Clear();
            }
            this.OnChanged(new EventArgs());
        }

        /// <summary>
        /// Determines whether the collection contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public bool Contains(Tag item) {
            if (item == null) { return false; }
            return this.BaseCollection.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Tag[] array, int arrayIndex) {
            this.BaseCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items contained in the collection.
        /// </summary>
        public int Count {
            get { return this.BaseCollection.Count; }
        }

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire collection.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        public int IndexOf(Tag item) {
            return this.BaseCollection.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public void Insert(int index, Tag item) {
            if (item == null) { throw new ArgumentNullException("item", "Item cannot be null."); }
            if (this[item.Name] != null) { throw new ArgumentOutOfRangeException("item", "Duplicate name in collection."); }
            if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException("item", "Item cannot be in other collection."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }

            item.OwnerCollection = this;
            this.BaseCollection.Insert(index, item);
            this.NameLookupDictionary.Add(item.Name, item);
            this.OnChanged(new EventArgs());
        }

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return this._isReadOnly; }
            private set { this._isReadOnly = value; }
        }

        /// <summary>
        /// Removes the item from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="System.ArgumentNullException">Item cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Item does not belong to this collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public bool Remove(Tag item) {
            if (item == null) { throw new ArgumentNullException("item", "Item cannot be null."); }
            if (item.OwnerCollection != this) { throw new ArgumentOutOfRangeException("item", "Item does not belong to this collection."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }
            
            if (this.BaseCollection.Remove(item) && this.NameLookupDictionary.Remove(item.Name)) {
                item.OwnerCollection = null;
                this.OnChanged(new EventArgs());
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
            if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }
            
            var item = this[index];
            item.OwnerCollection = null;
            this.BaseCollection.Remove(item);
            this.NameLookupDictionary.Remove(item.Name);
            this.OnChanged(new EventArgs());
        }
        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
        /// </summary>
        public IEnumerator<Tag> GetEnumerator() {
            return this.BaseCollection.GetEnumerator();
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a non-generic collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.BaseCollection.GetEnumerator();
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        /// <exception cref="System.NotSupportedException">Collection is read-only.</exception>
        public Tag this[int index] {
            get { return this.BaseCollection[index]; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Collection is read-only."); }
            
                var item = this[index];

                item.OwnerCollection = null;
                this.BaseCollection.RemoveAt(index);
                this.NameLookupDictionary.Remove(item.Name);

                if (value == null) { throw new ArgumentNullException("value", "Value cannot be null."); }
                if (this[item.Name] != null) { throw new ArgumentOutOfRangeException("value", "Duplicate name in collection."); }
                if (item.OwnerCollection != null) { throw new ArgumentOutOfRangeException("value", "Item cannot be in other collection."); }

                item.OwnerCollection = this;
                this.BaseCollection.Insert(index, item);
                this.NameLookupDictionary.Add(item.Name, item);
                this.OnChanged(new EventArgs());
            }
        }

        #endregion


        #region Clone

        /// <summary>
        /// Creates a copy of the collection.
        /// </summary>
        public TagCollection Clone() {
            var collection = new TagCollection();
            foreach (var item in this) {
                collection.Add(item.Clone());
            }
            return collection;
        }

        /// <summary>
        /// Creates a read-only copy of the collection.
        /// </summary>
        public TagCollection AsReadOnly() {
            var collection = new TagCollection();
            foreach (var item in this) {
                collection.Add(item.AsReadOnly());
            }
            collection.IsReadOnly = true;
            return collection;
        }
        
        #endregion
        

        /// <summary>
        /// Returns next unique name.
        /// </summary>
        /// <param name="baseName">Base name to which numeric suffix will be added.</param>
        /// <exception cref="System.ArgumentNullException">Base name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Base name contains invalid characters.</exception>
        public string GetUniqueName(string baseName) {
            if (baseName == null) { throw new ArgumentNullException("baseName", "Base name cannot be null."); }
            if (!Tag.NameRegex.IsMatch(baseName)) { throw new ArgumentNullException("baseName", "Base name contains some invalid characters."); }
            var index = 1;
            var name = baseName;
            while (this[name] != null) {
                index++;
                name = string.Format(CultureInfo.InvariantCulture, "{0}{1}", baseName, index);
            }
            return name;
        }


        /// <summary>
        /// Returns state of tag or null if tag is not present.
        /// </summary>
        /// <param name="name">Tag name.</param>
        public bool? GetState(string name) {
            var tag = this[name];
            return (tag != null) ? (bool?)tag.State : null;
        }


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
            var ev = this.Changed;
            if (ev != null) { ev(this, e); }
        }

        #endregion

    }
}
