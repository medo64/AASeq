using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace AASeq;

/// <summary>
/// Boolean value.
/// </summary>
public sealed class AABooleanValue : AAValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AABooleanValue(Boolean value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public Boolean Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AABooleanValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AABooleanValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AABooleanValue(value);
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
            if (trimmed.Equals("True", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("Yes", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("T", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("+", StringComparison.InvariantCultureIgnoreCase)) {
                result = true;
                return true;
            } else if (trimmed.Equals("False", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("No", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("F", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("N", StringComparison.InvariantCultureIgnoreCase)
                || trimmed.Equals("-", StringComparison.InvariantCultureIgnoreCase)) {
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

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AABooleanValue otherValue) {
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
    /// Implicit conversion into a boolean.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator bool(AABooleanValue obj)
       => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AABooleanValue obj)
        => obj.ToString();

    #endregion Operations


    #region AAValue

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
    public override Single? AsSingle()
        => Value ? 1 : 0;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Value ? 1 : 0;

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
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => null;

    /// <inheritdoc/>
    public override AAFieldCollection? AsFieldCollection()
        => null;


    /// <inheritdoc/>
    public override AAValue Clone()
        => new AABooleanValue(Value);

    #endregion AAValue

}
