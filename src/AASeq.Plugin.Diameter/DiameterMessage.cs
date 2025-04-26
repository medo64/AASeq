namespace AASeqPlugin;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

/// <summary>
/// Diameter message.
/// </summary>
internal sealed record DiameterMessage {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="flags">Command flags.</param>
    /// <param name="commandCode">Command code.</param>
    /// <param name="applicationId">Application ID.</param>
    /// <param name="hopByHopIdentifier">Hop-by-hop identifier.</param>
    /// <param name="endToEndIdentifier">End-to-end identifier.</param>
    /// <param name="avps">AVP collection.</param>
    public DiameterMessage(byte flags, uint commandCode, uint applicationId, uint hopByHopIdentifier, uint endToEndIdentifier, IList<DiameterAvp> avps) {
        if (commandCode is > 16777215) { throw new ArgumentOutOfRangeException(nameof(commandCode), "Command code cannot be larger than 16777215."); }
        if (avps is null) { throw new ArgumentNullException(nameof(avps), "AVPs cannot be null."); }

        Flags = flags;
        CommandCode = commandCode;
        ApplicationId = applicationId;
        HopByHopIdentifier = hopByHopIdentifier;
        EndToEndIdentifier = endToEndIdentifier;
        Avps = avps;
    }

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="commandFlags">Command flags.</param>
    /// <param name="commandCode">Command code.</param>
    /// <param name="applicationId">Application ID.</param>
    /// <param name="avps">AVP collection.</param>
    public DiameterMessage(byte commandFlags, uint commandCode, uint applicationId, IList<DiameterAvp> avps)
        : this(commandFlags, commandCode, applicationId, GetNewHopByHopIdentifier(), GetNewEndToEndIdentifier(), avps) {
    }


    /// <summary>
    /// Gets length for the message.
    /// </summary>
    public int Length {
        get {
            var messageLength = 20;
            foreach (var avp in Avps) {
                messageLength += avp.LengthWithPadding;
            }
            return messageLength;
        }
    }


    /// <summary>
    /// Get command flags.
    /// </summary>
    public byte Flags { get; init; }

    /// <summary>
    /// Get if request flag is set.
    /// </summary>
    public bool HasRequestFlag {
        get { return (this.Flags & 0x80) != 0; }
    }

    /// <summary>
    /// Get if proxiable flag is set.
    /// </summary>
    public bool HasProxiableFlag {
        get { return (this.Flags & 0x40) != 0; }
    }

    /// <summary>
    /// Get if error flag is set.
    /// </summary>
    public bool HasErrorFlag {
        get { return (this.Flags & 0x20) != 0; }
    }

    /// <summary>
    /// Get if potentially re-transmitted message flag is set.
    /// </summary>
    public bool HasRetransmittedFlag {
        get { return (this.Flags & 0x20) != 0; }
    }


    /// <summary>
    /// Gets command code.
    /// </summary>
    public uint CommandCode { get; init; }

    /// <summary>
    /// Gets application ID.
    /// </summary>
    public uint ApplicationId { get; init; }


    /// <summary>
    /// Gets hop-by-hop identifier.
    /// </summary>
    public uint HopByHopIdentifier { get; init; }

    /// <summary>
    /// Gets end-to-end identifier.
    /// </summary>
    public uint EndToEndIdentifier { get; init; }


    /// <summary>
    /// Gets AVP collection.
    /// </summary>
    public IList<DiameterAvp> Avps { get; init; }


    /// <summary>
    /// Write message into the span.
    /// </summary>
    /// <param name="destination">Destination span.</param>
    public void WriteTo(Span<byte> destination) {
        BinaryPrimitives.WriteUInt32BigEndian(destination[0..4], (uint)((1 << 24) | Length));
        BinaryPrimitives.WriteUInt32BigEndian(destination[4..8], (uint)((uint)(Flags << 24) | (uint)CommandCode));
        BinaryPrimitives.WriteUInt32BigEndian(destination[8..12], ApplicationId);
        BinaryPrimitives.WriteUInt32BigEndian(destination[8..12], HopByHopIdentifier);
        BinaryPrimitives.WriteUInt32BigEndian(destination[8..12], EndToEndIdentifier);

        var offset = 20;
        foreach (var avp in Avps) {
            var length = avp.LengthWithPadding;
            avp.WriteTo(destination[offset..(offset + length)]);
            offset += length;
        }
    }


    #region Helper

    private static uint? LastHopByHopIdentifier;
    private static readonly Lock LastHopByHopIdentifierLock = new();

    private static uint GetNewHopByHopIdentifier() {
        lock (LastHopByHopIdentifierLock) {
            if (LastHopByHopIdentifier is null) {
                LastHopByHopIdentifier = GetRandomUInt32();
            } else {
                LastHopByHopIdentifier += 1;
            }
            return LastHopByHopIdentifier.Value;
        }
    }

    private static uint GetNewEndToEndIdentifier() {
        return (uint)(((uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds() << 20) | (GetRandomUInt32() & 0x000FFFFF));
    }

    private static readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();  // needed due to .NET Standard 2.0
    private const int RandomBufferSize = 2048;
    private static readonly ThreadLocal<byte[]> RandomBuffer = new(() => {
        return GC.AllocateArray<byte>(RandomBufferSize, pinned: true);
    });
    private static readonly ThreadLocal<int> RandomBufferIndex = new(() => RandomBufferSize);  // initialize to the end of the buffer

    private static uint GetRandomUInt32() {
        var buffer = RandomBuffer.Value!;
        var bufferIndex = RandomBufferIndex.Value;

        if (bufferIndex + 4 > RandomBufferSize) {  // new random number needed
            Random.GetBytes(buffer);
            bufferIndex = 0;
        }

        var bytes = new byte[4];
        Buffer.BlockCopy(buffer, bufferIndex, bytes, 0, 4);
        RandomBufferIndex.Value = bufferIndex + 4;
        return BinaryPrimitives.ReadUInt32BigEndian(bytes);
    }

    #endregion Helper

}
