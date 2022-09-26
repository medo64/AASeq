using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace AASeq;

/// <summary>
/// String value.
/// </summary>
public sealed class AAStringValue : AAValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public AAStringValue(String value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        Value = value;
    }


    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public String Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AAStringValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AAStringValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AAStringValue(value);
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


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AAStringValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is String objValue) {
            return Value.Equals(objValue);
        } else {
            return false;
        }
    }

    /// <summary>
    /// Returns a hash code for the current object.
    /// </summary>
    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    #endregion


    #region Operators

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AAStringValue obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => AABooleanValue.TryParseValue(Value, out var result) ? result : null;

    protected override SByte? ConvertToInt8()
        => AAInt8Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int16? ConvertToInt16()
        => AAInt16Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int32? ConvertToInt32()
        => AAInt32Value.TryParseValue(Value, out var result) ? result : null;

    protected override Int64? ConvertToInt64()
        => AAInt64Value.TryParseValue(Value, out var result) ? result : null;

    protected override Byte? ConvertToUInt8()
        => AAUInt8Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt16? ConvertToUInt16()
        => AAUInt16Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt32? ConvertToUInt32()
        => AAUInt32Value.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToUInt64()
        => AAUInt64Value.TryParseValue(Value, out var result) ? result : null;

    protected override Single? ConvertToFloat32()
        => AAFloat32Value.TryParseValue(Value, out var result) ? result : null;

    protected override Double? ConvertToFloat64()
        => AAFloat64Value.TryParseValue(Value, out var result) ? result : null;

    protected override String? ConvertToString()
        => ToString();

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    protected override ReadOnlyMemory<Byte>? ConvertToBinary()
        => Utf8.GetBytes(Value);

    protected override DateTimeOffset? ConvertToDateTime()
        => AADateTimeValue.TryParseValue(Value, out var result) ? result : null;

    protected override DateOnly? ConvertToDate()
        => AADateValue.TryParseValue(Value, out var result) ? result : null;

    protected override TimeOnly? ConvertToTime()
        => AATimeValue.TryParseValue(Value, out var result) ? result : null;

    protected override TimeSpan? ConvertToDuration()
        => AADurationValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPAddress()
        => AAIPAddressValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv4Address()
        => AAIPv4AddressValue.TryParseValue(Value, out var result) ? result : null;

    protected override IPAddress? ConvertToIPv6Address()
        => AAIPv6AddressValue.TryParseValue(Value, out var result) ? result : null;

    protected override UInt64? ConvertToSize()
        => AASizeValue.TryParseValue(Value, out var result) ? result : null;

    protected override AAFieldCollection? ConvertToFieldCollection()
        => null;

    #endregion Convert

}
