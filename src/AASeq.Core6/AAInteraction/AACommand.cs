using System;
using System.Diagnostics;

namespace AASeq;

/// <summary>
/// Command.
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class AACommand : AAInteraction {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="name">Command name.</param>
    /// <exception cref="ArgumentNullException">Name cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Name contains invalid characters.</exception>
    public AACommand(string name)
        : base(name) {
    }

    public AACommand(string name, AAFieldCollection fields)
        : base(name, fields) {
    }

    #region Clone

    /// <summary>
    /// Creates a copy of the command.
    /// </summary>
    public override AAInteraction Clone() {
        return new AACommand(Name, (AAFieldCollection)Fields.Clone());
    }

    #endregion Clone

}
