using System;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace AASeq;

/// <summary>
/// Base value class.
/// </summary>
[DebuggerDisplay("{AsString()}")]
public abstract class AAValue {

    #region As

    /// <summary>
    /// Returns value object if it's of a Boolean type or null otherwise.
    /// </summary>
    public AABooleanValue? AsBooleanObject()
        => this as AABooleanValue;

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Boolean? AsBoolean()
        => ConvertToBoolean();

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Boolean AsBoolean(Boolean defaultValue)
        => ConvertToBoolean() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Int8 type or null otherwise.
    /// </summary>
    public AAInt8Value? AsInt8Object()
        => this as AAInt8Value;

    /// <summary>
    /// Returns Int8 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public SByte? AsInt8()
        => ConvertToInt8();

    /// <summary>
    /// Returns Int8 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public SByte AsInt8(SByte defaultValue)
        => ConvertToInt8() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Int16 type or null otherwise.
    /// </summary>
    public AAInt16Value? AsInt16Object()
        => this as AAInt16Value;

    /// <summary>
    /// Returns Int16 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Int16? AsInt16()
        => ConvertToInt16();

    /// <summary>
    /// Returns Int16 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int16 AsInt16(Int16 defaultValue)
        => ConvertToInt16() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Int32 type or null otherwise.
    /// </summary>
    public AAInt32Value? AsInt32Object()
        => this as AAInt32Value;

    /// <summary>
    /// Returns Int32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Int32? AsInt32()
        => ConvertToInt32();

    /// <summary>
    /// Returns Int32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int32 AsInt32(Int32 defaultValue)
        => ConvertToInt32() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Int64 type or null otherwise.
    /// </summary>
    public AAInt64Value? AsInt64Object()
        => this as AAInt64Value;

    /// <summary>
    /// Returns Int64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Int64? AsInt64()
        => ConvertToInt64();

    /// <summary>
    /// Returns Int64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Int64 AsInt64(Int64 defaultValue)
        => ConvertToInt64() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a UInt8 type or null otherwise.
    /// </summary>
    public AAUInt8Value? AsUInt8Object()
        => this as AAUInt8Value;

    /// <summary>
    /// Returns UInt8 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Byte? AsUInt8()
        => ConvertToUInt8();

    /// <summary>
    /// Returns UInt8 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Byte AsUInt8(Byte defaultValue)
        => ConvertToUInt8() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a UInt16 type or null otherwise.
    /// </summary>
    public AAUInt16Value? AsUInt16Object()
        => this as AAUInt16Value;

    /// <summary>
    /// Returns UInt16 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public UInt16? AsUInt16()
        => ConvertToUInt16();

    /// <summary>
    /// Returns UInt16 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt16 AsUInt16(UInt16 defaultValue)
        => ConvertToUInt16() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a UInt32 type or null otherwise.
    /// </summary>
    public AAUInt32Value? AsUInt32Object()
        => this as AAUInt32Value;

    /// <summary>
    /// Returns UInt32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public UInt32? AsUInt32()
        => ConvertToUInt32();

    /// <summary>
    /// Returns UInt32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt32 AsUInt32(UInt32 defaultValue)
        => ConvertToUInt32() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a UInt64 type or null otherwise.
    /// </summary>
    public AAUInt64Value? AsUInt64Object()
        => this as AAUInt64Value;

    /// <summary>
    /// Returns UInt64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public UInt64? AsUInt64()
        => ConvertToUInt64();

    /// <summary>
    /// Returns UInt64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt64 AsUInt64(UInt64 defaultValue)
        => ConvertToUInt64() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Float32 type or null otherwise.
    /// </summary>
    public AAFloat32Value? AsFloat32Object()
        => this as AAFloat32Value;

    /// <summary>
    /// Returns Float32 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Single? AsFloat32()
        => ConvertToFloat32();

    /// <summary>
    /// Returns Float32 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Single AsFloat32(Single defaultValue)
        => ConvertToFloat32() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Float64 type or null otherwise.
    /// </summary>
    public AAFloat64Value? AsFloat64Object()
        => this as AAFloat64Value;

    /// <summary>
    /// Returns Float64 value of an object if conversion is possible or null otherwise.
    /// </summary>
    public Double? AsFloat64()
        => ConvertToFloat64();

    /// <summary>
    /// Returns Float64 value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Double AsFloat64(Double defaultValue)
        => ConvertToFloat64() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a String type or null otherwise.
    /// </summary>
    public AAStringValue? AsStringObject()
        => this as AAStringValue;

    /// <summary>
    /// Returns String value of an object if conversion is possible or null otherwise.
    /// </summary>
    public String? AsString()
        => ConvertToString();

    /// <summary>
    /// Returns String value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public String AsString(String defaultValue)
        => ConvertToString() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Binary type or null otherwise.
    /// </summary>
    public AABinaryValue? AsBinaryObject()
        => this as AABinaryValue;

