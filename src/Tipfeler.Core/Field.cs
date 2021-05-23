using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace Tipfeler {

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
                Value = value;
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
                Name = name;
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
            get { return _name; }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Name cannot be null."); }
                if (!Field.NameRegex.IsMatch(value)) { throw new ArgumentOutOfRangeException(nameof(value), "Name contains invalid characters."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                try {
                    _name = value;
                    OnChanged(new EventArgs());
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
            get { return _value; }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                _value = value;
                if (_subfields != null) {
                    foreach (var subfield in _subfields) {
                        subfield.OwnerCollection = null;
                    }
                    _subfields = null;
                }
                OnChanged(new EventArgs());
            }
        }

        private FieldCollection _subfields;
        /// <summary>
        /// Gets subfields.
        /// </summary>
        public FieldCollection Subfields {
            get {
                if (_subfields == null) {
                    if (IsReadOnly) { return null; } //just in case when it is both Read-only and it has value.
                    _subfields = new FieldCollection();
                    _subfields.Changed += delegate (Object sender, EventArgs e) {
                        _value = null; //reset value any time subfields are changed
                        OnChanged(new EventArgs());
                    };
                }
                return _subfields;
            }
        }


        private TagCollection _tags;
        /// <summary>
        /// Gets tags defined for given field.
        /// </summary>
        public TagCollection Tags {
            get {
                if (_tags == null) {
                    if (IsReadOnly) { return null; } //should never happen because AsReadOnly will always create an empty collection.
                    _tags = new TagCollection();
                    _tags.Changed += delegate (Object sender, EventArgs e) {
                        OnChanged(new EventArgs());
                    };
                }
                return _tags;
            }
        }


        /// <summary>
        /// Gets whether there is value.
        /// </summary>
        public Boolean HasValue {
            get { return (_value != null); }
        }

        /// <summary>
        /// Gets whether there are any subfields.
        /// It will return true whenever value is not present, regardless of whether subfields are populated.
        /// </summary>
        public Boolean HasSubfields {
            get { return !HasValue; }
        }

        /// <summary>
        /// Gets whether any tag is present.
        /// </summary>
        public Boolean HasTags {
            get { return (_tags != null) && (_tags.Count > 0); }
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
                if (Value == null) { return null; }
                if (Byte.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                } else {
                    var text = Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Byte.TryParse(text[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Int16.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                } else {
                    var text = Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int16.TryParse(text[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Int32.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                } else {
                    var text = Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int32.TryParse(text[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Int64.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                } else {
                    var text = Value.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        && Int64.TryParse(text[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)) {
                        return value;
                    }
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Single.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Boolean.TryParse(Value, out var value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (Decimal.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString(CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (DateTime.TryParse(Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var value)) {
                    return value;
                } else if (Int64.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var longValue)) {
                    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(longValue);
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.Value.ToString("O", CultureInfo.InvariantCulture);
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
                if (Value == null) { return null; }
                if (IPAddress.TryParse(Value, out var value)) {
                    return value;
                }
                return null;
            }
            set {
                if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
                if (IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                Value = value.ToString();
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
            return (obj is Field other) && (Field.NameComparer.Compare(Name, other.Name) == 0);
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
            return Value;
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
        /// Creates the copy of the field.
        /// </summary>
        public Field Clone() {
            Field field;
            if (HasValue) {
                field = new Field(Name, Value);
            } else {
                field = new Field(Name);
                foreach (var subfield in Subfields) {
                    field.Subfields.Add(subfield.Clone());
                }
            }
            foreach (var tag in Tags) { field.Tags.Add(tag.Clone()); }
            return field;
        }

        /// <summary>
        /// Creates a read-only copy of the field.
        /// </summary>
        public Field AsReadOnly() {
            Field field;
            if (HasValue) {
                field = new Field(Name, Value);
            } else {
                field = new Field(Name) {
                    _subfields = Subfields.AsReadOnly()
                };
            }
            field._tags = Tags.AsReadOnly();
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
            return field?.Value;
        }

        #endregion


        #region Name types

        /// <summary>
        /// Gets if field is to be treated as a header.
        /// </summary>
        public Boolean IsModifier {
            get { return Name.StartsWith(".", StringComparison.Ordinal); }
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
