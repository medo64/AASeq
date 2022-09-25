using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AASeq;

/// <summary>
/// Null value.
/// </summary>
public sealed class AANullValue : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public AANullValue() { }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
#pragma warning disable IDE0060 // Remove unused parameter
    public static AANullValue Parse(string text) {
#pragma warning restore IDE0060 // Remove unused parameter
        return new AANullValue();
    }

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
#pragma warning disable IDE0060 // Remove unused parameter
    public static bool TryParse(string? text, [NotNullWhen(true)] out AANullValue? result) {
#pragma warning restore IDE0060 // Remove unused parameter
        result = new AANullValue();
        return true;
    }

    #endregion Parse


    #region ToString

    /// <summary>
    /// Returns string representation of an object.
    /// </summary>
    public override string ToString() {
        return String.Empty;
    }

    #endregion ToString


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AANullValue) {
            return true;
        } else if (obj is null) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Returns a hash code for the current object.
    /// </summary>
    public override int GetHashCode() {
        return string.Empty.GetHashCode();  // the same hash code for all
    }

    #endregion


    #region Operators

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AANullValue obj)
        => obj.ToString();

    #endregion Operators


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
    protected override ReadOnlyMemory<Byte>? ConvertToBinary() => null;
    protected override Single? ConvertToFloat32() => null;
    protected override Double? ConvertToFloat64() => null;
    protected override DateTimeOffset? ConvertToDateTime() => null;
    protected override DateOnly? ConvertToDate() => null;
    protected override TimeOnly? ConvertToTime() => null;
    protected override TimeSpan? ConvertToDuration() => null;
    protected override IPAddress? ConvertToIPAddress() => null;
    protected override IPAddress? ConvertToIPv4Address() => null;
    protected override IPAddress? ConvertToIPv6Address() => null;
    protected override UInt64? ConvertToSize() => null;

    #endregion Convert

}
