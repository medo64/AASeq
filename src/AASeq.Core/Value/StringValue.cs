namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

/// <summary>
/// String value.
/// </summary>
public sealed class StringValue : AnyValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public StringValue(String value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        Value = value;
    }


    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public String Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out StringValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new StringValue(value);
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
        if (obj is StringValue otherValue) {
            return Value.Equals(otherValue.Value, StringComparison.Ordinal);
        } else if (obj is String objValue) {
            return Value.Equals(objValue, StringComparison.Ordinal);
        } else {
            return false;
        }
    }

    /// <summary>
    /// Returns a hash code for the current object.
    /// </summary>
    public override int GetHashCode() {
        return Value.GetHashCode(StringComparison.Ordinal);
    }

    #endregion


    #region Operators

    /// <summary>
    /// Convert object into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static String ToString(StringValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(StringValue obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => BooleanValue.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => UInt8Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => UInt16Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => UInt32Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => UInt64Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => Int8Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => Int16Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => Int32Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Int64Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Half? AsHalf()
        => Float16Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => Float32Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Float64Value.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override DateTimeOffset? AsDateTimeOffset()
        => DateTimeValue.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => DateValue.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override TimeOnly? AsTimeOnly()
        => TimeValue.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override TimeSpan? AsTimeSpan()
        => DurationValue.TryParseValue(Value, out var result) ? result : null;

    /// <inheritdoc/>
    public override String? AsString()
        => ToString();

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => Utf8.GetBytes(Value);

    #endregion AnyValue

}
