using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace AASeq;

/// <summary>
/// Int16 value.
/// </summary>
public sealed class AAInt16Value : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AAInt16Value(Int16 value) {
        Value = value;
    }


    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AAInt16Value Parse(string text) {
        if (TryParse(text, out var value)) {
            return value;
        } else {
            throw new FormatException("Cannot parse text.");
        }
    }

    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out AAInt16Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new AAInt16Value(value);
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
    internal static bool TryParseValue(string? text, out Int16 result) {
        return Int16.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
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


    /// <summary>
    /// Gets value.
    /// </summary>
    public Int16 Value { get; }

    #endregion ToString


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AAInt16Value otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is Int16 objValue) {
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
    /// Implicit conversion into a short.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator short(AAInt16Value obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AAInt16Value obj)
        => obj.ToString();

    #endregion Operators


    #region AAValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => Value != 0;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value is >= 0 and <= Byte.MaxValue ? (Byte)Value : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => Value is >= 0 ? (UInt16)Value : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => Value >= 0 ? (UInt32)Value : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => Value >= 0 ? (UInt64)Value : null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => Value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)Value : null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => Value;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => Value;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => Value;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Value;

    /// <inheritdoc/>
    public override DateTimeOffset? AsDateTimeOffset()
        => null;

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => null;

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
        var buffer = new byte[2];
        BinaryPrimitives.WriteInt16BigEndian(buffer, Value);
        return buffer;
    }

    /// <inheritdoc/>
    public override AAFieldCollection? AsFieldCollection()
        => null;

    #endregion AAValuet

}