    /// <summary>
    /// Returns Binary value of an object if conversion is possible or null otherwise.
    /// </summary>
    public ReadOnlyMemory<Byte>? AsBinary()
        => ConvertToBinary();

    /// <summary>
    /// Returns Size value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public ReadOnlyMemory<Byte> AsBinary(ReadOnlyMemory<Byte> defaultValue)
        => ConvertToBinary() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a DateTime type or null otherwise.
    /// </summary>
    public AADateTimeValue? AsDateTimeObject()
        => this as AADateTimeValue;

    /// <summary>
    /// Returns DateTime value of an object if conversion is possible or null otherwise.
    /// </summary>
    public DateTimeOffset? AsDateTime()
        => ConvertToDateTime();

    /// <summary>
    /// Returns DateTime value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public DateTimeOffset AsDateTime(DateTimeOffset defaultValue)
        => ConvertToDateTime() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Date type or null otherwise.
    /// </summary>
    public AADateValue? AsDateObject()
        => this as AADateValue;

    /// <summary>
    /// Returns Date value of an object if conversion is possible or null otherwise.
    /// </summary>
    public DateOnly? AsDate()
        => ConvertToDate();

    /// <summary>
    /// Returns Date value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public DateOnly AsDate(DateOnly defaultValue)
        => ConvertToDate() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Time type or null otherwise.
    /// </summary>
    public AATimeValue? AsTimeObject()
        => this as AATimeValue;

    /// <summary>
    /// Returns Time value of an object if conversion is possible or null otherwise.
    /// </summary>
    public TimeOnly? AsTime()
        => ConvertToTime();

    /// <summary>
    /// Returns Time value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public TimeOnly AsTime(TimeOnly defaultValue)
        => ConvertToTime() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Duration type or null otherwise.
    /// </summary>
    public AADurationValue? AsDurationObject()
        => this as AADurationValue;

    /// <summary>
    /// Returns Duration value of an object if conversion is possible or null otherwise.
    /// </summary>
    public TimeSpan? AsDuration()
        => ConvertToDuration();

    /// <summary>
    /// Returns Duration value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public TimeSpan AsDuration(TimeSpan defaultValue)
        => ConvertToDuration() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a IPAddress type or null otherwise.
    /// </summary>
    public AAIPAddressValue? AsIPAddressObject()
        => this as AAIPAddressValue;

    /// <summary>
    /// Returns IPAddress value of an object if conversion is possible or null otherwise.
    /// </summary>
    public IPAddress? AsIPAddress()
        => ConvertToIPAddress();

    /// <summary>
    /// Returns IPAddress value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public IPAddress AsIPAddress(IPAddress defaultValue)
        => ConvertToIPAddress() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a IPv4Address type or null otherwise.
    /// </summary>
    public AAIPv4AddressValue? AsIPv4AddressObject()
        => this as AAIPv4AddressValue;

    /// <summary>
    /// Returns IPv4Address value of an object if conversion is possible or null otherwise.
    /// </summary>
    public IPAddress? AsIPv4Address()
        => ConvertToIPv4Address();

    /// <summary>
    /// Returns IPv4Address value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public IPAddress AsIPv4Address(IPAddress defaultValue)
        => ConvertToIPv4Address() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a IPv6Address type or null otherwise.
    /// </summary>
    public AAIPv6AddressValue? AsIPv6AddressObject()
        => this as AAIPv6AddressValue;

    /// <summary>
    /// Returns IPv6Address value of an object if conversion is possible or null otherwise.
    /// </summary>
    public IPAddress? AsIPv6Address()
        => ConvertToIPv6Address();

    /// <summary>
    /// Returns IPv6Address value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public IPAddress AsIPv6Address(IPAddress defaultValue)
        => ConvertToIPv6Address() ?? defaultValue;


    /// <summary>
    /// Returns value object if it's of a Size type or null otherwise.
    /// </summary>
    public AASizeValue? AsSizeObject()
        => this as AASizeValue;

    /// <summary>
    /// Returns Size value of an object if conversion is possible or null otherwise.
    /// </summary>
    public UInt64? AsSize()
        => ConvertToSize();

    /// <summary>
    /// Returns Size value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public UInt64 AsSize(UInt64 defaultValue)
        => ConvertToSize() ?? defaultValue;


    /// <summary>
    /// Returns collection if conversion is possible or null otherwise.
    /// </summary>
    public AAFieldCollection? AsFieldCollection()
        => ConvertToFieldCollection();

    #endregion As


    #region Operators

    /// <summary>
    /// Implicit conversion into a Boolean value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Boolean value)
        => new AABooleanValue(value);

    /// <summary>
    /// Implicit conversion into a Int8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(SByte value)
        => new AAInt8Value(value);

