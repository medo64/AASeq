using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AASeq;

/// <summary>
/// Field.
/// </summary>
[DebuggerDisplay("{Name}: {Value}")]
public sealed record AAField {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null. -or- Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AAField(string name, AAValue value) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }

        Name = name;
        _value = value;
    }


    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; }

    private AAValue _value;
    /// <summary>
    /// Gets/sets field value.
    /// </summary>
    public AAValue Value {
        get { return _value; }
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            _value = value;
        }
    }

    private readonly Lazy<AATagCollection> _tags = new();
    /// <summary>
    /// Gets tags defined for given field.
    /// </summary>
    public AATagCollection Tags {
        get { return _tags.Value; }
    }

    /// <summary>
    /// Gets whether any tag is present.
    /// </summary>
    public Boolean HasTags {
        get { return Tags.Count > 0; }
    }


    #region Name types

    /// <summary>
    /// Gets if field is to be treated as a header.
    /// </summary>
    public Boolean IsHeader {
        get { return Name.StartsWith(".", StringComparison.Ordinal); }
    }

    #endregion Name types


    #region Overrides

    /// <summary>
    /// Returns a hash code for the current object.
    /// </summary>
    public override int GetHashCode() {
        return Name.GetHashCode();
    }

    #endregion


    #region Validation

    internal static StringComparer NameComparer => StringComparer.OrdinalIgnoreCase;


    private static readonly Regex NameRegex = new(@"^\.?[\p{L}\p{Nd}][\p{L}\p{Nd}-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and dashes
    /// <summary>
    /// Returns true if name is valid.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <exception cref="NullReferenceException">Name cannot be null.</exception>
    public static bool IsNameValid(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        return AAField.NameRegex.IsMatch(name);
    }

    #endregion Validation

}
