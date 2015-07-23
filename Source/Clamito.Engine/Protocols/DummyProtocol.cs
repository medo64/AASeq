using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Clamito {

    /// <summary>
    /// Dummy protocol.
    /// </summary>
    public sealed class DummyProtocol : ProtocolPlugin {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public DummyProtocol() { }


        #region Definition

        /// <summary>
        /// Gets unique name for protocol.
        /// </summary>
        public override string Name { get { return "Dummy"; } }

        /// <summary>
        /// Gets protocol behaviour model.
        /// </summary>
        public override ProtocolPluginModel Model { get { return ProtocolPluginModel.Peer; } }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public override string Description { get { return "Peer without a specific protocol."; } }

        #endregion


        #region Setup

        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="data">Protocol data.</param>
        public override ResultCollection Initialize(FieldCollection data) {
            return true;
        }

        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="data">Message data.</param>
        public override ResultCollection Send(FieldCollection data) {
            return true;
        }

        /// <summary>
        /// Setups message content for next receive.
        /// To be used only with dummy protocol.
        /// </summary>
        /// <param name="data">Message content.</param>
        public void PokeReceive(FieldCollection data) {
            this.ContentQueue.Enqueue(data);
        }

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="data">Message data.</param>
        public override ResultCollection Receive(out FieldCollection data) {
            if (this.ContentQueue.Count > 0) {
                data = this.ContentQueue.Dequeue();
                return true;
            } else {
                data = null;
                return ErrorResult.NewError("No content to return.");
            }
        }

        #endregion


        #region Content buffer

        private readonly Queue<FieldCollection> ContentQueue = new Queue<FieldCollection>();

        #endregion

    }
}
