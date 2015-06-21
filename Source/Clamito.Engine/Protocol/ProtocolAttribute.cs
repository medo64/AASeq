using System;

namespace Clamito {

    /// <summary>
    /// Options for a protocol plugin.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ProtocolAttribute : Attribute {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="model">Protocol model.</param>
        public ProtocolAttribute(ProtocolModel model)
            : this(null, model) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Protocol name.</param>
        /// <param name="model">Protocol model.</param>
        public ProtocolAttribute(String name, ProtocolModel model) {
            this.Model = model;
            this.Name = name;
            this.DisplayName = null;
            this.Description = null;
        }


        /// <summary>
        /// Gets protocol model.
        /// </summary>
        public ProtocolModel Model { get; private set; }

        /// <summary>
        /// Gets name of the protocol.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Gets display name of the protocol.
        /// </summary>
        public String DisplayName { get; set; }

        /// <summary>
        /// Gets description of the protocol.
        /// </summary>
        public String Description { get; set; }

    }



    /// <summary>
    /// Protocol model.
    /// </summary>
    public enum ProtocolModel {
        /// <summary>
        /// Protocol implements both server and client behavior.
        /// </summary>
        Peer = 0,
        /// <summary>
        /// Protocol expects to be contacted by a remote client.
        /// </summary>
        Client = 1,
        /// <summary>
        /// Protocol sends requests to a remote server.
        /// </summary>
        Server = 2,
    }
}
