using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Clamito.Plugin {
    /// <summary>
    /// Ping protocol client.
    /// </summary>
    public class PingProtocolPlugin : ProtocolPlugin {

        #region Definition

        /// <summary>
        /// Gets unique name for protocol.
        /// </summary>
        public override string Name { get { return "Ping"; } }

        /// <summary>
        /// Gets protocol behaviour model.
        /// </summary>
        public override ProtocolPluginModel Model { get { return ProtocolPluginModel.Client; } }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public override string Description { get { return "Ping client protocol."; } }

        #endregion


        #region Setup

        /// <summary>
        /// Starts protocol and allocates all needed resources.
        /// </summary>
        /// <param name="data">Protocol data.</param>
        public override IEnumerable<Failure> Initialize(FieldCollection data) {
            foreach (var node in data.PathsWithValue) {
                if (node.Path.Equals("Host", StringComparison.OrdinalIgnoreCase)) {
                    hostNameOrAddress = node.Field.Value;
                } else {
                    yield return Failure.NewWarning("Unknown field: {0}.", node.Path);
                }
            }
            yield break;
        }


        private string hostNameOrAddress = null;

        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="data">Message data.</param>
        public override IEnumerable<Failure> Send(FieldCollection data) {
            int timeout = 1000;
            int bufferSize = 128;
            bool dontFragment = false;
            int timeToLive = 128;

            foreach (var node in data.PathsWithValue) {
                if (node.Path.Equals("Timeout", StringComparison.OrdinalIgnoreCase)) {
                    timeout = node.Field?.ValueAsInt32 ?? timeout;
                    if (timeout < 10) { timeout = 10; }
                    if (timeout > 1000) { timeout = 1000; }
                } else if (node.Path.Equals("BufferSize", StringComparison.OrdinalIgnoreCase)) {
                    bufferSize = node.Field?.ValueAsInt32 ?? bufferSize;
                    if (bufferSize < 16) { bufferSize = 16; }
                    if (bufferSize > 65500) { bufferSize = 65500; }
                } else if (node.Path.Equals(".TimeToLive", StringComparison.OrdinalIgnoreCase)) {
                    timeToLive = node.Field?.ValueAsInt32 ?? timeToLive;
                } else if (node.Path.Equals(".DontFragment", StringComparison.OrdinalIgnoreCase)) {
                    dontFragment = node.Field?.ValueAsBoolean ?? dontFragment;
                } else {
                    //TODO report warning
                }
            }

            var guid = Guid.NewGuid();
            var buffer = new byte[bufferSize];
            Buffer.BlockCopy(guid.ToByteArray(), 0, buffer, 0, 16);

            var pingOptions = new PingOptions(timeToLive, dontFragment);
            var ping = new Ping();

            var reply = ping.Send(hostNameOrAddress, timeout, buffer, pingOptions);
            replies.Enqueue(reply);

            yield break;
        }

        private readonly Queue<PingReply> replies = new Queue<PingReply>();

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="receivedData">Message data. Must be empty; will be filled by function.</param>
        public override IEnumerable<Failure> Receive(FieldCollection receivedData) {
            var reply = replies.Dequeue();
            receivedData.Add("Status", reply.Status.ToString());
            receivedData.Add("RoundtripTime", reply.RoundtripTime.ToString());
            yield break;
        }

        #endregion


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        public override FieldCollection GetDefaultData() {
            return new FieldCollection();
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public override IEnumerable<Failure> ValidateData(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            foreach (var node in data.AllPaths) {
                if (!node.Path.Equals("Timeout", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals("BufferSize", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals(".DontFragment", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals(".TimeToLive", StringComparison.OrdinalIgnoreCase)) {
                    yield return Failure.NewWarning("Unknown field: {0}.", node);
                }
            }
        }

        #endregion

    }
}
