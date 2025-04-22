namespace AASeq;
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Ping endpoint.
/// </summary>
internal sealed class Serial : IEndpointPlugin, IDisposable {

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static IEndpointPlugin CreateInstance(AASeqNodes configuration) {
        return new Serial(configuration);
    }


    private Serial(AASeqNodes configuration) {
        var portName = configuration["PortName"].AsString(GetDefaultPortName());
        var baudRate = configuration["BaudRate"].AsInt32(DefaultBaudRate);
        var parity = configuration["Parity"].AsString(DefaultParity) switch {
            "N" or "None" => Parity.None,
            "O" or "Odd" => Parity.Odd,
            "E" or "Even" => Parity.Even,
            "M" or "Mark" => Parity.Mark,
            "S" or "Space" => Parity.Space,
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Unknown parity: {configuration["Parity"]}."),
        };
        var dataBits = configuration["DataBits"].AsInt32(DefaultDataBits);
        var stopBits = configuration["StopBits"].AsDouble(DefaultStopBits) switch {
            1.0 => StopBits.One,
            1.5 => StopBits.OnePointFive,
            2.0 => StopBits.Two,
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Unknown stop bits: {configuration["StopBits"]}."),
        };

        Eol = configuration["EOL"].AsString(DefaultEol).ToUpperInvariant() switch {
            "LF" or "\n" => "\n",
            "CRLF" or "\r\n" => "\r\n",
            "CR" or "\r" => "\r",
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), $"Unsupported line ending."),
        };

        Port = new SerialPort(portName, baudRate, parity, dataBits, stopBits) {
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
            NewLine = Eol
        };
        Port.Open();
    }

    private readonly SerialPort Port;
    private readonly string Eol;

    /// <summary>
    /// Returns true, if message was successfully sent.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="data">Data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SendAsync(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken) {
        switch (messageName.ToUpperInvariant()) {
            case "WRITELINE": {
                    var text = data["Text"].AsString("");
                    Port.WriteLine(text);
                    await Task.CompletedTask.ConfigureAwait(false);
                }
                break;

            default: throw new ArgumentOutOfRangeException(nameof(messageName), $"Unknown message: {messageName}");
        }
    }

    /// <summary>
    /// Returns true, if message was successfully received.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="messageName">Message name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Tuple<string, AASeqNodes>> ReceiveAsync(Guid id, string messageName, CancellationToken cancellationToken) {
        // id is ignored because

        switch (messageName.ToUpperInvariant()) {
            case "READLINE": {
                    messageName = "ReadLine";
                    Port.ReadLine();
                    var text = Port.ReadLine();
                    return await Task.FromResult(new Tuple<string, AASeqNodes>("ReadLine", [new AASeqNode("Text", text)])).ConfigureAwait(false);
                }

            default: throw new ArgumentOutOfRangeException(nameof(messageName), $"Unknown message: {messageName}");
        }
    }


    private static string GetDefaultPortName() {
        string? firstSerialPort = null;
        foreach (var port in SerialPort.GetPortNames()) {
            firstSerialPort = port;
            break;
        }
        return firstSerialPort ?? throw new InvalidOperationException("No serial ports available.");
    }


    private const int DefaultBaudRate = 9600;
    private const string DefaultParity = "None";
    private const int DefaultDataBits = 8;
    private const double DefaultStopBits = 1.0;
    private const string DefaultEol = "\n";


    #region IDisposable

    void IDisposable.Dispose() {
        Port.Dispose();
    }

    #endregion IDisposable

}
