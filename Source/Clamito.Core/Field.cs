using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Clamito {

    /// <summary>
    /// Field.
    /// </summary>
    [DebuggerDisplay("{Name} = {Value}")]
    public sealed class Field {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null. -or- Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Field(string name, string value)
            : this(name) {

            try {
                this.Value = value;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException("value", exNull.Message);
            }
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Field(string name) {
            try {
                this.Name = name;
            } catch (ArgumentNullException exNull) {
                throw new ArgumentNullException("name", exNull.Message);
            } catch (ArgumentOutOfRangeException exRange) {
                throw new ArgumentOutOfRangeException("name", exRange.Message);
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
                if (value == null) { throw new ArgumentNullException("value", "Name cannot be null."); }
                if (!Field.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException("value", "Name contains invalid characters."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    this._name = value;
                    this.OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException("value", "Name already exists in collection.");
                }
            }
        }

        private string _value;
        /// <summary>
        /// Gets/sets field value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string Value {
            get { return this._value; }
            set {
                if (value == null) { throw new ArgumentNullException("value", "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this._value = value;
                if (this._subfields != null) {
                    foreach (var subfield in this._subfields) {
                        subfield.OwnerCollection = null;
                    }
                    this._subfields = null;
                }
                this.OnChanged(new EventArgs());
            }
        }

        private FieldCollection _subfields;
        /// <summary>
        /// Gets subfields.
        /// </summary>
        public FieldCollection Subfields {
            get {
                if (this._subfields == null) {
                    if (this.IsReadOnly) { return null; } //just in case when it is both Read-only and it has value.
                    this._subfields = new FieldCollection();
                    this._subfields.Changed += delegate(Object sender, EventArgs e) {
                        this._value = null; //reset value any time subfields are changed
                        this.OnChanged(new EventArgs());
                    };
                }
                return this._subfields;
            }
        }


        private TagCollection _modifiers;
        /// <summary>
        /// Gets framework tags defined for given field.
        /// </summary>
        public TagCollection Modifiers {
            get {
                if (this._modifiers == null) {
                    if (this.IsReadOnly) { return null; } //should never happen because AsReadOnly will always create an empty collection.
                    this._modifiers = new TagCollection();
                    this._modifiers.Changed += delegate(Object sender, EventArgs e) {
                        this.OnChanged(new EventArgs());
                    };
                }
                return this._modifiers;
            }
        }

        private TagCollection _tags;
        /// <summary>
        /// Gets tags defined for given field.
        /// </summary>
        public TagCollection Tags {
            get {
                if (this._tags == null) {
                    if (this.IsReadOnly) { return null; } //should never happen because AsReadOnly will always create an empty collection.
                    this._tags = new TagCollection();
                    this._tags.Changed += delegate(Object sender, EventArgs e) {
                        this.OnChanged(new EventArgs());
                    };
                }
                return this._tags;
            }
        }


        /// <summary>
        /// Gets whether there is value.
        /// </summary>
        public Boolean HasValue {
            get { return (this._value != null); }
        }

        /// <summary>
        /// Gets whether there are any subfields.
        /// It will return true whenever value is not present, regardless of whether subfields are populated.
        /// </summary>
        public Boolean HasSubfields {
            get { return !this.HasValue; }
        }

        /// <summary>
        /// Gets whether any tag is present.
        /// </summary>
        public Boolean HasTags {
            get { return (this._tags != null) && (this._tags.Count > 0); }
        }

        /// <summary>
        /// Gets whether any modifier is present.
        /// </summary>
        public Boolean HasModifiers {
            get { return (this._modifiers != null) && (this._modifiers.Count > 0); }
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
            var other = obj as Field;
            return (other != null) && (Field.NameComparer.Compare(this.Name, other.Name) == 0);
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
            return this.Value;
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
        /// Creates the copy of the field.
        /// </summary>
        public Field Clone() {
            Field field;
            if (this.HasValue) {
                field = new Field(this.Name, this.Value);
            } else {
                field = new Field(this.Name);
                foreach (var subfield in this.Subfields) {
                    field.Subfields.Add(subfield.Clone());
                }
            }
            foreach (var tag in this.Modifiers) { field.Modifiers.Add(tag.Clone()); }
            foreach (var tag in this.Tags) { field.Tags.Add(tag.Clone()); }
            return field;
        }

        /// <summary>
        /// Creates a read-only copy of the field.
        /// </summary>
        public Field AsReadOnly() {
            Field field;
            if (this.HasValue) {
                field = new Field(this.Name, this.Value);
            } else {
                field = new Field(this.Name);
                field._subfields = this.Subfields.AsReadOnly();
            }
            field._modifiers = this.Modifiers.AsReadOnly();
            field._tags = this.Tags.AsReadOnly();
            field.IsReadOnly = true;
            return field;
        }

        #endregion


        #region Implicit

        /// <summary>
        /// Converts field to string.
        /// </summary>
        /// <param name="field">Field.</param>
        public static implicit operator String(Field field) {
            return (field != null) ? field.Value : null;
        }

        #endregion


        #region Name types

        /// <summary>
        /// Gets whether field belongs to content.
        /// </summary>
        public Boolean IsContent {
            get { return !this.IsHeader; }
        }

        /// <summary>
        /// Gets if field is to be treated as header.
        /// </summary>
        public Boolean IsHeader {
            get { return this.Name.StartsWith("@", StringComparison.Ordinal); }
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException("name", "Name cannot be null."); }
            return Field.NameRegex.IsMatch(name);
        }


        internal static readonly Regex NameRegex = new Regex(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and dashes
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal FieldCollection OwnerCollection { get; set; }

    }
}
