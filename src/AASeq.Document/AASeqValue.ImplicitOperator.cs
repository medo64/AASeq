namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

[SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "As* methods exist")]
public sealed partial record AASeqValue {

    /// <summary>
    /// Returns value as a String.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator String?(AASeqValue value) => value?.AsString();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(String value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Boolean.
    /// </summary>
    public static implicit operator Boolean(AASeqValue value) => value?.AsBoolean() ?? default;

    /// <summary>
    /// Returns value as a Boolean.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Boolean?(AASeqValue value) => value?.AsBoolean();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Boolean value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a SByte.
    /// </summary>
    public static implicit operator SByte(AASeqValue value) => value?.AsSByte() ?? default;

    /// <summary>
    /// Returns value as a SByte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator SByte?(AASeqValue value) => value?.AsSByte();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(SByte value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a SByte.
    /// </summary>
    public static implicit operator Byte(AASeqValue value) => value?.AsByte() ?? default;

    /// <summary>
    /// Returns value as a SByte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Byte?(AASeqValue value) => value?.AsByte();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Byte value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Int16.
    /// </summary>
    public static implicit operator Int16(AASeqValue value) => value?.AsInt16() ?? default;

    /// <summary>
    /// Returns value as a Int16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Int16?(AASeqValue value) => value?.AsInt16();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Int16 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a UInt16.
    /// </summary>
    public static implicit operator UInt16(AASeqValue value) => value?.AsUInt16() ?? default;

    /// <summary>
    /// Returns value as a UInt16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator UInt16?(AASeqValue value) => value?.AsUInt16();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(UInt16 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Int32.
    /// </summary>
    public static implicit operator Int32(AASeqValue value) => value?.AsInt32() ?? default;

    /// <summary>
    /// Returns value as a Int32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Int32?(AASeqValue value) => value?.AsInt32();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Int32 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a UInt32.
    /// </summary>
    public static implicit operator UInt32(AASeqValue value) => value?.AsUInt32() ?? default;

    /// <summary>
    /// Returns value as a UInt32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator UInt32?(AASeqValue value) => value?.AsUInt32();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(UInt32 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Int64.
    /// </summary>
    public static implicit operator Int64(AASeqValue value) => value?.AsInt64() ?? default;

    /// <summary>
    /// Returns value as a Int64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Int64?(AASeqValue value) => value?.AsInt64();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Int64 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a UInt64.
    /// </summary>
    public static implicit operator UInt64(AASeqValue value) => value?.AsUInt64() ?? default;

    /// <summary>
    /// Returns value as a UInt64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator UInt64?(AASeqValue value) => value?.AsUInt64();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(UInt64 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Int128.
    /// </summary>
    public static implicit operator Int128(AASeqValue value) => value?.AsInt128() ?? default;

    /// <summary>
    /// Returns value as a Int128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Int128?(AASeqValue value) => value?.AsInt128();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Int128 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a UInt128.
    /// </summary>
    public static implicit operator UInt128(AASeqValue value) => value?.AsUInt128() ?? default;

    /// <summary>
    /// Returns value as a UInt128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator UInt128?(AASeqValue value) => value?.AsUInt128();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(UInt128 value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Half.
    /// </summary>
    public static implicit operator Half(AASeqValue value) => value?.AsHalf() ?? default;

    /// <summary>
    /// Returns value as a Half.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Half?(AASeqValue value) => value?.AsHalf();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Half value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Single.
    /// </summary>
    public static implicit operator Single(AASeqValue value) => value?.AsSingle() ?? default;

    /// <summary>
    /// Returns value as a Single.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Single?(AASeqValue value) => value?.AsSingle();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Single value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Double.
    /// </summary>
    public static implicit operator Double(AASeqValue value) => value?.AsDouble() ?? default;

    /// <summary>
    /// Returns value as a Double.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Double?(AASeqValue value) => value?.AsDouble();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Double value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Decimal.
    /// </summary>
    public static implicit operator Decimal(AASeqValue value) => value?.AsDecimal() ?? default;

    /// <summary>
    /// Returns value as a Decimal.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Decimal?(AASeqValue value) => value?.AsDecimal();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Decimal value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a DateTimeOffset.
    /// </summary>
    public static implicit operator DateTimeOffset(AASeqValue value) => value?.AsDateTimeOffset() ?? default;

    /// <summary>
    /// Returns value as a DateTimeOffset.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator DateTimeOffset?(AASeqValue value) => value?.AsDateTimeOffset();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(DateTimeOffset value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a DateOnly.
    /// </summary>
    public static implicit operator DateOnly(AASeqValue value) => value?.AsDateOnly() ?? default;

    /// <summary>
    /// Returns value as a DateOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator DateOnly?(AASeqValue value) => value?.AsDateOnly();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(DateOnly value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a TimeOnly.
    /// </summary>
    public static implicit operator TimeOnly(AASeqValue value) => value?.AsTimeOnly() ?? default;

    /// <summary>
    /// Returns value as a TimeOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator TimeOnly?(AASeqValue value) => value?.AsTimeOnly();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(TimeOnly value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a TimeSpan.
    /// </summary>
    public static implicit operator TimeSpan(AASeqValue value) => value?.AsTimeSpan() ?? default;

    /// <summary>
    /// Returns value as a TimeSpan.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator TimeSpan?(AASeqValue value) => value?.AsTimeSpan();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(TimeSpan value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a IPAddress.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator IPAddress?(AASeqValue value) => value?.AsIPAddress();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(IPAddress value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Uri.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Uri?(AASeqValue value) => value?.AsUri();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Uri value) => new() { RawValue = value };


    /// <summary>
    /// Returns value as a Guid.
    /// </summary>
    public static implicit operator Guid(AASeqValue value) => value?.AsGuid() ?? default;

    /// <summary>
    /// Returns value as a Guid.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public static implicit operator Guid?(AASeqValue value) => value?.AsGuid();

    /// <summary>
    /// Returns AASeqValue.
    /// </summary>
    /// <param name="value">Value.</param>
    public static implicit operator AASeqValue(Guid value) => new() { RawValue = value };

}
