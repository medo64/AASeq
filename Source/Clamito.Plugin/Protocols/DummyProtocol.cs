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
        public override ProtocolModel Model { get { return ProtocolModel.Peer; } }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public override string Description { get { return "Peer without a specific protocol."; } }

        #endregion


        #region Setup

        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public override ResultCollection Initialize(FieldCollection properties) {
            return true;
        }

        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="content">Message content.</param>
        public override ResultCollection Send(FieldCollection content) {
            return true;
        }

        /// <summary>
        /// Setups message content for next receive.
        /// To be used only with dummy protocol.
        /// </summary>
        /// <param name="content">Message content.</param>
        public void PokeReceive(FieldCollection content) {
            this.ContentQueue.Enqueue(content);
        }

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="content">Message content.</param>
        public override ResultCollection Receive(out FieldCollection content) {
            if (this.ContentQueue.Count > 0) {
                content = this.ContentQueue.Dequeue();
                return true;
            } else {
                content = null;
                return ErrorResult.NewError("No content to return.");
            }
        }

        #endregion


        #region Content buffer

        private readonly Queue<FieldCollection> ContentQueue = new Queue<FieldCollection>();

        #endregion

    }
}
