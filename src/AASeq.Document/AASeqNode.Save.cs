namespace AASeq;
using System;
using System.IO;

public sealed partial class AASeqNode : IFormattable {

    /// <summary>
    /// Saves node to a stream.
    /// </summary>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    public void Save(Stream stream) {
        AASeqNodes.Save(new AASeqNodes([this]), stream);
    }

    /// <summary>
    /// Saves node to a stream.
    /// </summary>
    /// <param name="writer">Destination writer.</param>
    public void Save(TextWriter writer) {
        AASeqNodes.Save(new AASeqNodes([this]), writer);
    }

    /// <summary>
    /// Saves node to a stream.
    /// </summary>
    /// <param name="stream">Destination stream that will contain the UTF-8 encoded text.</param>
    /// <param name="options">Output options.</param>
    public void Save(Stream stream, AASeqOutputOptions options) {
        AASeqNodes.Save(new AASeqNodes([this]), stream, options);
    }

    /// <summary>
    /// Saves node to a stream.
    /// </summary>
    /// <param name="writer">Destination writer.</param>
    /// <param name="options">Output options.</param>
    public void Save(TextWriter writer, AASeqOutputOptions options) {
        AASeqNodes.Save(new AASeqNodes([this]), writer, options);
    }

    /// <summary>
    /// Saves node to a file.
    /// </summary>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    public void Save(string filePath) {
        AASeqNodes.Save(new AASeqNodes([this]), filePath);
    }

    /// <summary>
    /// Saves node to a file.
    /// </summary>
    /// <param name="filePath">Path to the destination UTF-8 encoded file.</param>
    /// <param name="options">Output options.</param>
    public void Save(string filePath, AASeqOutputOptions options) {
        AASeqNodes.Save(new AASeqNodes([this]), filePath, options);
    }


    #region IFormattable

    /// <summary>
    /// Returns text representation of the nodes.
    /// </summary>
    public override string ToString() {
        return ToString(format: null, formatProvider: null);
    }

    /// <summary>
    /// Returns text representation of the nodes.
    /// </summary>
    /// <param name="format">Not used.</param>
    /// <param name="formatProvider">Not used.</param>
    public string ToString(string? format, IFormatProvider? formatProvider) {
        return AASeqNodes.ToString(Nodes);
    }

    #endregion IFormattable

}
