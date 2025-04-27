namespace AASeqPlugin;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AASeq;

/// <summary>
/// Diameter client.
/// </summary>
internal sealed class DiameterClientThread : IDiameterThread, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="remote">Remote endpoint.</param>
    /// <param name="capabilityExchangeNodes">Nodes for Capability-Exchange-Request.</param>
    /// <param name="diameterWatchdogNodes">Nodes for Diameter-Watchdog-Request.</param>
    public DiameterClientThread(IPEndPoint remote, AASeqNodes capabilityExchangeNodes, AASeqNodes diameterWatchdogNodes) {
        Remote = remote;

        Client = new TcpClient();
        Client.Connect(Remote);

        Thread = new Thread(Run) {
            IsBackground = true,
            Name = "DiameterClientThread",
        };
        Thread.Start();
    }

    private readonly IPEndPoint Remote;
    private TcpClient Client;
    private readonly Thread Thread;
    private readonly CancellationTokenSource Cancellation = new();

    private void Run() {
        var cancel = Cancellation.Token;

        while (true) {
            if (cancel.IsCancellationRequested) { return; }

            try {
                using var stream = Client.GetStream();
                using var diameter = new DiameterStream(stream);

                var cer = new DiameterMessage(0, 0, 0, 0, 0, []);
                diameter.WriteMessage(cer);

                var cea = diameter.ReadMessage();

                while (true) {
                    var message = diameter.ReadMessage();
                }
            } catch (Exception ex) {
                if (cancel.IsCancellationRequested) { return; }
                Debug.WriteLine($"[AASeq.Plugin.Diameter] {Remote}: {ex.Message}");
                Thread.Sleep(1000);
            }

            try {
                Client.Close();
                Client = new TcpClient();
                Client.Connect(Remote);
                Debug.WriteLine($"[AASeq.Plugin.Diameter] {Remote}: Connection reestablished");
            } catch (Exception ex) {
                Debug.WriteLine($"[AASeq.Plugin.Diameter] {Remote}: {ex.Message}");
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
