namespace AASeq;
using System;
using System.Diagnostics;
using System.Net;

/// <summary>
/// AASeq value.
/// </summary>
[DebuggerDisplay("{RawValue,nq}")]
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
        RawValue = ValidateValue(value);
    }


    /// <summary>
    /// Gets if value is null.
    /// </summary>
    public bool IsNull {
        get { return RawValue is null; }
    }

    /// <summary>
    /// Gets if value is positive infinity.
    /// </summary>
    public bool IsPositiveInfinity {
        get {
            if (RawValue is Double f64) {
                return Double.IsPositiveInfinity(f64);
            } else if (RawValue is Single f32) {
                return Single.IsPositiveInfinity(f32);
            } else if (RawValue is Half f16) {
                return Half.IsPositiveInfinity(f16);
            }
            return false;
        }
    }

    /// <summary>
    /// Gets if value is negative infinity.
    /// </summary>
    public bool IsNegativeInfinity {
        get {
            if (RawValue is Double f64) {
                return Double.IsNegativeInfinity(f64);
            } else if (RawValue is Single f32) {
                return Single.IsNegativeInfinity(f32);
            } else if (RawValue is Half f16) {
                return Half.IsNegativeInfinity(f16);
            }
            return false;
        }
    }


    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public object? RawValue { get; private init; }


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
