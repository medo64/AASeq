namespace AASeq;
using System;
using System.Net;

public sealed partial class AASeqNode {

    /// <summary>
    /// Returns value as a String.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public String GetValue(String defaultValue) {
        return AASeqValue.TryConvertString(Value, out String? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Boolean.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Boolean GetValue(Boolean defaultValue) {
        return AASeqValue.TryConvertBoolean(Value, out Boolean? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a SByte.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public SByte GetValue(SByte defaultValue) {
        return AASeqValue.TryConvertSByte(Value, out SByte? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Byte.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Byte GetValue(Byte defaultValue) {
        return AASeqValue.TryConvertByte(Value, out Byte? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Int16.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int16 GetValue(Int16 defaultValue) {
        return AASeqValue.TryConvertInt16(Value, out Int16? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a UInt16.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt16 GetValue(UInt16 defaultValue) {
        return AASeqValue.TryConvertUInt16(Value, out UInt16? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Int32.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int32 GetValue(Int32 defaultValue) {
        return AASeqValue.TryConvertInt32(Value, out Int32? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a UInt32.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt32 GetValue(UInt32 defaultValue) {
        return AASeqValue.TryConvertUInt32(Value, out UInt32? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Int64.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int64 GetValue(Int64 defaultValue) {
        return AASeqValue.TryConvertInt64(Value, out Int64? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a UInt64.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt64 GetValue(UInt64 defaultValue) {
        return AASeqValue.TryConvertUInt64(Value, out UInt64? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Int128.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Int128 GetValue(Int128 defaultValue) {
        return AASeqValue.TryConvertInt128(Value, out Int128? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a UInt128.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public UInt128 GetValue(UInt128 defaultValue) {
        return AASeqValue.TryConvertUInt128(Value, out UInt128? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a Half.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Half GetValue(Half defaultValue) {
        return AASeqValue.TryConvertHalf(Value, out Half? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Single.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Single GetValue(Single defaultValue) {
        return AASeqValue.TryConvertSingle(Value, out Single? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Double.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Double GetValue(Double defaultValue) {
        return AASeqValue.TryConvertDouble(Value, out Double? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a Decimal.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Decimal GetValue(Decimal defaultValue) {
        return AASeqValue.TryConvertDecimal(Value, out Decimal? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a DateTimeOffset.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public DateTimeOffset GetValue(DateTimeOffset defaultValue) {
        return AASeqValue.TryConvertDateTimeOffset(Value, out DateTimeOffset? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a DateOnly.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public DateOnly GetValue(DateOnly defaultValue) {
        return AASeqValue.TryConvertDateOnly(Value, out DateOnly? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a TimeOnly.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public TimeOnly GetValue(TimeOnly defaultValue) {
        return AASeqValue.TryConvertTimeOnlyValue(Value, out TimeOnly? result) ? result.Value : defaultValue;
    }

    /// <summary>
    /// Returns value as a TimeSpan.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public TimeSpan GetValue(TimeSpan defaultValue) {
        return AASeqValue.TryConvertTimeSpan(Value, out TimeSpan? result) ? result.Value : defaultValue;
    }


    /// <summary>
    /// Returns value as a IPAddress.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public IPAddress GetValue(IPAddress defaultValue) {
        return AASeqValue.TryConvertIPAddress(Value, out IPAddress? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Uri.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Uri GetValue(Uri defaultValue) {
        return AASeqValue.TryConvertUri(Value, out Uri? result) ? result : defaultValue;
    }


    /// <summary>
    /// Returns value as a Uri.
    /// </summary>
    /// <param name="defaultValue">Default value.</param>
    public Guid GetValue(Guid defaultValue) {
        return AASeqValue.TryConvertGuid(Value, out Guid? result) ? result.Value : defaultValue;
    }

}
