namespace AASeq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;

internal class Variables : IDictionary<string, string> {

    public Variables(ILogger logger, PluginManager pluginManager) {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        PluginManager = pluginManager ?? throw new ArgumentNullException(nameof(pluginManager));
    }

    private readonly ILogger Logger;
    private readonly PluginManager PluginManager;

    private readonly Dictionary<string, string> Vars = new(StringComparer.OrdinalIgnoreCase);


    #region IDictionary<string, string>

    /// <summary>
    /// Get or set a variable.
    /// On get, if variable is not found, return empty string.
    /// On set, if variable already exists, it will be overwritten.
    /// </summary>
    /// <param name="key">Key.</param>
    public string this[string key] {
        get { return TryGetValueOrEnvironment(key, out var value) ? value : string.Empty; }
        set {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentOutOfRangeException.ThrowIfNullOrEmpty(key);
            Vars[key] = value ?? string.Empty;
        }
    }

    /// <summary>
    /// Gets all keys.
    /// </summary>
    public ICollection<string> Keys => Vars.Keys;

    /// <summary>
    /// Gets all values.
    /// </summary>
    public ICollection<string> Values => Vars.Values;

    /// <summary>
    /// Gets the number of variables.
    /// </summary>
    public int Count => Vars.Count;

    /// <summary>
    /// Gets whether dictionary is empty.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds a new variable.
    /// </summary>
    /// <param name="key">Variable name.</param>
    /// <param name="value">Variable value.</param>
    public void Add(string key, string value) {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(key);
        Vars.Add(key, value ?? string.Empty);
    }

    /// <summary>
    /// Adds a new variable.
    /// </summary>
    /// <param name="item">Item.</param>
    public void Add(KeyValuePair<string, string> item) {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(item.Key);
        Vars.Add(item.Key, item.Value ?? string.Empty);
    }

