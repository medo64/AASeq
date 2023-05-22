namespace AASeq;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

/// <summary>
/// Tag.
/// </summary>
[DebuggerDisplay("{IsSystem ? \"@\" : \"\",nq}{State ? Name : \"!\" + Name,nq}")]
public sealed partial class Tag
    : IEquatable<Tag>
    , IEqualityOperators<Tag, Tag, bool> {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="state">State.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public Tag(string name, bool state) {
        Helpers.ThrowIfArgumentNull(name, nameof(name), "Name cannot be null.");
        Helpers.ThrowIfArgumentFalse(IsNameValid(name), nameof(name), "Name contains invalid characters.");

        if (name.StartsWith("@", NameComparison)) {
            Name = name[1..];
            State = state;
            IsSystem = true;
        } else {
            Name = name;
            State = state;
            IsSystem = false;
        }
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public Tag(string name)
        : this(name, true) {
    }


    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets state for a tag.
    /// </summary>
    public bool State { get; init; }


    /// <summary>
    /// Gets if tag is considered a system tag.
    /// </summary>
    public bool IsSystem { get; }


    #region IEquatable<Tag>

    /// <inheritdoc/>
    public bool Equals(Tag? other) {
        return (other is not null) && NameComparer.Equals(Name, other.Name) && (State == other.State) && (IsSystem == other.IsSystem);
    }

    #endregion IEquatable<Tag>


    #region IEqualityOperators<Tag, Tag, bool>

    /// <inheritdoc/>
    public static bool operator ==(Tag? obj1, Tag? obj2) {
        return (obj1 is not null) && obj1.Equals(obj2);
    }

    /// <inheritdoc/>
    public static bool operator !=(Tag? obj1, Tag? obj2) {
        return !(obj1 == obj2);
    }

    #endregion IEqualityOperators<Tag, Tag, bool>


    #region Overrides

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        return (obj is Tag other) && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode() {
        return Name.GetHashCode(NameComparison);
    }

    #endregion Overrides


    #region Validation

    internal static StringComparer NameComparer => StringComparer.OrdinalIgnoreCase;
    internal static StringComparison NameComparison => StringComparison.OrdinalIgnoreCase;

    [GeneratedRegex("^@?\\p{L}[\\p{L}\\p{Nd}]*$")]  // allowed only letters and numbers; can start with at sign (@)
    private static partial Regex GeneratedNameRegex();
    private static readonly Regex NameRegex = GeneratedNameRegex();

    /// <summary>
    /// Returns true if name is valid.
    /// </summary>
    /// <param name="name">Name.</param>
    public static bool IsNameValid(string name) {
        return name != null && NameRegex.IsMatch(name);
    }

    #endregion Validation

}
