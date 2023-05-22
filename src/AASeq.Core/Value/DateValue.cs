namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// Date value.
/// </summary>
public sealed class DateValue : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public DateValue(DateOnly value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public DateOnly Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    internal static bool TryParse(string? text, [NotNullWhen(true)] out DateValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new DateValue(value);
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
    internal static bool TryParseValue(string? text, out DateOnly result) {
        if (DateTime.TryParseExact(text, DateTimeValue.ParseDateFormats, CultureInfo.InvariantCulture, DateTimeValue.ParseStyle, out var resultDate)) {
            result = new DateOnly(resultDate.Year, resultDate.Month, resultDate.Day);
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
        return ToString(DateTimeValue.ParseDateFormats[0]);
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
        if (obj is DateValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is DateOnly objValue) {
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
    /// Conversion into a DateOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static DateOnly ToDateOnly(DateValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a DateOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator DateOnly(DateValue obj)
        => ToDateOnly(obj);

    /// <summary>
    /// Conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(DateValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(DateValue obj)
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
    public override UInt64? AsUInt64() {
        var seconds = AsDateTimeOffset()?.ToUnixTimeSeconds();
        return seconds >= 0 ? (UInt64)seconds : null;
    }

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => null;

    /// <inheritdoc/>
    public override Int32? AsInt32() {
        var seconds = AsInt64();
        return (seconds is >= Int32.MinValue and <= Int32.MaxValue) ? (Int32)seconds : null;
    }

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => AsDateTimeOffset()?.ToUnixTimeSeconds();

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
        => new DateTimeOffset(Value.Year, Value.Month, Value.Day, 0, 0, 0, 0, new TimeSpan());

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => Value;

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

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    #endregion AnyValue

}
