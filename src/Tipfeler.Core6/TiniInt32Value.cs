using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Int32 value.
/// </summary>
public sealed record TiniInt32Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniInt32Value(Int32 value) {
        _value = value;
    }


    private Int32 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public Int32 Value {
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
            result = new TiniInt32Value(value);
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
    internal static bool TryParseValue(string? text, out Int32 result) {
        return Int32.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value != 0;

    protected override SByte? ConvertToInt8()
        => Value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)Value : null;

    protected override Int16? ConvertToInt16()
        => Value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)Value : null;

    protected override Int32? ConvertToInt32()
        => Value;

    protected override Int64? ConvertToInt64()
        => Value;

    protected override Byte? ConvertToUInt8()
        => Value is >= 0 and <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is >= 0 and <= UInt16.MaxValue ? (UInt16)Value : null;

    protected override UInt32? ConvertToUInt32()
        => Value >= 0 ? (UInt32)Value : null;

    protected override UInt64? ConvertToUInt64()
        => Value >= 0 ? (UInt64)Value : null;

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
