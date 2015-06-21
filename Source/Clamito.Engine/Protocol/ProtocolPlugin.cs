using System;
using System.Collections.Generic;

namespace Clamito {

    /// <summary>
    /// Protocol interface.
    /// </summary>
    public abstract class ProtocolPlugin : IDisposable {

        #region Setup
        
        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public abstract ResultCollection Initialize(FieldCollection properties);

        /// <summary>
        /// Stops protocol and releases all resources.
        /// </summary>
        public virtual ResultCollection Terminate() {
            this.Dispose();
            return true;
        }
        
        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="content">Message content.</param>
        public abstract ResultCollection Send(FieldCollection content);

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="content">Message content.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "0#", Justification = "This method is to be used only by experienced protocol developers so there won't be any usability issues.")]
        public abstract ResultCollection Receive(out FieldCollection content);

        #endregion


        #region IDisposable

        /// <summary>
        /// Releases all non-managed resources.
        /// </summary>
        ~ProtocolPlugin() {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        /// <param name="disposing">True if disposing managed objects.</param>
        protected virtual void Dispose(bool disposing) {
            //override this method if disposal is needed
        }

        #endregion


        #region Properties

        /// <summary>
        /// Returns default properties.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public virtual FieldCollection GetDefaultProperties() {
            return null;
        }

        /// <summary>
        /// Throws exception if property cannot be validated.
        /// </summary>
        /// <param name="properties">Properties to validate.</param>
        public virtual ResultCollection ValidateProperties(FieldCollection properties) {
            if ((properties != null) && (properties.Count > 0)) {
                var results = new List<ErrorResult>();
                //foreach (var item in FieldCollection.EnumerateTreeValues(properties)) {
                    //results.Add(ErrorResult.NewWarning("Cannot validate property {0}.", item.Key));
                //}
                return new ResultCollection(results);
            } else {
                return new ResultCollection();
            }
        }

        #endregion

    }
}
