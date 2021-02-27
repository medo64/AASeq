using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
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
                throw new ArgumentNullException(nameof(value), exNull.Message);
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
                if (!Field.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException(nameof(value), "Name contains invalid characters."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    this._name = value;
                    this.OnChanged(new EventArgs());
                } catch (ArgumentOutOfRangeException) {
                    throw new ArgumentOutOfRangeException(nameof(value), "Name already exists in collection.");
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
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
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
                    this._subfields.Changed += delegate (Object sender, EventArgs e) {
                        this._value = null; //reset value any time subfields are changed
                        this.OnChanged(new EventArgs());
                    };
                }
                return this._subfields;
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
                    this._tags.Changed += delegate (Object sender, EventArgs e) {
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


        #region Value conversions

        /// <summary>
        /// Gets/sets field value as Byte.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Byte? ValueAsByte {
            get {
                if (this.Value == null) { return null; }
                Byte value;
                if (Byte.TryParse(this.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    var text = this.Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Byte.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets/sets field value as Int16.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Int32? ValueAsInt16 {
            get {
                if (this.Value == null) { return null; }
                Int16 value;
                if (Int16.TryParse(this.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    var text = this.Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int16.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets/sets field value as Int32.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Int32? ValueAsInt32 {
            get {
                if (this.Value == null) { return null; }
                Int32 value;
                if (Int32.TryParse(this.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    var text = this.Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int32.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets/sets field value as Int64.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Int64? ValueAsInt64 {
            get {
                if (this.Value == null) { return null; }
                Int64 value;
                if (Int64.TryParse(this.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    var text = this.Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int64.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Gets/sets field value as Single.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Single? ValueAsSingle {
            get {
                if (this.Value == null) { return null; }
                Single value;
                if (Single.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets/sets field value as Double.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Double? ValueAsDouble {
            get {
                if (this.Value == null) { return null; }
                Double value;
                if (Double.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Gets/sets field value as Boolean.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Boolean? ValueAsBoolean {
            get {
                if (this.Value == null) { return null; }
                Boolean value;
                if (Boolean.TryParse(this.Value, out value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Gets/sets field value as Decimal.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public Decimal? ValueAsDecimal {
            get {
                if (this.Value == null) { return null; }
                Decimal value;
                if (Decimal.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Gets/sets field value as DateTime.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public DateTime? ValueAsTime {
            get {
                if (this.Value == null) { return null; }
                DateTime value;
                Int64 longValue;
                if (DateTime.TryParse(this.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out value)) {
                    return value;
                } else if (Int64.TryParse(this.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out longValue)) {
                    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(longValue);
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.Value.ToString("O", CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// Gets/sets field value as IPAddress.
        /// If value cannot be converted, null will be returned.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public IPAddress ValueAsIPAddress {
            get {
                if (this.Value == null) { return null; }
                IPAddress value;
                if (IPAddress.TryParse(this.Value, out value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                this.Value = value.ToString();
            }
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
        /// Gets if field is to be treated as a header.
        /// </summary>
        public Boolean IsModifier {
            get { return this.Name.StartsWith(".", StringComparison.Ordinal); }
        }

        #endregion


        /// <summary>
        /// Returns true if name is valid.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <exception cref="System.NullReferenceException">Name cannot be null.</exception>
        public static bool IsNameValid(string name) {
            if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
            return Field.NameRegex.IsMatch(name);
        }


        internal static readonly Regex NameRegex = new Regex(@"^\.?[\p{L}\p{Nd}][\p{L}\p{Nd}-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and dashes
        internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
        internal FieldCollection OwnerCollection { get; set; }

    }
}
