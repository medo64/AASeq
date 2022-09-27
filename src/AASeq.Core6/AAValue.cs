using System;
using System.Diagnostics;
using System.Net;

namespace AASeq;

/// <summary>
/// Base value class.
/// </summary>
[DebuggerDisplay("{AsString()}")]
public abstract class AAValue {

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


    #region As

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract Boolean? AsBoolean();

    /// <summary>
    /// Returns Boolean value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public Boolean AsBoolean(Boolean defaultValue) => AsBoolean() ?? defaultValue;


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
    /// Returns IPAddress value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract IPAddress? AsIPAddress();

    /// <summary>
    /// Returns IPAddress value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public IPAddress AsIPAddress(IPAddress defaultValue) => AsIPAddress() ?? defaultValue;


    /// <summary>
    /// Returns Binary value of an object if conversion is possible or null otherwise.
    /// </summary>
    public abstract ReadOnlyMemory<Byte>? AsReadOnlyMemory();

    /// <summary>
    /// Returns Size value of an object if conversion is possible or default value otherwise.
    /// </summary>
    public ReadOnlyMemory<Byte> AsReadOnlyMemory(ReadOnlyMemory<Byte> defaultValue) => AsReadOnlyMemory() ?? defaultValue;


    /// <summary>
    /// Returns collection if conversion is possible or null otherwise.
    /// </summary>
    public abstract AAFieldCollection? AsFieldCollection();

    #endregion As


    #region Clone

    /// <summary>
    /// Returns a cloned copy of an object.
    /// </summary>
    public abstract AAValue Clone();

    #endregion Clone

}
