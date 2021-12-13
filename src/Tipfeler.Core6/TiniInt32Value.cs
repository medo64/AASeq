using System;
using System.Buffers.Binary;
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


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static TiniInt32Value Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniInt32Value? result) {
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
    /// Implicit conversion into an int.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator int(TiniInt32Value obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniInt32Value obj)
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
        => ToString();

    protected override Byte[]? ConvertToBinary() {
        var buffer = new byte[4];
        BinaryPrimitives.WriteInt32BigEndian(buffer, Value);
        return buffer;
    }

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

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
        => ConvertToUInt64();

    #endregion Convert

}
