using System;
using System.Reflection;

namespace Clamito {

    /// <summary>
    /// Protocol proxy.
    /// </summary>
    public sealed class ProtocolProxy : IDisposable {

        internal ProtocolProxy(ProtocolResolver protocolPlugin) {
            this.Plugin = protocolPlugin;

            Log.WriteInformation("Creating {0} protocol proxy.", this.Plugin.Name);
            this.Instance = Activator.CreateInstance(protocolPlugin.Type);
            Log.WriteVerbose("Created {0} protocol proxy.", this.Plugin.Name);
        }


        private readonly ProtocolResolver Plugin;
        private Object Instance;


        #region Setup

        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public ResultCollection Initialize(FieldCollection properties) {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            Log.WriteVerbose("Initializing {0} protocol proxy.", this.Plugin.Name);
            var result = ((ProtocolPlugin)this.Instance).Initialize(properties);
            Log.WriteInformation("Initialized {0} protocol proxy.", this.Plugin.Name);
            return result;
        }

        /// <summary>
        /// Stops protocol and releases all resources.
        /// </summary>
        public ResultCollection Terminate() {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            Log.WriteVerbose("Terminating {0} protocol proxy.", this.Plugin.Name);
            var result = ((ProtocolPlugin)this.Instance).Terminate();
            Log.WriteInformation("Terminated {0} protocol proxy.", this.Plugin.Name);
            return result;
        }

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        public void Dispose() {
            var disposableInstance = this.Instance as IDisposable;
            if (disposableInstance != null) {
                Log.WriteVerbose("Disposing {0} protocol proxy.", this.Plugin.Name);
                disposableInstance.Dispose();
                Log.WriteInformation("Disposed {0} protocol proxy.", this.Plugin.Name);
            }

            GC.SuppressFinalize(this);
        }

        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="content">Message content.</param>
        public ResultCollection Send(FieldCollection content) {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            Log.WriteInformation("Sending content for {0} protocol proxy.", this.Plugin.Name);
            var result = ((ProtocolPlugin)this.Instance).Send(content);
            Log.WriteVerbose("Sent content for {0} protocol proxy.", this.Plugin.Name);
            return result as ResultCollection;
        }

        /// <summary>
        /// Setups message content for next receive.
        /// To be used only with dummy protocol.
        /// Content clone will be made if necessary.
        /// </summary>
        /// <param name="content">Message content.</param>
        internal void PokeReceive(FieldCollection content) {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            var dummy = this.Instance as DummyProtocol;
            if (dummy != null) {
                Log.WriteInformation("Poking content for {0} protocol proxy.", this.Plugin.Name);
                dummy.PokeReceive(content.AsReadOnly());
                Log.WriteVerbose("Poked content for {0} protocol proxy.", this.Plugin.Name);
            }
        }

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="content">Message content.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "0#", Justification = "This method is to be used only by experienced protocol developers so there won't be any usability issues.")]
        public ResultCollection Receive(out FieldCollection content) {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            Log.WriteInformation("Receiving content for {0} protocol proxy.", this.Plugin.Name);
            var result = ((ProtocolPlugin)this.Instance).Receive(out content);
            Log.WriteVerbose("Received content for {0} protocol proxy.", this.Plugin.Name);
            return result;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Returns default properties.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method mirrors method in ProtocolPlugin and thus it should use the same signature.")]
        public FieldCollection GetDefaultProperties() {
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            Log.WriteVerbose("Retrieving default properties for {0} protocol proxy.", this.Plugin.Name);
            var result = ((ProtocolPlugin)this.Instance).GetDefaultProperties();
            Log.WriteInformation("Retrieved default properties for {0} protocol proxy.", this.Plugin.Name);
            return result;
        }

        /// <summary>
        /// Throws exception if property cannot be validated.
        /// </summary>
        /// <param name="properties">Properties to validate.</param>
        public ResultCollection ValidateProperties(FieldCollection properties) {
            Log.WriteInformation("Validating properties for {0} protocol proxy.", this.Plugin.Name);
            if (this.Instance == null) { throw new ObjectDisposedException("Instance has already been disposed."); }
            var result = ((ProtocolPlugin)this.Instance).ValidateProperties(properties);
            Log.WriteVerbose("Validated properties for {0} protocol proxy.", this.Plugin.Name);
            return result;
        }

        #endregion

    }
}
