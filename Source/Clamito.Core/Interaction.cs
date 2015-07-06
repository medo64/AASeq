using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Clamito {

    /// <summary>
    /// Interaction.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public abstract class Interaction {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        internal protected Interaction(string name) {
            try {
                this.Name = name;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException(nameof(name), exNull.Message);
            } catch (ArgumentOutOfRangeException exRange) {
                throw new ArgumentOutOfRangeException(nameof(name), exRange.Message);
            }
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
                if (value == null) { throw new ArgumentNullException(nameof(value), "Name cannot be null."); }
                if (!Interaction.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException(nameof(value), "Name contains invalid characters."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    this._name = value;
                    this.OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException(nameof(value), "Name already exists in collection.");
                }
            }
        }


        private string _description;
        /// <summary>
        /// Gets/sets description.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string Description {
            get { return this._description ?? ""; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                if (value == null) { value = ""; }
                this._description = value;
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
            return base.Equals(obj);
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
            return this.Name;
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            return Interaction.NameRegex.IsMatch(name);
        }

        /// <summary>
        /// Gets interaction kind.
        /// </summary>
        public abstract InteractionKind Kind { get; }

        /// <summary>
        /// Gets whether interaction type is Message.
        /// </summary>
        public bool IsMessage {
            get { return (this is Message); }
        }


        #region Clone

        private bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating whether item is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return this._isReadOnly; }
            protected set { this._isReadOnly = value; }
        }

        /// <summary>
        /// Creates a copy of the interaction.
        /// </summary>
        public abstract Interaction Clone();

        /// <summary>
        /// Creates a read-only copy of the interaction.
        /// </summary>
        public abstract Interaction AsReadOnly();

        #endregion


        internal static readonly Regex NameRegex = new Regex(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and dashes
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal InteractionCollection OwnerCollection { get; set; }

    }
}
