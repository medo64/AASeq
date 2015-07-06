using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Clamito {

    /// <summary>
    /// Endpoint.
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public sealed class Endpoint {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Endpoint(string name)
            : this(name, null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="protocolName">Protocol used for the endpoint.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Endpoint(string name, string protocolName) {
            try {
                this.Name = name;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException(nameof(name), exNull.Message);
            } catch (ArgumentOutOfRangeException exRange) {
                throw new ArgumentOutOfRangeException(nameof(name), exRange.Message);
            }

            this.ProtocolName = protocolName;
            this.Properties = new FieldCollection();
            this.Properties.Changed += delegate(Object sender, EventArgs e) {
                this.OnChanged(new EventArgs());
            };
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
                if (!Endpoint.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException(nameof(value), "Name contains invalid characters."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    if (this.OwnerCollection != null) {
                        string oldName = this._name, newName = value;
                        Endpoint oldItem, newItem;
                        this.OwnerCollection.NameLookupDictionary.TryGetValue(oldName, out oldItem);
                        this.OwnerCollection.NameLookupDictionary.TryGetValue(newName, out newItem);
                        if ((newItem != null) && (oldItem != newItem)) { throw new ArgumentOutOfRangeException(nameof(value), "Item already exists in collection."); }
                        this.OwnerCollection.NameLookupDictionary.Remove(oldName);
                        this.OwnerCollection.NameLookupDictionary.Add(newName, oldItem);
                    }
                    this._name = value;
                    this.OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException(nameof(value), "Name already exists in collection.");
                }
            }
        }

        private string _displayName;
        /// <summary>
        /// Gets/sets display name for an endpoint.
        /// If display name is set to null, it will mirror name.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string DisplayName {
            get { return string.IsNullOrEmpty(this._displayName) ? this.Name : this._displayName; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this._displayName = value;
                this.OnChanged(new EventArgs());
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

        private string _protocolName;
        /// <summary>
        /// Gets/sets protocol name for the endpoint.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string ProtocolName {
            get { return this._protocolName; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this._protocolName = value;
                this.OnChanged(new EventArgs());
            }
        }


        /// <summary>
        /// Gets content field collection.
        /// </summary>
        public FieldCollection Properties { get; private set; }


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
            var other = obj as Endpoint;
            return (other != null) && (Endpoint.NameComparer.Compare(this.Name, other.Name) == 0);
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
            return this.DisplayName;
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
        /// Creates a copy of the endpoint.
        /// </summary>
        public Endpoint Clone() {
            var endpoint = new Endpoint(this.Name, this.ProtocolName) { DisplayName = this.DisplayName, Description = this.Description };
            foreach (var field in this.Properties) {
                endpoint.Properties.Add(field.Clone());
            }
            return endpoint;
        }

        /// <summary>
        /// Creates a read-only copy of the endpoint.
        /// </summary>
        public Endpoint AsReadOnly() {
            var endpoint = new Endpoint(this.Name, this.ProtocolName) { DisplayName = this.DisplayName, Description = this.Description };
            endpoint.Properties = this.Properties.AsReadOnly();
            endpoint.IsReadOnly = true;
            return endpoint;
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            return Endpoint.NameRegex.IsMatch(name);
        }


        internal static readonly Regex NameRegex = new Regex(@"^[\p{L}][\p{L}\p{Nd}_]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and underscores; must start with letter
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal EndpointCollection OwnerCollection { get; set; }

    }
}
