namespace AASeq;
using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Float32 value.
/// </summary>
public sealed class Float32Value : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public Float32Value(Single value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public Single Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static Float32Value Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out Float32Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new Float32Value(value);
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
    internal static bool TryParseValue(string? text, out Single result) {
        return Single.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
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
        if (obj is Float32Value otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is Single objValue) {
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
    /// Convert object into a Single.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static Single ToSingle(Float32Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a Single.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Single(Float32Value obj)
        => ToSingle(obj);

    /// <summary>
    /// Convert object into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(Float32Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(Float32Value obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => null;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value is >= Byte.MinValue and <= Byte.MaxValue ? (Byte)Value : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => Value is >= UInt16.MinValue and <= UInt16.MaxValue ? (UInt16)Value : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => Value is >= UInt32.MinValue and <= UInt32.MaxValue ? (UInt32)Value : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => Value is >= UInt64.MinValue and <= UInt64.MaxValue ? (UInt64)Value : null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => Value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)Value : null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => Value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)Value : null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => Value is >= Int32.MinValue and <= Int32.MaxValue ? (Int32)Value : null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value is >= Int64.MinValue and <= Int64.MaxValue ? (Int32)Value : null;

    /// <inheritdoc/>
    public override Half? AsHalf() {
        var res = (Half)Value;
        return Single.IsFinite(Value) && Half.IsInfinity(res) ? null : res;
    }

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
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory() {
        var buffer = new byte[4];
        BinaryPrimitives.WriteSingleBigEndian(buffer, Value);
        return buffer;
    }

    #endregion AnyValue

}
