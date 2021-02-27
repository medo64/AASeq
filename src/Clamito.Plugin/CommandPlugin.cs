using System;
using System.Collections.Generic;

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
            get { return Name; }
        }

        #endregion


        #region Instance

        /// <summary>
        /// Creates new instance of current class.
        /// </summary>
        public CommandPlugin CreateInstance() {
            return (CommandPlugin)Activator.CreateInstance(GetType());
        }

        #endregion


        #region Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="data">Command data.</param>
        public abstract IEnumerable<Failure> Execute(FieldCollection data);

        #endregion


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        public virtual FieldCollection GetDefaultData() {
            return null;
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public virtual IEnumerable<Failure> ValidateData(FieldCollection data) {
            yield break;
        }

        #endregion

    }
}
