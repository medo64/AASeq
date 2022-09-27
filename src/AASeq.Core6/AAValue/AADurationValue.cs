using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text;

namespace AASeq;

/// <summary>
/// Date value.
/// </summary>
public sealed class AADurationValue : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AADurationValue(TimeSpan value) {
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public TimeSpan Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AADurationValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AADurationValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AADurationValue(value);
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
    internal static bool TryParseValue(string? text, out TimeSpan result) {
        if (TimeSpan.TryParseExact(text, AADateTimeValue.ParseTimeSpanFormats, CultureInfo.InvariantCulture, TimeSpanStyles.None, out result)) {
            return true;
        } else if (decimal.TryParse(text, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var decimalValue)) {
            var totalTicks = decimalValue * 10000000;
            if (totalTicks is >= long.MinValue and <= long.MaxValue) {
                result = new TimeSpan((long)totalTicks);
                return true;
            }
        } else if (text is not null) {  // try splitting into parts and parsing that way
            var parts = new List<string>();
            bool expectNumber = true;
            var lastStart = 0;
            for (var i = 0; i < text.Length; i++) {  // go over chars to split into number/unit pairs
                var ch = text[i];
                if (expectNumber) {
                    if (!char.IsDigit(ch) && !char.IsWhiteSpace(ch) && (ch != '.') && (ch != ',')) {
                        parts.Add(text[lastStart..i].Trim());
                        lastStart = i;
                        expectNumber = false;
                    }
                } else {
                    if (!char.IsLetter(ch) && !char.IsWhiteSpace(ch)) {
                        parts.Add(text[lastStart..i].Trim());
                        lastStart = i;
                        expectNumber = true;
                    }
                }
            }
            parts.Add(text[lastStart..]);

            // lets add all parts we have here
            var totalTicks = 0L;
            var allGood = true;
            for (var i = 0; i < parts.Count; i += 2) {
                var numberPart = parts[i];
                var unitPart = (i + 1) < parts.Count ? parts[i + 1] : "";
                if (decimal.TryParse(numberPart, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var number)) {
                    switch (unitPart.ToUpperInvariant()) {
                        case "D": totalTicks += (long)Math.Round(number * 86400 * 10000000, 0); break;
                        case "H": totalTicks += (long)Math.Round(number * 3600 * 10000000, 0); break;
                        case "M": totalTicks += (long)Math.Round(number * 60 * 10000000, 0); break;
                        case "S": case "": totalTicks += (long)Math.Round(number * 10000000, 0); break;
                        case "MS": totalTicks += (long)Math.Round(number * 10000, 0); break;
                        case "US": totalTicks += (long)Math.Round(number * 10, 0); break;
                        case "NS": totalTicks += (long)Math.Round(number / 100, 0); break;
                        default: allGood = false; break;
                    }
                } else {
                    allGood = false;
                    break;
                }
            }
            if (allGood) {
                result = new TimeSpan(totalTicks);
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
        var sb = new StringBuilder();
        if (Value.Days >= 1) {  // add days only if they exist
            sb.Append(Value.Days.ToString(CultureInfo.InvariantCulture));
            sb.Append('.');
        }
        sb.AppendFormat(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", Value.Hours, Value.Minutes, Value.Seconds);

        var nanos = (Value.Ticks % 10000000).ToString("0000000", CultureInfo.InvariantCulture);
        var i = nanos.Length - 1;
        for (; i >= 0; i--) {  // find where first non-zero is
            if (nanos[i] != '0') { break; }
        }
        if (i > -1) {  // add only significant zeros
            sb.Append('.');
            sb.Append(nanos[0..(i + 1)]);
        }

        return sb.ToString();
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
        if (obj is AADurationValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is TimeSpan objValue) {
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
    public static implicit operator TimeSpan(AADurationValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AADurationValue obj)
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
    public override UInt32? AsUInt32() {
        var seconds = Value.Ticks / 10000000;
        if (seconds is >= UInt32.MinValue and <= UInt32.MaxValue) { return (UInt32)seconds; }
        return null;
    }

    /// <inheritdoc/>
    public override UInt64? AsUInt64() {
        var seconds = Value.Ticks / 10000000;
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
        var seconds = Value.Ticks / 10000000;
        if (seconds is >= Int32.MinValue and <= Int32.MaxValue) { return (Int32)seconds; }
        return null;
    }

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => Value.Ticks / 10000000;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => null;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => Value.Ticks / 10000000.0;

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
        => Value;

    /// <inheritdoc/>
    public override String? AsString()
        => ToString();

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => null;

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => null;

    /// <inheritdoc/>
    public override AAFieldCollection? AsFieldCollection()
        => null;


    /// <inheritdoc/>
    public override AAValue Clone()
        => new AADurationValue(Value);

    #endregion AAValue

}
