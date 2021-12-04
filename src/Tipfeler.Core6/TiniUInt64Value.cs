using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// UInt64 value.
/// </summary>
public sealed record TiniUInt64Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniUInt64Value(UInt64 value) {
        _value = value;
    }


    private UInt64 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public UInt64 Value {
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
        => Value is <= Int32.MaxValue ? (Int32)Value : null;

    protected override Int64? ConvertToInt64()
        => Value is <= Int64.MaxValue ? (Int64)Value : null;

    protected override Byte? ConvertToUInt8()
        => Value is <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is <= UInt16.MaxValue ? (UInt16)Value : null;

    protected override UInt32? ConvertToUInt32()
        => Value is <= UInt32.MaxValue ? (UInt32)Value : null;

    protected override UInt64? ConvertToUInt64()
        => Value;

    protected override Single? ConvertToFloat32()
        => Value;

    protected override Double? ConvertToFloat64()
        => Value;

    protected override String? ConvertToString()
        => Value.ToString("0", CultureInfo.InvariantCulture);

    protected override DateTimeOffset? ConvertToDateTime()
        => DateTimeOffset.UnixEpoch.AddSeconds(Value);

    protected override DateOnly? ConvertToDate()
        => new DateOnly(1970, 1, 1).AddDays((int)(Value / 86400));

    protected override TimeOnly? ConvertToTime()
        => null;

    #endregion Convert

}
