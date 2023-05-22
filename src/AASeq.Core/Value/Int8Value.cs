namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Int8 value.
/// </summary>
public sealed class Int8Value : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public Int8Value(SByte value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public SByte Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out Int8Value? result) {
        if (TryParseValue(text, out var value)) {
            result = new Int8Value(value);
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
    internal static bool TryParseValue(string? text, out SByte result) {
        return SByte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
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
        if (obj is Int8Value otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is SByte objValue) {
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
    /// Convert object to SByte.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static SByte ToSByte(Int8Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a SByte.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator SByte(Int8Value obj)
        => ToSByte(obj);


    /// <summary>
    /// Convert object to String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(Int8Value obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(Int8Value obj)
        => ToString(obj);

    #endregion Operators


    #region Value

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => Value != 0;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value is >= 0 ? (Byte)Value : null;

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
        => Value;

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
    public override Half? AsHalf()
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
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => new byte[] { (byte)Value };

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    #endregion AnyValue

}
