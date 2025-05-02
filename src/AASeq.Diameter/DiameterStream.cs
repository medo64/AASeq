namespace AASeq.Diameter;
using System;
using System.IO;

/// <summary>
/// Diameter protocol stream.
/// </summary>
public sealed class DiameterStream : Stream, IDisposable {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="stream">Stream to use in order to fetch bytes.</param>
    public DiameterStream(Stream stream) {
        BaseStream = stream;
    }


    private readonly Stream BaseStream;


    #region Timeout properties

    /// <summary>
    /// Gets whether timeout properties are usable.
    /// </summary>
    public override bool CanTimeout {
        get { return BaseStream.CanTimeout; }
    }

    /// <summary>
    /// Gets or sets the amount of time a read operation blocks waiting for data.
    /// </summary>
    public override Int32 ReadTimeout {
        get { return BaseStream.ReadTimeout; }
        set { BaseStream.ReadTimeout = value; }
    }

    /// <summary>
    /// Gets or sets the amount of time a write operation blocks waiting for data.
    /// </summary>
    public override Int32 WriteTimeout {
        get { return BaseStream.WriteTimeout; }
        set { BaseStream.WriteTimeout = value; }
    }

    #endregion


    #region Block properties

    private int _ReadBlockSize = 16384;
    /// <summary>
    /// Gets or sets block size for read operations.
    /// Messages larger than this will be read in multiple blocks.
    /// Default value is 16 KB.
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException">Value must be between 4 and 65536 bytes. -or- Value must be set in 4-byte increments.</exception>
    public int ReadBlockSize {
        get { return _ReadBlockSize; }
        set {
            if (value is < 4 or > 65536) { throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 4 and 65536 bytes."); }
            if ((value % 4) != 0) { throw new ArgumentOutOfRangeException(nameof(value), "Value must be set in 4-byte increments."); }
            _ReadBlockSize = value;
        }
    }

    private int _WriteBlockSize = 16384;
    /// <summary>
    /// Gets or sets block size for write operations.
    /// Messages larger than this will be written in multiple blocks.
    /// Default value is 16 KB.
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException">Value must be between 4 and 65536 bytes. -or- Value must be set in 4-byte increments.</exception>
    public int WriteBlockSize {
        get { return _WriteBlockSize; }
        set {
            if (value is < 4 or > 65536) { throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 4 and 65536 bytes."); }
            if ((value % 4) != 0) { throw new ArgumentOutOfRangeException(nameof(value), "Value must be set in 4-byte increments."); }
            _WriteBlockSize = value;
        }
    }

    #endregion


    #region Message

    /// <summary>
    /// Returns single message read from stream.
    /// </summary>
    /// <exception cref="System.TimeoutException"></exception>
    /// <exception cref="System.FormatException"></exception>
    /// <exception cref="System.IO.EndOfStreamException">No bytes left to read.</exception>
    public DiameterMessage ReadMessage() {
        var headBuffer = new byte[4];
        BaseStream.ReadExactly(headBuffer, 0, 4);
        if (headBuffer[0] != 1) { throw new FormatException("Only Diameter version 1 is supported."); }
        int messageLength = (headBuffer[1] << 16) + (headBuffer[2] << 8) + headBuffer[3];
        if (((messageLength % 4) != 0) || (messageLength < 20)) { throw new FormatException("Invalid message length."); }

        var buffer = new byte[messageLength];
        var index = 4; //skip first 4 bytes
        var bytesLeft = messageLength - 4; //because first four bytes are already read (but not copied)
        while (bytesLeft > 0) {
            var bytesExpected = (bytesLeft > ReadBlockSize) ? ReadBlockSize : bytesLeft;
            var bytesRead = BaseStream.Read(buffer, index, bytesExpected);
            if (bytesRead == 0) { throw new EndOfStreamException("No bytes left to read."); }
            index += bytesRead;
            bytesLeft -= bytesRead;
        }

        try {
            return DiameterMessage.ReadFrom(new Span<byte>(buffer));
        } catch (FormatException) {
            throw;
        } catch (Exception ex) {
            throw new FormatException("Cannot parse message.", ex);
        }
    }


    /// <summary>
    /// Writes single message to diameter stream.
    /// </summary>
    /// <param name="message">Message to write</param>
    /// <exception cref="System.TimeoutException"></exception>
    /// <exception cref="System.ArgumentNullException">Message cannot be null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Message cannot be longer then 16777215 bytes.</exception>
    public void WriteMessage(DiameterMessage message) {
        if (message == null) { throw new ArgumentNullException(nameof(message), "Message cannot be null."); }

        var bytes = new byte[message.Length];
        message.WriteTo(bytes);

        var index = 0;
        var bytesLeft = bytes.Length;
        while (bytesLeft > 0) {
            var bytesToSend = (bytesLeft > WriteBlockSize) ? WriteBlockSize : bytesLeft;
            BaseStream.Write(bytes, index, bytesToSend);
            index += bytesToSend;
            bytesLeft -= bytesToSend;
        }
    }

    #endregion


    #region Stream

    /// <summary>
    /// Gets a value indicating whether the current stream supports reading.
    /// </summary>
    public override bool CanRead {
        get { return BaseStream.CanRead; }
    }

    /// <summary>
    /// Gets a value indicating whether the current stream supports seeking.
    /// Value is always false.
    /// </summary>
    public override bool CanSeek {
        get { return false; }
    }

    /// <summary>
    /// Gets a value indicating whether the current stream supports writing.
    /// </summary>
    public override bool CanWrite {
        get { return BaseStream.CanWrite; }
    }

    /// <summary>
    /// Clears all buffers for this stream and causes any buffered data to be written.
    /// </summary>
    public override void Flush() {
        BaseStream.Flush();
    }

    /// <summary>
    /// Gets the length in bytes of the stream.
    /// Not supported.
    /// </summary>
    public override long Length {
        get { throw new NotSupportedException("Operation is not supported."); }
    }

    /// <summary>
    /// Gets or sets the current position of this stream.
    /// Not supported.
    /// </summary>
    public override long Position {
        get { throw new NotSupportedException("Operation is not supported."); }
        set { throw new NotSupportedException("Operation is not supported."); }
    }

    /// <summary>
    /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// Not supported.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source. </param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
    public override int Read(byte[] buffer, int offset, int count) {
        throw new NotSupportedException("Operation is not supported.");
    }

    /// <summary>
    /// Sets the position within the current stream.
    /// Not supported.
    /// </summary>
    /// <param name="offset">A byte offset relative to the origin parameter.</param>
    /// <param name="origin">A value indicating the reference point used to obtain the new position.</param>
    public override long Seek(long offset, SeekOrigin origin) {
        throw new NotSupportedException("Operation is not supported.");
    }

    /// <summary>
    /// Sets the length of the current stream.
    /// Not supported.
    /// </summary>
    /// <param name="value">The desired length of the current stream in bytes.</param>
    public override void SetLength(long value) {
        throw new NotSupportedException("Operation is not supported.");
    }

    /// <summary>
    /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
    /// <param name="count">The number of bytes to be written to the current stream.</param>
    /// Not supported.
    public override void Write(byte[] buffer, int offset, int count) {
        throw new NotSupportedException("Operation is not supported.");
    }

    #endregion


    #region IDisposable

    /// <summary>
    /// Disposes the DiameterStream and the underlying BaseStream.
    /// </summary>
    protected override void Dispose(bool disposing) {
        if (disposing) {
            BaseStream?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion IDisposable

}
