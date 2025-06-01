namespace AASeqPlugin;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Extensions.Logging;
using AASeq;
using AASeq.Diameter;

/// <summary>
/// Diameter client.
/// </summary>
internal sealed class DiameterClientThread : IDiameterThread, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="pluginClass">Plugin class.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="remote">Remote endpoint.</param>
    /// <param name="capabilityExchangeRequestNodes">Nodes for Capability-Exchange-Request.</param>
    /// <param name="diameterWatchdogRequestNodes">Nodes for Diameter-Watchdog-Request.</param>
    public DiameterClientThread(Diameter pluginClass, ILogger logger, string remoteHost, int remotePort, AASeqNodes capabilityExchangeRequestNodes, AASeqNodes diameterWatchdogRequestNodes, int watchdogInterval, bool useNagle) {
        PluginClass = pluginClass;
        Logger = logger;
        RemoteHost = remoteHost;
        RemotePort = remotePort;
        CapabilityExchangeRequestNodes = capabilityExchangeRequestNodes;
        DeviceWatchdogRequestNodes = diameterWatchdogRequestNodes;
        WatchdogIntervalMS = watchdogInterval * 1000 - 500;
        UseNagle = useNagle;

        Thread = new Thread(Run) {
            IsBackground = true,
            Name = "DiameterClientThread",
        };
    }

    private readonly Diameter PluginClass;
    private readonly ILogger Logger;

    private readonly string RemoteHost;
    private readonly int RemotePort;
    private readonly AASeqNodes CapabilityExchangeRequestNodes;
    private readonly AASeqNodes DeviceWatchdogRequestNodes;
    private readonly int WatchdogIntervalMS;
    private readonly bool UseNagle;

    private readonly Thread Thread;
    private readonly ManualResetEvent PassedCE = new(false);  // tracks if CER was successful
    private readonly CancellationTokenSource Cancellation = new();

    private void Run() {
        var cancel = Cancellation.Token;

        while (true) {
            if (cancel.IsCancellationRequested) { return; }

            TcpClient tcpClient = new TcpClient();
            tcpClient.NoDelay = !UseNagle;
            IPEndPoint lastEndpoint = new IPEndPoint(IPAddress.Any, 0);
            try {
                var connectResult = tcpClient.BeginConnect(RemoteHost, RemotePort, requestCallback: null, state: null);
                var waitSuccess = connectResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                if (waitSuccess) {
                    tcpClient.EndConnect(connectResult);
                } else {
                    throw new TimeoutException("Timeout waiting for connection to establish.");
                }
                lastEndpoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint!;
                Log.Connected(Logger, lastEndpoint);
            } catch (Exception ex) {
                Log.ConnectionError(Logger, ex, ex.Message);
                Thread.Sleep(1000);
                continue;
            }

            try {
                using var stream = tcpClient.GetStream();
                using var diameter = new DiameterStream(stream);

                using var dwrTimer = new Timer((_) => {
                    var msElapsed = Environment.TickCount64 - Interlocked.Read(ref PluginClass.LastMessageTimestamp);
                    if (msElapsed > WatchdogIntervalMS) {
                        Interlocked.Exchange(ref PluginClass.LastMessageTimestamp, Environment.TickCount64);
                        var dwr = DiameterEncoder.Encode("Device-Watchdog-Request", DeviceWatchdogRequestNodes);
                        Log.MessageOut(Logger, "Device-Watchdog-Request");
                        diameter.WriteMessage(dwr);
                    }
                }, null, 0, 1000);

                var cer = DiameterEncoder.Encode("Capabilities-Exchange-Request", CapabilityExchangeRequestNodes);
                Log.MessageOut(Logger, "Capabilities-Exchange-Request");
                diameter.WriteMessage(cer);

                while (true) {
                    var message = diameter.ReadMessage();
                    Interlocked.Exchange(ref PluginClass.LastMessageTimestamp, Environment.TickCount64);

                    if ((message.ApplicationId == 0) && (message.CommandCode == 257)) {
                        var resultCode = default(uint?);
                        foreach (var avp in message.Avps) {
                            if (avp.Code == 268) {  // Result-Code
                                resultCode = BinaryPrimitives.ReadUInt32BigEndian(avp.GetData());
                                break;
                            }
                        }
                        if (resultCode is null) {
                            Log.MessageIn(Logger, "Capabilities-Exchange-Answer (no Result-Code)");
                        } else if (resultCode == 2001) {
                            Log.MessageIn(Logger, "Capabilities-Exchange-Answer (DIAMETER_SUCCESS)");
                            PassedCE.Set();
                            PluginClass.DiameterStream = diameter;
                        } else {
                            Log.MessageIn(Logger, $"Capabilities-Exchange-Answer ({resultCode})");
                        }
                    } else if ((message.ApplicationId == 0) && (message.CommandCode == 280)) {
                        var resultCode = default(uint?);
                        foreach (var avp in message.Avps) {
                            if (avp.Code == 268) {  // Result-Code
                                resultCode = BinaryPrimitives.ReadUInt32BigEndian(avp.GetData());
                                break;
                            }
                        }
                        if (resultCode is null) {
                            Log.MessageIn(Logger, "Device-Watchdog-Answer (no Result-Code)");
                        } else if (resultCode == 2001) {
                            Log.MessageIn(Logger, "Device-Watchdog-Answer (DIAMETER_SUCCESS)");
                            PassedCE.Set();
                            PluginClass.DiameterStream = diameter;
                        } else {
                            Log.MessageIn(Logger, $"Device-Watchdog-Answer ({resultCode})");
                        }
                    } else {
                        var nodes = DiameterEncoder.Decode(message, out var messageName);
                        if (PluginClass.StorageAwaiting.Remove((message.HopByHopIdentifier, message.EndToEndIdentifier), out var guid)) {
                            PluginClass.Storage[guid] = (messageName, nodes);
                        } else {
                            Log.MessageIn(Logger, $"Diameter message {messageName} (no matching HopByHopIdentifier/EndToEndIdentifier)");
                        }
                    }
                }
            } catch (Exception ex) {
                PassedCE.Reset();
                PluginClass.DiameterStream = null;
                if (cancel.IsCancellationRequested) { return; }
                Log.ReadError(Logger, ex, ex.Message);
                Debug.WriteLine($"[AASeq.Plugin.Diameter] {lastEndpoint}: {ex.Message}");
                Thread.Sleep(1000);
            }

            try {
                tcpClient.Close();
                tcpClient = new TcpClient();
            } catch (Exception ex) {
                Log.ConnectionError(Logger, ex, ex.Message);
                Thread.Sleep(1000);
            }
        }
    }


    #region IDiameterThread

    public void Start(CancellationToken cancellationToken) {
        Thread.Start();
        PassedCE.WaitOne();
    }

    public void Stop() {
        Cancellation.Cancel();
        Thread.Join();
    }

    #endregion IDiameterThread


    #region IDisposable

    public void Dispose() {
        Cancellation.Dispose();
    }

    #endregion IDisposable

}
