using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Float64 value.
/// </summary>
public sealed record TiniFloat64Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniFloat64Value(Double value) {
        _value = value;
    }


    private Double _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public Double Value {
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
    public static TiniFloat64Value Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniFloat64Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniFloat64Value(value);
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
    internal static bool TryParseValue(string? text, out Double result) {
        return Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
    }

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        return Value.ToString(CultureInfo.InvariantCulture);
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
    /// Implicit conversion into a double.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator double(TiniFloat64Value obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniFloat64Value obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value != 0;

    protected override SByte? ConvertToInt8()
        => Value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)Value : null;

    protected override Int16? ConvertToInt16()
        => Value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)Value : null;

    protected override Int32? ConvertToInt32()
        => Value is >= Int32.MinValue and <= Int32.MaxValue ? (Int32)Value : null;

    protected override Int64? ConvertToInt64()
        => Value is >= Int64.MinValue and <= Int64.MaxValue ? (Int32)Value : null;

    protected override Byte? ConvertToUInt8()
        => Value is >= Byte.MinValue and <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is >= UInt16.MinValue and <= UInt16.MaxValue ? (UInt16)Value : null;

    protected override UInt32? ConvertToUInt32()
        => Value is >= UInt32.MinValue and <= UInt32.MaxValue ? (UInt32)Value : null;

    protected override UInt64? ConvertToUInt64()
        => Value is >= UInt64.MinValue and <= UInt64.MaxValue ? (UInt64)Value : null;

    protected override Single? ConvertToFloat32()
        => (Single)Value;

    protected override Double? ConvertToFloat64()
        => Value;

    protected override String? ConvertToString()
        => ToString();

    protected override DateTimeOffset? ConvertToDateTime()
        => DateTimeOffset.UnixEpoch.AddSeconds(Value);

    protected override DateOnly? ConvertToDate()
        => new DateOnly(1970, 1, 1).AddDays((int)(Value / 86400f));

    protected override TimeOnly? ConvertToTime()
        => null;

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

    protected override UInt64? ConvertToBinarySize()
        => null;

    #endregion Convert

}
