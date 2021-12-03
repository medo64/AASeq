using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// Int8 value.
/// </summary>
public sealed record TiniInt8Value : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniInt8Value(SByte value) {
        _value = value;
    }


    private SByte _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public SByte Value {
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
        return Value;
    }

    protected override Int16? ConvertToInt16() {
        return Value;
    }

    protected override Int32? ConvertToInt32() {
        return Value;
    }

    protected override Int64? ConvertToInt64() {
        return Value;
    }

    protected override Byte? ConvertToUInt8() {
        return Value is >= 0 ? (Byte)Value : null;
    }

    protected override UInt16? ConvertToUInt16() {
        return Value is >= 0 ? (UInt16)Value : null;
    }

    protected override UInt32? ConvertToUInt32() {
        return Value >= 0 ? (UInt32)Value : null;
    }

    protected override UInt64? ConvertToUInt64() {
        return Value >= 0 ? (UInt64)Value : null;
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
