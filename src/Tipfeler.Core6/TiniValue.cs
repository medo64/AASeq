using System;
using System.Diagnostics;

namespace Tipfeler;

/// <summary>
/// Base value class.
/// </summary>
[DebuggerDisplay("{AsString()}")]
public abstract record TiniValue {

    /// <summary>
    /// Returns value object if it's of a Boolean type or null otherwise.
    /// </summary>
    public TiniBooleanValue? AsBooleanValue()
        => this as TiniBooleanValue;

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
    public TiniInt8Value? AsInt8Value()
        => this as TiniInt8Value;

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
    public TiniInt16Value? AsInt16Value()
        => this as TiniInt16Value;

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
    public TiniInt32Value? AsInt32Value()
        => this as TiniInt32Value;

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
    public TiniInt64Value? AsInt64Value()
        => this as TiniInt64Value;

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
    public TiniUInt8Value? AsUInt8Value()
        => this as TiniUInt8Value;

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
    public TiniUInt16Value? AsUInt16Value()
        => this as TiniUInt16Value;

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
    public TiniUInt32Value? AsUInt32Value()
        => this as TiniUInt32Value;

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
    public TiniUInt64Value? AsUInt64Value()
        => this as TiniUInt64Value;

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
    public TiniFloat32Value? AsFloat32Value()
        => this as TiniFloat32Value;

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
    public TiniFloat64Value? AsFloat64Value()
        => this as TiniFloat64Value;

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
    public TiniStringValue? AsStringValue()
        => this as TiniStringValue;

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


    #region Operators

    /// <summary>
    /// Implicit conversion into a Boolean value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Boolean value)
        => new TiniBooleanValue(value);

    /// <summary>
    /// Implicit conversion into a Int8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(SByte value)
        => new TiniInt8Value(value);

    /// <summary>
    /// Implicit conversion into a Int16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int16 value)
        => new TiniInt16Value(value);

    /// <summary>
    /// Implicit conversion into a Int32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int32 value)
        => new TiniInt32Value(value);

    /// <summary>
    /// Implicit conversion into a Int64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Int64 value)
        => new TiniInt64Value(value);

    /// <summary>
    /// Implicit conversion into a UInt8 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Byte value)
        => new TiniUInt8Value(value);

    /// <summary>
    /// Implicit conversion into a UInt16 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt16 value)
        => new TiniUInt16Value(value);

    /// <summary>
    /// Implicit conversion into a UInt32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt32 value)
        => new TiniUInt32Value(value);

    /// <summary>
    /// Implicit conversion into a UInt64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(UInt64 value)
        => new TiniUInt64Value(value);

    /// <summary>
    /// Implicit conversion into a Float32 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Single value)
        => new TiniFloat32Value(value);

    /// <summary>
    /// Implicit conversion into a Float64 value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(Double value)
        => new TiniFloat64Value(value);

    /// <summary>
    /// Implicit conversion into a String value object.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator TiniValue(String value)
        => new TiniStringValue(value);

    #endregion Operators


    #region Convert

    /// <summary>
    /// Returns Boolean if object can be converted or null otherwise.
    /// </summary>
    protected abstract Boolean? ConvertToBoolean();

    /// <summary>
    /// Returns Int8 if object can be converted or null otherwise.
    /// </summary>
    protected abstract SByte? ConvertToInt8();

    /// <summary>
    /// Returns Int16 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int16? ConvertToInt16();

    /// <summary>
    /// Returns Int32 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int32? ConvertToInt32();

    /// <summary>
    /// Returns Int64 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Int64? ConvertToInt64();

    /// <summary>
    /// Returns UInt8 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Byte? ConvertToUInt8();

    /// <summary>
    /// Returns UInt16 if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt16? ConvertToUInt16();

    /// <summary>
    /// Returns UInt32 if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt32? ConvertToUInt32();

    /// <summary>
    /// Returns UInt64 if object can be converted or null otherwise.
    /// </summary>
    protected abstract UInt64? ConvertToUInt64();

    /// <summary>
    /// Returns Float32 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Single? ConvertToFloat32();

    /// <summary>
    /// Returns Float64 if object can be converted or null otherwise.
    /// </summary>
    protected abstract Double? ConvertToFloat64();

    /// <summary>
    /// Returns String if object can be converted or null otherwise.
    /// </summary>
    protected abstract String? ConvertToString();

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
