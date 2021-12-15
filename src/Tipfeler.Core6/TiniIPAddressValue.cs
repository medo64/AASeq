using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace Tipfeler;

/// <summary>
/// IPAddress value.
/// </summary>
public sealed record TiniIPAddressValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Address family not supported.</exception>
    public TiniIPAddressValue(IPAddress value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        if (value.AddressFamily is not AddressFamily.InterNetwork and not AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
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
            if (value.AddressFamily is not AddressFamily.InterNetwork and not AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
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
    public static TiniIPAddressValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniIPAddressValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniIPAddressValue(value);
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
            if (address.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6) {
                result = address;
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
        return Value.ToString();
    }

    #endregion ToString


    #region Operators

    /// <summary>
    /// Implicit conversion into an IPAddress.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator IPAddress(TiniIPAddressValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniIPAddressValue obj)
        => obj.ToString();

    #endregion Operators


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
        => ToString();

    protected override ReadOnlyMemory<Byte>? ConvertToBinary()
        => Value.GetAddressBytes();

    protected override DateTimeOffset? ConvertToDateTime()
        => null;

    protected override DateOnly? ConvertToDate()
        => null;

    protected override TimeOnly? ConvertToTime()
        => null;

    protected override TimeSpan? ConvertToDuration()
        => null;

    protected override IPAddress? ConvertToIPAddress()
        => Value;

    protected override IPAddress? ConvertToIPv4Address()
        => Value.AddressFamily is AddressFamily.InterNetwork ? Value : null;

    protected override IPAddress? ConvertToIPv6Address()
        => Value.AddressFamily is AddressFamily.InterNetworkV6 ? Value : null;

    protected override UInt64? ConvertToSize()
        => null;

    #endregion Convert

}
