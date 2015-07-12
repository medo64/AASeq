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


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public virtual FieldCollection GetDefaultData() {
            return null;
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public virtual ResultCollection ValidateData(FieldCollection data) {
            return true;
        }

        #endregion

    }
}
