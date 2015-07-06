using System;

namespace Clamito {

    /// <summary>
    /// Protocol model.
    /// </summary>
    public enum ProtocolPluginModel {
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
