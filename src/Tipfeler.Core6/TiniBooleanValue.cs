using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Boolean value.
/// </summary>
public sealed record TiniBooleanValue : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniBooleanValue(Boolean value) {
        _value = value;
    }


    private Boolean _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public Boolean Value {
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
            result = new TiniBooleanValue(value);
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
    internal static bool TryParseValue(string? text, out bool result) {
        if (text != null) {
            var trimmed = text.Trim();
            if (trimmed.Equals("True", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("Yes", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("T", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("+", StringComparison.InvariantCultureIgnoreCase)) {
                result = true;
                return true;
            } else if (trimmed.Equals("False", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("No", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("F", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("N", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("-", StringComparison.InvariantCultureIgnoreCase)) {
                result = false;
                return true;
            } else if (Int32.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue)) {
                result = intValue != 0;
                return true;
            }
        }

        result = default;
        return false;
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value;

    protected override SByte? ConvertToInt8()
        => Value ? (sbyte)1 : (sbyte)0;

    protected override Int16? ConvertToInt16()
        => Value ? (Int16)1 : (Int16)0;

    protected override Int32? ConvertToInt32()
        => Value ? 1 : 0;

    protected override Int64? ConvertToInt64()
        => Value ? 1 : 0;

    protected override Byte? ConvertToUInt8()
        => Value ? (byte)1 : (byte)0;

    protected override UInt16? ConvertToUInt16()
        => Value ? (UInt16)1 : (UInt16)0;

    protected override UInt32? ConvertToUInt32()
        => Value ? 1u : 0;

    protected override UInt64? ConvertToUInt64()
        => Value ? 1u : 0;

    protected override Single? ConvertToFloat32()
        => Value ? 1 : 0;

    protected override Double? ConvertToFloat64()
        => Value ? 1 : 0;

    protected override String? ConvertToString()
        => Value ? "True" : "False";

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
