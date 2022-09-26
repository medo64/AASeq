using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace AASeq;

/// <summary>
/// IPv4Address value.
/// </summary>
public sealed class AAIPv4AddressValue : AAValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Address family not supported.</exception>
    public AAIPv4AddressValue(IPAddress value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        if (value.AddressFamily is not AddressFamily.InterNetwork) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
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
            if (value.AddressFamily is not AddressFamily.InterNetwork) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
            _value = value;
        }
    }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static AAIPv4AddressValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out AAIPv4AddressValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new AAIPv4AddressValue(value);
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
            if (address.AddressFamily is AddressFamily.InterNetwork) {
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


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is AAIPv4AddressValue otherValue) {
            return Value.Equals(otherValue.Value);
        } else if (obj is IPAddress objValue) {
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
    /// Implicit conversion into an IPAddress.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator IPAddress(AAIPv4AddressValue obj)
        => obj.Value;

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(AAIPv4AddressValue obj)
        => obj.ToString();

    #endregion Operators


    #region Convert

    protected override bool? ConvertToBoolean()
        => null;

    protected override sbyte? ConvertToInt8()
        => null;

    protected override short? ConvertToInt16()
        => null;

    protected override int? ConvertToInt32()
        => null;

    protected override long? ConvertToInt64()
        => null;

    protected override byte? ConvertToUInt8()
        => null;

    protected override ushort? ConvertToUInt16()
        => null;

    protected override uint? ConvertToUInt32()
        => null;

    protected override ulong? ConvertToUInt64()
        => null;

    protected override float? ConvertToFloat32()
        => null;

    protected override double? ConvertToFloat64()
        => null;

    protected override string? ConvertToString()
        => ToString();

    protected override ReadOnlyMemory<byte>? ConvertToBinary()
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
        => Value;

    protected override IPAddress? ConvertToIPv6Address()
        => null;

    protected override ulong? ConvertToSize()
        => null;

    protected override AAFieldCollection? ConvertToFieldCollection()
        => null;

    #endregion Convert

}
