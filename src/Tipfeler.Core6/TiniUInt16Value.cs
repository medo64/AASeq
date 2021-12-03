using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// UInt16 value.
/// </summary>
public sealed record TiniUInt16Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniUInt16Value(UInt16 value) {
        _value = value;
    }


    private UInt16 _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public UInt16 Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean() {
        return Value != 0;
    }

    protected override SByte? ConvertToInt8() {
        return Value is <= (Byte)SByte.MaxValue ? (SByte)Value : null;
    }

    protected override Int16? ConvertToInt16() {
        return Value is <= (UInt16)Int16.MaxValue ? (Int16)Value : null;
    }

    protected override Int32? ConvertToInt32() {
        return Value;
    }

    protected override Int64? ConvertToInt64() {
        return Value;
    }

    protected override Byte? ConvertToUInt8() {
        return Value is <= Byte.MaxValue ? (Byte)Value : null;
    }

    protected override UInt16? ConvertToUInt16() {
        return Value;
    }

    protected override UInt32? ConvertToUInt32() {
        return Value;
    }

    protected override UInt64? ConvertToUInt64() {
        return Value;
    }

    protected override Single? ConvertToFloat32() {
        return Value;
    }

    protected override Double? ConvertToFloat64() {
        return Value;
    }

    protected override String? ConvertToString() {
        return Value.ToString("0", CultureInfo.InvariantCulture);
    }

    #endregion Convert

}
