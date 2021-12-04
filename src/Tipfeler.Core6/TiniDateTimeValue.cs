using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// DateTime value.
/// </summary>
public sealed record TiniDateTimeValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniDateTimeValue(DateTimeOffset value) {
        _value = value;
    }


    private DateTimeOffset _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public DateTimeOffset Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => null;

    protected override SByte? ConvertToInt8()
        => null;

    protected override Int16? ConvertToInt16()
        => null;

    protected override Int32? ConvertToInt32()
        => null;

    protected override Int64? ConvertToInt64()
        => Value.ToUnixTimeSeconds();

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32()
        => null;

    protected override UInt64? ConvertToUInt64() {
        var value = Value.ToUnixTimeSeconds();
        return value >= 0 ? (UInt64)value : null;
    }

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => Value.ToUnixTimeMilliseconds() / 1000.0;

    protected override String? ConvertToString()
        => Value.ToString("yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture);

    protected override DateTimeOffset? ConvertToDateTime()
        => Value;

    protected override DateOnly? ConvertToDate()
        => new DateOnly(Value.Year, Value.Month, Value.Day);

    protected override TimeOnly? ConvertToTime()
        => new TimeOnly(Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

    #endregion Convert


    internal static readonly string[] ParseFormats = new string[] {
        "YYYY-mm-dd'T'HH:mm:ss.ffffffK",
        "YYYY-mm-dd'T'HH:mm:ss.fffK",
        "YYYY-mm-dd'T'HH:mm:ssK",
        "YYYY-mm-dd'T'HH:mm:ss.ffffff",
        "YYYY-mm-dd'T'HH:mm:ss.fff",
        "YYYY-mm-dd'T'HH:mm:ss",
        "YYYYmmdd'T'HHmmss.ffffffK",
        "YYYYmmdd'T'HHmmss.ffffff",
        "YYYYmmdd'T'HHmmss.fffK",
        "YYYYmmdd'T'HHmmss.fff",
        "YYYYmmdd'T'HHmmssK",
        "YYYYmmdd'T'HHmmss",
        "YYYY-mm-dd' 'HH:mm:ss.ffffff K",
        "YYYY-mm-dd' 'HH:mm:ss.ffffff",
        "YYYY-mm-dd' 'HH:mm:ss.fff K",
        "YYYY-mm-dd' 'HH:mm:ss.fff",
        "YYYY-mm-dd' 'HH:mm:ss K",
        "YYYY-mm-dd' 'HH:mm:ss",
    };

}
