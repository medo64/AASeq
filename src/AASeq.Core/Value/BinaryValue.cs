namespace AASeq;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text;


/// <summary>
/// Binary value.
/// </summary>
public sealed class BinaryValue : AnyValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public BinaryValue(ReadOnlyMemory<Byte> value) {
        Helpers.ThrowIfArgumentNull(value, nameof(value), "Value cannot be null.");
        Value = value;
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
    public BinaryValue(Byte[] value) {
        Helpers.ThrowIfArgumentNull(value, nameof(value), "Value cannot be null.");
        var buffer = new byte[value.Length];
        Buffer.BlockCopy(value, 0, buffer, 0, buffer.Length);
        Value = new ReadOnlyMemory<byte>(buffer);
    }


    /// <summary>
    /// Gets value.
    /// </summary>
    public ReadOnlyMemory<Byte> Value { get; }


    #region Parse

    /// <summary>
    /// Returns value object converted from given text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <exception cref="FormatException">Cannot parse text.</exception>
    public static BinaryValue Parse(string text) {
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
    public static bool TryParse(string? text, [NotNullWhen(true)] out BinaryValue? result) {
        if (TryParseValue(text, out var value)) {
            result = new BinaryValue(value);
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


    #region Overrides

    /// <inheritdoc/>
    public override bool Equals(object? obj) {
        if (obj is BinaryValue otherValue) {
            return Value.Span.SequenceEqual(otherValue.Value.Span);
        } else if (obj is ReadOnlyMemory<Byte> objValue) {
            return Value.Span.SequenceEqual(objValue.Span);
        } else if (obj is byte[] objArray) {
            return Value.Span.SequenceEqual(objArray.AsSpan<Byte>());
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
    /// Convert object to binary.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static ReadOnlyMemory<Byte> ToReadOnlyMemory(BinaryValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value;
    }

    /// <summary>
    /// Implicit conversion into a ReadOnlyMemory structure.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator ReadOnlyMemory<Byte>(BinaryValue obj)
       => ToReadOnlyMemory(obj);


    /// <summary>
    /// Convert object to byte array.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static Byte[] ToByteArray(BinaryValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value.ToArray();
    }


    /// <summary>
    /// Implicit conversion into a Byte array.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator Byte[](BinaryValue obj)
       => ToByteArray(obj);


    /// <summary>
    /// Convert object to String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static string ToString(BinaryValue obj) {
        Helpers.ThrowIfArgumentNull(obj, nameof(obj), "Parameter cannot be null.");
        return obj.Value.ToString();
    }

    /// <summary>
    /// Implicit conversion into a String.
    /// </summary>
    /// <param name="obj">Value object.</param>
    public static implicit operator String(BinaryValue obj)
        => ToString(obj);

    #endregion Operations


    #region AnyValue

    /// <inheritdoc/>
    public override Boolean? AsBoolean()
        => null;

    /// <inheritdoc/>
    public override Byte? AsByte() {
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

    /// <inheritdoc/>
    public override UInt16? AsUInt16() {
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

    /// <inheritdoc/>
    public override UInt32? AsUInt32() {
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

    /// <inheritdoc/>
    public override UInt64? AsUInt64() {
        return Value.Length switch {
            1 => Value.Span[0],
            2 => BinaryPrimitives.ReadUInt16BigEndian(Value.Span),
            4 => BinaryPrimitives.ReadUInt32BigEndian(Value.Span),
            8 => BinaryPrimitives.ReadUInt64BigEndian(Value.Span),
            _ => null,
        };
    }


    /// <inheritdoc/>
    public override SByte? AsSByte() {
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

    /// <inheritdoc/>
    public override Int16? AsInt16() {
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

    /// <inheritdoc/>
    public override Int32? AsInt32() {
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

    /// <inheritdoc/>
    public override Int64? AsInt64() {
        return Value.Length switch {
            1 => (SByte)Value.Span[0],
            2 => BinaryPrimitives.ReadInt16BigEndian(Value.Span),
            4 => BinaryPrimitives.ReadInt32BigEndian(Value.Span),
            8 => BinaryPrimitives.ReadInt64BigEndian(Value.Span),
            _ => null,
        };
    }

    /// <inheritdoc/>
    public override Half? AsHalf() {
        return Value.Length switch {
            2 => BinaryPrimitives.ReadHalfBigEndian(Value.Span),
            4 => (Half)BinaryPrimitives.ReadSingleBigEndian(Value.Span),
            8 => (Half)BinaryPrimitives.ReadDoubleBigEndian(Value.Span),
            _ => null,
        };
    }

    /// <inheritdoc/>
    public override Single? AsSingle() {
        return Value.Length switch {
            2 => (Single)BinaryPrimitives.ReadHalfBigEndian(Value.Span),
            4 => BinaryPrimitives.ReadSingleBigEndian(Value.Span),
            8 => (Single)BinaryPrimitives.ReadDoubleBigEndian(Value.Span),
            _ => null,
        };
    }

    /// <inheritdoc/>
    public override Double? AsDouble() {
        return Value.Length switch {
            2 => (Double)BinaryPrimitives.ReadHalfBigEndian(Value.Span),
            4 => BinaryPrimitives.ReadSingleBigEndian(Value.Span),
            8 => BinaryPrimitives.ReadDoubleBigEndian(Value.Span),
            _ => null,
        };
    }

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

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    /// <inheritdoc/>
    public override String? AsString()
        => Utf8.GetString(Value.Span);

    /// <inheritdoc/>
    public override ReadOnlyMemory<Byte>? AsReadOnlyMemory() {
        return Value;
    }

    #endregion AnyValue

}
