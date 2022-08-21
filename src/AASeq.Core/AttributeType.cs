namespace AASeq;

public enum AttributeType {

    /// <summary>
    /// Empty value.
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Boolean value.
    /// Equivalent to System.Boolean.
    /// </summary>
    Boolean = 10,

    /// <summary>
    /// 8-bit signed integer.
    /// Equivalent to System.SByte.
    /// </summary>
    Int8 = 20,

    /// <summary>
    /// 16-bit signed integer.
    /// Equivalent to System.Int16.
    /// </summary>
    Int16 = 21,

    /// <summary>
    /// 32-bit signed integer.
    /// Equivalent to System.Int32.
    /// </summary>
    Int32 = 22,

    /// <summary>
    /// 64-bit signed integer.
    /// Equivalent to System.Int64.
    /// </summary>
    Int64 = 23,

    /// <summary>
    /// 8-bit unsigned integer.
    /// Equivalent to System.Byte.
    /// </summary>
    UInt8 = 30,

    /// <summary>
    /// 16-bit signed integer.
    /// Equivalent to System.UInt16.
    /// </summary>
    UInt16 = 31,

    /// <summary>
    /// 32-bit signed integer.
    /// Equivalent to System.UInt32.
    /// </summary>
    UInt32 = 32,

    /// <summary>
    /// 64-bit signed integer.
    /// Equivalent to System.UInt64.
    /// </summary>
    UInt64 = 33,

    /// <summary>
    /// 16-bit floating point number.
    /// Equivalent to System.Half.
    /// </summary>
    Float16 = 40,

    /// <summary>
    /// 32-bit floating point number.
    /// Equivalent to System.Single.
    /// </summary>
    Float32 = 41,

    /// <summary>
    /// 64-bit floating point number.
    /// Equivalent to System.Double.
    /// </summary>
    Float64 = 42,

    /// <summary>
    /// Unicode character.
    /// Equivalent to System.Char.
    /// </summary>
    Char = 50,

    /// <summary>
    /// Unicode string.
    /// Equivalent to System.String.
    /// </summary>
    String = 51,

    /// <summary>
    /// Binary bytes.
    /// Equivalent to System.Byte[].
    /// </summary>
    Binary = 60,

    /// <summary>
    /// Date and time combined.
    /// Equivalent to System.DateTimeOffset.
    /// </summary>
    DateTime = 70,

    /// <summary>
    /// Date.
    /// Equivalent to System.DateOnly.
    /// </summary>
    Date = 71,

    /// <summary>
    /// Time.
    /// Equivalent to System.TimeOnly.
    /// </summary>
    Time = 72,

    /// <summary>
    /// Duration.
    /// Equivalent to System.TimeSpan.
    /// </summary>
    Duration = 73,

    /// <summary>
    /// IP address.
    /// Equivalent to System.Net.IPAddress.
    /// </summary>
    IPAddress = 80,

    /// <summary>
    /// Collection of attributes.
    /// Equivalent to System.Collections.Generic.List&lt;Attribute&gt;.
    /// </summary>
    Collection = 1000,

    /// <summary>
    /// Array of attributes.
    /// Equivalent to Attribute[].
    /// </summary>
    Array = 1001,

}
