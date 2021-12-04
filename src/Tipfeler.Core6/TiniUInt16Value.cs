using System;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// UInt16 value.
/// </summary>
public sealed record TiniUInt16Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniUInt16Value(UInt16 value) {
        _value = value;
    }


    private UInt16 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public UInt16 Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value != 0;

    protected override SByte? ConvertToInt8()
        => Value is <= (Byte)SByte.MaxValue ? (SByte)Value : null;

    protected override Int16? ConvertToInt16()
        => Value is <= (UInt16)Int16.MaxValue ? (Int16)Value : null;

    protected override Int32? ConvertToInt32()
        => Value;

    protected override Int64? ConvertToInt64()
        => Value;

    protected override Byte? ConvertToUInt8()
        => Value is <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value;

    protected override UInt32? ConvertToUInt32()
        => Value;

    protected override UInt64? ConvertToUInt64()
        => Value;

    protected override Single? ConvertToFloat32()
        => Value;

    protected override Double? ConvertToFloat64()
        => Value;

    protected override String? ConvertToString()
        => Value.ToString("0", CultureInfo.InvariantCulture);

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => null;

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    #endregion Convert

}
