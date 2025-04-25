namespace AASeqPlugin;
using System;
using System.Diagnostics;
using System.IO;


/// <summary>
/// Diameter AVP.
/// </summary>
internal sealed record DiameterAvp {

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="code">AVP code.</param>
    /// <param name="flags">AVP flags.</param>
    /// <param name="vendorId">AVP vendor ID.</param>
    /// <param name="data">AVP data</param>
    internal DiameterAvp(uint code, byte flags, uint? vendorId, byte[] data) {
        Code = code;
        Flags = flags;
        VendorId = vendorId;
        Data = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
    }

    /// <summary>
    /// Creates new instance.
    /// </summary>
    /// <param name="code">AVP code.</param>
    /// <param name="vendorBit">AVP vendor bit.</param>
    /// <param name="mandatoryBit">AVP mandatory bit.</param>
    /// <param name="protectedBit">AVP protected bit.</param>
    /// <param name="vendorId">AVP vendor ID.</param>
    /// <param name="data">AVP data</param>
    internal DiameterAvp(uint code, bool vendorBit, bool mandatoryBit, bool protectedBit, uint? vendorId, byte[] data)
        : this(code, (byte)((vendorBit ? 0x80 : 0) + (byte)(mandatoryBit ? 0x40 : 0) + (byte)(protectedBit ? 0x20 : 0)), vendorId, data) {
    }


    /// <summary>
    /// Gets AVP code.
    /// </summary>
    public uint Code { get; init; }

    /// <summary>
    /// Gets AVP flags.
    /// </summary>
    public byte Flags { get; init; }

    /// <summary>
    /// Gets whether AVP is vendor-specific.
    /// </summary>
    public Boolean IsVendor {
        get { return (Flags & 0x80) != 0; }
    }

    /// <summary>
    /// Gets whether AVP is mandatory.
    /// This indicates whether support of the AVP is required.
    /// </summary>
    public Boolean IsMandatory {
        get { return (Flags & 0x40) != 0; }
    }

    /// <summary>
    /// Gets whether AVP is protected.
    /// This indicates the need for encryption for end-to-end security.
    /// </summary>
    public Boolean IsProtected {
        get { return (Flags & 0x20) != 0; }
    }

    /// <summary>
    /// Gets length for whole AVP.
    /// </summary>
    public Int32 Length {
        get { return ((VendorId is null) ? 8 : 12) + DataLength; }
    }

    /// <summary>
    /// Gets AVP vendor.
    /// </summary>
    public uint? VendorId { get; init; }

    /// <summary>
    /// Gets padded length for whole AVP.
    /// </summary>
    public Int32 PaddedLength {
        get { return ((VendorId is null) ? 8 : 12) + PaddedDataLength; }
    }

    /// <summary>
    /// Gets length of AVP data.
    /// </summary>
    public Int32 DataLength {
        get { return Data.Length; }
    }

    /// <summary>
    /// Gets padded length for AVP.
    /// </summary>
    public Int32 PaddedDataLength {
        get {
            var length = DataLength;
            var lengthMod = (length % 4);
            return (lengthMod == 0) ? length : length + 4 - lengthMod;
        }
    }

    private byte[] Data { get; init; }

    /// <summary>
    /// Returns data bytes.
    /// </summary>
    public Byte[] GetData() {
        var bytes = new Byte[Data.Length];
        Buffer.BlockCopy(Data, 0, bytes, 0, bytes.Length);
        return bytes;
    }


    /// <summary>
    /// Write AVP into the stream.
    /// </summary>
    /// <param name="stream">Stream</param>
    public void WriteTo(Stream stream) {
        stream.WriteByte(Flags);  // TODO implement
    }

}
