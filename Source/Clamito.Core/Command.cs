using System;
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
            : base(name) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="parameters">Parameters.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters.</exception>
        public Command(string name, string parameters)
            : this(name) {
            this.Parameters = parameters;
        }


        private string _parameters;
        /// <summary>
        /// Gets/sets parameters.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public string Parameters {
            get { return this._parameters ?? ""; }
            set {
                if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }
                if (value == null) { value = ""; }
                this._parameters = value;
                this.OnChanged(new EventArgs());
            }
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
            return new Command(this.Name, this.Parameters) { Description = this.Description };
        }

        /// <summary>
        /// Creates a read-only copy of the command.
        /// </summary>
        public override Interaction AsReadOnly() {
            return new Command(this.Name, this.Parameters) { Description = this.Description, IsReadOnly = true };
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            if (string.IsNullOrEmpty(this.Parameters)) {
                return this.Name;
            } else {
                return this.Name + " " + this.Parameters;
            }
        }

        #endregion

    }
}
