using System;
using System.ComponentModel;

namespace AASeq;

/// <summary>
/// Interaction kind.
/// </summary>
public enum InteractionKind {
    /// <summary>
    /// Invalid value.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    None = 0,
    /// <summary>
    /// Message.
    /// </summary>
    Message = 1,
    /// <summary>
    /// Command.
    /// </summary>
    Command = 2,
}
