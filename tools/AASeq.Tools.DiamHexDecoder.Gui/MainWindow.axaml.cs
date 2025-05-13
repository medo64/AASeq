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
            txtMessage.Text = Decoder.GetDecodedText(hexStream, includeAllFlags, includeTypeAnnotations);
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

}
