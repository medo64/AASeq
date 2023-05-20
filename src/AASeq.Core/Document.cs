namespace AASeq;

using System;
using System.IO;

/// <summary>
/// AASeq document.
/// </summary>
public sealed record Document {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="stream">Input stream.</param>
    /// <exception cref="ArgumentNullException">Stream cannot be null.</exception>
    public Document(Stream stream) {
        if (stream == null) { throw new ArgumentNullException(nameof(stream), "Stream cannot be null."); }

        using var bStream = new BufferedStream(stream);
        var lines = DocumentLine.GetLines(bStream);
    }

}
