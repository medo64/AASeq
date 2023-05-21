namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Time value.
/// </summary>
public sealed class TimeValue : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TimeValue(TimeOnly value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public TimeOnly Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static TimeValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TimeValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TimeValue(value);
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
    internal static bool TryParseValue(string? text, out TimeOnly result) {
        if (DateTime.TryParseExact(text, DateTimeValue.ParseTimeFormats, CultureInfo.InvariantCulture, DateTimeValue.ParseStyle, out var resultTime)) {
            var result1 = new TimeOnly(resultTime.Hour, resultTime.Minute, resultTime.Second);
            result = new TimeOnly(result1.Ticks + resultTime.Ticks % 10000000);  // since TimeOnly has no AddTicks method
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
        return ToString(DateTimeValue.ParseTimeFormats[0]);
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
        if (obj is TimeValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is TimeOnly objValue) {
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
    /// Convert object into a TimeOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static TimeOnly ToTimeOnly(TimeValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a TimeOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator TimeOnly(TimeValue obj)
        => ToTimeOnly(obj);

    /// <summary>
    /// ImpliConvert object into String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static String ToString(TimeValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(TimeValue obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => null;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => (Int32)(Value.Ticks / 10_000_000);

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value.Ticks / 10_000_000;

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
        => Value;

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
