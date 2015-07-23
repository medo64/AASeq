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
        public override ResultCollection Initialize(FieldCollection data) {
            foreach (var node in data.PathsWithValue) {
                if (node.Path.Equals("Host", StringComparison.OrdinalIgnoreCase)) {
                    this.hostNameOrAddress = node.Field.Value;
                } else {
                    //TODO report warning
                }
            }

            return true;
        }


        private string hostNameOrAddress = null;

        #endregion


        #region Flow

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="data">Message data.</param>
        public override ResultCollection Send(FieldCollection data) {
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

            var reply = ping.Send(this.hostNameOrAddress, timeout, buffer, pingOptions);
            this.replies.Enqueue(reply);

            return true;
        }

        private Queue<PingReply> replies = new Queue<PingReply>();

        /// <summary>
        /// Returns received message or null if timeout occurred.
        /// </summary>
        /// <param name="data">Message content.</param>
        public override ResultCollection Receive(out FieldCollection data) {
            var replyData = new FieldCollection();

            var reply = this.replies.Dequeue();

            replyData.Add("Status", reply.Status.ToString());
            replyData.Add("RoundtripTime", reply.RoundtripTime.ToString());

            data = replyData;
            return true;
        }

        #endregion


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public override FieldCollection GetDefaultData() {
            return new FieldCollection();
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public override ResultCollection ValidateData(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            var errors = new List<ErrorResult>();
            foreach (var node in data.AllPaths) {
                if (!node.Path.Equals("Timeout", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals("BufferSize", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals(".DontFragment", StringComparison.OrdinalIgnoreCase)
                    && !node.Path.Equals(".TimeToLive", StringComparison.OrdinalIgnoreCase)) {
                    errors.Add(ErrorResult.NewWarning("Unknown field: {0}.", node));
                }
            }
            return new ResultCollection(errors);
        }

        #endregion

    }
}
