namespace AASeqPlugin;
using System;
using System.Buffers.Binary;

/// <summary>
/// Diameter AVP.
/// </summary>
internal sealed record DiameterAvp {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="code">AVP code.</param>
    /// <param name="flags">AVP flags.</param>
    /// <param name="vendorCode">AVP vendor code.</param>
    /// <param name="data">AVP data</param>
    internal DiameterAvp(UInt32 code, Byte flags, UInt32? vendorCode, Byte[] data) {
        Code = code;
        Flags = flags;
        VendorCode = vendorCode;
        Data = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
    }

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="avpDefinition">AVP definition.</param>
    /// <param name="data">AVP data</param>
    internal DiameterAvp(AvpDictionaryEntry avpDefinition, Byte[] data)
        : this(
              avpDefinition.Code,
              (byte)((avpDefinition.Vendor is not null ? 0x80 : 0x00) + (avpDefinition.MandatoryBit == AvpBitState.Must ? 0x40 : 0x00)),
              avpDefinition.Vendor?.Code,
              data
            ) { }

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="code">AVP code.</param>
    /// <param name="vendorBit">AVP vendor bit.</param>
    /// <param name="mandatoryBit">AVP mandatory bit.</param>
    /// <param name="protectedBit">AVP protected bit.</param>
    /// <param name="vendorCode">AVP vendor code.</param>
    /// <param name="data">AVP data</param>
    internal DiameterAvp(UInt32 code, bool vendorBit, bool mandatoryBit, bool protectedBit, UInt32? vendorCode, Byte[] data)
        : this(code, (byte)((vendorBit ? 0x80 : 0) + (byte)(mandatoryBit ? 0x40 : 0) + (byte)(protectedBit ? 0x20 : 0)), vendorCode, data) {
    }


    /// <summary>
    /// Gets AVP code.
    /// </summary>
    public UInt32 Code { get; init; }

    /// <summary>
    /// Gets AVP flags.
    /// </summary>
    public Byte Flags { get; init; }

    /// <summary>
    /// Gets whether AVP is vendor-specific.
    /// </summary>
    public bool HasVendorFlag {
        get { return (Flags & 0x80) != 0; }
    }

    /// <summary>
    /// Gets whether AVP is mandatory.
    /// This indicates whether support of the AVP is required.
    /// </summary>
    public bool HasMandatoryFlag {
        get { return (Flags & 0x40) != 0; }
    }

    /// <summary>
    /// Gets whether AVP is protected.
    /// This indicates the need for encryption for end-to-end security.
    /// </summary>
    public bool HasProtectedFlag {
        get { return (Flags & 0x20) != 0; }
    }

    /// <summary>
    /// Gets length for whole AVP.
    /// </summary>
    public int Length {
        get { return ((VendorCode is null) ? 8 : 12) + DataLength; }
    }

    /// <summary>
    /// Gets length for whole AVP including padding.
    /// </summary>
    public int LengthWithPadding {
        get { return ((VendorCode is null) ? 8 : 12) + DataLengthWithPadding; }
    }

    /// <summary>
    /// Gets AVP vendor.
    /// </summary>
    public UInt32? VendorCode { get; init; }

    /// <summary>
    /// Gets length of AVP data.
    /// </summary>
    public int DataLength {
        get { return Data.Length; }
    }

    /// <summary>
    /// Gets padded length for AVP.
    /// </summary>
    public int DataLengthWithPadding {
        get {
            var length = DataLength;
            var lengthMod = (length % 4);
            return (lengthMod == 0) ? length : length + 4 - lengthMod;
        }
    }

    private Byte[] Data { get; init; }

    /// <summary>
    /// Returns data bytes.
    /// </summary>
    public byte[] GetData() {
        var bytes = new Byte[Data.Length];
        Buffer.BlockCopy(Data, 0, bytes, 0, bytes.Length);
        return bytes;
    }


    /// <summary>
    /// Write AVP into the span.
    /// </summary>
    /// <param name="destination">Destination span.</param>
    public void WriteTo(Span<byte> destination) {
        BinaryPrimitives.WriteUInt32BigEndian(destination[0..4], Code);
        var flagsAndLength = (uint)((uint)(Flags << 24) | (uint)Length);
        BinaryPrimitives.WriteUInt32BigEndian(destination[4..8], flagsAndLength);
        if (VendorCode is not null) {
            BinaryPrimitives.WriteUInt32BigEndian(destination[8..12], VendorCode.Value);
            new Span<byte>(Data).CopyTo(destination[12..]);
        } else {
            new Span<byte>(Data).CopyTo(destination[8..]);
        }
    }

    /// <summary>
    /// Reads AVP from the span.
    /// </summary>
    /// <param name="source">Source span.</param>
    public static DiameterAvp ReadFrom(Span<byte> source) {
        var code = BinaryPrimitives.ReadUInt32BigEndian(source[0..4]);

        var flagsAndLength = BinaryPrimitives.ReadUInt32BigEndian(source[4..8]);
        var flags = (byte)(flagsAndLength >> 24);
        var length = (int)(flagsAndLength & 0x00FFFFFF);

        var vendorId = (flags & 0x80) != 0 ? BinaryPrimitives.ReadUInt32BigEndian(source[8..12]) : default(uint?);
        var dataOffset = vendorId is null ? 8 : 12;
        var dataLength = length - dataOffset;

        var bytes = new byte[dataLength];
        source.Slice(dataOffset, dataLength).CopyTo(bytes);

        return new DiameterAvp(code, flags, vendorId, bytes);
    }

}
