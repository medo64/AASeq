using System;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Null value.
/// </summary>
public sealed record TiniNullValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public TiniNullValue() { }


    #region Convert

    protected override Boolean? ConvertToBoolean() => null;
    protected override SByte? ConvertToInt8() => null;
    protected override Int16? ConvertToInt16() => null;
    protected override Int32? ConvertToInt32() => null;
    protected override Int64? ConvertToInt64() => null;
    protected override Byte? ConvertToUInt8() => null;
    protected override UInt16? ConvertToUInt16() => null;
    protected override UInt32? ConvertToUInt32() => null;
    protected override UInt64? ConvertToUInt64() => null;
    protected override String? ConvertToString() => null;
    protected override Single? ConvertToFloat32() => null;
    protected override Double? ConvertToFloat64() => null;
    protected override DateTimeOffset? ConvertToDateTime() => null;
    protected override DateOnly? ConvertToDate() => null;
    protected override TimeOnly? ConvertToTime() => null;
    protected override IPAddress? ConvertToIPAddress() => null;
    protected override IPAddress? ConvertToIPv4Address() => null;
    protected override IPAddress? ConvertToIPv6Address() => null;

    #endregion Convert

}
