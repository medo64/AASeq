using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AASeq;

/// <summary>
/// Message.
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class AAMessage : AAInteraction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Message name.</param>
    /// <param name="source">Source endpoint.</param>
    /// <param name="destination">Destination endpoint.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null. -or- Source cannot be null. -or- Destination cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters. -or- Source and destination cannot be the same.</exception>
    public AAMessage(string name, AAEndpoint source, AAEndpoint destination)
        : this(name, source, destination, new AAFieldCollection()) {
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Message name.</param>
    /// <param name="source">Source endpoint.</param>
    /// <param name="destination">Destination endpoint.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null. -or- Source cannot be null. -or- Destination cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters. -or- Source and destination cannot be the same.</exception>
    public AAMessage(string name, AAEndpoint source, AAEndpoint destination, AAFieldCollection fields)
        : base(name, fields) {
        if (source is null) { throw new ArgumentNullException(nameof(source), "Source cannot be null."); }
        if (destination is null) { throw new ArgumentNullException(nameof(destination), "Destination cannot be null."); }
        if (source.Name.Equals(destination.Name, AAEndpoint.NameComparison)) { throw new ArgumentOutOfRangeException(nameof(destination), "Source and destination cannot be the same."); }
        Source = source;
        Destination = destination;
    }


    /// <summary>
    /// Gets source.
    /// </summary>
    public AAEndpoint Source { get; }

    /// <summary>
    /// Gets destination.
    /// </summary>
    public AAEndpoint Destination { get; }


    #region Clone

    /// <summary>
    /// Creates a copy of the message.
    /// </summary>
    public override AAInteraction Clone() {
        var message = new AAMessage(Name, Source.Clone(), Destination.Clone(), (AAFieldCollection)Fields.Clone());
        return message;
    }

    #endregion

}
