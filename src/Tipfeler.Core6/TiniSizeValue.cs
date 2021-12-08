using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace Tipfeler;

/// <summary>
/// UInt64 value.
/// </summary>
public sealed record TiniSizeValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniSizeValue(UInt64 value) {
        _value = value;
    }


    private UInt64 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public UInt64 Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static TiniSizeValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniSizeValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniSizeValue(value);
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
    internal static bool TryParseValue(string? text, out UInt64 result) {
        var decimalResult = -1M;  // dummy value

        if (text != null) {
            int splitIndex = 0;
            foreach (var ch in text) {  // find first character
                if (char.IsLetter(ch)) { break; }
                splitIndex += 1;
            }
            if (splitIndex > 0) {  // we have the split
                if (decimal.TryParse(text[..splitIndex], NumberStyles.Float, CultureInfo.InvariantCulture, out decimalResult)) {
                    if (splitIndex < text.Length) {  // we may have unit
                        var unitText = text[splitIndex..].Trim();
                        if (unitText.Length > 0) {  // we have unit
                            switch (unitText.ToUpperInvariant()) {
                                case "K": decimalResult *= 1000; break;
                                case "M": decimalResult *= 1000000; break;
                                case "G": decimalResult *= 1000000000; break;
                                case "P": decimalResult *= 1000000000000; break;
                                case "KI": decimalResult *= 1024; break;
                                case "MI": decimalResult *= 1048576; break;
                                case "GI": decimalResult *= 1073741824; break;
                                case "PI": decimalResult *= 1099511627776; break;
                                default: decimalResult = -1; break;  // unknown unit
                            }
                        }
                    }
                }
            }
        }

        if (decimalResult < 0) {  // nobody changed it (or entered a negative number)
            result = default;
            return false;
        } else if (decimalResult == 0) {  // zero stays zero
            result = 0;
            return true;
        } else if (decimalResult <= 1) {  // round to 1 if 1 or less
            result = 1;
            return true;
        } else {  // round properly everything more than 1
            var rounded = Math.Round(decimalResult, MidpointRounding.AwayFromZero);
            if (rounded is >= UInt64.MinValue and <= UInt64.MaxValue) {
                result = (UInt64)rounded;
                return true;
            } else {  // to big for 64-bit integer
                result = default;
                return false;
            }
        }
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


    #region Operators

    /// <summary>
    /// Implicit conversion into an unsigned long.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator ulong(TiniSizeValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniSizeValue obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => Value != 0;

    protected override SByte? ConvertToInt8()
        => Value is <= (Byte)SByte.MaxValue ? (SByte)Value : null;

    protected override Int16? ConvertToInt16()
        => Value is <= (UInt16)Int16.MaxValue ? (Int16)Value : null;

    protected override Int32? ConvertToInt32()
        => Value is <= Int32.MaxValue ? (Int32)Value : null;

    protected override Int64? ConvertToInt64()
        => Value is <= Int64.MaxValue ? (Int64)Value : null;

    protected override Byte? ConvertToUInt8()
        => Value is <= Byte.MaxValue ? (Byte)Value : null;

    protected override UInt16? ConvertToUInt16()
        => Value is <= UInt16.MaxValue ? (UInt16)Value : null;

    protected override UInt32? ConvertToUInt32()
        => Value is <= UInt32.MaxValue ? (UInt32)Value : null;

    protected override UInt64? ConvertToUInt64()
        => Value;

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => null;

    protected override String? ConvertToString()
        => ToString();

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => null;

    protected override TimeSpan? ConvertToDuration()
        => null;

    protected override IPAddress? ConvertToIPAddress()
        => null;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    protected override UInt64? ConvertToSize()
        => ConvertToUInt64();

    #endregion Convert

}