    /// <summary>
    /// Clears all variables.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Clear() {
        Vars.Clear();
    }

    /// <summary>
    /// Returns if the variable exists.
    /// </summary>
    /// <param name="item">Item.</param>
    public bool Contains(KeyValuePair<string, string> item) {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(item.Key);
        if (TryGetValueOrEnvironment(item.Key, out var value)) {
            if (string.Equals(item.Value, value, StringComparison.Ordinal)) { return true; }
        }
        return false;
    }

    /// <summary>
    /// Returns if the variable exists.
    /// </summary>
    /// <param name="key">Variable name.</param>
    public bool ContainsKey(string key) {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(key);
        return Vars.ContainsKey(key);
    }

    /// <summary>
    /// Copies all variables to an array.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <param name="arrayIndex">Start index.</param>
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) {
        ((ICollection)Vars).CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
        return Vars.GetEnumerator();
    }

    /// <summary>
    /// Removes a variable.
    /// </summary>
    /// <param name="key">Variable name.</param>
    public bool Remove(string key) {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(key);
        return Vars.Remove(key);
    }

    public bool Remove(KeyValuePair<string, string> item) {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentOutOfRangeException.ThrowIfNullOrEmpty(item.Key);
        if (Vars.TryGetValue(item.Key, out var value)) {
            if (string.Equals(item.Value, value, StringComparison.Ordinal)) {
                Vars.Remove(item.Key);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Tries to get the value of the variable.
    /// </summary>
    /// <param name="key">Variable name.</param>
    /// <param name="value">Output value.</param>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value) {
        return TryGetValueOrEnvironment(key, out value);
    }

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
        return Vars.GetEnumerator();
    }

    #endregion IDictionary<string, string>


    /// <summary>
    /// Expands variables.
    /// </summary>
    /// <param name="nodes">Nodes.</param>
    public AASeqNodes GetExpanded(AASeqNodes nodes) {
        var clone = nodes.Clone();
        ExpandNodes(clone);
        return clone;
    }


    private void ExpandNodes(AASeqNodes nodes) {
        foreach (var node in nodes) {
            if (node.Value.RawValue is string value) {  // only replace string values
                if (value.Contains('$', StringComparison.Ordinal)) {
                    node.Value = ExpandValue(value);
                }
            }
            if (node.Nodes.Count > 0) { ExpandNodes(node.Nodes); }
        }
    }

    private enum State { Text, ParameterStart, SimpleParameter, ComplexParameter, ComplexParameterWithInstructions }

    /// <summary>
    /// Performs basic shell parameter expansion.
    /// </summary>
    /// <remarks>
    /// https://www.gnu.org/software/bash/manual/html_node/Shell-Parameter-Expansion.html
    /// The following substitutions are supported:
    ///     ${parameter-word}
    ///     ${parameter=word}
    ///     ${parameter+word}
    ///     ${parameter:-word}
    ///     ${parameter:=word}
    ///     ${parameter:+word}
    ///     ${parameter:offset}
    ///     ${parameter:offset:length}
    ///     ${!parameter}
    ///     ${#parameter}
    ///     ${parameter@U}
    ///     ${parameter@u}
    ///     ${parameter@L}
    /// </remarks>
    private string ExpandValue(string text) {
        if (text == null) { throw new ArgumentNullException(nameof(text), "Text cannot be null."); }

        var state = State.Text;
        var sbOutput = new StringBuilder();
        var sbParameterName = new StringBuilder();
        var sbParameterInstructions = new StringBuilder();
        var braceLevel = 0;

        var queue = new Queue<char?>();
        foreach (var ch in text) {
            queue.Enqueue(ch);
        }
        queue.Enqueue(null);  // marker for end of parsing

        while (queue.Count > 0) {
            var ch = queue.Dequeue();

            switch (state) {
                case State.Text: {
                        if (ch is '$') {
                            sbParameterName.Clear();
                            state = State.ParameterStart;
                        } else {
                            sbOutput.Append(ch);
                        }
                    }
                    break;

                case State.ParameterStart: {
                        if (ch is '{') {  // start complex parameter
                            state = State.ComplexParameter;
                            braceLevel = 1;
                        } else if (ch.HasValue && (char.IsLetterOrDigit(ch.Value) || (ch is '_'))) {  // normal variable
                            sbParameterName.Append(ch);
                            state = State.SimpleParameter;
                        } else {  // just assume it's normal text
                            sbOutput.Append('$');
                            sbOutput.Append(ch);
                            state = State.Text;
                        }
                    }
                    break;

                case State.SimpleParameter: {
                        if (ch.HasValue && (char.IsLetterOrDigit(ch.Value) || (ch is '_'))) {  // continue as variable
                            sbParameterName.Append(ch);
                        } else if (ch.HasValue && (ch is '$')) {  // next variable starts immediatelly
                            OnRetrieveParameter(sbParameterName.ToString(), null, out var value);
                            sbOutput.Append(value);
                            sbParameterName.Clear();
                        } else {  // parameter done
                            if (sbParameterName.Length > 0) {
                                OnRetrieveParameter(sbParameterName.ToString(), null, out var value);
                                sbOutput.Append(value);
                            } else if (ch is null) {  // lone ending $
                                sbOutput.Append('$');
                            }
                            if (ch is not null) { sbOutput.Append(ch); }  // only add char if not null
                            state = State.Text;
                        }
                    }
                    break;

                case State.ComplexParameter: {
                        if (ch is '}') {  // parameter done
                            OnRetrieveParameter(sbParameterName.ToString(), null, out var value);
                            sbOutput.Append(value);
                            state = State.Text;
                        } else if ((sbParameterName.Length == 0) && (ch is '!' or '#')) { //indirection must be the first character
                            sbParameterInstructions.Clear();
                            sbParameterInstructions.Append(ch);
                            state = State.ComplexParameterWithInstructions;
                        } else if (ch is '+' or '-' or ':' or '=' or '@' or ',' or '^') {
                            sbParameterInstructions.Clear();
                            sbParameterInstructions.Append(ch);
                            state = State.ComplexParameterWithInstructions;
                        } else {
                            sbParameterName.Append(ch);
                        }
                    }
                    break;

                case State.ComplexParameterWithInstructions: {
                        if ((ch is '}') && (braceLevel == 1)) {  // parameter done
                            var instructions = ExpandValue(sbParameterInstructions.ToString());
                            var parameterName = sbParameterName.ToString();

                            if (string.IsNullOrEmpty(parameterName) && instructions.StartsWith("!")) {  // ${!parameter} indirect
                                var parameterNameQuery = instructions[1..];
                                OnRetrieveParameter(parameterNameQuery, null, out var indirectParameterName);
                                if (!string.IsNullOrEmpty(indirectParameterName)) {
                                    OnRetrieveParameter(indirectParameterName, null, out var value);
                                    sbOutput.Append(value);
                                }
                            } else if (string.IsNullOrEmpty(parameterName) && instructions.StartsWith("#")) {  // ${#parameter} parameter length
                                var innerParameterName = instructions[1..];
                                OnRetrieveParameter(innerParameterName, null, out var value);
                                if (string.IsNullOrEmpty(value)) { value = ""; }
                                sbOutput.Append(value.Length.ToString(CultureInfo.InvariantCulture));
                            } else if (instructions.StartsWith(":+")) {  // ${parameter:+word} use alternate value even if empty
                                var alternateValue = instructions[2..];
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (!string.IsNullOrEmpty(value)) {
                                    sbOutput.Append(alternateValue);
                                }
                            } else if (instructions.StartsWith("+")) {  // ${parameter+word} use alternate value
                                var alternateValue = instructions[1..];
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    sbOutput.Append(alternateValue);
                                }
                            } else if (instructions.StartsWith(":-")) {  // ${parameter:-word} use default even if empty
                                var defaultValue = instructions[2..];
                                OnRetrieveParameter(parameterName, defaultValue, out var value);
                                if (string.IsNullOrEmpty(value)) { value = defaultValue; }  // also replace if it's empty
                                sbOutput.Append(value);
                            } else if (instructions.StartsWith("-")) {  // ${parameter-word} use default
                                var defaultValue = instructions[1..];
                                OnRetrieveParameter(parameterName, defaultValue, out var value);
                                sbOutput.Append(value);
                            } else if (instructions.StartsWith(":=")) {  // ${parameter:=word} use default and set variable even if empty
                                var defaultValue = instructions[2..];
                                OnRetrieveParameter(parameterName, defaultValue, out var value);
                                if (string.IsNullOrEmpty(value)) { value = defaultValue; }  // also replace if it's empty
                                sbOutput.Append(value);
                                Vars[parameterName] = value;
                            } else if (instructions.StartsWith("=")) {  // ${parameter=word} use default and set variable
                                var defaultValue = instructions[1..];
                                OnRetrieveParameter(parameterName, defaultValue, out var value);
                                sbOutput.Append(value);
                                Vars[parameterName] = value ?? string.Empty;
                            } else if (instructions.StartsWith(":")) {  // ${parameter:offset:length} substring
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    var newValue = GetSubstring(value, instructions[1..]);
                                    if (newValue != null) { sbOutput.Append(newValue); }
                                }
                            } else if (instructions.StartsWith("@")) {  // ${parameter@operator} perform modifications
                                var oper = instructions[1..];
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    var newValue = (oper) switch {
                                        "U" => value.ToUpperInvariant(),
                                        "u" => value[0..1].ToUpperInvariant() + value[1..],
                                        "L" => value.ToLowerInvariant(),
                                        _ => value,
                                    };
                                    sbOutput.Append(newValue);
                                }
                            } else if (instructions == "^^") {  // ${parameter^^} uppercase
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    sbOutput.Append(value.ToUpperInvariant());
                                }
                            } else if (instructions == "^") {  // ${parameter^} uppercase first letter
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    sbOutput.Append(value[0..1].ToUpperInvariant() + value[1..]);
                                }
                            } else if (instructions == ",,") {  // ${parameter,,} lowercase
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    sbOutput.Append(value.ToLowerInvariant());
                                }
                            } else if (instructions == ",") {  // ${parameter,} lowercase first letter
                                OnRetrieveParameter(parameterName, null, out var value);
                                if (value != null) {
                                    sbOutput.Append(value[0..1].ToLowerInvariant() + value[1..]);
                                }
                            } else {
                                OnRetrieveParameter(parameterName, null, out var value);
                                sbOutput.Append(value);
                            }

                            state = State.Text;
                        } else {
                            sbParameterInstructions.Append(ch);
                            if (ch is '{') {
                                braceLevel += 1;
                            } else if (ch is '}') {
                                braceLevel -= 1;
                            }
                        }
                    }
                    break;

            }
        }

        return sbOutput.ToString();
    }

    private void OnRetrieveParameter(string name, string? defaultValue, out string? value) {
        if (!TryGetValueOrEnvironment(name, out value)) {
            value = defaultValue;
        }
    }

    private static string? GetSubstring(string value, string arguments) {
        var parts = arguments.Split(':', StringSplitOptions.None);
        string? startText = (parts.Length >= 1) && (parts[0].Length > 0) ? parts[0] : null;
        string? lengthText = (parts.Length >= 2) && (parts[1].Length > 0) ? parts[1] : null;

        int length;
        if (startText == null) {  // output whole thing
            return value;
        } else {
            if (!int.TryParse(startText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var start)) { return null; }
            if (start < 0) { start = value.Length + start; }
            if ((start < 0) || (start > value.Length)) { return null; }
            if (lengthText == null) {
                length = value.Length - start;
            } else {
                if (!int.TryParse(lengthText, NumberStyles.Integer, CultureInfo.InvariantCulture, out length)) { return null; }
                if (length < 0) { length = value.Length - start + length; }
                if (length < 0) { return null; }
                if (start + length > value.Length) { length = value.Length - start; }
            }
            return value.Substring(start, length);
        }
    }


    private Dictionary<string, string> EnvironmentVarTranslation = new(StringComparer.OrdinalIgnoreCase);

    private bool TryGetValueOrEnvironment(string key, [MaybeNullWhen(false)] out string value) {
        if (key is null) { value = null; return false; }
        if (Vars.TryGetValue(key, out value)) { return true; }

        var variablePlugin = PluginManager.FindVariablePlugin(key);
        if (variablePlugin is not null) {
            var variableValue = variablePlugin.GetVariableValue(Logger, "");  // TODO: add argument
            if (variableValue is not null) {
                value = variableValue;
                return true;
            }
        }

        if (!EnvironmentVarTranslation.TryGetValue(key, out var envName)) {
            var sb = new StringBuilder(key.Length);
            foreach (var ch in key) {
                sb.Append(char.IsLetterOrDigit(ch) ? char.ToUpperInvariant(ch) : '_');
            }
            envName = sb.ToString().TrimEnd('_');
            if (string.IsNullOrEmpty(envName)) { value = null; return false; }
            EnvironmentVarTranslation[key] = envName;
        }

        if (Environment.GetEnvironmentVariable(envName) is string envValue) {
            value = envValue;
            return true;
        }

        value = string.Empty;
        return false;
    }

}
