namespace AASeqPlugin;
using System;
using System.Net;
using AASeq;

/// <summary>
/// Diameter client.
/// </summary>
internal sealed class DiameterServerThread : IDiameterThread, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="local">Local endpoint.</param>
    /// <param name="capabilityExchangeNodes">Nodes for Capability-Exchange-Answer.</param>
    /// <param name="diameterWatchdogNodes">Nodes for Diameter-Watchdog-Request.</param>
    public DiameterServerThread(IPEndPoint local, AASeqNodes capabilityExchangeNodes, AASeqNodes diameterWatchdogNodes) {
        throw new NotSupportedException("Diameter server not supported.");
    }


    #region IDiameterThread

    public void Stop() {
    }

    #endregion IDiameterThread


    #region IDisposable

    public void Dispose() {
    }

    #endregion IDisposable

}
