namespace AASeqPlugin;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AASeq;
using Microsoft.Extensions.Logging;

/// <summary>
/// Diameter client.
/// </summary>
internal sealed class DiameterClientThread : IDiameterThread, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="remote">Remote endpoint.</param>
    /// <param name="capabilityExchangeNodes">Nodes for Capability-Exchange-Request.</param>
    /// <param name="diameterWatchdogNodes">Nodes for Diameter-Watchdog-Request.</param>
    public DiameterClientThread(ILogger logger, IPEndPoint remote, AASeqNodes capabilityExchangeNodes, AASeqNodes diameterWatchdogNodes) {
        Remote = remote;
        Logger = logger;

        Client = new TcpClient();
        var connectResult = Client.BeginConnect(Remote.Address, Remote.Port, requestCallback: null, state: null);
        var waitSuccess = connectResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
        if (waitSuccess) {
            Client.EndConnect(connectResult);
        } else {
            throw new TimeoutException("Timeout waiting for connection to establish.");
        }

        Thread = new Thread(Run) {
            IsBackground = true,
            Name = "DiameterClientThread",
        };
        Thread.Start();
    }

    private readonly ILogger Logger;
    private readonly IPEndPoint Remote;
    private TcpClient Client;
    private readonly Thread Thread;
    private readonly CancellationTokenSource Cancellation = new();

    private void Run() {
        var cancel = Cancellation.Token;

        while (true) {
            if (cancel.IsCancellationRequested) { return; }

            if (Client.Connected) {
                try {
                    using var stream = Client.GetStream();
                    using var diameter = new DiameterStream(stream);

                    //var cer = new DiameterMessage(0, 0, 0, 0, 0, []);
                    //diameter.WriteMessage(cer);

                    //var cea = diameter.ReadMessage();

                    while (true) {
                        var message = diameter.ReadMessage();
                    }
                } catch (Exception ex) {
                    if (cancel.IsCancellationRequested) { return; }
                    Log.ReadError(Logger, Remote, ex, ex.Message);
                    Debug.WriteLine($"[AASeq.Plugin.Diameter] {Remote}: {ex.Message}");
                    Thread.Sleep(1000);
                }
            }

            try {
                Client.Close();
                Client = new TcpClient();

                Client = new TcpClient();
                var connectResult = Client.BeginConnect(Remote.Address, Remote.Port, requestCallback: null, state: null);
                var waitSuccess = connectResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                if (waitSuccess) {
                    Client.EndConnect(connectResult);
                } else {
                    throw new TimeoutException("Timeout waiting for connection to establish.");
                }

                Log.Reconnected(Logger, Remote);
            } catch (Exception ex) {
                Log.ConnectionError(Logger, Remote, ex, ex.Message);
                Thread.Sleep(1000);
            }
        }
    }


    #region IDiameterThread

    public void Stop() {
        Cancellation.Cancel();
        Client.Close();  // to force the read to stop
        Thread.Join();
    }

    #endregion IDiameterThread


    #region IDisposable

    public void Dispose() {
        Cancellation.Dispose();
        Client.Dispose();
    }

    #endregion IDisposable

}
