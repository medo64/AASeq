namespace AASeq;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;

/// <summary>
/// Base value class.
/// </summary>
[DebuggerDisplay("{AsString(),nq}")]
public abstract class AnyValue {

    #region Operators

    /// <summary>
    /// Conversion into a BooleanValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static BooleanValue FromBoolean(Boolean value) {
        return new BooleanValue(value);
    }

    /// <summary>
    /// Implicit conversion into a BooleanValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Boolean value)
        => FromBoolean(value);


    /// <summary>
    /// Conversion into a Int8Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Int8Value FromSByte(SByte value) {
        return new Int8Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Int16Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(SByte value)
        => FromSByte(value);


    /// <summary>
    /// Conversion into a Int16Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Int16Value FromInt16(Int16 value) {
        return new Int16Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Int16Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Int16 value)
        => FromInt16(value);


    /// <summary>
    /// Conversion into a Int32Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Int32Value FromInt32(Int32 value) {
        return new Int32Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Int32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Int32 value)
        => FromInt32(value);


    /// <summary>
    /// Conversion into a Int64Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Int64Value FromInt64(Int64 value) {
        return new Int64Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Int64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Int64 value)
        => FromInt64(value);


    /// <summary>
    /// Conversion into a UInt8Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static UInt8Value FromByte(Byte value) {
        return new UInt8Value(value);
    }

    /// <summary>
    /// Implicit conversion into a UInt8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Byte value)
        => FromByte(value);


    /// <summary>
    /// Conversion into a UInt16Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static UInt16Value FromUInt16(UInt16 value) {
        return new UInt16Value(value);
    }

    /// <summary>
    /// Implicit conversion into a UInt16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(UInt16 value)
        => FromUInt16(value);


    /// <summary>
    /// Conversion into a UInt32Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static UInt32Value FromUInt32(UInt32 value) {
        return new UInt32Value(value);
    }

    /// <summary>
    /// Implicit conversion into a UInt32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(UInt32 value)
        => FromUInt32(value);


    /// <summary>
    /// Conversion into a UInt64Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static UInt64Value FromUInt64(UInt64 value) {
        return new UInt64Value(value);
    }

    /// <summary>
    /// Implicit conversion into a UInt64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(UInt64 value)
        => FromUInt64(value);


    /// <summary>
    /// Conversion into a Float16Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Float16Value FromHalf(Half value) {
        return new Float16Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Float16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Half value)
        => FromHalf(value);


    /// <summary>
    /// Conversion into a Float32Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Float32Value FromSingle(Single value) {
        return new Float32Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Float32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Single value)
        => FromSingle(value);


    /// <summary>
    /// Conversion into a Float64Value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static Float64Value FromDouble(Double value) {
        return new Float64Value(value);
    }

    /// <summary>
    /// Implicit conversion into a Float64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Double value)
        => FromDouble(value);


    /// <summary>
    /// Conversion into a DateTimeValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static DateTimeValue FromDateTimeOffset(DateTimeOffset value) {
        return new DateTimeValue(value);
    }

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(DateTimeOffset value)
        => FromDateTimeOffset(value);

    /// <summary>
    /// Conversion into a DateTimeValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static DateTimeValue FromDateTime(DateTime value) {
        return new DateTimeValue(value);
    }

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(DateTime value)
        => FromDateTime(value);


    /// <summary>
    /// Conversion into a DateValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static DateValue FromDateOnly(DateOnly value) {
        return new DateValue(value);
    }

    /// <summary>
    /// Implicit conversion into a Date value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(DateOnly value)
        => FromDateOnly(value);


    /// <summary>
    /// Conversion into a TimeValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static TimeValue FromTimeOnly(TimeOnly value) {
        return new TimeValue(value);
    }

    /// <summary>
    /// Implicit conversion into a Time value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(TimeOnly value)
        => FromTimeOnly(value);


    /// <summary>
    /// Conversion into a DurationValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static DurationValue FromTimeSpan(TimeSpan value) {
        return new DurationValue(value);
    }

    /// <summary>
    /// Implicit conversion into a Duration value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(TimeSpan value)
        => FromTimeSpan(value);


    /// <summary>
    /// Conversion into a StringValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static StringValue FromString(String value) {
        return new StringValue(value);
    }

    /// <summary>
    /// Implicit conversion into a String value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(String value)
        => FromString(value);


    /// <summary>
    /// Conversion into a BinaryValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static BinaryValue FromReadOnlyMemory(ReadOnlyMemory<Byte> value) {
        return new BinaryValue(value);
    }

    /// <summary>
    /// Implicit conversion into a BinaryValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(ReadOnlyMemory<Byte> value) {
        return FromReadOnlyMemory(value);
    }


    /// <summary>
    /// Conversion into a BinaryValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static BinaryValue FromByteArray(Byte[] value) {
        return new BinaryValue(value);
    }

    /// <summary>
    /// Implicit conversion into a BinaryValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(Byte[] value) {
        return FromByteArray(value);
    }


    /// <summary>
    /// Conversion into a IPAddressValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static IPAddressValue FromIPAddress(IPAddress value) {
        return new IPAddressValue(value);
    }

    /// <summary>
    /// Implicit conversion into a IPAddressValue object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AnyValue(IPAddress value) {
        return FromIPAddress(value);
    }

    #endregion Operators


    #region As

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract bool? AsBoolean();

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public bool AsBoolean(bool defaultValue) => AsBoolean() ?? defaultValue;


    /// <summary>
    /// Returns Int8 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract SByte? AsSByte();

    /// <summary>
    /// Returns Int8 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public SByte AsSByte(SByte defaultValue) => AsSByte() ?? defaultValue;


    /// <summary>
    /// Returns Int16 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Int16? AsInt16();

    /// <summary>
    /// Returns Int16 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int16 AsInt16(Int16 defaultValue) => AsInt16() ?? defaultValue;


    /// <summary>
    /// Returns Int32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Int32? AsInt32();

    /// <summary>
    /// Returns Int32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int32 AsInt32(Int32 defaultValue) => AsInt32() ?? defaultValue;


    /// <summary>
    /// Returns Int64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Int64? AsInt64();

    /// <summary>
    /// Returns Int64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int64 AsInt64(Int64 defaultValue) => AsInt64() ?? defaultValue;


    /// <summary>
    /// Returns UInt8 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Byte? AsByte();

    /// <summary>
    /// Returns UInt8 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Byte AsByte(Byte defaultValue) => AsByte() ?? defaultValue;


    /// <summary>
    /// Returns UInt16 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract UInt16? AsUInt16();

    /// <summary>
    /// Returns UInt16 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt16 AsUInt16(UInt16 defaultValue) => AsUInt16() ?? defaultValue;


    /// <summary>
    /// Returns UInt32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract UInt32? AsUInt32();

    /// <summary>
    /// Returns UInt32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt32 AsUInt32(UInt32 defaultValue) => AsUInt32() ?? defaultValue;


    /// <summary>
    /// Returns UInt64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract UInt64? AsUInt64();

    /// <summary>
    /// Returns UInt64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt64 AsUInt64(UInt64 defaultValue) => AsUInt64() ?? defaultValue;


    /// <summary>
    /// Returns Float16 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Half? AsHalf();

    /// <summary>
    /// Returns Float16 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Half AsHalf(Half defaultValue) => AsHalf() ?? defaultValue;


    /// <summary>
    /// Returns Float32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Single? AsSingle();

    /// <summary>
    /// Returns Float32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Single AsSingle(Single defaultValue) => AsSingle() ?? defaultValue;


    /// <summary>
    /// Returns Float64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Double? AsDouble();

    /// <summary>
    /// Returns Float64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Double AsDouble(Double defaultValue) => AsDouble() ?? defaultValue;


    /// <summary>
    /// Returns DateTime value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract DateTimeOffset? AsDateTimeOffset();

    /// <summary>
    /// Returns DateTime value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public DateTimeOffset AsDateTimeOffset(DateTimeOffset defaultValue) => AsDateTimeOffset() ?? defaultValue;


    /// <summary>
    /// Returns Date value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract DateOnly? AsDateOnly();

    /// <summary>
    /// Returns Date value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public DateOnly AsDateOnly(DateOnly defaultValue) => AsDateOnly() ?? defaultValue;


    /// <summary>
    /// Returns Time value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract TimeOnly? AsTimeOnly();

    /// <summary>
    /// Returns Time value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public TimeOnly AsTimeOnly(TimeOnly defaultValue) => AsTimeOnly() ?? defaultValue;


    /// <summary>
    /// Returns Duration value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract TimeSpan? AsTimeSpan();

    /// <summary>
    /// Returns Duration value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public TimeSpan AsTimeSpan(TimeSpan defaultValue) => AsTimeSpan() ?? defaultValue;


    /// <summary>
    /// Returns String value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract String? AsString();

    /// <summary>
    /// Returns String value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public String AsString(String defaultValue) => AsString() ?? defaultValue;


    /// <summary>
    /// Returns ReadOnlyMemory value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract ReadOnlyMemory<Byte>? AsReadOnlyMemory();

    /// <summary>
    /// Returns ReadOnlyMemory value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public ReadOnlyMemory<Byte> AsReadOnlyMemory(ReadOnlyMemory<Byte> defaultValue) => AsReadOnlyMemory() ?? defaultValue;


    /// <summary>
    /// Returns IPAddress value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract IPAddress? AsIPAddress();

    /// <summary>
    /// Returns IPAddress value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public IPAddress AsIPAddress(IPAddress defaultValue) => AsIPAddress() ?? defaultValue;

    #endregion As


    #region Parse

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="result">Conversion result.</param>
    public static bool TryParse(string? text, [NotNullWhen(true)] out AnyValue? result) {
        return TryParse(text, tags: null, out result);
    }

    /// <summary>
    /// Returns true if text can be converted with the value object in the output parameter.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="tags">Tags to help with conversion.</param>
    /// <param name="result">Conversion result.</param>
    /// <exception cref="">Conflicting conversion tags present.</exception>
    public static bool TryParse(string? text, TagCollection? tags, [NotNullWhen(true)] out AnyValue? result) {
        var type = GetConversionType(tags, out var forceBase64);
        if (text is null) {
            result = null;
            return false;
        }

        switch (type) {
            case ConversionType.Boolean: {
                    var success = BooleanValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Int8: {
                    var success = Int8Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Int16: {
                    var success = Int16Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Int32: {
                    var success = Int32Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Int64: {
                    var success = Int64Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.UInt8: {
                    var success = UInt8Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.UInt16: {
                    var success = UInt16Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.UInt32: {
                    var success = UInt32Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.UInt64: {
                    var success = UInt64Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Float16: {
                    var success = Float16Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Float32: {
                    var success = Float32Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Float64: {
                    var success = Float64Value.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.DateTime: {
                    var success = DateTimeValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Date: {
                    var success = DateValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Time: {
                    var success = TimeValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Duration: {
                    var success = DurationValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.String: {
                    var success = StringValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.Binary: {
                    if (forceBase64) {
                        try {
                            var bytes = Convert.FromBase64String(text);
                            result = new BinaryValue(bytes);
                            return true;
                        } catch (FormatException) {
                            result = null;
                            return false;
                        }
                    } else {
                        var success = BinaryValue.TryParse(text, out var resultValue);
                        result = resultValue;
                        return success;
                    }
                }
            case ConversionType.IPAddress: {
                    var success = IPAddressValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.IPv4Address: {
                    var success = IPv4AddressValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
            case ConversionType.IPv6Address: {
                    var success = IPv6AddressValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }

            default: // auto-detect
                if (text.StartsWith("0x", StringComparison.Ordinal)) {  // binary
                    var success = BinaryValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                } else if ((text.Length > 0) && (text[0] is >= '0' and <= '9')) {
                    if (text.Contains('.', StringComparison.Ordinal)) {  // float
                        var success = Float64Value.TryParse(text, out var resultValue);
                        result = resultValue;
                        return success;
                    } else {  // int
                        var success = Int64Value.TryParse(text, out var resultValue);
                        result = resultValue;
                        return success;
                    }
                } else {  // string
                    var success = StringValue.TryParse(text, out var resultValue);
                    result = resultValue;
                    return success;
                }
        }
    }

    private enum ConversionType {
        Auto = 0,
        Boolean,
        Int8,
        Int16,
        Int32,
        Int64,
        UInt8,
        UInt16,
        UInt32,
        UInt64,
        Float16,
        Float32,
        Float64,
        DateTime,
        Date,
        Time,
        Duration,
        String,
        Binary,
        IPAddress,
        IPv4Address,
        IPv6Address,
    }

    private static ConversionType GetConversionType(TagCollection? tags, out bool forceBase64) {
        if (tags == null) {
            forceBase64 = false;
            return ConversionType.Auto;
        }

        var type = ConversionType.Auto;
        forceBase64 = false;

        foreach (var tag in tags.EnumerateSystemTags()) {
            if (tag.State) {  // only consider enabled tags
                if (Tag.NameComparer.Equals(tag.Name, "bool") || Tag.NameComparer.Equals(tag.Name, "boolean")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Boolean, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "int8")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Int8, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "int16")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Int16, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "int32")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Int32, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "int64") || Tag.NameComparer.Equals(tag.Name, "int")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Int64, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "uint8")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.UInt8, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "uint16")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.UInt16, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "uint32")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.UInt32, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "uint64") || Tag.NameComparer.Equals(tag.Name, "uint")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.UInt64, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "float16")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Float16, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "float32")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Float32, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "float64") || Tag.NameComparer.Equals(tag.Name, "float")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Float64, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "datetime")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.DateTime, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "date")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Date, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "time")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Time, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "duration")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Duration, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "string")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.String, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "binary")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.Binary, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "ip")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.IPAddress, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "ipv4")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.IPv4Address, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "ipv6")) {
                    type = ThrowIfAlreadyPresent(type, ConversionType.IPv6Address, nameof(tags));
                } else if (Tag.NameComparer.Equals(tag.Name, "base64")) {
                    forceBase64 = true;
                }
            }
        }

        if (type == ConversionType.Auto) {
            if (forceBase64) { return ConversionType.Binary; }
        } else if (type != ConversionType.Binary) {
            if (forceBase64) { throw new ArgumentOutOfRangeException(nameof(tags), $"Conflicting conversion tags present (both '{type}' and 'base64' specified)."); }
        }

        return type;
    }

    [StackTraceHidden]
    private static ConversionType ThrowIfAlreadyPresent(ConversionType currentType, ConversionType newType, string argumentName) {
        if (currentType != ConversionType.Auto) {
            throw new ArgumentOutOfRangeException(argumentName, $"Conflicting conversion tags present (both '{currentType}' and '{newType}' specified).");
        }
        return newType;
    }

    #endregion Parse

}
