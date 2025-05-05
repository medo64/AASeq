namespace AASeq.Diameter;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

/// <summary>
/// Diameter message.
/// </summary>
public sealed record DiameterMessage {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="flags">Command flags.</param>
    /// <param name="commandCode">Command code.</param>
    /// <param name="applicationId">Application ID.</param>
    /// <param name="hopByHopIdentifier">Hop-by-hop identifier.</param>
    /// <param name="endToEndIdentifier">End-to-end identifier.</param>
    /// <param name="avps">AVP collection.</param>
    public DiameterMessage(byte flags, uint commandCode, uint applicationId, uint? hopByHopIdentifier, uint? endToEndIdentifier, IList<DiameterAvp> avps) {
        if (commandCode is > 16777215) { throw new ArgumentOutOfRangeException(nameof(commandCode), "Command code cannot be larger than 16777215."); }
        if (avps is null) { throw new ArgumentNullException(nameof(avps), "AVPs cannot be null."); }

        Flags = flags;
        CommandCode = commandCode;
        ApplicationId = applicationId;
        HopByHopIdentifier = hopByHopIdentifier ?? GetNewHopByHopIdentifier();
        EndToEndIdentifier = endToEndIdentifier ?? GetNewEndToEndIdentifier();
        Avps = avps;
    }

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="flags">Command flags.</param>
    /// <param name="commandCode">Command code.</param>
    /// <param name="applicationId">Application ID.</param>
    /// <param name="avps">AVP collection.</param>
    public DiameterMessage(byte flags, uint commandCode, uint applicationId, IList<DiameterAvp> avps)
        : this(flags, commandCode, applicationId, hopByHopIdentifier: null, endToEndIdentifier: null, avps) {
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
        get { return (Flags & 0x80) != 0; }
    }

    /// <summary>
    /// Get if proxiable flag is set.
    /// </summary>
    public bool HasProxiableFlag {
        get { return (Flags & 0x40) != 0; }
    }

    /// <summary>
    /// Get if error flag is set.
    /// </summary>
    public bool HasErrorFlag {
        get { return (Flags & 0x20) != 0; }
    }

    /// <summary>
    /// Get if potentially re-transmitted message flag is set.
    /// </summary>
    public bool HasRetransmittedFlag {
        get { return (Flags & 0x10) != 0; }
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
        BinaryPrimitives.WriteUInt32BigEndian(destination[12..16], HopByHopIdentifier);
        BinaryPrimitives.WriteUInt32BigEndian(destination[16..20], EndToEndIdentifier);

        var offset = 20;
        foreach (var avp in Avps) {
            var length = avp.LengthWithPadding;
            avp.WriteTo(destination[offset..(offset + length)]);
            offset += length;
        }
    }

    /// <summary>
    /// Reads message from the span.
    /// </summary>
    /// <param name="source">Source span.</param>
    public static DiameterMessage ReadFrom(Span<byte> source) {
        var versionAndLength = BinaryPrimitives.ReadUInt32BigEndian(source[0..4]);
        //var version = versionAndLength >> 24;  // already read in calling function
        //var length = (int)(versionAndLength & 0x00FFFFFF);  // already read in calling function

        var flagsAndCommandCode = BinaryPrimitives.ReadUInt32BigEndian(source[4..8]);
        var flags = (byte)(flagsAndCommandCode >> 24);
        var commandCode = (uint)(flagsAndCommandCode & 0x00FFFFFF);

        var applicationId = BinaryPrimitives.ReadUInt32BigEndian(source[8..12]);

        var hopByHopIdentifier = BinaryPrimitives.ReadUInt32BigEndian(source[12..16]);
        var endToEndIdentifier = BinaryPrimitives.ReadUInt32BigEndian(source[16..20]);

        var offset = 20;
        var avps = new List<DiameterAvp>();
        while (offset < source.Length) {
            var avp = DiameterAvp.ReadFrom(source[offset..]);
            avps.Add(avp);
            offset += avp.LengthWithPadding;
        }

        return new DiameterMessage(flags, commandCode, applicationId, hopByHopIdentifier, endToEndIdentifier, avps);
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
