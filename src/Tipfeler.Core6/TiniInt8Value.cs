using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Int8 value.
/// </summary>
public sealed record TiniInt8Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniInt8Value(SByte value) {
        _value = value;
    }


    private SByte _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public SByte Value {
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
            result = new TiniInt8Value(value);
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
    internal static bool TryParseValue(string? text, out SByte result) {
        return SByte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value != 0;

    protected override SByte? ConvertToInt8()
        => Value;

    protected override Int16? ConvertToInt16()
        => Value;

    protected override Int32? ConvertToInt32()
        => Value;

    protected override Int64? ConvertToInt64()
        => Value;

    protected override Byte? ConvertToUInt8()
        => Value is >= 0 ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is >= 0 ? (UInt16)Value : null;

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
