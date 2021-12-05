using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text;

namespace Tipfeler;

/// <summary>
/// Date value.
/// </summary>
public sealed record TiniDurationValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniDurationValue(TimeSpan value) {
        _value = value;
    }


    private TimeSpan _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public TimeSpan Value {
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
    public static TiniDurationValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniDurationValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniDurationValue(value);
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
    internal static bool TryParseValue(string? text, out TimeSpan result) {
        if (TimeSpan.TryParseExact(text, TiniDateTimeValue.ParseTimeSpanFormats, CultureInfo.InvariantCulture, TimeSpanStyles.None, out result)) {
            return true;
        } else if (decimal.TryParse(text, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var decimalValue)) {
            var totalTicks = decimalValue * 10000000;
            if (totalTicks is >= long.MinValue and <= long.MaxValue) {
                result = new TimeSpan((long)totalTicks);
                return true;
            }
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
        var sb = new StringBuilder();
        if (Value.Days >= 1) {  // add days only if they exist
            sb.Append(Value.Days.ToString(CultureInfo.InvariantCulture));
            sb.Append('.');
        }
        sb.AppendFormat(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", Value.Hours, Value.Minutes, Value.Seconds);

        var nanos = (Value.Ticks % 10000000).ToString("0000000", CultureInfo.InvariantCulture);
        var i = nanos.Length - 1;
        for (; i >= 0; i--) {  // find where first non-zero is
            if (nanos[i] != '0') { break; }
        }
        if (i > -1) {  // add only significant zeros
            sb.Append('.');
            sb.Append(nanos[0..(i + 1)]);
        }

        return sb.ToString();
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
    public static implicit operator TimeSpan(TiniDurationValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniDurationValue obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => null;

    protected override SByte? ConvertToInt8()
        => null;

    protected override Int16? ConvertToInt16()
        => null;

    protected override Int32? ConvertToInt32() {
        var seconds = Value.Ticks / 10000000;
        if (seconds is >= Int32.MinValue and <= Int32.MaxValue) { return (Int32)seconds; }
        return null;
    }

    protected override Int64? ConvertToInt64()
        => Value.Ticks / 10000000;

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32() {
        var seconds = Value.Ticks / 10000000;
        if (seconds is >= UInt32.MinValue and <= UInt32.MaxValue) { return (UInt32)seconds; }
        return null;
    }

    protected override UInt64? ConvertToUInt64() {
        var seconds = Value.Ticks / 10000000;
        return seconds >= 0 ? (UInt64)seconds : null;
    }

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => Value.Ticks / 10000000.0;

    protected override String? ConvertToString()
        => ToString();

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => null;

    protected override TimeSpan? ConvertToDuration()
        => Value;

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    protected override UInt64? ConvertToSize()
        => null;

    protected override UInt64? ConvertToBinarySize()
        => null;

    #endregion Convert

}
