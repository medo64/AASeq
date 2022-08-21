using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace AASeq;

/// <summary>
/// String value.
/// </summary>
public sealed class ValueString : Value {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public ValueString(String value) {
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
    public static ValueString Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out ValueString? result) {
        if (TryParseValue(text, out var value)) {
            result = new ValueString(value);
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
    public static implicit operator string(ValueString obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => ValueBoolean.TryParseValue(Value, out var result) ? result : null;

    protected override SByte? ConvertToInt8()
        => ValueInt8.TryParseValue(Value, out var result) ? result : null;

    protected override Int16? ConvertToInt16()
        => ValueInt16.TryParseValue(Value, out var result) ? result : null;

    protected override Int32? ConvertToInt32()
        => ValueInt32.TryParseValue(Value, out var result) ? result : null;

    protected override Int64? ConvertToInt64()
        => ValueInt64.TryParseValue(Value, out var result) ? result : null;

    protected override Byte? ConvertToUInt8()
        => ValueUInt8.TryParseValue(Value, out var result) ? result : null;

    protected override UInt16? ConvertToUInt16()
        => ValueUInt16.TryParseValue(Value, out var result) ? result : null;

    protected override UInt32? ConvertToUInt32()
        => ValueUInt32.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToUInt64()
        => ValueUInt64.TryParseValue(Value, out var result) ? result : null;

    protected override Single? ConvertToFloat32()
        => ValueFloat32.TryParseValue(Value, out var result) ? result : null;

    protected override Double? ConvertToFloat64()
        => ValueFloat64.TryParseValue(Value, out var result) ? result : null;

    protected override String? ConvertToString()
        => ToString();

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    protected override ReadOnlyMemory<Byte>? ConvertToBinary()
        => Utf8.GetBytes(Value);

    protected override DateTimeOffset? ConvertToDateTime()
        => ValueDateTime.TryParseValue(Value, out var result) ? result : null;

    protected override DateOnly? ConvertToDate()
        => ValueDate.TryParseValue(Value, out var result) ? result : null;

    protected override TimeOnly? ConvertToTime()
        => ValueTime.TryParseValue(Value, out var result) ? result : null;

    protected override TimeSpan? ConvertToDuration()
        => ValueDuration.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPAddress()
        => ValueIPAddress.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv4Address()
        => ValueIPv4Address.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv6Address()
        => ValueIPv6Address.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToSize()
        => ValueSize.TryParseValue(Value, out var result) ? result : null;

    #endregion Convert

}
