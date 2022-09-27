using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace AASeq;

/// <summary>
/// Date value.
/// </summary>
public sealed class AADateValue : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AADateValue(DateOnly value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public DateOnly Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AADateValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AADateValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AADateValue(value);
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
        if (DateTime.TryParseExact(text, AADateTimeValue.ParseDateFormats, CultureInfo.InvariantCulture, AADateTimeValue.ParseStyle, out var resultDate)) {
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
        return ToString(AADateTimeValue.ParseDateFormats[0]);
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
        if (obj is AADateValue otherValue) {
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
    /// Implicit conversion into a DateOnly.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator DateOnly(AADateValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AADateValue obj)
        => obj.ToString();

    #endregion Operators


    #region AAValue

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
    public override Int32? AsInt32()
        => null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => AsDateTimeOffset()?.ToUnixTimeSeconds();

    /// <inheritdoc/>
    public override Single? AsSingle()
        => null;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => AsUInt64();

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
    public override IPAddress? AsIPAddress()
        => null;

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => null;

    /// <inheritdoc/>
    public override AAFieldCollection? AsFieldCollection()
        => null;

    #endregion AAValue

}
