using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

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


    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniDateTimeValue(value);
            return true;
        } else {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Returns true if text can be converted with the value in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    internal static bool TryParseValue(string? text, out DateTimeOffset result) {
        if (DateTime.TryParseExact(text, ParseDateTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultDateTime)) {
            result = resultDateTime;
            return true;
        } else if (DateTime.TryParseExact(text, ParseDateFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultDate)) {
            result = resultDate;
            return true;
        } else if (DateTime.TryParseExact(text, ParseTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultTime)) {
            result = resultTime;
            return true;
        }

        result = default;
        return false;
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

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    #endregion Convert


    #region Constants

    internal static readonly string[] ParseDateTimeFormats = new string[] {
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF K",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF K",
        "yyyyMMdd'T'HHmmss.FFFFFFF K",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF",
        "yyyyMMdd'T'HHmmss.FFFFFFF",
        "yyyy-MM-dd'T'HH:mm K",
        "yyyy-MM-dd HH:mm K",
        "yyyyMMdd'T'HHmm K",
        "yyyy-MM-dd'T'HH:mm",
        "yyyy-MM-dd HH:mm",
        "yyyyMMdd'T'HHmm",
    };

    internal static readonly string[] ParseDateFormats = new string[] {
        "yyyy-mm-dd",
    };

    internal static readonly string[] ParseTimeFormats = new string[] {
        "HH:mm:ss.FFFFFFF",
        "HH:mm",
    };

    internal static readonly DateTimeStyles ParseStyle = DateTimeStyles.AllowLeadingWhite
                                                  | DateTimeStyles.AllowInnerWhite
                                                  | DateTimeStyles.AllowTrailingWhite
                                                  | DateTimeStyles.AllowWhiteSpaces
                                                  | DateTimeStyles.AssumeUniversal
                                                  | DateTimeStyles.AdjustToUniversal;

    #endregion Constants

}
