using System;
using System.Collections.Generic;
using System.Globalization;

namespace Clamito {

    /// <summary>
    /// Protocol interface.
    /// </summary>
    public abstract class ProtocolPlugin : PluginBase {

        #region Definition

        /// <summary>
        /// Gets protocol behaviour model.
        /// </summary>
        public abstract ProtocolPluginModel Model { get; }

        /// <summary>
        /// Gets display name for protocol.
        /// </summary>
        public override string DisplayName {
            get { return this.Name + " " + this.Model.ToString().ToLower(CultureInfo.CurrentCulture); }
        }

        #endregion


        #region Instance

        /// <summary>
        /// Creates new instance of current class.
        /// </summary>
        public ProtocolPlugin CreateInstance() {
            return (ProtocolPlugin)Activator.CreateInstance(this.GetType());
        }

        #endregion


        #region Setup

        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="data">Protocol data.</param>
        public abstract ResultCollection Initialize(FieldCollection data);

        /// <summary>
        /// Stops protocol and releases all resources.
        /// </summary>
        public virtual ResultCollection Terminate() {
            this.Dispose();
            return true;
        }

        #endregion


        #region Execute

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="data">Message data.</param>
        public abstract ResultCollection Send(FieldCollection data);

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="data">Message content.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "0#", Justification = "This method is to be used only by experienced protocol developers so there won't be any usability issues.")]
        public abstract ResultCollection Receive(out FieldCollection data);

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
