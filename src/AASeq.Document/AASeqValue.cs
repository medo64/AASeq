namespace AASeq;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

/// <summary>
/// AASeq value.
/// </summary>
[DebuggerDisplay("{Value,nq}")]
public sealed partial class AASeqValue {

    /// <summary>
    /// Creates a new value.
    /// </summary>
    /// <param name="value">Value.</param>
    public AASeqValue(Object? value) {
        Value = ValidateValue(value);
    }


    private object? _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public object? Value {
        get { return _value; }
        set { _value = ValidateValue(value); }
    }


    /// <summary>
    /// Returns string representation.
    /// </summary>
    public override string? ToString() {
        return AsString();
    }


    /// <summary>
    /// Gets a null value.
    /// </summary>
    public static AASeqValue Null => new(null);


    #region Helpers

    private static object? ValidateValue(object? value) {
        return value switch {
            null => null,
            Boolean => value,         // bool
            SByte => value,           // i8
            Byte => value,            // u8
            Int16 => value,           // i16
            UInt16 => value,          // u16
            Int32 => value,           // i32
            UInt32 => value,          // u32
            Int64 => value,           // i64
            UInt64 => value,          // u64
            Int128 => value,          // i128
            UInt128 => value,         // u128
            Half => value,            // f16
            Single => value,          // f32
            Double => value,          // f64
            Decimal => value,         // d128
            DateTimeOffset => value,  // datetime
            DateOnly => value,        // date
            TimeOnly => value,        // time
            TimeSpan => value,        // duration
            IPAddress => value,       // ip
            Regex => value,           // regex
            Uri => value,             // uri
            Guid => value,            // uuid
            Byte[] => value,          // hex
            String => value,          // string
            _ => throw new ArgumentOutOfRangeException(nameof(value), "Unknown value type."),
        };
    }

    #endregion Helpers

}
