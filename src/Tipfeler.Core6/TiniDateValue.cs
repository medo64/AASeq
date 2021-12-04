using System;
using System.Globalization;

namespace Tipfeler;

/// <summary>
/// Date value.
/// </summary>
public sealed record TiniDateValue : TiniValue {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="value">Value.</param>
    public TiniDateValue(DateOnly value) {
        _value = value;
    }


    private DateOnly _value;
    /// <summary>
    /// Gets/sets value.
    /// </summary>
    public DateOnly Value {
        get => _value;
        set {
            _value = value;
            OnChanged();
        }
    }


    #region Convert

    protected override Boolean? ConvertToBoolean()
        => null;

    protected override SByte? ConvertToInt8()
        => null;

    protected override Int16? ConvertToInt16()
        => null;

    protected override Int32? ConvertToInt32()
        => null;

    protected override Int64? ConvertToInt64()
        => ConvertToDateTime()?.ToUnixTimeSeconds();

    protected override Byte? ConvertToUInt8()
        => null;

    protected override UInt16? ConvertToUInt16()
        => null;

    protected override UInt32? ConvertToUInt32()
        => null;

    protected override UInt64? ConvertToUInt64() {
        var seconds = ConvertToDateTime()?.ToUnixTimeSeconds();
        return seconds >= 0 ? (UInt64)seconds : null;
    }

    protected override Single? ConvertToFloat32()
        => null;

    protected override Double? ConvertToFloat64()
        => ConvertToUInt64();

    protected override String? ConvertToString()
        => Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    protected override DateTimeOffset? ConvertToDateTime()
        => new DateTimeOffset(Value.Year, Value.Month, Value.Day, 0, 0, 0, 0, new TimeSpan());

    protected override DateOnly? ConvertToDate()
        => Value;

    protected override TimeOnly? ConvertToTime()
        => null;



    #endregion Convert

}
