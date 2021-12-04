using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// Int64 value.
/// </summary>
public sealed record TiniInt64Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniInt64Value(Int64 value) {
        _value = value;
    }


    private Int64 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public Int64 Value {
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
        => Value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)Value : null;

    protected override Int16? ConvertToInt16()
        => Value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)Value : null;

    protected override Int32? ConvertToInt32()
        => Value is >= Int32.MinValue and <= Int32.MaxValue ? (Int32)Value : null;

    protected override Int64? ConvertToInt64()
        => Value;

    protected override Byte? ConvertToUInt8()
        => Value is >= 0 and <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is >= 0 and <= UInt16.MaxValue ? (UInt16)Value : null;

    protected override UInt32? ConvertToUInt32()
        => Value is >= 0 and <= UInt32.MaxValue ? (UInt32)Value : null;

    protected override UInt64? ConvertToUInt64()
        => Value >= 0 ? (UInt64)Value : null;

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
