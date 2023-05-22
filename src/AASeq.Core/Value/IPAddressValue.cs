namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// IPAddress value.
/// </summary>
public sealed class IPAddressValue : AnyValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Address family not supported.</exception>
    public IPAddressValue(IPAddress value) {
        if (value == null) { throw new ArgumentNullException(nameof(value), "Value cannot be null."); }
        if (value.AddressFamily is not AddressFamily.InterNetwork and not AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException(nameof(value), "Address family not supported."); }
        Value = value;
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public IPAddress Value { get; }


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    internal static bool TryParse(string? text, [NotNullWhen(true)] out IPAddressValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new IPAddressValue(value);
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


    #region Overrides

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    public override bool Equals(object? obj) {
        if (obj is IPAddressValue otherValue) {
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
    /// Conversion into a IPAddress.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static IPAddress ToIPAddress(IPAddressValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into an IPAddress.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator IPAddress(IPAddressValue obj)
        => ToIPAddress(obj);

    /// <summary>
    /// Conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static String ToString(IPAddressValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(IPAddressValue obj)
        => ToString(obj);

    #endregion Operators


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => null;

    /// <inheritdoc/>
    public override Byte? AsByte()
        => null;

    /// <inheritdoc/>
    public override UInt16? AsUInt16()
        => null;

    /// <inheritdoc/>
    public override UInt32? AsUInt32()
        => null;

    /// <inheritdoc/>
    public override UInt64? AsUInt64()
        => null;

    /// <inheritdoc/>
    public override SByte? AsSByte()
        => null;

    /// <inheritdoc/>
    public override Int16? AsInt16()
        => null;

    /// <inheritdoc/>
    public override Int32? AsInt32()
        => null;

    /// <inheritdoc/>
    public override Int64? AsInt64()
        => null;

    /// <inheritdoc/>
    public override Half? AsHalf()
        => null;

    /// <inheritdoc/>
    public override Single? AsSingle()
        => null;

    /// <inheritdoc/>
    public override Double? AsDouble()
        => null;

    /// <inheritdoc/>
    public override DateTimeOffset? AsDateTimeOffset()
        => null;

    /// <inheritdoc/>
    public override DateOnly? AsDateOnly()
        => null;

    /// <inheritdoc/>
    public override TimeOnly? AsTimeOnly()
        => null;

    /// <inheritdoc/>
    public override TimeSpan? AsTimeSpan()
        => null;

    /// <inheritdoc/>
    public override String? AsString()
        => ToString();

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory()
        => Value.GetAddressBytes();

    /// <inheritdoc/>
    public override IPAddress? AsIPAddress()
        => Value;

    #endregion AnyValue

}
