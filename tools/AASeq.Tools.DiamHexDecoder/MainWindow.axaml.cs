namespace DiamHexDecoder;
using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AASeq;
using AASeq.Diameter;
using System.Text.Unicode;
using System.Text;

internal partial class MainWindow : Window {

    public MainWindow() {
        InitializeComponent();

        txtHexStream.TextChanged += txtHexStream_TextChanged;
        chbIncludeAllFlags.IsCheckedChanged += txtHexStream_TextChanged;
        chbIncludeTypeAnnotations.IsCheckedChanged += txtHexStream_TextChanged;
        btnCopy.Click += btnCopy_Click;
    }


    public void txtHexStream_TextChanged(object? sender, RoutedEventArgs e) {
        lblError.IsVisible = false;

        var hexStream = txtHexStream.Text ?? string.Empty;
        var includeAllFlags = chbIncludeAllFlags.IsChecked ?? false;
        var includeTypeAnnotations = chbIncludeTypeAnnotations.IsChecked ?? false;

        try {
            byte[] bytes;

            try {
                using var memory = new MemoryStream();
                for (int i = 0; i < hexStream.Length; i += 2) {
                    var hex = hexStream.Substring(i, 2);
                    var b = Convert.ToByte(hex, 16);
                    memory.WriteByte(b);
                }
                memory.Position = 0;
                bytes = memory.ToArray();
            } catch (Exception) {
                throw new InvalidOperationException("Cannot decode hex stream.");
            }

            try {
                var message = DiameterMessage.ReadFrom(bytes);
                if (message is null) { throw new InvalidOperationException("Failed to decode message."); }

                var nodes = DiameterEncoder.Decode(message, includeAllFlags, out var messageName);
                var node = new AASeqNode(messageName, message.HasRequestFlag ? ">Remote" : "<Remote", nodes);

                using var outStream = new MemoryStream();
                node.Save(outStream, includeTypeAnnotations ? AASeqOutputOptions.Default : OutputOptionsWithoutType );

                txtMessage.Text = Utf8.GetString(outStream.ToArray());
            } catch (Exception) {
                throw new InvalidOperationException("Cannot decode diameter message.");
            }

        } catch (Exception ex) {
            txtMessage.Text = "";
            lblError.IsVisible = true;
            lblError.Content = ex.Message;
        }

        btnCopy.IsEnabled = txtMessage.Text.Length > 0;
    }

    public void btnCopy_Click(object? sender, RoutedEventArgs e) {
        Clipboard?.SetTextAsync(txtMessage.Text).GetAwaiter().GetResult();
    }


    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private static readonly AASeqOutputOptions OutputOptionsWithoutType = AASeqOutputOptions.Default with { NoTypeAnnotation = true };

}
