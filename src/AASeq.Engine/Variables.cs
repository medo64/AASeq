namespace AASeq;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Text;

internal class Variables {

    public Variables() {
    }

    public Variables(IDictionary variables) {
        foreach (DictionaryEntry entry in variables) {
            var key = entry.Key?.ToString();
            if (string.IsNullOrEmpty(key)) { continue; }

            var value = entry.Value?.ToString();
            if (string.IsNullOrEmpty(value)) { continue; }

            Vars[key] = value;
        }
    }

    private readonly Dictionary<string, string> Vars = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Get or set a variable.
    /// On get, if variable is not found, return empty string.
    /// On set, if variable already exists, it will be overwritten.
    /// </summary>
    /// <param name="key">Key.</param>
    public string this[string key] {
        get { return Vars.TryGetValue(key, out var value) ? value : string.Empty; }
        set { Vars[key] = value; }
    }


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
        if (!Vars.TryGetValue(name, out value)) {
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

}
