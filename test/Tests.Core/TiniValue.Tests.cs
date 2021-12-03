using Tipfeler;
using Xunit;

namespace Tests.Core;

public class TiniValueTests {

    #region Implicit

    [Fact(DisplayName = "TiniValue: Implicit Boolean")]
    public void ImplicitBoolean() {
        TiniValue x = true;
        Assert.IsType<TiniBooleanValue>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int8")]
    public void ImplicitInt8() {
        TiniValue x = (sbyte)42;
        Assert.IsType<TiniInt8Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int16")]
    public void ImplicitInt16() {
        TiniValue x = (short)42;
        Assert.IsType<TiniInt16Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int32")]
    public void ImplicitInt32() {
        TiniValue x = 42;
        Assert.IsType<TiniInt32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Int64")]
    public void ImplicitInt64() {
        TiniValue x = (long)42;
        Assert.IsType<TiniInt64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt8")]
    public void ImplicitUInt8() {
        TiniValue x = (byte)42;
        Assert.IsType<TiniUInt8Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt16")]
    public void ImplicitUInt16() {
        TiniValue x = (ushort)42;
        Assert.IsType<TiniUInt16Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt32")]
    public void ImplicitUInt32() {
        TiniValue x = (uint)42;
        Assert.IsType<TiniUInt32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit UInt64")]
    public void ImplicitUInt64() {
        TiniValue x = (ulong)42;
        Assert.IsType<TiniUInt64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float32")]
    public void ImplicitFloat32() {
        TiniValue x = (float)42;
        Assert.IsType<TiniFloat32Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit Float64")]
    public void ImplicitFloat64() {
        TiniValue x = (double)42;
        Assert.IsType<TiniFloat64Value>(x);
    }

    [Fact(DisplayName = "TiniValue: Implicit String")]
    public void ImplicitString() {
        TiniValue x = "A";
        Assert.IsType<TiniStringValue>(x);
    }

    #endregion Implicit

    #region AsValue

    [Fact(DisplayName = "TiniValue: AsBooleanValue()")]
    public void AsValueBoolean() {
        TiniValue x = true;
        Assert.NotNull(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsInt8Value()")]
    public void AsInt8Value() {
        TiniValue x = (sbyte)32;
        Assert.Null(x.AsBooleanValue());
        Assert.NotNull(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsInt16Value()")]
    public void AsInt16Value() {
        TiniValue x = (short)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.NotNull(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsInt32Value()")]
    public void AsInt32Value() {
        TiniValue x = 32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.NotNull(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsInt64Value()")]
    public void AsInt64Value() {
        TiniValue x = (long)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.NotNull(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsUInt8Value()")]
    public void AsUInt8Value() {
        TiniValue x = (byte)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.NotNull(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsUInt16Value()")]
    public void AsUInt16Value() {
        TiniValue x = (ushort)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.NotNull(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsUInt32Value()")]
    public void AsUInt32Value() {
        TiniValue x = (uint)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.NotNull(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsUInt64Value()")]
    public void AsUInt64Value() {
        TiniValue x = (ulong)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.NotNull(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsFloat32Value()")]
    public void AsFloat32Value() {
        TiniValue x = (float)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.NotNull(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsFloat64Value()")]
    public void AsFloat64Value() {
        TiniValue x = (double)32;
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.NotNull(x.AsFloat64Value());
        Assert.Null(x.AsStringValue());
    }

    [Fact(DisplayName = "TiniValue: AsStringValue()")]
    public void AsStringValue() {
        TiniValue x = "A";
        Assert.Null(x.AsBooleanValue());
        Assert.Null(x.AsInt8Value());
        Assert.Null(x.AsInt16Value());
        Assert.Null(x.AsInt32Value());
        Assert.Null(x.AsInt64Value());
        Assert.Null(x.AsUInt8Value());
        Assert.Null(x.AsUInt16Value());
        Assert.Null(x.AsUInt32Value());
        Assert.Null(x.AsUInt64Value());
        Assert.Null(x.AsFloat32Value());
        Assert.Null(x.AsFloat64Value());
        Assert.NotNull(x.AsStringValue());
    }

    #endregion AsValue

    #region As

    [Fact(DisplayName = "TiniValue: AsBoolean()")]
    public void AsBoolean() {
        TiniValue x = true;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)1, x.AsInt8());
        Assert.Equal((short)1, x.AsInt16());
        Assert.Equal(1, x.AsInt32());
        Assert.Equal(1, x.AsInt64());
        Assert.Equal((byte)1, x.AsUInt8());
        Assert.Equal((ushort)1, x.AsUInt16());
        Assert.Equal((uint)1, x.AsUInt32());
        Assert.Equal((ulong)1, x.AsUInt64());
        Assert.Equal(1.0f, x.AsFloat32());
        Assert.Equal(1.0, x.AsFloat64());
        Assert.Equal("True", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsInt8()")]
    public void AsInt8() {
        TiniValue x = (sbyte)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsInt16()")]
    public void AsInt16() {
        TiniValue x = (short)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsInt32()")]
    public void AsInt32() {
        TiniValue x = 42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsInt64()")]
    public void AsInt64() {
        TiniValue x = (long)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsUInt8()")]
    public void AsUInt8() {
        TiniValue x = (byte)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsUInt16()")]
    public void AsUInt16() {
        TiniValue x = (ushort)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsUInt32()")]
    public void AsUInt32() {
        TiniValue x = (uint)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsUInt64()")]
    public void AsUInt64() {
        TiniValue x = (ulong)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: Float32()")]
    public void AsFloat32() {
        TiniValue x = (float)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: Float64()")]
    public void AsFloat64() {
        TiniValue x = (double)42;
        Assert.True(x.AsBoolean());
        Assert.Equal((sbyte)42, x.AsInt8());
        Assert.Equal((short)42, x.AsInt16());
        Assert.Equal(42, x.AsInt32());
        Assert.Equal(42, x.AsInt64());
        Assert.Equal((byte)42, x.AsUInt8());
        Assert.Equal((ushort)42, x.AsUInt16());
        Assert.Equal((uint)42, x.AsUInt32());
        Assert.Equal((ulong)42, x.AsUInt64());
        Assert.Equal(42.0f, x.AsFloat32());
        Assert.Equal(42.0, x.AsFloat64());
        Assert.Equal("42", x.AsString());
    }

    [Fact(DisplayName = "TiniValue: AsString()")]
    public void AsString() {
        TiniValue x = "A";
        Assert.Null(x.AsBoolean());
        Assert.Null(x.AsInt8());
        Assert.Null(x.AsInt16());
        Assert.Null(x.AsInt32());
        Assert.Null(x.AsInt64());
        Assert.Null(x.AsUInt8());
        Assert.Null(x.AsUInt16());
        Assert.Null(x.AsUInt32());
        Assert.Null(x.AsUInt64());
        Assert.Null(x.AsFloat32());
        Assert.Null(x.AsFloat64());
        Assert.Equal("A", x.AsString());
    }

    #endregion AsValue

}
