using System;

namespace Clamito {

    /// <summary>
    /// Command interface.
    /// </summary>
    public abstract class CommandPlugin : PluginBase {

        #region Definition

        /// <summary>
        /// Gets display name for protocol.
        /// </summary>
        public override string DisplayName {
            get { return this.Name; }
        }

        #endregion


        #region Instance

        /// <summary>
        /// Creates new instance of current class.
        /// </summary>
        public CommandPlugin CreateInstance() {
            return (CommandPlugin)Activator.CreateInstance(this.GetType());
        }

        #endregion


        #region Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="data">Command data.</param>
        public abstract ResultCollection Execute(FieldCollection data);

        #endregion

    }
}
