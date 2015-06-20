using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Clamito {

    /// <summary>
    /// Tag.
    /// </summary>
    [DebuggerDisplay("{Name} = {State}")]
    public sealed class Tag {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="state">State.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Tag(string name, bool state) {
            try {
                this.Name = name;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException("name", exNull.Message);
            } catch (ArgumentOutOfRangeException exRange) {
                throw new ArgumentOutOfRangeException("name", exRange.Message);
            }

            this.State = state;
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Tag(string name)
            : this(name, true) {
        }


        private string _name;
        /// <summary>
        /// Gets/sets name.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters. -or- Name already exists in collection.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string Name {
            get { return this._name; }
            set {
                if (value == null) { throw new ArgumentNullException("value", "Name cannot be null."); }
                if (!Tag.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException("value", "Name contains invalid characters."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    if (this.OwnerCollection != null) {
                        string oldName = this._name, newName = value;
                        Tag oldItem, newItem;
                        this.OwnerCollection.NameLookupDictionary.TryGetValue(oldName, out oldItem);
                        this.OwnerCollection.NameLookupDictionary.TryGetValue(newName, out newItem);
                        if ((newItem != null) && (oldItem != newItem)) { throw new ArgumentOutOfRangeException("value", "Item already exists in collection."); }
                        this.OwnerCollection.NameLookupDictionary.Remove(oldName);
                        this.OwnerCollection.NameLookupDictionary.Add(newName, oldItem);
                    }
                    this._name = value;
                    this.OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException("value", "Name already exists in collection.");
                }
            }
        }

        private bool _state;
        /// <summary>
        /// Gets/sets state name for an tag.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public bool State {
            get { return this._state; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this._state = value;
                this.OnChanged(new EventArgs());
            }
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
            if (this.OwnerCollection != null) { this.OwnerCollection.OnChanged(new EventArgs()); }
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj) {
            var other = obj as Tag;
            return (other != null) && (Tag.NameComparer.Compare(this.Name, other.Name) == 0);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return this.State ? this.Name : "!" + this.Name;
        }

        #endregion


        #region Name types

        /// <summary>
        /// Gets whether tag should be processed in a normal manner.
        /// </summary>
        public Boolean IsNormal {
            get { return !this.IsFramework; }
        }

        /// <summary>
        /// Gets whether tag is processed as a part of framework.
        /// </summary>
        public Boolean IsFramework {
            get { return this.Name.StartsWith(".", StringComparison.Ordinal); }
        }

        #endregion


        #region Clone

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether item is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return this._isReadOnly; }
            private set { this._isReadOnly = value; }
        }


        /// <summary>
        /// Creates a copy of the tag.
        /// </summary>
        public Tag Clone() {
            return new Tag(this.Name, this.State) { IsReadOnly = true };
        }

        /// <summary>
        /// Creates a read-only copy of the tag.
        /// </summary>
        public Tag AsReadOnly() {
            return new Tag(this.Name, this.State) { IsReadOnly = true };
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException("name", "Name cannot be null."); }
            return Tag.NameRegex.IsMatch(name);
        }


        internal static readonly Regex NameRegex = new Regex(@"^[\p{L}\p{Nd}]+$"); //allowed only letters and numbers
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal TagCollection OwnerCollection { get; set; }

    }
}
