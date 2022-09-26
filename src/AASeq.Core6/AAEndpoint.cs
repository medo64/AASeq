using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AASeq;

/// <summary>
/// Endpoint.
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class AAEndpoint {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AAEndpoint(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }

        Name = name;
        PluginName = String.Empty;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="pluginName">Protocol used for the endpoint.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null. -or- Plugin name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AAEndpoint(string name, string pluginName) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        if (pluginName == null) { throw new ArgumentNullException(nameof(pluginName), "Plugin name cannot be null."); }
        if (!IsNameValid(name)) { throw new ArgumentOutOfRangeException(nameof(name), "Name contains invalid characters."); }

        Name = name;
        PluginName = pluginName;
    }


    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets plugin name for the endpoint.
    /// If empty, no plugin is to be used.
    /// </summary>
    public string PluginName { get; }

    private readonly Lazy<AAFieldCollection> _data = new();
    /// <summary>
    /// Gets data fields collection.
    /// </summary>
    public AAFieldCollection Data {
        get { return _data.Value; }
    }


    #region Validation

    internal static StringComparer NameComparer => StringComparer.OrdinalIgnoreCase;
    internal static StringComparison NameComparison => StringComparison.OrdinalIgnoreCase;

    private static readonly Regex NameRegex = new(@"^[\p{L}][\p{L}\p{Nd}_]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline); //allowed letters, numbers and underscores; must start with letter
    /// <summary>
    /// Returns true if name is valid.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <exception cref="NullReferenceException">Name cannot be null.</exception>
    public static bool IsNameValid(string name) {
        if (name == null) { throw new ArgumentNullException(nameof(name), "Name cannot be null."); }
        return AAEndpoint.NameRegex.IsMatch(name);
    }

    #endregion Validation

}
