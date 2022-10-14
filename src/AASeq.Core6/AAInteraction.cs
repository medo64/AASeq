using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AASeq;

/// <summary>
/// Interaction.
/// </summary>
[DebuggerDisplay("{Name}")]
public abstract class AAInteraction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="fields">Fields.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal protected AAInteraction(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }
        Name = name;
        Fields = new AAFieldCollection();
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="fields">Fields.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    internal protected AAInteraction(string name, AAFieldCollection fields) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }
        if (fields == null) { throw new ArgumentNullException(nameof(fields), "Fields cannot be null."); }
        Name = name;
        Fields = fields;
    }


    /// <summary>
    /// Gets/sets name.
    /// </summary>
    public string Name { get; }


    /// <summary>
    /// Gets field collection.
    /// </summary>
    public AAFieldCollection Fields { get; private set; }

    /// <summary>
    /// Sets Data collection.
    /// </summary>
    /// <param name="fields">Fields.</param>
    /// <exception cref="ArgumentNullException">Fields cannot be null.</exception>
    /// <exception cref="NotSupportedException">Object is read-only.</exception>
    public void ReplaceFields(AAFieldCollection fields) {
        if (fields == null) { throw new ArgumentNullException(nameof(fields), "Fields cannot be null."); }
        Fields = fields;
    }


    /// <summary>
    /// Gets whether interaction type is Command.
    /// </summary>
    public bool IsCommand {
        get { return (this is AACommand); }
    }

    /// <summary>
    /// Gets whether interaction type is Message.
    /// </summary>
    public bool IsMessage {
        get { return (this is AAMessage); }
    }


    #region Validation

    internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
    internal static StringComparison NameComparison => StringComparison.OrdinalIgnoreCase;

    internal static readonly Regex NameRegex = new(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}-]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and dashes
    /// <summary>
    /// Returns true if name is valid.
    /// </summary>
    /// <param name="name">Name.</param>
    public static bool IsNameValid(string name) {
        return name != null && NameRegex.IsMatch(name);
    }

    #endregion Validation


    #region Clone

    /// <summary>
    /// Creates a copy of the interaction.
    /// </summary>
    public abstract AAInteraction Clone();

    #endregion

}
