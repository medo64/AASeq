namespace AASeq;
using System;
using System.Net;
using System.Text.RegularExpressions;

public sealed partial record AASeqValue {

    /// <summary>
    /// Returns value as a String.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public String? AsString() {
        return TryConvertString(Value, out String? result) ? result : null;
    }

    /// <summary>
    /// Returns value as a String.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public String AsString(String defaultValue) {
        return TryConvertString(Value, out String? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Boolean.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Boolean? AsBoolean() {
        return TryConvertBoolean(Value, out Boolean? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Boolean.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Boolean AsBoolean(Boolean defaultValue) {
        return TryConvertBoolean(Value, out Boolean? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a SByte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public SByte? AsSByte() {
        return TryConvertSByte(Value, out SByte? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a SByte.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public SByte AsSByte(SByte defaultValue) {
        return TryConvertSByte(Value, out SByte? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Byte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Byte? AsByte() {
        return TryConvertByte(Value, out Byte? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Byte.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Byte AsByte(Byte defaultValue) {
        return TryConvertByte(Value, out Byte? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Int16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Int16? AsInt16() {
        return TryConvertInt16(Value, out Int16? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Int16.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int16 AsInt16(Int16 defaultValue) {
        return TryConvertInt16(Value, out Int16? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a UInt16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public UInt16? AsUInt16() {
        return TryConvertUInt16(Value, out UInt16? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a UInt16.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt16 AsUInt16(UInt16 defaultValue) {
        return TryConvertUInt16(Value, out UInt16? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Int32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Int32? AsInt32() {
        return TryConvertInt32(Value, out Int32? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Int32.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int32 AsInt32(Int32 defaultValue) {
        return TryConvertInt32(Value, out Int32? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a UInt32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public UInt32? AsUInt32() {
        return TryConvertUInt32(Value, out UInt32? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a UInt32.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt32 AsUInt32(UInt32 defaultValue) {
        return TryConvertUInt32(Value, out UInt32? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Int64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Int64? AsInt64() {
        return TryConvertInt64(Value, out Int64? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Int64.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int64 AsInt64(Int64 defaultValue) {
        return TryConvertInt64(Value, out Int64? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a UInt64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public UInt64? AsUInt64() {
        return TryConvertUInt64(Value, out UInt64? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a UInt64.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt64 AsUInt64(UInt64 defaultValue) {
        return TryConvertUInt64(Value, out UInt64? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Int128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Int128? AsInt128() {
        return TryConvertInt128(Value, out Int128? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Int128.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int128 AsInt128(Int128 defaultValue) {
        return TryConvertInt128(Value, out Int128? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a UInt128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public UInt128? AsUInt128() {
        return TryConvertUInt128(Value, out UInt128? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a UInt128.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt128 AsUInt128(UInt128 defaultValue) {
        return TryConvertUInt128(Value, out UInt128? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Half.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Half? AsHalf() {
        return TryConvertHalf(Value, out Half? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Half.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Half AsHalf(Half defaultValue) {
        return TryConvertHalf(Value, out Half? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Single.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Single? AsSingle() {
        return TryConvertSingle(Value, out Single? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Single.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Single AsSingle(Single defaultValue) {
        return TryConvertSingle(Value, out Single? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Double.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Double? AsDouble() {
        return TryConvertDouble(Value, out Double? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Double.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Double AsDouble(Double defaultValue) {
        return TryConvertDouble(Value, out Double? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Decimal.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Decimal? AsDecimal() {
        return TryConvertDecimal(Value, out Decimal? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Decimal.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Decimal AsDecimal(Decimal defaultValue) {
        return TryConvertDecimal(Value, out Decimal? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a DateTimeOffset.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public DateTimeOffset? AsDateTimeOffset() {
        return TryConvertDateTimeOffset(Value, out DateTimeOffset? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a DateTimeOffset.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public DateTimeOffset AsDateTimeOffset(DateTimeOffset defaultValue) {
        return TryConvertDateTimeOffset(Value, out DateTimeOffset? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a DateOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public DateOnly? AsDateOnly() {
        return TryConvertDateOnly(Value, out DateOnly? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a DateOnly.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public DateOnly AsDateOnly(DateOnly defaultValue) {
        return TryConvertDateOnly(Value, out DateOnly? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a TimeOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public TimeOnly? AsTimeOnly() {
        return TryConvertTimeOnlyValue(Value, out TimeOnly? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a TimeOnly.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public TimeOnly AsTimeOnly(TimeOnly defaultValue) {
        return TryConvertTimeOnlyValue(Value, out TimeOnly? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a TimeSpan.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public TimeSpan? AsTimeSpan() {
        return TryConvertTimeSpan(Value, out TimeSpan? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a TimeSpan.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public TimeSpan AsTimeSpan(TimeSpan defaultValue) {
        return TryConvertTimeSpan(Value, out TimeSpan? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a IPAddress.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public IPAddress? AsIPAddress() {
        return TryConvertIPAddress(Value, out IPAddress? result) ? result : null;
    }

    /// <summary>
    /// Returns value as a IPAddress.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public IPAddress AsIPAddress(IPAddress defaultValue) {
        return TryConvertIPAddress(Value, out IPAddress? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Regex.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Regex? AsRegex() {
        return TryConvertRegex(Value, out Regex? result) ? result : null;
    }

    /// <summary>
    /// Returns value as a Regex.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Regex AsRegex(Regex defaultValue) {
        return TryConvertRegex(Value, out Regex? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Uri.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Uri? AsUri() {
        return TryConvertUri(Value, out Uri? result) ? result : null;
    }

    /// <summary>
    /// Returns value as a Uri.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Uri AsUri(Uri defaultValue) {
        return TryConvertUri(Value, out Uri? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Guid.
    /// Null is returned if value cannot be converted.
    /// </summary>
    public Guid? AsGuid() {
        return TryConvertGuid(Value, out Guid? result) ? result.Value : null;
    }

    /// <summary>
    /// Returns value as a Uri.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Guid AsGuid(Guid defaultValue) {
        return TryConvertGuid(Value, out Guid? result) ? result.Value : defaultValue;
    }

}
