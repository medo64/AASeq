using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;

namespace AASeq;

/// <summary>
/// UInt64 value.
/// </summary>
public sealed class AASizeValue : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public AASizeValue(UInt64 value) {
        _value = value;
    }


    private UInt64 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public UInt64 Value {
        get => _value;
        set { _value = value; }
    }


    /// <summary>
    /// Gets value in Kilo units.
    /// </summary>
    public double ValueInKilo => Value / (double)ScaleFactorKilo;

    /// <summary>
    /// Gets value in Mega units.
    /// </summary>
    public double ValueInMega => Value / (double)ScaleFactorMega;

    /// <summary>
    /// Gets value in Giga units.
    /// </summary>
    public double ValueInGiga => Value / (double)ScaleFactorGiga;

    /// <summary>
    /// Gets value in Tera units.
    /// </summary>
    public double ValueInTera => Value / (double)ScaleFactorTera;

    /// <summary>
    /// Gets value in Peta units.
    /// </summary>
    public double ValueInPeta => Value / (double)ScaleFactorPeta;

    /// <summary>
    /// Gets value in Kibi units.
    /// </summary>
    public double ValueInKibi => Value / (double)ScaleFactorKibi;

    /// <summary>
    /// Gets value in Mebi units.
    /// </summary>
    public double ValueInMebi => Value / (double)ScaleFactorMebi;

    /// <summary>
    /// Gets value in Gibi units.
    /// </summary>
    public double ValueInGibi => Value / (double)ScaleFactorGibi;

    /// <summary>
    /// Gets value in Tebi units.
    /// </summary>
    public double ValueInTebi => Value / (double)ScaleFactorTebi;

    /// <summary>
    /// Gets value in Pebi units.
    /// </summary>
    public double ValueInPebi => Value / (double)ScaleFactorPebi;



    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AASizeValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AASizeValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AASizeValue(value);
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
                                case "K": decimalResult *= ScaleFactorKilo; break;
                                case "M": decimalResult *= ScaleFactorMega; break;
                                case "G": decimalResult *= ScaleFactorGiga; break;
                                case "T": decimalResult *= ScaleFactorTera; break;
                                case "P": decimalResult *= ScaleFactorPeta; break;
                                case "KI": decimalResult *= ScaleFactorKibi; break;
                                case "MI": decimalResult *= ScaleFactorMebi; break;
                                case "GI": decimalResult *= ScaleFactorGibi; break;
                                case "TI": decimalResult *= ScaleFactorTebi; break;
                                case "PI": decimalResult *= ScaleFactorPebi; break;
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


    /// <summary>
    /// Returns string representation of an object in Kilo units.
    /// </summary>
    public string ToKiloString() {
        return ValueInKilo.ToString(CultureInfo.InvariantCulture) + "k";
    }

    /// <summary>
    /// Returns string representation of an object in Kilo units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToKiloString(string? format) {
        return ValueInKilo.ToString(format, CultureInfo.InvariantCulture) + "k";
    }

    /// <summary>
    /// Returns string representation of an object in Mega units.
    /// </summary>
    /// <returns></returns>
    public string ToMegaString() {
        return ValueInMega.ToString(CultureInfo.InvariantCulture) + "M";
    }

    /// <summary>
    /// Returns string representation of an object in Mega units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToMegaString(string? format) {
        return ValueInMega.ToString(format, CultureInfo.InvariantCulture) + "M";
    }

    /// <summary>
    /// Returns string representation of an object in Giga units.
    /// </summary>
    public string ToGigaString() {
        return ValueInGiga.ToString(CultureInfo.InvariantCulture) + "G";
    }

    /// <summary>
    /// Returns string representation of an object in Giga units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToGigaString(string? format) {
        return ValueInGiga.ToString(format, CultureInfo.InvariantCulture) + "G";
    }

    /// <summary>
    /// Returns string representation of an object in Tera units.
    /// </summary>
    /// <returns></returns>
    public string ToTeraString() {
        return ValueInTera.ToString(CultureInfo.InvariantCulture) + "T";
    }

    /// <summary>
    /// Returns string representation of an object in Tera units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToTeraString(string? format) {
        return ValueInTera.ToString(format, CultureInfo.InvariantCulture) + "T";
    }

    /// <summary>
    /// Returns string representation of an object in Peta units.
    /// </summary>
    /// <returns></returns>
    public string ToPetaString() {
        return ValueInPeta.ToString(CultureInfo.InvariantCulture) + "P";
    }

    /// <summary>
    /// Returns string representation of an object in Peta units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToPetaString(string? format) {
        return ValueInPeta.ToString(format, CultureInfo.InvariantCulture) + "P";
    }


    /// <summary>
    /// Returns string representation of an object in Kibi units.
    /// </summary>
    public string ToKibiString() {
        return ValueInKibi.ToString(CultureInfo.InvariantCulture) + "Ki";
    }

    /// <summary>
    /// Returns string representation of an object in Kibi units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToKibiString(string? format) {
        return ValueInKibi.ToString(format, CultureInfo.InvariantCulture) + "Ki";
    }

    /// <summary>
    /// Returns string representation of an object in Mebi  units.
    /// </summary>
    public string ToMebiString() {
        return ValueInMebi.ToString(CultureInfo.InvariantCulture) + "Mi";
    }

    /// <summary>
    /// Returns string representation of an object in Mebi units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToMebiString(string? format) {
        return ValueInMebi.ToString(format, CultureInfo.InvariantCulture) + "Mi";
    }

    /// <summary>
    /// Returns string representation of an object in Gibi units.
    /// </summary>
    public string ToGibiString() {
        return ValueInGibi.ToString(CultureInfo.InvariantCulture) + "Gi";
    }

    /// <summary>
    /// Returns string representation of an object in Gibi units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToGibiString(string? format) {
        return ValueInGibi.ToString(format, CultureInfo.InvariantCulture) + "Gi";
    }

    /// <summary>
    /// Returns string representation of an object in Tebi units.
    /// </summary>
    public string ToTebiString() {
        return ValueInTebi.ToString(CultureInfo.InvariantCulture) + "Ti";
    }

    /// <summary>
    /// Returns string representation of an object in Tebi units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToTebiString(string? format) {
        return ValueInTebi.ToString(format, CultureInfo.InvariantCulture) + "Ti";
    }

    /// <summary>
    /// Returns string representation of an object in Pebi units.
    /// </summary>
    public string ToPebiString() {
        return ValueInPebi.ToString(CultureInfo.InvariantCulture) + "Pi";
    }

    /// <summary>
    /// Returns string representation of an object in Pebi units.
    /// </summary>
    /// <param name="format">Format for the object.</param>
    public string ToPebiString(string? format) {
        return ValueInPebi.ToString(format, CultureInfo.InvariantCulture) + "Pi";
    }


    /// <summary>
    /// Returns number scaled to a SI unit.
    /// </summary>
    public string ToScaledSIString() {
        return ToScaledSIString(3);
    }

    /// <summary>
    /// Returns number scaled to a SI unit and a defined number of significant digits.
    /// </summary>
    /// <param name="significantDigits">Number of significant digits.</param>
    /// <exception cref="ArgumentOutOfRangeException">Significant digits are to be between 1 and 9.</exception>
    public string ToScaledSIString(int significantDigits) {
        if (significantDigits is < 1 or > 9) { throw new ArgumentOutOfRangeException(nameof(significantDigits), "Significant digits are to be between 1 and 10."); }
        return ToScaledString(significantDigits, 1000, new string[] { "", "k", "M", "G", "T", "P" });
    }

    /// <summary>
    /// Returns number scaled to a binary unit.
    /// </summary>
    public string ToScaledBinaryString() {
        return ToScaledBinaryString(3);
    }

    /// <summary>
    /// Returns number scaled to a binary unit and a defined number of significant digits.
    /// </summary>
    /// <param name="significantDigits">Number of significant digits.</param>
    /// <exception cref="ArgumentOutOfRangeException">Significant digits are to be between 1 and 9.</exception>
    public string ToScaledBinaryString(int significantDigits) {
        if (significantDigits is < 1 or > 9) { throw new ArgumentOutOfRangeException(nameof(significantDigits), "Significant digits are to be between 1 and 10."); }
        return ToScaledString(significantDigits, 1024, new string[] { "", "Ki", "Mi", "Gi", "Ti", "Pi" });
    }

    private string ToScaledString(int significantDigits, int multiplier, string[] unitText) {
        var scaledDecimalMin = Math.Pow(10, significantDigits - 1);
        var scaledDecimalMax = Math.Pow(10, significantDigits);

        var initValue = (double)Value;

        var value = initValue;
        var factor = 0;
        for (var i = 1; i <= 5; i++) {
            if (value <= (ulong)multiplier) { break; }
            value /= multiplier;
            factor += 1;
        }

        var decimalDigits = 0;  // to figure where decimal point goes
        if (factor > 0) {  // only decimal point when going into kilo or higher
            while ((value < scaledDecimalMin) && (Math.Round(value, 1) < initValue)) {
                value *= 10;
                decimalDigits += 1;
            }
        }

        var decimalMultiplier = 0;  // to figure if there are more zeros
        while (value >= scaledDecimalMax) {
            value /= 10;
            decimalMultiplier += 1;
        }

        var wholeValue = Math.Round(value, 0, MidpointRounding.AwayFromZero);
        var wholeNumber = wholeValue.ToString(CultureInfo.InvariantCulture);
        if (decimalDigits == 0) {
            return wholeNumber + new string('0', decimalMultiplier) + unitText[factor];
        } else {
            return wholeNumber[0..(wholeNumber.Length - decimalDigits)] + "." + wholeNumber[^decimalDigits..] + unitText[factor];
        }
    }

    #endregion ToString


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AASizeValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is UInt64 objValue) {
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
    /// Implicit conversion into an unsigned long.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator ulong(AASizeValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AASizeValue obj)
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

    protected override ReadOnlyMemory<Byte>? ConvertToBinary() {
        var buffer = new byte[8];
        BinaryPrimitives.WriteUInt64BigEndian(buffer, Value);
        return buffer;
    }

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


    #region ScaleFactor

    private const UInt64 ScaleFactorKilo = 1000;
    private const UInt64 ScaleFactorMega = 1000000;
    private const UInt64 ScaleFactorGiga = 1000000000;
    private const UInt64 ScaleFactorTera = 1000000000000;
    private const UInt64 ScaleFactorPeta = 1000000000000000;
    private const UInt64 ScaleFactorKibi = 1024;
    private const UInt64 ScaleFactorMebi = 1048576;
    private const UInt64 ScaleFactorGibi = 1073741824;
    private const UInt64 ScaleFactorTebi = 1099511627776;
    private const UInt64 ScaleFactorPebi = 1125899906842624;

    #endregion ScaleFactor

}
