using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text;

namespace Tipfeler;

/// <summary>
/// Binary value.
/// </summary>
public sealed record TiniBinaryValue : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public TiniBinaryValue(ReadOnlyMemory<Byte> value) {
        _value = value;
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public TiniBinaryValue(Byte[] value) {
        var buffer = new byte[value.Length];
        Buffer.BlockCopy(value, 0, buffer, 0, buffer.Length);
        _value = new ReadOnlyMemory<byte>(buffer);
    }


    private ReadOnlyMemory<Byte> _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public ReadOnlyMemory<Byte> Value {
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
    public static TiniBinaryValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out TiniBinaryValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new TiniBinaryValue(value);
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
    internal static bool TryParseValue(string? text, [NotNullWhen(true)] out Byte[]? result) {
        if ((text != null) && (text.Length % 2 == 0)) {
            var startAt = text.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase) ? 2 : 0;
            var output = new Queue<byte>();
            for (var i = startAt; i < text.Length; i += 2) {
                if (!byte.TryParse(text.AsSpan(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var byteValue)) {  // give up if we cannot parse it
                    result = default;
                    return false;
                }
                output.Enqueue(byteValue);
            }
            result = output.ToArray();
            return true;
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
        var sb = new StringBuilder("0x");
        foreach (var b in Value.Span) {
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
        }
        return sb.ToString();
    }

    #endregion ToString


    #region Operators

    /// <summary>
    /// Implicit conversion into a ReadOnlyMemory structure.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator ReadOnlyMemory<Byte>(TiniBinaryValue obj)
       => obj.Value;

    /// <summary>
    /// Implicit conversion into a byte array.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Byte[](TiniBinaryValue obj)
       => obj.Value.ToArray();

    /// <summary>
    /// Implicit conversion into a string.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator string(TiniBinaryValue obj)
        => obj.ToString();

    #endregion Operations


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => null;

    protected override SByte? ConvertToInt8() {
        switch (Value.Length) {
            case 1: return (SByte)Value.Span[0];
            case 2: {
                    var value = BinaryPrimitives.ReadInt16BigEndian(Value.Span);
                    return value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)value : null;
                }
            case 4: {
                    var value = BinaryPrimitives.ReadInt32BigEndian(Value.Span);
                    return value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)value : null;
                }
            case 8: {
                    var value = BinaryPrimitives.ReadInt64BigEndian(Value.Span);
                    return value is >= SByte.MinValue and <= SByte.MaxValue ? (SByte)value : null;
                }
            default: return null;
        };
    }

    protected override Int16? ConvertToInt16() {
        switch (Value.Length) {
            case 1: return (SByte)Value.Span[0];
            case 2: return BinaryPrimitives.ReadInt16BigEndian(Value.Span);
            case 4: {
                    var value = BinaryPrimitives.ReadInt32BigEndian(Value.Span);
                    return value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)value : null;
                }
            case 8: {
                    var value = BinaryPrimitives.ReadInt64BigEndian(Value.Span);
                    return value is >= Int16.MinValue and <= Int16.MaxValue ? (Int16)value : null;
                }
            default: return null;
        };
    }

    protected override Int32? ConvertToInt32() {
        switch (Value.Length) {
            case 1: return (SByte)Value.Span[0];
            case 2: return BinaryPrimitives.ReadInt16BigEndian(Value.Span);
            case 4: return BinaryPrimitives.ReadInt32BigEndian(Value.Span);
            case 8: {
                    var value = BinaryPrimitives.ReadInt64BigEndian(Value.Span);
                    return value is >= Int32.MinValue and <= Int32.MaxValue ? (Int32)value : null;
                }
            default: return null;
        };
    }

    protected override Int64? ConvertToInt64() {
        return Value.Length switch {
            1 => (SByte)Value.Span[0],
            2 => BinaryPrimitives.ReadInt16BigEndian(Value.Span),
            4 => BinaryPrimitives.ReadInt32BigEndian(Value.Span),
            8 => BinaryPrimitives.ReadInt64BigEndian(Value.Span),
            _ => null,
        };
    }

    protected override Byte? ConvertToUInt8() {
        switch (Value.Length) {
            case 1: return Value.Span[0];
            case 2: {
                    var value = BinaryPrimitives.ReadUInt16BigEndian(Value.Span);
                    return value is <= Byte.MaxValue ? (Byte)value : null;
                }
            case 4: {
                    var value = BinaryPrimitives.ReadUInt32BigEndian(Value.Span);
                    return value is <= Byte.MaxValue ? (Byte)value : null;
                }
            case 8: {
                    var value = BinaryPrimitives.ReadUInt64BigEndian(Value.Span);
                    return value is <= Byte.MaxValue ? (Byte)value : null;
                }
            default: return null;
        };
    }

    protected override UInt16? ConvertToUInt16() {
        switch (Value.Length) {
            case 1: return Value.Span[0];
            case 2: return BinaryPrimitives.ReadUInt16BigEndian(Value.Span);
            case 4: {
                    var value = BinaryPrimitives.ReadUInt32BigEndian(Value.Span);
                    return value is <= UInt16.MaxValue ? (UInt16)value : null;
                }
            case 8: {
                    var value = BinaryPrimitives.ReadUInt64BigEndian(Value.Span);
                    return value is <= UInt16.MaxValue ? (UInt16)value : null;
                }
            default: return null;
        };
    }

    protected override UInt32? ConvertToUInt32() {
        switch (Value.Length) {
            case 1: return Value.Span[0];
            case 2: return BinaryPrimitives.ReadUInt16BigEndian(Value.Span);
            case 4: return BinaryPrimitives.ReadUInt32BigEndian(Value.Span);
            case 8: {
                    var value = BinaryPrimitives.ReadUInt64BigEndian(Value.Span);
                    return value is <= UInt32.MaxValue ? (UInt32)value : null;
                }
            default: return null;
        };
    }

    protected override UInt64? ConvertToUInt64() {
        return Value.Length switch {
            1 => Value.Span[0],
            2 => BinaryPrimitives.ReadUInt16BigEndian(Value.Span),
            4 => BinaryPrimitives.ReadUInt32BigEndian(Value.Span),
            8 => BinaryPrimitives.ReadUInt64BigEndian(Value.Span),
            _ => null,
        };
    }

    protected override Single? ConvertToFloat32() {
        return Value.Length switch {
            4 => BinaryPrimitives.ReadSingleBigEndian(Value.Span),
            8 => (float)BinaryPrimitives.ReadDoubleBigEndian(Value.Span),
            _ => null,
        };
    }

    protected override Double? ConvertToFloat64() {
        return Value.Length switch {
            4 => BinaryPrimitives.ReadSingleBigEndian(Value.Span),
            8 => BinaryPrimitives.ReadDoubleBigEndian(Value.Span),
            _ => null,
        };
    }

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    protected override String? ConvertToString()
        => Utf8.GetString(Value.Span);

    protected override ReadOnlyMemory<Byte>? ConvertToBinary() {
        return Value;
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
        => (Value.Length is 4 or 16) ? new IPAddress(Value.Span) : null;

    protected override IPAddress? ConvertToIPv4Address()
        => (Value.Length is 4) ? new IPAddress(Value.Span) : null;

    protected override IPAddress? ConvertToIPv6Address()
        => (Value.Length is 16) ? new IPAddress(Value.Span) : null;

    protected override UInt64? ConvertToSize()
        => ConvertToUInt64();

    #endregion Convert

}
