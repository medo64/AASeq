using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AASeq;

/// <summary>
/// Tag.
/// </summary>
[DebuggerDisplay("{State ? Name : \"!\" + Name}")]
public sealed class AATag {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="state">State.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AATag(string name, bool state) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }
        Name = name;
        State = state;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AATag(string name)
        : this(name, true) {
    }


    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets state for a tag.
    /// </summary>
    public bool State { get; }


    #region Overrides

    /// <summary>
    /// Returns a hash code for the current object.
    /// </summary>
    public override int GetHashCode() {
        return Name.GetHashCode();
    }

    #endregion Overrides


    #region Validation

    internal static StringComparer NameComparer => StringComparer.OrdinalIgnoreCase;

    private static readonly Regex NameRegex = new(@"^@?\p{L}[\p{L}\p{Nd}]*$");  // allowed only letters and numbers; can start with at sign (@)
    /// <summary>
    /// Returns true if name is valid.
    /// </summary>
    /// <param name="name">Name.</param>
    public static bool IsNameValid(string name) {
        return (name != null) && NameRegex.IsMatch(name);
    }

    #endregion Validation

}
