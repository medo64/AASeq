using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Time value.
/// </summary>
public sealed record TiniTimeValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniTimeValue(TimeOnly value) {
        _value = value;
    }


    private TimeOnly _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public TimeOnly Value {
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
            result = new TiniTimeValue(value);
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
    internal static bool TryParseValue(string? text, out TimeOnly result) {
        if (DateTime.TryParseExact(text, TiniDateTimeValue.ParseTimeFormats, CultureInfo.InvariantCulture, TiniDateTimeValue.ParseStyle, out var resultTime)) {
            result = new TimeOnly(resultTime.Hour, resultTime.Minute, resultTime.Second, resultTime.Millisecond);
            return true;
        } else {
            result = default;
            return false;
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
        => null;

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32()
        => null;

    protected override UInt64? ConvertToUInt64()
        => null;

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => null;

    protected override String? ConvertToString()
        => Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => Value;

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    #endregion Convert

}
