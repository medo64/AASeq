using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Tipfeler {

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
                Name = name;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException(nameof(name), exNull.Message);
            } catch (ArgumentOutOfRangeException exRange) {
                throw new ArgumentOutOfRangeException(nameof(name), exRange.Message);
            }

            State = state;
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
            get { return _name; }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Name cannot be null."); }
                if (!Tag.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException(nameof(value), "Name contains invalid characters."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    if (OwnerCollection != null) {
                        string oldName = _name, newName = value;
                        OwnerCollection.NameLookupDictionary.TryGetValue(oldName, out var oldItem);
                        OwnerCollection.NameLookupDictionary.TryGetValue(newName, out var newItem);
                        if ((newItem != null) && (oldItem != newItem)) { throw new ArgumentOutOfRangeException(nameof(value), "Item already exists in collection."); }
                        OwnerCollection.NameLookupDictionary.Remove(oldName);
                        OwnerCollection.NameLookupDictionary.Add(newName, oldItem);
                    }
                    _name = value;
                    OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException(nameof(value), "Name already exists in collection.");
                }
            }
        }

        private bool _state;
        /// <summary>
        /// Gets/sets state name for an tag.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public bool State {
            get { return _state; }
            set {
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                _state = value;
                OnChanged(new EventArgs());
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
            Changed?.Invoke(this, e);
            if (OwnerCollection != null) { OwnerCollection.OnChanged(new EventArgs()); }
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj) {
            return (obj is Tag other) && (Tag.NameComparer.Compare(Name, other.Name) == 0);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return State ? Name : "!" + Name;
        }

        #endregion


        #region Name types

        /// <summary>
        /// Gets whether tag should be processed in a normal manner.
        /// </summary>
        public Boolean IsNormal {
            get { return !IsFramework; }
        }

        /// <summary>
        /// Gets whether tag is processed as a part of framework.
        /// </summary>
        public Boolean IsFramework {
            get { return Name.StartsWith(".", StringComparison.Ordinal); }
        }

        #endregion


        #region Clone

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether item is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return _isReadOnly; }
            private set { _isReadOnly = value; }
        }


        /// <summary>
        /// Creates a copy of the tag.
        /// </summary>
        public Tag Clone() {
            return new Tag(Name, State) { IsReadOnly = true };
        }

        /// <summary>
        /// Creates a read-only copy of the tag.
        /// </summary>
        public Tag AsReadOnly() {
            return new Tag(Name, State) { IsReadOnly = true };
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            return Tag.NameRegex.IsMatch(name);
        }


        internal static readonly Regex NameRegex = new Regex(@"^\.?[\p{L}\p{Nd}]+$"); //allowed only letters and numbers
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal TagCollection OwnerCollection { get; set; }

    }
}
