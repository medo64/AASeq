namespace AASeq;
using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Float16 value.
/// </summary>
public sealed class Float16Value : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public Float16Value(Half value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public Half Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out Float16Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new Float16Value(value);
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
    internal static bool TryParseValue(string? text, out Half result) {
        return Half.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
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
        if (obj is Float16Value otherValue) {
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
    /// Conversion into a Half.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static Half ToHalf(Float16Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a Half.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Half(Float16Value obj)
        => ToHalf(obj);

    /// <summary>
    /// Conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(Float16Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(Float16Value obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => null;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => (Value >= (Half)Byte.MinValue) && (Value <= (Half)Byte.MaxValue) ? (Byte)Value : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => (Value >= (Half)UInt16.MinValue) && (Value <= (Half)UInt16.MaxValue) ? (UInt16)Value : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => (Value >= (Half)UInt32.MinValue) && (Value <= (Half)UInt32.MaxValue) ? (UInt32)Value : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => (Value >= (Half)UInt64.MinValue) && (Value <= (Half)UInt64.MaxValue) ? (UInt64)Value : null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => (Value >= (Half)SByte.MinValue) && (Value <= (Half)SByte.MaxValue) ? (SByte)Value : null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => (Value >= (Half)Int16.MinValue) && (Value <= (Half)Int16.MaxValue) ? (Int16)Value : null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => (Value >= (Half)Int32.MinValue) && (Value <= (Half)Int32.MaxValue) ? (Int32)Value : null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => (Value >= (Half)Int64.MinValue) && (Value <= (Half)Int64.MaxValue) ? (Int32)Value : null;

    /// <inheritdoc/>
    public override Half? AsHalf()
        => Value;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => (Single)Value;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => (Double)Value;

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
        var buffer = new byte[2];
        BinaryPrimitives.WriteHalfBigEndian(buffer, Value);
        return buffer;
    }

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    #endregion AnyValue

}