    /// <summary>
    /// Implicit conversion into a Int16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Int16 value)
        => new AAInt16Value(value);

    /// <summary>
    /// Implicit conversion into a Int32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Int32 value)
        => new AAInt32Value(value);

    /// <summary>
    /// Implicit conversion into a Int64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Int64 value)
        => new AAInt64Value(value);

    /// <summary>
    /// Implicit conversion into a UInt8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Byte value)
        => new AAUInt8Value(value);

    /// <summary>
    /// Implicit conversion into a UInt16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(UInt16 value)
        => new AAUInt16Value(value);

    /// <summary>
    /// Implicit conversion into a UInt32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(UInt32 value)
        => new AAUInt32Value(value);

    /// <summary>
    /// Implicit conversion into a UInt64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(UInt64 value)
        => new AAUInt64Value(value);

    /// <summary>
    /// Implicit conversion into a Float32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Single value)
        => new AAFloat32Value(value);

    /// <summary>
    /// Implicit conversion into a Float64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Double value)
        => new AAFloat64Value(value);

    /// <summary>
    /// Implicit conversion into a String value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(String value)
        => new AAStringValue(value);

    /// <summary>
    /// Implicit conversion into a Binary value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(ReadOnlyMemory<Byte> value) {
        return new AABinaryValue(value);
    }

    /// <summary>
    /// Implicit conversion into a Binary value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(Byte[] value) {
        return new AABinaryValue(value);
    }

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(DateTimeOffset value)
        => new AADateTimeValue(value);

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(DateTime value)
        => new AADateTimeValue(value);

    /// <summary>
    /// Implicit conversion into a Date value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(DateOnly value)
        => new AADateValue(value);

    /// <summary>
    /// Implicit conversion into a Time value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(TimeOnly value)
        => new AATimeValue(value);

    /// <summary>
    /// Implicit conversion into a Duration value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(TimeSpan value)
        => new AADurationValue(value);

    /// <summary>
    /// Implicit conversion into a IPAddress value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AAValue(IPAddress value)
        => new AAIPAddressValue(value);

    #endregion Operators


    #region Convert

    /// <summary>
    /// Returns Boolean value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Boolean? ConvertToBoolean();

    /// <summary>
    /// Returns Int8 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract SByte? ConvertToInt8();

    /// <summary>
    /// Returns Int16 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int16? ConvertToInt16();

    /// <summary>
    /// Returns Int32 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int32? ConvertToInt32();

    /// <summary>
    /// Returns Int64 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int64? ConvertToInt64();

    /// <summary>
    /// Returns UInt8 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Byte? ConvertToUInt8();

    /// <summary>
    /// Returns UInt16 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt16? ConvertToUInt16();

    /// <summary>
    /// Returns UInt32 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt32? ConvertToUInt32();

    /// <summary>
    /// Returns UInt64 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt64? ConvertToUInt64();

    /// <summary>
    /// Returns Float32 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Single? ConvertToFloat32();

    /// <summary>
    /// Returns Float64 value if object can be converted or null otherwise.
    /// </summary>
    protected abstract Double? ConvertToFloat64();

    /// <summary>
    /// Returns Binary value if object can be converted or null otherwise.
    /// </summary>
    protected abstract ReadOnlyMemory<Byte>? ConvertToBinary();

    /// <summary>
    /// Returns String value if object can be converted or null otherwise.
    /// </summary>
    protected abstract String? ConvertToString();

    /// <summary>
    /// Returns DateTime value if object can be converted or null otherwise.
    /// </summary>
    protected abstract DateTimeOffset? ConvertToDateTime();

    /// <summary>
    /// Returns Date value if object can be converted or null otherwise.
    /// </summary>
    protected abstract DateOnly? ConvertToDate();

    /// <summary>
    /// Returns Time value if object can be converted or null otherwise.
    /// </summary>
    protected abstract TimeOnly? ConvertToTime();

    /// <summary>
    /// Returns Duration value if object can be converted or null otherwise.
    /// </summary>
    protected abstract TimeSpan? ConvertToDuration();

    /// <summary>
    /// Returns IPAddress value if object can be converted or null otherwise.
    /// </summary>
    protected abstract IPAddress? ConvertToIPAddress();

    /// <summary>
    /// Returns IPv4Address value if object can be converted or null otherwise.
    /// </summary>
    protected abstract IPAddress? ConvertToIPv4Address();

    /// <summary>
    /// Returns IPv6Address value if object can be converted or null otherwise.
    /// </summary>
    protected abstract IPAddress? ConvertToIPv6Address();

    /// <summary>
    /// Returns Size value if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt64? ConvertToSize();

    /// <summary>
    /// Returns collection if object can be converted or null otherwise.
    /// </summary>
    protected abstract AAFieldCollection? ConvertToFieldCollection();

    #endregion Convert

}
