using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace AASeq;

/// <summary>
/// UInt64 value.
/// </summary>
public sealed class AAUInt64Value : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AAUInt64Value(UInt64 value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public UInt64 Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AAUInt64Value Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AAUInt64Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new AAUInt64Value(value);
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
    internal static bool TryParseValue(string? text, out UInt64 result) {
        return UInt64.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
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


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AAUInt64Value otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is UInt64 objValue) {
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
    /// Implicit conversion into an unsigned long.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator ulong(AAUInt64Value obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AAUInt64Value obj)
        => obj.ToString();

    #endregion Operators


    #region AAValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => Value != 0;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value is <= Byte.MaxValue ? (Byte)Value : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => Value is <= UInt16.MaxValue ? (UInt16)Value : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => Value is <= UInt32.MaxValue ? (UInt32)Value : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => Value;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => Value is <= (Byte)SByte.MaxValue ? (SByte)Value : null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => Value is <= (UInt16)Int16.MaxValue ? (Int16)Value : null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => Value is <= Int32.MaxValue ? (Int32)Value : null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value is <= Int64.MaxValue ? (Int64)Value : null;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => Value;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Value;

    /// <inheritdoc/>
    public override DateTimeOffset? AsDateTimeOffset()
        => DateTimeOffset.UnixEpoch.AddSeconds(Value);

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => new DateOnly(1970, 1, 1).AddDays((int)(Value / 86400));

    /// <inheritdoc/>
    public override TimeOnly? AsTimeOnly()
        => null;

    /// <inheritdoc/>
    public override TimeSpan? AsTimeSpan()
        => null;

    /// <inheritdoc/>
    public override String? AsString()
        => ToString();

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory() {
        var buffer = new byte[8];
        BinaryPrimitives.WriteUInt64BigEndian(buffer, Value);
        return buffer;
    }

    /// <inheritdoc/>
    public override AAFieldCollection? AsFieldCollection()
        => null;

    #endregion AAValuet

}
