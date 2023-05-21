namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

/// <summary>
/// DateTime value.
/// </summary>
public sealed class DateTimeValue : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public DateTimeValue(DateTimeOffset value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public DateTimeOffset Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static DateTimeValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out DateTimeValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new DateTimeValue(value);
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
    internal static bool TryParseValue(string? text, out DateTimeOffset result) {
        if (DateTimeOffset.TryParseExact(text, ParseDateTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out result)) {
            return true;
        } else if (DateTime.TryParseExact(text, ParseDateFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultDate)) {
            result = resultDate;
            return true;
        } else if (DateTime.TryParseExact(text, ParseTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultTime)) {
            result = resultTime;
            return true;
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
        if (Value.UtcTicks < 864000000000) {  // time-only
            return ToString(ParseTimeFormats[0]);
        } else if ((Value.Offset.Ticks == 0) && (Value.UtcTicks % 864000000000 == 0)) {  // date-only
            return ToString(ParseDateFormats[0]);
        } else {  // both date and time
            return ToString(ParseDateTimeFormats[0]);
        }
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
        if (obj is DateTimeValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is DateTimeOffset objValue) {
            return Value.Equals(objValue);
        } else if (obj is DateTime objDateValue) {
            return Value.Equals(new DateTimeOffset(objDateValue));
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
    /// Convert object into a DateTimeOffset.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static DateTimeOffset ToDateTimeOffset(DateTimeValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a DateTimeOffset.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator DateTimeOffset(DateTimeValue obj)
        => ToDateTimeOffset (obj);

    /// <summary>
    /// Convert object into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static String ToString(DateTimeValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(DateTimeValue obj)
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
        var value = Value.ToUnixTimeSeconds();
        return value >= 0 ? (UInt64)value : null;
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
        => Value.ToUnixTimeSeconds();

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
        => Value;

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => new DateOnly(Value.Year, Value.Month, Value.Day);

    /// <inheritdoc/>
    public override TimeOnly? AsTimeOnly()
        => new TimeOnly(Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

    /// <inheritdoc/>
    public override TimeSpan? AsTimeSpan()
        => null;

    /// <inheritdoc/>
    public override String? AsString()
        => Value.ToString("yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture);

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => null;

    #endregion AnyValue


    #region Constants

    internal static readonly string[] ParseDateTimeFormats = new string[] {
        "yyyy-MM-dd HH:mm:ss.FFFFFFF K",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF K",
        "yyyyMMdd'T'HHmmss.FFFFFFF K",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyyMMdd'T'HHmmss.FFFFFFF",
        "yyyy-MM-dd HH:mm K",
        "yyyy-MM-dd'T'HH:mm K",
        "yyyyMMdd'T'HHmm K",
        "yyyy-MM-dd HH:mm",
        "yyyy-MM-dd'T'HH:mm",
        "yyyyMMdd'T'HHmm",
    };

    internal static readonly string[] ParseDateFormats = new string[] {
        "yyyy-MM-dd",
    };

    internal static readonly string[] ParseTimeFormats = new string[] {
        "HH:mm:ss.FFFFFFF",
        "HH:mm",
    };

    internal static readonly string[] ParseTimeSpanFormats = new string[] {
        @"d\.h\:mm\:ss\.FFFFFFF",
        @"d\.h\:mm\:ss",
        @"h\:mm\:ss\.FFFFFFF",
        @"h\:mm\:ss",
        @"d\.h\:mm",
        @"m\:ss\.FFFFFFF",
        @"m\:ss",
    };

    internal static readonly DateTimeStyles ParseStyle = DateTimeStyles.AllowLeadingWhite
                                                       | DateTimeStyles.AllowInnerWhite
                                                       | DateTimeStyles.AllowTrailingWhite
                                                       | DateTimeStyles.AllowWhiteSpaces
                                                       | DateTimeStyles.AssumeLocal;

    #endregion Constants

}
