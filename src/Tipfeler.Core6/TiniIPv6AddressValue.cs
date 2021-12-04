using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace Tipfeler;

/// <summary>
/// IPv6Address value.
/// </summary>
public sealed record TiniIPv6AddressValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Address family not supported.</exception>
    public TiniIPv6AddressValue(IPAddress value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        if (value.AddressFamily is not AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
        _value = value;
    }


    private IPAddress _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Address family not supported.</exception>
    public IPAddress Value {
        get => _value;
        set {
            if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
            if (value.AddressFamily is not AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
            _value = value;
            OnChanged();
        }
    }


    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniIPv6AddressValue(value);
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
    internal static bool TryParseValue(string? text, [NotNullWhen(true)] out IPAddress? result) {
        if (IPAddress.TryParse(text, out var address)) {
            if (address.AddressFamily is AddressFamily.InterNetworkV6) {
                result = address;
                return true;
            }
        }
        result = default;
        return false;
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => null;

    protected override SByte? ConvertToInt8()
        => null;

    protected override Int16? ConvertToInt16()
        => null;

    protected override Int32? ConvertToInt32()
        => null;

    protected override Int64? ConvertToInt64()
        => null;

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32()
        => null;

    protected override UInt64? ConvertToUInt64()
        => null;

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => null;

    protected override String? ConvertToString()
        => Value.ToString();

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => null;

    protected override IPAddress? ConvertToIPAddress()
        => Value;

    protected override IPAddress? ConvertToIPv4Address()
        => null;

    protected override IPAddress? ConvertToIPv6Address()
        => Value;

    #endregion Convert

}
