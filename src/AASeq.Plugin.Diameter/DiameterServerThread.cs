namespace AASeqPlugin;
using System;
using System.Net;
using AASeq;
using Microsoft.Extensions.Logging;

/// <summary>
/// Diameter client.
/// </summary>
internal sealed class DiameterServerThread : IDiameterThread, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="local">Local endpoint.</param>
    /// <param name="capabilityExchangeAnswerNodes">Nodes for Capability-Exchange-Answer.</param>
    /// <param name="diameterWatchdogRequestNodes">Nodes for Diameter-Watchdog-Request.</param>
    public DiameterServerThread(ILogger logger, IPEndPoint local, AASeqNodes capabilityExchangeAnswerNodes, AASeqNodes diameterWatchdogRequestNodes) {
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
