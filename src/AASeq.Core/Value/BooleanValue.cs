namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>
/// Boolean value.
/// </summary>
public sealed class BooleanValue : AnyValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public BooleanValue(Boolean value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public Boolean Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out BooleanValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new BooleanValue(value);
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
    internal static bool TryParseValue(string? text, out bool result) {
        if (text != null) {
            var trimmed = text.Trim();
            if (trimmed.Equals("True", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("Yes", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("T", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("Y", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("+", StringComparison.OrdinalIgnoreCase)) {
                result = true;
                return true;
            } else if (trimmed.Equals("False", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("No", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("F", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("N", StringComparison.OrdinalIgnoreCase)
                || trimmed.Equals("-", StringComparison.OrdinalIgnoreCase)) {
                result = false;
                return true;
            } else if (Int32.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue)) {
                result = intValue != 0;
                return true;
            }
        }

        result = default;
        return false;
    }

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        return Value ? "True" : "False";
    }

    #endregion ToString


    #region Overrides

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        if (obj is BooleanValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is Boolean objValue) {
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
    /// Convert object to Boolean.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static Boolean ToBoolean(BooleanValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a Boolean.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Boolean(BooleanValue obj)
        => ToBoolean(obj);


    /// <summary>
    /// Convert object to String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(BooleanValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(BooleanValue obj)
        => ToString(obj);

    #endregion Operations


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => Value;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => Value ? (byte)1 : (byte)0;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => Value ? (UInt16)1 : (UInt16)0;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => Value ? 1u : 0;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => Value ? 1u : 0;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => Value ? (sbyte)1 : (sbyte)0;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => Value ? (Int16)1 : (Int16)0;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => Value ? 1 : 0;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value ? 1 : 0;

    /// <inheritdoc/>
    public override Half? AsHalf()
        => null;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => null;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => null;

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
        => null;

    #endregion AnyValue

}
