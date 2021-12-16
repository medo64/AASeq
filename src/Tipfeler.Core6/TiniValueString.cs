using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Tipfeler;

/// <summary>
/// String value.
/// </summary>
public sealed class TiniValueString : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public TiniValueString(String value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        _value = value;
    }


    private String _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public String Value {
        get => _value;
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
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
    public static TiniValueString Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniValueString? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniValueString(value);
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

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        return Value;
    }

    #endregion ToString


    #region Operators

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniValueString obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => TiniValueBoolean.TryParseValue(Value, out var result) ? result : null;

    protected override SByte? ConvertToInt8()
        => TiniValueInt8.TryParseValue(Value, out var result) ? result : null;

    protected override Int16? ConvertToInt16()
        => TiniValueInt16.TryParseValue(Value, out var result) ? result : null;

    protected override Int32? ConvertToInt32()
        => TiniValueInt32.TryParseValue(Value, out var result) ? result : null;

    protected override Int64? ConvertToInt64()
        => TiniValueInt64.TryParseValue(Value, out var result) ? result : null;

    protected override Byte? ConvertToUInt8()
        => TiniValueUInt8.TryParseValue(Value, out var result) ? result : null;

    protected override UInt16? ConvertToUInt16()
        => TiniValueUInt16.TryParseValue(Value, out var result) ? result : null;

    protected override UInt32? ConvertToUInt32()
        => TiniValueUInt32.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToUInt64()
        => TiniValueUInt64.TryParseValue(Value, out var result) ? result : null;

    protected override Single? ConvertToFloat32()
        => TiniValueFloat32.TryParseValue(Value, out var result) ? result : null;

    protected override Double? ConvertToFloat64()
        => TiniValueFloat64.TryParseValue(Value, out var result) ? result : null;

    protected override String? ConvertToString()
        => ToString();

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    protected override ReadOnlyMemory<Byte>? ConvertToBinary()
        => Utf8.GetBytes(Value);

    protected override DateTimeOffset? ConvertToDateTime()
        => TiniValueDateTime.TryParseValue(Value, out var result) ? result : null;

    protected override DateOnly? ConvertToDate()
        => TiniValueDate.TryParseValue(Value, out var result) ? result : null;

    protected override TimeOnly? ConvertToTime()
        => TiniValueTime.TryParseValue(Value, out var result) ? result : null;

    protected override TimeSpan? ConvertToDuration()
        => TiniValueDuration.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPAddress()
        => TiniValueIPAddress.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv4Address()
        => TiniValueIPv4Address.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv6Address()
        => TiniValueIPv6Address.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToSize()
        => TiniValueSize.TryParseValue(Value, out var result) ? result : null;

    #endregion Convert

}
