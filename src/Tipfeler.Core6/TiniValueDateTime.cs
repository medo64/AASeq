using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// DateTime value.
/// </summary>
public sealed class TiniValueDateTime : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniValueDateTime(DateTimeOffset value) {
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


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static TiniValueDateTime Parse(string text) {
        if (TryParse(text, out var value)) {
            return value;
        } else {
            throw new FormatException("Cannot parse text.");
        }
    }

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniValueDateTime? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniValueDateTime(value);
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
        if (DateTimeOffset.TryParseExact(text, ParseDateTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out result)) {
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

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        if (Value.UtcTicks < 864000000000) {  // time-only
            return ToString(ParseTimeFormats[0]);
        } else if ((Value.Offset.Ticks == 0) && (Value.UtcTicks % 864000000000 == 0)) {  // date-only
            return ToString(ParseDateFormats[0]);
        } else {  // both date and time
            return ToString(ParseDateTimeFormats[0]);
        }
    }

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToString(string? format) {
        return Value.ToString(format, CultureInfo.InvariantCulture);
    }

    #endregion ToString


    #region Operators

    /// <summary>
    /// Implicit conversion into a DateTimeOffset.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator DateTimeOffset(TiniValueDateTime obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniValueDateTime obj)
        => obj.ToString();

    #endregion Operators


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

    protected override ReadOnlyMemory<Byte>? ConvertToBinary()
        => null;

    protected override DateTimeOffset? ConvertToDateTime()
        => Value;

    protected override DateOnly? ConvertToDate()
        => new DateOnly(Value.Year, Value.Month, Value.Day);

    protected override TimeOnly? ConvertToTime()
        => new TimeOnly(Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

    protected override TimeSpan? ConvertToDuration()
        => null;

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    protected override UInt64? ConvertToSize()
        => null;

    #endregion Convert


    #region Constants

    internal static readonly string[] ParseDateTimeFormats = new string[] {
        "yyyy-MM-dd HH:mm:ss.FFFFFFF K",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF K",
        "yyyyMMdd'T'HHmmss.FFFFFFF K",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyyMMdd'T'HHmmss.FFFFFFF",
        "yyyy-MM-dd HH:mm K",
        "yyyy-MM-dd'T'HH:mm K",
        "yyyyMMdd'T'HHmm K",
        "yyyy-MM-dd HH:mm",
        "yyyy-MM-dd'T'HH:mm",
        "yyyyMMdd'T'HHmm",
    };

    internal static readonly string[] ParseDateFormats = new string[] {
        "yyyy-MM-dd",
    };

    internal static readonly string[] ParseTimeFormats = new string[] {
        "HH:mm:ss.FFFFFFF",
        "HH:mm",
    };

    internal static readonly string[] ParseTimeSpanFormats = new string[] {
        @"d\.h\:mm\:ss\.FFFFFFF",
        @"d\.h\:mm\:ss",
        @"h\:mm\:ss\.FFFFFFF",
        @"h\:mm\:ss",
        @"d\.h\:mm",
        @"m\:ss\.FFFFFFF",
        @"m\:ss",
    };

    internal static readonly DateTimeStyles ParseStyle = DateTimeStyles.AllowLeadingWhite
                                                       | DateTimeStyles.AllowInnerWhite
                                                       | DateTimeStyles.AllowTrailingWhite
                                                       | DateTimeStyles.AllowWhiteSpaces
                                                       | DateTimeStyles.AssumeLocal;

    #endregion Constants

}
