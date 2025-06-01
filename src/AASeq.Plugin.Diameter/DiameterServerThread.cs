namespace AASeqPlugin;
using System;
using System.Net;
using System.Threading;
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
    public DiameterServerThread(Diameter pluginClass, ILogger logger, string localHost, int localPort, AASeqNodes capabilityExchangeAnswerNodes, AASeqNodes diameterWatchdogRequestNodes, int watchdogInterval, bool useNagle) {
        throw new NotSupportedException("Diameter server not supported.");
    }


    #region IDiameterThread

    public void Start(CancellationToken cancellationToken) {
    }

    public void Stop() {
    }

    public IPEndPoint Endpoint { get { return new IPEndPoint(IPAddress.Any, 0); } }

    #endregion IDiameterThread


    #region IDisposable

    public void Dispose() {
    }

    #endregion IDisposable

}
