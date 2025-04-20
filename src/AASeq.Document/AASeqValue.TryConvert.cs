namespace AASeq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;

public sealed partial record AASeqValue {

    /// <summary>
    /// Returns true if value can be converted to String.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertString(object? value, [NotNullWhen(true)] out String? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            null => null,
            DateTimeOffset val => val.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFFFFFzzz", CultureInfo.InvariantCulture),
            DateOnly val => val.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            TimeOnly val => val.ToString("HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture),
            TimeSpan val => ConvertToDurationString(val),
            Byte[] val => ConvertToHexString(val),
            String val => val,
            _ => value.ToString(),
        };
        return (result is not null);
    }


    /// <summary>
    /// Returns true if value can be converted to Boolean.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertBoolean(object? value, [NotNullWhen(true)] out Boolean? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val,
            SByte val => val != 0,
            Byte val => val != 0,
            Int16 val => val != 0,
            UInt16 val => val != 0,
            Int32 val => val != 0,
            UInt32 val => val != 0,
            Int64 val => val != 0,
            UInt64 val => val != 0,
            Int128 val => val != 0,
            UInt128 val => val != 0,
            String val => TryParseBoolean(val, out var res) ? (Boolean)res! : null,
            _ => null,
        };
        return (result is not null);
    }


    /// <summary>
    /// Returns true if value can be converted to SByte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertSByte(object? value, [NotNullWhen(true)] out SByte? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (SByte)1 : (SByte)0,
            SByte val => val,
            Byte val => (val is <= (Byte)SByte.MaxValue) ? (SByte)val : null,
            Int16 val => (val is >= SByte.MinValue and <= SByte.MaxValue) ? (SByte)val : null,
            UInt16 val => val is <= (UInt16)SByte.MaxValue ? (SByte)val : null,
            Int32 val => (val is >= SByte.MinValue and <= SByte.MaxValue) ? (SByte)val : null,
            UInt32 val => val is <= (UInt32)SByte.MaxValue ? (SByte)val : null,
            Int64 val => (val is >= SByte.MinValue and <= SByte.MaxValue) ? (SByte)val : null,
            UInt64 val => (val is <= (UInt64)SByte.MaxValue) ? (SByte)val : null,
            Int128 val => (val >= SByte.MinValue && val <= SByte.MaxValue) ? (SByte)val : null,
            UInt128 val => (val <= (UInt16)SByte.MaxValue) ? (SByte)val : null,
            Half val => ConvertOrBust<SByte, Half>(val),
            Single val => ConvertOrBust<SByte, Single>(val),
            Double val => ConvertOrBust<SByte, Double>(val),
            Decimal val => ConvertOrBust<SByte, Decimal>(val),
            Byte[] val => GetIntFromBytes<SByte>(val),
            String val => TryParseSByte(val, out var res) ? (SByte)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Byte.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertByte(object? value, [NotNullWhen(true)] out Byte? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (Byte)1 : (Byte)0,
            SByte val => (val >= 0) ? (Byte)val : null,
            Byte val => val,
            Int16 val => (val is >= Byte.MinValue and <= Byte.MaxValue) ? (Byte)val : null,
            UInt16 val => (val is <= (UInt16)Byte.MaxValue) ? (Byte)val : null,
            Int32 val => (val is >= Byte.MinValue and <= Byte.MaxValue) ? (Byte)val : null,
            UInt32 val => (val is <= Byte.MaxValue) ? (Byte)val : null,
            Int64 val => (val is >= Byte.MinValue and <= Byte.MaxValue) ? (Byte)val : null,
            UInt64 val => (val is <= Byte.MaxValue) ? (Byte)val : null,
            Int128 val => (val >= Byte.MinValue && val <= Byte.MaxValue) ? (Byte)val : null,
            UInt128 val => (val <= Byte.MaxValue) ? (Byte)val : null,
            Half val => ConvertOrBust<Byte, Half>(val),
            Single val => ConvertOrBust<Byte, Single>(val),
            Double val => ConvertOrBust<Byte, Double>(val),
            Decimal val => ConvertOrBust<Byte, Decimal>(val),
            Byte[] val => GetIntFromBytes<Byte>(val),
            String val => TryParseByte(val, out var res) ? (Byte)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Int16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertInt16(object? value, [NotNullWhen(true)] out Int16? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (Int16)1 : (Int16)0,
            SByte val => val,
            Byte val => val,
            Int16 val => val,
            UInt16 val => (val is <= (UInt16)Int16.MaxValue) ? (Int16)val : null,
            Int32 val => (val is >= Int16.MinValue and <= Int16.MaxValue) ? (Int16)val : null,
            UInt32 val => (val is <= (UInt32)Int16.MaxValue) ? (Int16)val : null,
            Int64 val => (val is >= Int16.MinValue and <= Int16.MaxValue) ? (Int16)val : null,
            UInt64 val => (val is <= (UInt64)Int16.MaxValue) ? (Int16)val : null,
            Int128 val => (val >= Int16.MinValue && val <= Int16.MaxValue) ? (Int16)val : null,
            UInt128 val => (val <= (UInt16)Int16.MaxValue) ? (Int16)val : null,
            Half val => ConvertOrBust<Int16, Half>(val),
            Single val => ConvertOrBust<Int16, Single>(val),
            Double val => ConvertOrBust<Int16, Double>(val),
            Decimal val => ConvertOrBust<Int16, Decimal>(val),
            Byte[] val => GetIntFromBytes<Int16>(val),
            String val => TryParseInt16(val, out var res) ? (Int16)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to UInt16.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertUInt16(object? value, [NotNullWhen(true)] out UInt16? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (UInt16)1 : (UInt16)0,
            SByte val => (val >= 0) ? (UInt16)val : null,
            Byte val => val,
            Int16 val => (val >= 0) ? (UInt16)val : null,
            UInt16 val => val,
            Int32 val => (val is >= UInt16.MinValue and <= UInt16.MaxValue) ? (UInt16)val : null,
            UInt32 val => (val is <= UInt16.MaxValue) ? (UInt16)val : null,
            Int64 val => (val is >= UInt16.MinValue and <= UInt16.MaxValue) ? (UInt16)val : null,
            UInt64 val => (val is <= UInt16.MaxValue) ? (UInt16)val : null,
            Int128 val => (val >= UInt16.MinValue && val <= UInt16.MaxValue) ? (UInt16)val : null,
            UInt128 val => (val <= UInt16.MaxValue) ? (UInt16)val : null,
            Half val => ConvertOrBust<UInt16, Half>(val),
            Single val => ConvertOrBust<UInt16, Single>(val),
            Double val => ConvertOrBust<UInt16, Double>(val),
            Decimal val => ConvertOrBust<UInt16, Decimal>(val),
            Byte[] val => GetIntFromBytes<UInt16>(val),
            String val => TryParseUInt16(val, out var res) ? (UInt16)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Int32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertInt32(object? value, [NotNullWhen(true)] out Int32? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? 1 : 0,
            SByte val => val,
            Byte val => val,
            Int16 val => val,
            UInt16 val => val,
            Int32 val => val,
            UInt32 val => (val is <= (UInt32)Int32.MaxValue) ? (Int32)val : null,
            Int64 val => (val is >= Int32.MinValue and <= Int32.MaxValue) ? (Int32)val : null,
            UInt64 val => (val is <= (UInt64)Int16.MaxValue) ? (Int32)val : null,
            Int128 val => (val >= Int32.MinValue && val <= Int32.MaxValue) ? (Int32)val : null,
            UInt128 val => (val <= (UInt32)Int32.MaxValue) ? (Int32)val : null,
            Half val => ConvertOrBust<Int32, Half>(val),
            Single val => ConvertOrBust<Int32, Single>(val),
            Double val => ConvertOrBust<Int32, Double>(val),
            Decimal val => ConvertOrBust<Int32, Decimal>(val),
            DateTimeOffset val => ConvertToUnixSeconds<Int32>(val),
            Byte[] val => GetIntFromBytes<Int32>(val),
            String val => TryParseInt32(val, out var res) ? (Int32)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to UInt32.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertUInt32(object? value, [NotNullWhen(true)] out UInt32? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (UInt32)1 : 0,
            SByte val => (val >= 0) ? (UInt32)val : null,
            Byte val => val,
            Int16 val => (val >= 0) ? (UInt32)val : null,
            UInt16 val => val,
            Int32 val => (val >= 0) ? (UInt32)val : null,
            UInt32 val => val,
            Int64 val => (val is >= UInt32.MinValue and <= UInt32.MaxValue) ? (UInt32)val : null,
            UInt64 val => (val is <= UInt32.MaxValue) ? (UInt32)val : null,
            Int128 val => (val >= UInt32.MinValue && val <= UInt32.MaxValue) ? (UInt32)val : null,
            UInt128 val => (val <= UInt32.MaxValue) ? (UInt32)val : null,
            Half val => ConvertOrBust<UInt32, Half>(val),
            Single val => ConvertOrBust<UInt32, Single>(val),
            Double val => ConvertOrBust<UInt32, Double>(val),
            Decimal val => ConvertOrBust<UInt32, Decimal>(val),
            DateTimeOffset val => ConvertToUnixSeconds<UInt32>(val),
            Byte[] val => GetIntFromBytes<UInt32>(val),
            String val => TryParseUInt32(val, out var res) ? (UInt32)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Int64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertInt64(object? value, [NotNullWhen(true)] out Int64? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? 1 : 0,
            SByte val => val,
            Byte val => val,
            Int16 val => val,
            UInt16 val => val,
            Int32 val => val,
            UInt32 val => val,
            Int64 val => val,
            UInt64 val => (val is <= Int64.MaxValue) ? (Int64)val : null,
            Int128 val => (val >= Int64.MinValue && val <= Int64.MaxValue) ? (Int64)val : null,
            UInt128 val => (val <= (UInt64)Int64.MaxValue) ? (Int64)val : null,
            Half val => ConvertOrBust<Int64, Half>(val),
            Single val => ConvertOrBust<Int64, Single>(val),
            Double val => ConvertOrBust<Int64, Double>(val),
            Decimal val => ConvertOrBust<Int64, Decimal>(val),
            DateTimeOffset val => ConvertToUnixSeconds<Int64>(val),
            Byte[] val => GetIntFromBytes<Int64>(val),
            String val => TryParseInt64(val, out var res) ? (Int64)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to UInt64.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertUInt64(object? value, [NotNullWhen(true)] out UInt64? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (UInt64)1 : (UInt64)0,
            SByte val => (val >= 0) ? (UInt64)val : null,
            Byte val => val,
            Int16 val => (val >= 0) ? (UInt64)val : null,
            UInt16 val => val,
            Int32 val => (val >= 0) ? (UInt64)val : null,
            UInt32 val => val,
            Int64 val => (val >= 0) ? (UInt64)val : null,
            UInt64 val => val,
            Int128 val => (val >= UInt64.MinValue && val <= UInt64.MaxValue) ? (UInt64)val : null,
            UInt128 val => (val <= UInt64.MaxValue) ? (UInt64)val : null,
            Half val => ConvertOrBust<UInt64, Half>(val),
            Single val => ConvertOrBust<UInt64, Single>(val),
            Double val => ConvertOrBust<UInt64, Double>(val),
            Decimal val => ConvertOrBust<UInt64, Decimal>(val),
            DateTimeOffset val => ConvertToUnixSeconds<UInt64>(val),
            Byte[] val => GetIntFromBytes<UInt64>(val),
            String val => TryParseUInt64(val, out var res) ? (UInt64)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Int128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertInt128(object? value, [NotNullWhen(true)] out Int128? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? 1 : 0,
            SByte val => val,
            Byte val => val,
            Int16 val => val,
            UInt16 val => val,
            Int32 val => val,
            UInt32 val => val,
            Int64 val => val,
            UInt64 val => val,
            Int128 val => val,
            UInt128 val => (val <= (UInt64)Int64.MaxValue) ? (Int64)val : null,
            Half val => ConvertOrBust<Int128, Half>(val),
            Single val => ConvertOrBust<Int128, Single>(val),
            Double val => ConvertOrBust<Int128, Double>(val),
            Decimal val => ConvertOrBust<Int128, Decimal>(val),
            Byte[] val => GetIntFromBytes<Int128>(val),
            String val => TryParseInt128(val, out var res) ? (Int128)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to UInt128.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertUInt128(object? value, [NotNullWhen(true)] out UInt128? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Boolean val => val ? (UInt128)1 : 0,
            SByte val => (val >= 0) ? (UInt64)val : null,
            Byte val => val,
            Int16 val => (val >= 0) ? (UInt64)val : null,
            UInt16 val => val,
            Int32 val => (val >= 0) ? (UInt64)val : null,
            UInt32 val => val,
            Int64 val => (val >= 0) ? (UInt64)val : null,
            UInt64 val => val,
            Int128 val => (val >= 0) ? (UInt64)val : null,
            UInt128 val => val,
            Half val => ConvertOrBust<UInt128, Half>(val),
            Single val => ConvertOrBust<UInt128, Single>(val),
            Double val => ConvertOrBust<UInt128, Double>(val),
            Decimal val => ConvertOrBust<UInt128, Decimal>(val),
            Byte[] val => GetIntFromBytes<UInt128>(val),
            String val => TryParseUInt128(val, out var res) ? (UInt128)res! : null,
            _ => null,
        };
        return (result is not null);
    }


    /// <summary>
    /// Returns true if value can be converted to Half.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertHalf(object? value, [NotNullWhen(true)] out Half? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            SByte val => ConvertOrBust<Half, SByte>(val),
            Byte val => ConvertOrBust<Half, Byte>(val),
            Int16 val => ConvertOrBust<Half, Int16>(val),
            UInt16 val => ConvertOrBust<Half, UInt16>(val),
            Int32 val => ConvertOrBust<Half, Int32>(val),
            UInt32 val => ConvertOrBust<Half, UInt32>(val),
            Int64 val => ConvertOrBust<Half, Int64>(val),
            UInt64 val => ConvertOrBust<Half, UInt64>(val),
            Int128 val => ConvertOrBust<Half, Int128>(val),
            UInt128 val => ConvertOrBust<Half, UInt128>(val),
            Half val => val,
            Single val => ConvertOrBust<Half, Single>(val),
            Double val => ConvertOrBust<Half, Double>(val),
            Decimal val => ConvertOrBust<Half, Double>((double)val),
            String val => TryParseHalf(val, out var res) ? (Half)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Single.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertSingle(object? value, [NotNullWhen(true)] out Single? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            SByte val => ConvertOrBust<Single, SByte>(val),
            Byte val => ConvertOrBust<Single, Byte>(val),
            Int16 val => ConvertOrBust<Single, Int16>(val),
            UInt16 val => ConvertOrBust<Single, UInt16>(val),
            Int32 val => ConvertOrBust<Single, Int32>(val),
            UInt32 val => ConvertOrBust<Single, UInt32>(val),
            Int64 val => ConvertOrBust<Single, Int64>(val),
            UInt64 val => ConvertOrBust<Single, UInt64>(val),
            Int128 val => ConvertOrBust<Single, Int128>(val),
            UInt128 val => ConvertOrBust<Single, UInt128>(val),
            Half val => ConvertOrBust<Single, Half>(val),
            Single val => val,
            Double val => ConvertOrBust<Single, Double>(val),
            Decimal val => ConvertOrBust<Single, Double>((double)val),
            String val => TryParseSingle(val, out var res) ? (Single)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Double.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertDouble(object? value, [NotNullWhen(true)] out Double? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            SByte val => ConvertOrBust<Double, SByte>(val),
            Byte val => ConvertOrBust<Double, Byte>(val),
            Int16 val => ConvertOrBust<Double, Int16>(val),
            UInt16 val => ConvertOrBust<Double, UInt16>(val),
            Int32 val => ConvertOrBust<Double, Int32>(val),
            UInt32 val => ConvertOrBust<Double, UInt32>(val),
            Int64 val => ConvertOrBust<Double, Int64>(val),
            UInt64 val => ConvertOrBust<Double, UInt64>(val),
            Int128 val => ConvertOrBust<Double, Int128>(val),
            UInt128 val => ConvertOrBust<Double, UInt128>(val),
            Half val => ConvertOrBust<Double, Half>(val),
            Single val => ConvertOrBust<Double, Single>(val),
            Double val => val,
            Decimal val => ConvertOrBust<Double, Double>((double)val),
            DateTimeOffset val => (val - DateTimeOffset.UnixEpoch).TotalSeconds,
            DateOnly val => (new DateTimeOffset(val, TimeOnly.MinValue, TimeSpan.Zero) - DateTimeOffset.UnixEpoch).TotalSeconds,
            TimeOnly val => (new DateTimeOffset(DateOnly.FromDateTime(DateTime.UnixEpoch), val, TimeSpan.Zero) - DateTimeOffset.UnixEpoch).TotalSeconds,
            TimeSpan val => val.TotalSeconds,
            String val => TryParseDouble(val, out var res) ? (Double)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Decimal.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertDecimal(object? value, [NotNullWhen(true)] out Decimal? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            SByte val => ConvertOrBust<Decimal, SByte>(val),
            Byte val => ConvertOrBust<Decimal, Byte>(val),
            Int16 val => ConvertOrBust<Decimal, Int16>(val),
            UInt16 val => ConvertOrBust<Decimal, UInt16>(val),
            Int32 val => ConvertOrBust<Decimal, Int32>(val),
            UInt32 val => ConvertOrBust<Decimal, UInt32>(val),
            Int64 val => ConvertOrBust<Decimal, Int64>(val),
            UInt64 val => ConvertOrBust<Decimal, UInt64>(val),
            Int128 val => ConvertOrBust<Decimal, Int128>(val),
            UInt128 val => ConvertOrBust<Decimal, UInt128>(val),
            Half val => ConvertOrBust<Decimal, Half>(val),
            Single val => ConvertOrBust<Decimal, Single>(val),
            Double val => ConvertOrBust<Decimal, Double>(val),
            Decimal val => val,
            String val => TryParseDecimal(val, out var res) ? (Decimal)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to DateTimeOffset.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertDateTimeOffset(object? value, [NotNullWhen(true)] out DateTimeOffset? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Int32 val => DateTimeOffset.FromUnixTimeSeconds(val),
            UInt32 val => DateTimeOffset.FromUnixTimeSeconds(val),
            Int64 val => DateTimeOffset.FromUnixTimeSeconds(val),
            UInt64 val => (val < long.MaxValue) ? DateTimeOffset.FromUnixTimeSeconds((long)val) : null,
            DateTimeOffset val => val,
            DateOnly val => new DateTimeOffset(val, TimeOnly.MinValue, TimeSpan.Zero),
            TimeOnly val => null,
            TimeSpan val => null,
            String val => TryParseDateTimeOffset(val, out var res) ? (DateTimeOffset)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to DateOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertDateOnly(object? value, [NotNullWhen(true)] out DateOnly? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            DateTimeOffset val => new DateOnly(val.Year, val.Month, val.Day),
            DateOnly val => val,
            String val => TryParseDateOnly(val, out var res) ? (DateOnly)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to TimeOnly.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertTimeOnlyValue(object? value, [NotNullWhen(true)] out TimeOnly? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            DateTimeOffset val => new TimeOnly(val.Ticks % (TimeOnly.MaxValue.Ticks + 1)),
            TimeOnly val => val,
            String val => TryParseTimeOnly(val, out var res) ? (TimeOnly)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to TimeSpan.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertTimeSpan(object? value, [NotNullWhen(true)] out TimeSpan? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            SByte val => TimeSpan.FromSeconds(val),
            Byte val => TimeSpan.FromSeconds(val),
            Int16 val => TimeSpan.FromSeconds(val),
            UInt16 val => TimeSpan.FromSeconds(val),
            Int32 val => TimeSpan.FromSeconds(val),
            UInt32 val => TimeSpan.FromSeconds(val),
            Int64 val => TimeSpan.FromSeconds(val),
            UInt64 val => TimeSpan.FromSeconds(val),
            Int128 val => TimeSpan.FromSeconds((double)val),
            UInt128 val => TimeSpan.FromSeconds((double)val),
            Half val => TimeSpan.FromSeconds((double)val),
            Single val => TimeSpan.FromSeconds(val),
            Double val => TimeSpan.FromSeconds(val),
            Decimal val => TimeSpan.FromSeconds((double)val),
            DateTimeOffset val => null,
            DateOnly val => null,
            TimeOnly val => null,
            TimeSpan val => val,
            String val => TryParseTimeSpan(val, out var res) ? (TimeSpan)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to IPAddress.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertIPAddress(object? value, [NotNullWhen(true)] out IPAddress? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            IPAddress val => val,
            String val => TryParseIPAddress(val, out var res) ? (IPAddress)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Uri.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertUri(object? value, [NotNullWhen(true)] out Uri? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Uri val => val,
            String val => TryParseUri(val, out var res) ? (Uri)res! : null,
            _ => null,
        };
        return (result is not null);
    }

    /// <summary>
    /// Returns true if value can be converted to Guid.
    /// Null is returned if value cannot be converted.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="result">Converted value.</param>
    public static bool TryConvertGuid(object? value, [NotNullWhen(true)] out Guid? result) {
        if (value is AASeqValue innerValue) { value = innerValue.RawValue; }
        result = value switch {
            Guid val => val,
            String val => TryParseGuid(val, out var res) ? (Guid)res! : null,
            _ => null,
        };
        return (result is not null);
    }


    #region Conversions

    private static T? ConvertOrBust<T, V>(V value)
        where T : struct, INumber<T>
        where V : struct, INumber<V> {

        try {
            return T.CreateChecked(value);
        } catch (OverflowException) {  // TODO: fix not to use exceptions here; we're not in Java
            return null;
        }
    }

    private static T? ConvertToUnixSeconds<T>(DateTimeOffset value)
        where T : struct, IBinaryInteger<T>, IMinMaxValue<T> {
        var seconds = value.ToUnixTimeSeconds();
        try {
            return T.CreateChecked(seconds);
        } catch (OverflowException) {  // TODO: fix not to use exceptions here; we're not in Java
            return null;
        }
    }

    private static T? GetIntFromBytes<T>(Byte[] bytes)
        where T : struct, IBinaryInteger<T>, IBitwiseOperators<T, T, T>, IMinMaxValue<T>, IShiftOperators<T, Int32, T> {

        if (bytes is null) { return null; }
        if (bytes.Length is 0 or > 16) { return null; }
        var remainingBytes = Marshal.SizeOf<T>();
        T value = T.Zero;
        var allZero = true;
        for (var i = 0; i < bytes.Length; i++) {
            var b = bytes[i];
            if (b != 0) { allZero = false; }
            if (allZero) { continue; }  // don't bother counting bytes while they're 0

            remainingBytes -= 1;
            if (remainingBytes >= 0) {
                value <<= 8;
                value |= T.CreateTruncating(b);
            } else {
                if (b > 0) { return null; }  // there are more bytes than value would support
            }
        }
        return value;
    }

    private static String ConvertToHexString(Byte[] bytes) {
        if ((bytes is null) || (bytes.Length == 0)) { return String.Empty; }
        var sb = StringBuilderPool.Get();
        try {
            foreach (var b in bytes) {
                sb.Append(b.ToString("x2", CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        } finally {
            sb.Length = 0;
            StringBuilderPool.Return(sb);
        }
    }

    private static string ConvertToDurationString(TimeSpan val) {
        if (val.Ticks == 0) { return "0s"; }

        var sb = StringBuilderPool.Get();
        try {
            if (val.Days != 0) {
                if (sb.Length > 0) { sb.Append(' '); }
                sb.Append(Math.Abs(val.Days).ToString(CultureInfo.InvariantCulture) + "d");
            }

            if (val.Hours != 0) {
                if (sb.Length > 0) { sb.Append(' '); }
                sb.Append(Math.Abs(val.Hours).ToString(CultureInfo.InvariantCulture) + "h");
            }

            if (val.Minutes != 0) {
                if (sb.Length != 0) { sb.Append(' '); }
                sb.Append(Math.Abs(val.Minutes).ToString(CultureInfo.InvariantCulture) + "m");
            }

            if ((val.Seconds != 0) || (val.Milliseconds != 0) || (val.Microseconds != 0) || (val.Nanoseconds != 0)) {
                if (sb.Length > 0) { sb.Append(' '); }
                sb.Append(Math.Abs(val.Seconds).ToString(CultureInfo.InvariantCulture));
                if ((val.Milliseconds != 0) || (val.Microseconds != 0) || (val.Nanoseconds != 0)) {
                    sb.Append('.');
                    sb.Append(Math.Abs(val.Milliseconds).ToString("000", CultureInfo.InvariantCulture));
                }
                if ((val.Microseconds != 0) || (val.Nanoseconds != 0)) {
                    sb.Append(Math.Abs(val.Microseconds).ToString("000", CultureInfo.InvariantCulture));
                }
                if ((val.Nanoseconds != 0)) {
                    sb.Append(Math.Abs(val.Nanoseconds / 100).ToString("0", CultureInfo.InvariantCulture));
                }
                sb.Append('s');
            }

            if (val.Ticks < 0) { sb.Insert(0, '-'); }

            return sb.ToString();

        } finally {
            sb.Length = 0;
            StringBuilderPool.Return(sb);
        }
    }

    #endregion Conversions

}
