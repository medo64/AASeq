using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Clamito {

    /// <summary>
    /// Command.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public sealed class Command : Interaction {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Command(string name)
            : base(name, null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="fields">Fields.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Command(string name, IEnumerable<Field> fields)
            : this(name, null) {
            this.Data.AddRange(fields);
        }

        private Command(string name, FieldCollection fields)
            : base(name, fields) {
        }

        /// <summary>
        /// Gets interaction kind.
        /// </summary>
        public override InteractionKind Kind { get { return InteractionKind.Command; } }


        #region Clone

        /// <summary>
        /// Creates a copy of the command.
        /// </summary>
        public override Interaction Clone() {
            return new Command(this.Name, this.Data.Clone()) { Description = this.Description };
        }

        /// <summary>
        /// Creates a read-only copy of the command.
        /// </summary>
        public override Interaction AsReadOnly() {
            return new Command(this.Name, this.Data.AsReadOnly()) { Description = this.Description, IsReadOnly = true };
        }

        #endregion

    }
}
