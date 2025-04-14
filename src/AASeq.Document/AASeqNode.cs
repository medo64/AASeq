namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

/// <summary>
/// AASeq node.
/// </summary>
[DebuggerDisplay("{Name}, {Properties.Count == 1 ? \"1 property\" : Properties.Count + \" properties\",nq}, {Value is null ? \"(no value)\" : \"\\\"\" + Value.ToString() + \"\\\"\",nq}, {Nodes.Count == 1 ? \"1 child\" : Nodes.Count + \" children\",nq}")]
public sealed partial class AASeqNode {

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    public AASeqNode(string name)
        : this(name, AASeqValue.Null) {
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    /// <param name="value">Value.</param>
    public AASeqNode(string name, AASeqValue value) {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    /// <param name="value">Value.</param>
    public AASeqNode(string name, Object? value)
        : this(name, new AASeqValue(value)) {
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    /// <param name="nodes">Children nodes.</param>
    public AASeqNode(string name, IEnumerable<AASeqNode> nodes)
        : this(name, AASeqValue.Null, nodes) {
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    /// <param name="value">Value.</param>
    /// <param name="nodes">Children nodes.</param>
    public AASeqNode(string name, AASeqValue value, IEnumerable<AASeqNode> nodes)
        : this(name, value) {
        if (nodes is not null) {
            foreach (var node in nodes) {
                Nodes.Add(node);
            }
        }
    }

    /// <summary>
    /// Creates a new node.
    /// </summary>
    /// <param name="name">Node name.</param>
    /// <param name="value">Value.</param>
    /// <param name="nodes">Children nodes.</param>
    public AASeqNode(string name, Object? value, IEnumerable<AASeqNode> nodes)
        : this(name, new AASeqValue(value), nodes) {
    }


    private string _name = null!;  // just to keep CS8618 happy
    /// <summary>
    /// Gets/sets name of the node.
    /// </summary>
    public string Name {
        get { return _name; }
        set {
            ArgumentNullException.ThrowIfNull(value);
            ThrowIfNameIsInvalid(value);
            if (value.Length == 0) { throw new ArgumentOutOfRangeException(nameof(value), "Name cannot be empty."); }
            _name = value;
        }
    }

    /// <summary>
    /// Gets list of properties.
    /// </summary>
    public AASeqProperties Properties { get; } = [];

    private AASeqValue _value = AASeqValue.Null;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public AASeqValue Value {
        get { return _value; }
        set { _value = value ?? AASeqValue.Null; }
    }

    /// <summary>
    /// Gets list of children nodes.
    /// </summary>
    public AASeqNodes Nodes { get; } = [];


    /// <summary>
    /// Returns the clone of given node.
    /// </summary>
    public AASeqNode Clone() {
        var clone = new AASeqNode(Name, Value);
        foreach (var property in Properties) {
            clone.Properties.Add(property.Key, property.Value);
        }
        if (Nodes.Count > 0) {
            foreach (var node in Nodes) {
                clone.Nodes.Add(node.Clone());
            }
        }
        return clone;
    }


    #region Operators

    /// <summary>
    /// Implicit conversion from string.
    /// </summary>
    /// <param name="name">Node name.</param>
    public static implicit operator AASeqNode(string name) => new(name);

    /// <summary>
    /// Returns a new node with the specified name.
    /// </summary>
    /// <param name="name">Node name.</param>
    public static AASeqNode ToAASeqNode(string name) {
        return new AASeqNode(name);
    }

    #endregion Operators


    #region Helpers

    private static void ThrowIfNameIsInvalid([NotNull] string name, [CallerArgumentExpression(nameof(name))] string? paramName = null) {
        if ((name.Length == 0) || InvalidNameRegex().Match(name).Success) {
            throw new ArgumentOutOfRangeException(paramName, "Name is invalid.");
        }
    }

    [GeneratedRegex(@"[\s\p{Cc}\{\}\(\)\\/="";#]", RegexOptions.Compiled)]
    private static partial Regex InvalidNameRegex();

    #endregion Helpers

}
