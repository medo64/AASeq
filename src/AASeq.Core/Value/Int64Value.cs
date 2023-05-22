namespace AASeq;
using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Int64 value.
/// </summary>
public sealed class Int64Value : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public Int64Value(Int64 value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public Int64 Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    internal static bool TryParse(string? text, [NotNullWhen(true)] out Int64Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new Int64Value(value);
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
    internal static bool TryParseValue(string? text, out Int64 result) {
        return Int64.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
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
        if (obj is Int64Value otherValue) {
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
    /// Conversion into a Int64.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static Int64 ToInt64(Int64Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a Int64.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Int64(Int64Value obj)
        => ToInt64(obj);

    /// <summary>
    /// Conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static String ToString(Int64Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(Int64Value obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => Value != 0;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value is >= 0 and <= Byte.MaxValue ? (Byte)Value : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => Value is >= 0 and <= UInt16.MaxValue ? (UInt16)Value : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => Value is >= 0 and <= UInt32.MaxValue ? (UInt32)Value : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => Value >= 0 ? (UInt64)Value : null;

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
        => Value;

    /// <inheritdoc/>
    public override Half? AsHalf() {
        var res = (Half)Value;
        return Half.IsInfinity(res) ? null : res;
    }

    /// <inheritdoc/>
    public override Single? AsSingle()
        => Value;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Value;

    /// <inheritdoc/>
    public override DateTimeOffset? AsDateTimeOffset() {
        var minSeconds = (DateTimeOffset.MinValue.Ticks - DateTimeOffset.UnixEpoch.Ticks) / 10_000_000;
        var maxSeconds = (DateTimeOffset.MaxValue.Ticks - DateTimeOffset.UnixEpoch.Ticks) / 10_000_000;
        return (Value >= minSeconds) && (Value <= maxSeconds) ? DateTimeOffset.UnixEpoch.AddSeconds(Value) : null;
    }

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
        var buffer = new byte[8];
        BinaryPrimitives.WriteInt64BigEndian(buffer, Value);
        return buffer;
    }

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    #endregion AnyValue

}
