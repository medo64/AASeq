namespace AASeq;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

/// <summary>
/// AASeq value.
/// </summary>
[DebuggerDisplay("{Value,nq}")]
public sealed partial record AASeqValue {

    /// <summary>
    /// Creates a new null value.
    /// </summary>
    private AASeqValue() {
    }

    /// <summary>
    /// Creates a new value.
    /// </summary>
    /// <param name="value">Value.</param>
    public AASeqValue(Object? value) {
        Value = ValidateValue(value);
    }


    /// <summary>
    /// Gets if value is null.
    /// </summary>
    public bool IsNull {
        get { return Value is null; }
    }


    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public object? Value { get; private init; }


    /// <summary>
    /// Returns string representation.
    /// </summary>
    public override string? ToString() {
        return AsString();
    }


    private static AASeqValue _Null => new();
    /// <summary>
    /// Gets a null value.
    /// </summary>
    public static AASeqValue Null => _Null;


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
            Uri => value,             // uri
            Guid => value,            // uuid
            Byte[] => value,          // hex
            String => value,          // string
            _ => throw new ArgumentOutOfRangeException(nameof(value), "Unknown value type."),
        };
    }

    #endregion Helpers

}
