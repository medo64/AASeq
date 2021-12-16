using System;
using System.Diagnostics;
using System.Net;

namespace Tipfeler;

/// <summary>
/// Base value class.
/// </summary>
[DebuggerDisplay("{AsString()}")]
public abstract class TiniValue {

    #region As

    /// <summary>
    /// Returns value object if it's of a Boolean type or null otherwise.
    /// </summary>
    public TiniValueBoolean? AsBooleanObject()
        => this as TiniValueBoolean;

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
    public TiniValueInt8? AsInt8Object()
        => this as TiniValueInt8;

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
    public TiniValueInt16? AsInt16Object()
        => this as TiniValueInt16;

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
    public TiniValueInt32? AsInt32Object()
        => this as TiniValueInt32;

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
    public TiniValueInt64? AsInt64Object()
        => this as TiniValueInt64;

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
    public TiniValueUInt8? AsUInt8Object()
        => this as TiniValueUInt8;

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
    public TiniValueUInt16? AsUInt16Object()
        => this as TiniValueUInt16;

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
    public TiniValueUInt32? AsUInt32Object()
        => this as TiniValueUInt32;

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
    public TiniValueUInt64? AsUInt64Object()
        => this as TiniValueUInt64;

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
    public TiniValueFloat32? AsFloat32Object()
        => this as TiniValueFloat32;

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
    public TiniValueFloat64? AsFloat64Object()
        => this as TiniValueFloat64;

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
    public TiniValueString? AsStringObject()
        => this as TiniValueString;

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
    public TiniValueBinary? AsBinaryObject()
        => this as TiniValueBinary;

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
    public TiniValueDateTime? AsDateTimeObject()
        => this as TiniValueDateTime;

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
    public TiniValueDate? AsDateObject()
        => this as TiniValueDate;

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
    public TiniValueTime? AsTimeObject()
        => this as TiniValueTime;

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
    public TiniValueDuration? AsDurationObject()
        => this as TiniValueDuration;

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
    public TiniValueIPAddress? AsIPAddressObject()
        => this as TiniValueIPAddress;

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
    public TiniValueIPv4Address? AsIPv4AddressObject()
        => this as TiniValueIPv4Address;

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
    public TiniValueIPv6Address? AsIPv6AddressObject()
        => this as TiniValueIPv6Address;

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
    public TiniValueSize? AsSizeObject()
        => this as TiniValueSize;

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

    #endregion As


    #region Operators

    /// <summary>
    /// Implicit conversion into a Boolean value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Boolean value)
        => new TiniValueBoolean(value);

    /// <summary>
    /// Implicit conversion into a Int8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(SByte value)
        => new TiniValueInt8(value);

    /// <summary>
    /// Implicit conversion into a Int16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int16 value)
        => new TiniValueInt16(value);

    /// <summary>
    /// Implicit conversion into a Int32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int32 value)
        => new TiniValueInt32(value);

    /// <summary>
    /// Implicit conversion into a Int64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int64 value)
        => new TiniValueInt64(value);

    /// <summary>
    /// Implicit conversion into a UInt8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Byte value)
        => new TiniValueUInt8(value);

    /// <summary>
    /// Implicit conversion into a UInt16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt16 value)
        => new TiniValueUInt16(value);

    /// <summary>
    /// Implicit conversion into a UInt32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt32 value)
        => new TiniValueUInt32(value);

    /// <summary>
    /// Implicit conversion into a UInt64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt64 value)
        => new TiniValueUInt64(value);

    /// <summary>
    /// Implicit conversion into a Float32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Single value)
        => new TiniValueFloat32(value);

    /// <summary>
    /// Implicit conversion into a Float64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Double value)
        => new TiniValueFloat64(value);

    /// <summary>
    /// Implicit conversion into a String value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(String value)
        => new TiniValueString(value);

    /// <summary>
    /// Implicit conversion into a Binary value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(ReadOnlyMemory<Byte> value) {
        return new TiniValueBinary(value);
    }

    /// <summary>
    /// Implicit conversion into a Binary value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Byte[] value) {
        return new TiniValueBinary(value);
    }

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(DateTimeOffset value)
        => new TiniValueDateTime(value);

    /// <summary>
    /// Implicit conversion into a DateTime value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(DateTime value)
        => new TiniValueDateTime(value);

    /// <summary>
    /// Implicit conversion into a Date value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(DateOnly value)
        => new TiniValueDate(value);

    /// <summary>
    /// Implicit conversion into a Time value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(TimeOnly value)
        => new TiniValueTime(value);

    /// <summary>
    /// Implicit conversion into a Duration value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(TimeSpan value)
        => new TiniValueDuration(value);

    /// <summary>
    /// Implicit conversion into a IPAddress value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(IPAddress value)
        => new TiniValueIPAddress(value);

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

    #endregion Convert


    #region Events

    /// <summary>
    /// Call when value is changed.
    /// </summary>
    protected void OnChanged() {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Event raised when value is changed.
    /// </summary>
    public event EventHandler<EventArgs>? Changed;

    #endregion

}
