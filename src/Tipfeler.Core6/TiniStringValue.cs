using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace Tipfeler;

/// <summary>
/// String value.
/// </summary>
public sealed record TiniStringValue : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public TiniStringValue(String value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        _value = value;
    }


    private String _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public String Value {
        get => _value;
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean() {
        var trimmed = Value.Trim();
        if (trimmed.Equals("True", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("Yes", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("T", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("Y", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("+", StringComparison.InvariantCultureIgnoreCase)) {
            return true;
        } else if (trimmed.Equals("False", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("No", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("F", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("N", StringComparison.InvariantCultureIgnoreCase)
            || trimmed.Equals("-", StringComparison.InvariantCultureIgnoreCase)) {
            return false;
        } else if (Int32.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result != 0;
        }
        return null;
    }

    protected override SByte? ConvertToInt8() {
        if (SByte.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Int16? ConvertToInt16() {
        if (Int16.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Int32? ConvertToInt32() {
        if (Int32.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Int64? ConvertToInt64() {
        if (Int64.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Byte? ConvertToUInt8() {
        if (Byte.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override UInt16? ConvertToUInt16() {
        if (UInt16.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override UInt32? ConvertToUInt32() {
        if (UInt32.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override UInt64? ConvertToUInt64() {
        if (UInt64.TryParse(Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Single? ConvertToFloat32() {
        if (Single.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override Double? ConvertToFloat64() {
        if (Double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)) {
            return result;
        }
        return null;
    }

    protected override String? ConvertToString()
        => Value;

    protected override DateTimeOffset? ConvertToDateTime() {
        if (DateTime.TryParseExact(Value, ParseDateTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultDateTime)) {
            return resultDateTime;
        } else if (DateTime.TryParseExact(Value, ParseDateFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultDate)) {
            return resultDate;
        } else if (DateTime.TryParseExact(Value, ParseTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var resultTime)) {
            return resultTime;
        }
        return null;
    }

    protected override DateOnly? ConvertToDate() {
        if (DateTime.TryParseExact(Value, ParseDateFormats, CultureInfo.InvariantCulture, ParseStyle, out var result)) {
            return new DateOnly(result.Year, result.Month, result.Day);
        }
        return null;
    }

    protected override TimeOnly? ConvertToTime() {
        if (DateTime.TryParseExact(Value, ParseTimeFormats, CultureInfo.InvariantCulture, ParseStyle, out var result)) {
            return new TimeOnly(result.Hour, result.Minute, result.Second, result.Millisecond);
        }
        return null;
    }

    protected override IPAddress? ConvertToIPAddress() {
        if (IPAddress.TryParse(Value, out var address)) {
            if (address.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6) {
                return address;
            }
        }
        return null;
    }

    protected override IPAddress? ConvertToIPv4Address() {
        if (IPAddress.TryParse(Value, out var address)) {
            if (address.AddressFamily is AddressFamily.InterNetwork) {
                return address;
            }
        }
        return null;
    }

    protected override IPAddress? ConvertToIPv6Address() {
        if (IPAddress.TryParse(Value, out var address)) {
            if (address.AddressFamily is AddressFamily.InterNetworkV6) {
                return address;
            }
        }
        return null;
    }

    #endregion Convert

    #region Formats

    private static readonly DateTimeStyles ParseStyle = DateTimeStyles.AllowLeadingWhite
                                                      | DateTimeStyles.AllowInnerWhite
                                                      | DateTimeStyles.AllowTrailingWhite
                                                      | DateTimeStyles.AllowWhiteSpaces
                                                      | DateTimeStyles.AssumeUniversal
                                                      | DateTimeStyles.AdjustToUniversal;

    private static readonly string[] ParseDateTimeFormats = new string[] {
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF K",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF K",
        "yyyyMMdd'T'HHmmss.FFFFFFF K",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd HH:mm:ss.FFFFFFF",
        "yyyyMMdd'T'HHmmss.FFFFFFF",
        "yyyy-MM-dd'T'HH:mm K",
        "yyyy-MM-dd HH:mm K",
        "yyyyMMdd'T'HHmm K",
        "yyyy-MM-dd'T'HH:mm",
        "yyyy-MM-dd HH:mm",
        "yyyyMMdd'T'HHmm",
    };

    private static readonly string[] ParseDateFormats = new string[] {
        "yyyy-mm-dd",
    };

    private static readonly string[] ParseTimeFormats = new string[] {
        "HH:mm:ss.FFFFFFF",
        "HH:mm",
    };

    #endregion Formats

}
