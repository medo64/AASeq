using System;
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
        => Value.AddressFamily is AddressFamily.InterNetwork ? Value : null;

    protected override IPAddress? ConvertToIPv6Address()
        => Value.AddressFamily is AddressFamily.InterNetworkV6 ? Value : null;

    #endregion Convert

}
