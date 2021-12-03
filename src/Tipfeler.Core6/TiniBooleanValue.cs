using System;

namespace Tipfeler;

/// <summary>
/// Boolean value.
/// </summary>
public sealed record TiniBooleanValue : TiniValue {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniBooleanValue(Boolean value) {
        _value = value;
    }


    private Boolean _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public Boolean Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean() {
        return Value;
    }

    protected override SByte? ConvertToInt8() {
        return Value ? (sbyte)1 : (sbyte)0;
    }

    protected override Int16? ConvertToInt16() {
        return Value ? (Int16)1 : (Int16)0;
    }

    protected override Int32? ConvertToInt32() {
        return Value ? 1 : 0;
    }

    protected override Int64? ConvertToInt64() {
        return Value ? 1 : 0;
    }

    protected override Byte? ConvertToUInt8() {
        return Value ? (byte)1 : (byte)0;
    }

    protected override UInt16? ConvertToUInt16() {
        return Value ? (UInt16)1 : (UInt16)0;
    }

    protected override UInt32? ConvertToUInt32() {
        return Value ? 1u : 0;
    }

    protected override UInt64? ConvertToUInt64() {
        return Value ? 1u : 0;
    }

    protected override Single? ConvertToFloat32() {
        return Value ? 1 : 0;
    }

    protected override Double? ConvertToFloat64() {
        return Value ? 1 : 0;
    }

    protected override String? ConvertToString() {
        return Value ? "True" : "False";
    }

    #endregion Convert

}
