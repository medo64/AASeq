namespace AASeqPlugin;
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AASeq;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Serial : IEndpointPlugin, IDisposable {

    private Serial(ILogger logger, AASeqNodes configuration) {
        Logger = logger;

        string? firstSerialPort = null;
        foreach (var port in SerialPort.GetPortNames()) {
            firstSerialPort = port;
            break;
        }
        if (firstSerialPort is null) { throw new InvalidOperationException("No serial ports available."); }

        PortName = firstSerialPort;
        if (configuration.TryConsumeNode("PortName", out var portNode)) {
            if (portNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{portNode.Name}'."); }
            var portValue = portNode.Value.AsString();
            if (!string.IsNullOrWhiteSpace(portValue)) {
                PortName = portValue;
            } else {
                throw new InvalidOperationException($"Cannot convert 'PortName' value.");
            }
        }

        if (configuration.TryConsumeNode("BaudRate", out var baudNode)) {
            if (baudNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{baudNode.Name}'."); }
            var baudValue = baudNode.Value.AsInt32();
            if ((baudValue != null) && (baudValue.Value > 0)) {
                BaudRate = baudValue.Value;
            } else {
                throw new InvalidOperationException($"Cannot convert 'BaudRate' value.");
            }
        }

        if (configuration.TryConsumeNode("DataBits", out var dataNode)) {
            if (dataNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{dataNode.Name}'."); }
            var dataValue = dataNode.Value.AsInt32(8);
            if (dataValue is 7 or 8) {
                DataBits = dataValue;
            } else {
                throw new InvalidOperationException($"Cannot convert 'DataBits' value.");
            }
        }

        if (configuration.TryConsumeNode("Parity", out var parityNode)) {
            if (parityNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{parityNode.Name}'."); }
            Parity = parityNode.Value.AsString("None") switch {
                "N" or "None" => Parity.None,
                "O" or "Odd" => Parity.Odd,
                "E" or "Even" => Parity.Even,
                "M" or "Mark" => Parity.Mark,
                "S" or "Space" => Parity.Space,
                _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Cannot convert 'Parity' value."),
            };
        }

        if (configuration.TryConsumeNode("StopBits", out var stopNode)) {
            if (stopNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{stopNode.Name}'."); }
            StopBits = stopNode.Value.AsDouble(1) switch {
                1.0 => StopBits.One,
                1.5 => StopBits.OnePointFive,
                2.0 => StopBits.Two,
                _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Cannot convert 'StopBits' value."),
            };
        }

        if (configuration.TryConsumeNode("EOL", out var eolNode)) {
            if (eolNode.Properties.Count > 0) { logger.LogWarning($"Unrecognized properties on '{eolNode.Name}'."); }
            Eol = eolNode.Value.AsString(Eol).ToUpperInvariant() switch {
                "LF" or "\n" => "\n",
                "CRLF" or "\r\n" => "\r\n",
                "CR" or "\r" => "\r",
                _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Unsupported line ending."),
            };
        }

        if (configuration.Count > 0) { logger.LogWarning($"Unrecognized configuration node '{configuration[0].Name}'."); }
    }


    private readonly ILogger Logger;

    private readonly string PortName;
    private readonly int BaudRate = 9600;
    private readonly Parity Parity = Parity.None;
    private readonly int DataBits = 8;
    private readonly StopBits StopBits = StopBits.One;
    private readonly string Eol = "\r\n";

    private SerialPort? Port;


    /// <summary>
    /// Starts the endpoint.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task StartAsync(CancellationToken cancellationToken) {
        Logger.LogTrace($"Starting Serial endpoint @ {PortName}");
        Port = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
        Port.Open();
        Logger.LogTrace($"Started Serial endpoint @ {PortName}");
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Sends message to the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<AASeqNodes> SendAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        if (Port is null) { throw new InvalidOperationException("Port not set."); }

        switch (messageName.ToUpperInvariant()) {
            case "WRITELINE": {
                    if (parameters.TryConsumeNode("Text", out var node)) {
                        if (parameters.Count > 0) { Logger.LogWarning($"Unrecognized parameter '{parameters[0].Name}'."); }
                        if (node.Nodes.Count > 0) { Logger.LogWarning($"Unrecognized properties for '{parameters[0].Name}'."); }
                        var text = node.Value.AsString("");
                        var bytes = Utf8.GetBytes(text + Eol);
                        await Port.BaseStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
                        return new AASeqNodes(
                            [new AASeqNode("Text", text)]
                        );
                    } else {
                        throw new InvalidOperationException("No 'Text' specified.");
                    }
                }

            case "WRITEBYTES": {
                    if (parameters.TryConsumeNode("Bytes", out var node)) {
                        if (parameters.Count > 0) { Logger.LogWarning($"Unrecognized parameter '{parameters[0].Name}'."); }
                        if (node.Nodes.Count > 0) { Logger.LogWarning($"Unrecognized properties for '{parameters[0].Name}'."); }
                        var bytes = node.Value.AsByteArray() ?? throw new ArgumentNullException(nameof(parameters), "Cannot convert 'Bytes'.");
                        if (bytes.Length == 0) { throw new ArgumentOutOfRangeException(nameof(parameters), "Bytes are empty."); }
                        await Port.BaseStream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
                        return new AASeqNodes(
                            [new AASeqNode("Bytes", bytes)]
                        );
                    } else {
                        throw new InvalidOperationException("No 'Bytes' specified.");
                    }
                }

            case "DISCARD": {
                    if (parameters.Count > 0) { Logger.LogWarning($"Unrecognized parameter '{parameters[0].Name}'."); }
                    Port.DiscardInBuffer();
                    Port.DiscardOutBuffer();
                    BytesIndex = 0;
                    return new AASeqNodes();
                }

            default: throw new ArgumentOutOfRangeException(nameof(messageName), $"Unknown message: {messageName}");
        }
    }

    private readonly byte[] Bytes = new byte[4 * 1024 * 1024];
    private int BytesIndex;

    /// <summary>
    /// Receives message from the endpoint.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, AASeqNodes parameters, CancellationToken cancellationToken) {
        if (Port is null) { throw new InvalidOperationException("Port not set."); }

        // id is ignored because serial port is always sequential

        switch (messageName.ToUpperInvariant()) {
            case "READLINE": {
                    var i = 0;
                    var readMore = false;
                    while (true) {
                        if (i >= BytesIndex) { readMore = true; }
                        if (readMore) {
                            var read = await Port.BaseStream.ReadAsync(Bytes.AsMemory(BytesIndex, Bytes.Length - BytesIndex), cancellationToken).ConfigureAwait(false);
                            if (read > 0) { BytesIndex += read; }
                            readMore = false;
                        }

                        if (Bytes[i] == Eol[0]) {
                            if (Eol.Length == 2) {  // crlf special
                                if (i + 1 >= BytesIndex) {
                                    readMore = true;
                                    continue;
                                }
                                if (Bytes[i + 1] == Eol[1]) {
                                    Buffer.BlockCopy(Bytes, i + 2, Bytes, 0, BytesIndex - i - 2);
                                    BytesIndex = 0;
                                    var text = Utf8.GetString(Bytes, 0, i);
                                    return await Task.FromResult(new Tuple<string, AASeqNodes>("ReadLine", [new AASeqNode("Text", text)])).ConfigureAwait(false);
                                }
                            } else {
                                Buffer.BlockCopy(Bytes, i + 1, Bytes, 0, BytesIndex - i - 1);
                                BytesIndex = 0;
                                var text = Utf8.GetString(Bytes, 0, i);
                                return await Task.FromResult(new Tuple<string, AASeqNodes>("ReadLine", [new AASeqNode("Text", text)])).ConfigureAwait(false);
                            }
                        }

                        i++;
                    }
                }

            case "READBYTES": {
                    var count = parameters[".Count"].AsInt32(0);
                    if (count == 0) {
                        var expected = parameters["Bytes"].AsByteArray();
                        if ((expected != null) && (expected.Length > 0)) {
                            count = expected.Length;
                        }
                    }
                    if (count == 0) { throw new InvalidOperationException("Byte count is 0."); }

                    while (true) {
                        if (BytesIndex >= count) {
                            var bytes = new byte[count];
                            Buffer.BlockCopy(Bytes, 0, bytes, 0, count);
                            Buffer.BlockCopy(Bytes, 0, Bytes, 0, BytesIndex);
                            BytesIndex = 0;
                            return await Task.FromResult(new Tuple<string, AASeqNodes>(
                                    "ReadBytes",
                                    [new AASeqNode(".Count", bytes.Length), new AASeqNode("Bytes", bytes)]
                                )).ConfigureAwait(false);
                        }

                        var read = await Port.BaseStream.ReadAsync(Bytes.AsMemory(BytesIndex, Bytes.Length - BytesIndex), cancellationToken).ConfigureAwait(false);
                        if (read > 0) { BytesIndex += read; }
                    }
                }

            default: throw new ArgumentOutOfRangeException(nameof(messageName), $"Unknown message: {messageName}");
        }
    }


    #region IDisposable

    void IDisposable.Dispose() {
        if (Port is not null) {
            Port.Dispose();
            Port = null;
        }
    }

    #endregion IDisposable


    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(ILogger logger, AASeqNodes configuration) {
        return new Serial(logger, configuration);
    }

}
