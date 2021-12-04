using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Tipfeler;

/// <summary>
/// String value.
/// </summary>
public sealed record TiniStringValue : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public TiniStringValue(String value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        _value = value;
    }


    private String _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public String Value {
        get => _value;
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
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
            result = new TiniStringValue(value);
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
    internal static bool TryParseValue(string? text, [NotNullWhen(true)] out String? result) {
        if (text != null) {
            result = text;
            return true;
        } else {
            result = default;
            return false;
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => TiniBooleanValue.TryParseValue(Value, out var result) ? result : null;

    protected override SByte? ConvertToInt8()
        => TiniInt8Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int16? ConvertToInt16()
        => TiniInt16Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int32? ConvertToInt32()
        => TiniInt32Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int64? ConvertToInt64()
        => TiniInt64Value.TryParseValue(Value, out var result) ? result : null;

    protected override Byte? ConvertToUInt8()
        => TiniUInt8Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt16? ConvertToUInt16()
        => TiniUInt16Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt32? ConvertToUInt32()
        => TiniUInt32Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToUInt64()
        => TiniUInt64Value.TryParseValue(Value, out var result) ? result : null;

    protected override Single? ConvertToFloat32()
        => TiniFloat32Value.TryParseValue(Value, out var result) ? result : null;

    protected override Double? ConvertToFloat64()
        => TiniFloat64Value.TryParseValue(Value, out var result) ? result : null;

    protected override String? ConvertToString()
        => Value;

    protected override DateTimeOffset? ConvertToDateTime()
        => TiniDateTimeValue.TryParseValue(Value, out var result) ? result : null;

    protected override DateOnly? ConvertToDate()
        => TiniDateValue.TryParseValue(Value, out var result) ? result : null;

    protected override TimeOnly? ConvertToTime()
        => TiniTimeValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPAddress()
        => TiniIPAddressValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv4Address()
        => TiniIPv4AddressValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv6Address()
        => TiniIPv6AddressValue.TryParseValue(Value, out var result) ? result : null;

    #endregion Convert

}
