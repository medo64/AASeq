namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

public sealed partial class AASeqValue {

    /// <summary>
    /// Returns true if value can be parsed as Boolean.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseBoolean(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if ("true".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "t".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "yes".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "y".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = true;
            return true;
        } else if ("false".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "f".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "no".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "n".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = false;
            return true;
        } else if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && UInt128.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex != 0;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && UInt128.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin != 0;
            return true;
        } else if (UInt128.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt != 0;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as SByte.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseSByte(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && SByte.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && SByte.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (SByte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as Byte.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseByte(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && Byte.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && Byte.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (Byte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Int16.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseInt16(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && Int16.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && Int16.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (Int16.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as UInt16.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseUInt16(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && UInt16.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && UInt16.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (UInt16.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Int32.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseInt32(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && Int32.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && Int32.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (Int32.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as UInt32.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseUInt32(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && UInt32.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && UInt32.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (UInt32.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Int64.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseInt64(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && Int64.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && Int64.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (Int64.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as UInt64.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseUInt64(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && UInt64.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && UInt64.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (UInt64.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Int128.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseInt128(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && Int128.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && Int128.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (Int128.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as UInt128.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseUInt128(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            && UInt128.TryParse(s[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var valueHex)) {
            result = valueHex;
            return true;
        } else if (s.StartsWith("0b", StringComparison.OrdinalIgnoreCase)
            && UInt128.TryParse(s[2..], NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var valueBin)) {
            result = valueBin;
            return true;
        } else if (UInt128.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var valueInt)) {
            result = valueInt;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Half.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseHalf(string s, [MaybeNullWhen(false)] out object? result) {
        if ("NaN".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Half.NaN;
            return true;
        } else if ("+Inf".Equals(s, StringComparison.OrdinalIgnoreCase) || "Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Half.PositiveInfinity;
            return true;
        } else if ("-Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Half.NegativeInfinity;
            return true;
        } else if (Half.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as Single.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseSingle(string s, [MaybeNullWhen(false)] out object? result) {
        if ("NaN".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Single.NaN;
            return true;
        } else if ("+Inf".Equals(s, StringComparison.OrdinalIgnoreCase) || "Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Single.PositiveInfinity;
            return true;
        } else if ("-Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Single.NegativeInfinity;
            return true;
        } else if (Single.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as Double.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseDouble(string s, [MaybeNullWhen(false)] out object? result) {
        if ("NaN".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Double.NaN;
            return true;
        } else if ("+Inf".Equals(s, StringComparison.OrdinalIgnoreCase) || "Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Double.PositiveInfinity;
            return true;
        } else if ("-Inf".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = Double.NegativeInfinity;
            return true;
        } else if (Double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as Decimal.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseDecimal(string s, [MaybeNullWhen(false)] out object? result) {
        if (Decimal.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as DateTimeOffset.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseDateTimeOffset(string s, [MaybeNullWhen(false)] out object? result) {
        if ("now".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "current".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "today".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = DateTimeOffset.Now;
            return true;
        } else if ("tomorrow".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = DateTimeOffset.Now.AddDays(1);
            return true;
        } else if ("yesterday".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = DateTimeOffset.Now.AddDays(-1);
            return true;
        } else if (DateTimeOffset.TryParseExact(s, DateTimeOffsetFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as DateOnly.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseDateOnly(string s, [MaybeNullWhen(false)] out object? result) {
        if ("now".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "current".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "today".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = new DateOnly(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day);
            return true;
        } else if ("tomorrow".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = new DateOnly(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day).AddDays(1);
            return true;
        } else if ("yesterday".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = new DateOnly(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day).AddDays(-1);
            return true;
        } else if (DateTimeOffset.TryParseExact(s, DateTimeOffsetFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var value)) {
            result = new DateOnly(value.Year, value.Month, value.Day);
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as TimeOnly.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseTimeOnly(string s, [MaybeNullWhen(false)] out object? result) {
        if ("now".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "current".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "today".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "tomorrow".Equals(s, StringComparison.OrdinalIgnoreCase)
            || "yesterday".Equals(s, StringComparison.OrdinalIgnoreCase)) {
            result = new TimeOnly(DateTimeOffset.Now.Ticks % (TimeOnly.MaxValue.Ticks + 1));
            return true;
        } else if (DateTimeOffset.TryParseExact(s, DateTimeOffsetFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var value)) {
            result = new TimeOnly(value.Ticks % (TimeOnly.MaxValue.Ticks + 1));
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Returns true if value can be parsed as TimeSpan.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseTimeSpan(string s, [MaybeNullWhen(false)] out object? result) {
        if (string.IsNullOrEmpty(s)) {
            result = null;
            return false;
        }

        if (s.StartsWith('-') && TimeSpan.TryParseExact(s[1..], TimeSpanFormats, CultureInfo.InvariantCulture, out var valueNeg)) {
            result = valueNeg.Negate();
            return true;
        } else if (TimeSpan.TryParseExact(s, TimeSpanFormats, CultureInfo.InvariantCulture, out var valuePos)) {
            result = valuePos;
            return true;
        }


        var sbValue = StringBuilderPool.Get();
        var sbUnit = StringBuilderPool.Get();
        try {
            var queue = new Queue<char>(s.Trim());

            var ticks = 0L;
            var multiplier = +1;  // positive by default
            if (queue.Peek() is '-') {
                multiplier = -1;
                queue.Dequeue();  // use up the char
            } else if (queue.Peek() is '+') {
                queue.Dequeue();  // use up the char
            }

            while (queue.Count > 0) {
                try {
                    while (queue.Count > 0) {
                        var ch = queue.Peek();
                        if (ch is >= '0' and <= '9' or '.') {  // number
                            sbValue.Append(ch);
                            queue.Dequeue();  // use up the char
                        } else if (char.IsWhiteSpace(ch)) {
                            queue.Dequeue();  // use up the char
                        } else {
                            break;  // no more numbers, go for unit
                        }
                    }

                    while (queue.Count > 0) {
                        var ch = queue.Peek();
                        if (ch is >= '0' and <= '9') {  // number
                            break;  // done with numbers
                        } else if (char.IsWhiteSpace(ch)) {
                            queue.Dequeue();  // use up the char
                        } else {
                            sbUnit.Append(ch);
                            queue.Dequeue();  // use up the char
                        }
                    }

                    if (decimal.TryParse(sbValue.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) {
                        var addedTicks = sbUnit.ToString().ToUpperInvariant() switch {
                            "D" => (long)(TimeSpan.TicksPerDay * number),
                            "H" => (long)(TimeSpan.TicksPerHour * number),
                            "M" => (long)(TimeSpan.TicksPerMinute * number),
                            "S" => (long)(TimeSpan.TicksPerSecond * number),
                            "MS" => (long)(TimeSpan.TicksPerMillisecond * number),
                            "US" => (long)(TimeSpan.TicksPerMicrosecond * number),
                            "NS" => (long)(number / 100),
                            "" => (long)(TimeSpan.TicksPerSecond * number),  // default to seconds
                            _ => default(long?),
                        };
                        if (addedTicks is not null) {
                            ticks += addedTicks.Value;
                        } else {
                            result = null;
                            return false;
                        }
                    } else {
                        result = null;
                        return false;
                    }
                } finally {
                    sbValue.Length = 0;
                    sbUnit.Length = 0;
                }
            }

            result = new TimeSpan(multiplier * ticks);
            return true;

        } finally {
            sbValue.Length = 0;
            StringBuilderPool.Return(sbValue);

            sbUnit.Length = 0;
            StringBuilderPool.Return(sbUnit);
        }
    }


    /// <summary>
    /// Returns true if value can be parsed as IPAddress.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseIPAddress(string s, [MaybeNullWhen(false)] out object? result) {
        if (IPAddress.TryParse(s, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Regex.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseRegex(string s, [MaybeNullWhen(false)] out object? result) {
        try {
            result = new Regex(s, RegexOptions.None);
            return true;
        } catch (ArgumentException) {
            result = null;
            return false;
        }
    }


    /// <summary>
    /// Returns true if value can be parsed as Uri.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseUri(string s, [MaybeNullWhen(false)] out object? result) {
        if (Uri.TryCreate(s, UriKind.Absolute, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Guid.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseGuid(string s, [MaybeNullWhen(false)] out object? result) {
        if (Guid.TryParse(s, out var value)) {
            result = value;
            return true;
        }

        result = null;
        return false;
    }


    /// <summary>
    /// Returns true if value can be parsed as Guid.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    internal static bool TryParseBase64ByteArray(string s, [MaybeNullWhen(false)] out object? result) {
        try {
            result = Convert.FromBase64String(s);
            return true;
        } catch (FormatException) {
            result = null;
            return false;
        }
    }


    /// <summary>
    /// Returns true if value can be parsed as Guid.
    /// </summary>
    /// <param name="s">s.</param>
    /// <param name="result">Result.</param>
    public static bool TryParseByteArray(string s, [MaybeNullWhen(false)] out object? result) {
        if (s is null) { result = false; return false; }

        var buffer = new MemoryStream();

        var secondHalf = false;
        byte b = 0;
        foreach (var ch in s) {
            if (ch is >= '0' and <= '9') {
                b |= (byte)(ch - '0');
            } else if (ch is >= 'A' and <= 'F') {
                b |= (byte)(ch - 'A' + 10);
            } else if (ch is >= 'a' and <= 'f') {
                b |= (byte)(ch - 'a' + 10);
            } else if (char.IsWhiteSpace(ch) || (ch is '-' or '_')) {
                continue;
            } else {
                result = null;
                return false;
            }

            if (secondHalf) {
                buffer.WriteByte(b);
                secondHalf = false;  // reset for the next byte
                b = 0;  // reset for the next byte
            } else {
                secondHalf = true;  // finish the byte in the next iteration
                b <<= 4;  // shift left 4 bits to preserve the value
            }
        }
        if (!secondHalf) {  // event number of hex digits
            result = buffer.ToArray();
            return true;
        } else {
            result = null;
            return false;
        }
    }


    #region Helpers

    private static readonly string[] DateTimeOffsetFormats = [
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF zzz",
        "yyyy-MM-dd' 'HH:mm:ss.FFFFFFF zzz",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFzzz",
        "yyyy-MM-dd' 'HH:mm:ss.FFFFFFFzzz",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd' 'HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd",
        "HH:mm:ss",
        "HH:mm:ss.FFFFFFF",
    ];

    private static readonly string[] TimeSpanFormats = [
        "m':'ss",
        "mm':'ss",
        "h':'mm':'ss",
        "hh':'mm':'ss",
        "d':'hh':'mm':'ss",
        "m':'ss'.'FFFFFFF",
        "mm':'ss'.'FFFFFFF",
        "h':'mm':'ss'.'FFFFFFF",
        "hh':'mm':'ss'.'FFFFFFF",
        "d':'hh':'mm':'ss'.'FFFFFFF",
    ];

    #endregion Helpers

}
