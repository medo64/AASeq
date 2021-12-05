using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Date value.
/// </summary>
public sealed record TiniDateValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniDateValue(DateOnly value) {
        _value = value;
    }


    private DateOnly _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public DateOnly Value {
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
    public static TiniDateValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniDateValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniDateValue(value);
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
    internal static bool TryParseValue(string? text, out DateOnly result) {
        if (DateTime.TryParseExact(text, TiniDateTimeValue.ParseDateFormats, CultureInfo.InvariantCulture, TiniDateTimeValue.ParseStyle, out var resultDate)) {
            result = new DateOnly(resultDate.Year, resultDate.Month, resultDate.Day);
            return true;
        } else {
            result = default;
            return false;
        }
    }

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        return ToString(TiniDateTimeValue.ParseDateFormats[0]);
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
    /// Implicit conversion into a DateOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator DateOnly(TiniDateValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniDateValue obj)
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
        => ConvertToDateTime()?.ToUnixTimeSeconds();

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32()
        => null;

    protected override UInt64? ConvertToUInt64() {
        var seconds = ConvertToDateTime()?.ToUnixTimeSeconds();
        return seconds >= 0 ? (UInt64)seconds : null;
    }

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => ConvertToUInt64();

    protected override String? ConvertToString()
        => ToString();

    protected override DateTimeOffset? ConvertToDateTime()
        => new DateTimeOffset(Value.Year, Value.Month, Value.Day, 0, 0, 0, 0, new TimeSpan());

    protected override DateOnly? ConvertToDate()
        => Value;

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
