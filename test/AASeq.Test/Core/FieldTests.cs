using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Net;

namespace AASeq.Test;

[TestClass]
public class FieldTests {

    [TestMethod]
    public void Field_Basic() {
        var x = new Field("Test", "Value");
        Assert.AreEqual("Test", x.Name);
        Assert.AreEqual("Value", x.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Field_NameCannotBeNull() {
        var x = new Field(null, "Value");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Field_NameCannotBeEmpty() {
        var x = new Field("", "Value");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Field_NameCannotBeChangedToNull() {
        var x = new Field("Test", "Something");
        x.Name = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Field_NameCannotBeChangedToEmpty() {
        var x = new Field("Test", "Something");
        x.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Field_ValueCannotBeNull() {
        var x = new Field("Test", null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Field_ValueCannotBeChangedToNull() {
        var x = new Field("Test", "Something");
        x.Value = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Field_ValidationFailed() {
        var x = new Field("3_", "Dummy");
    }

    [TestMethod]
    public void Field_ValueOverwritesSubfields() {
        var x = new Field("Test");
        x.Subfields.Add(new Field("Test"));
        x.Value = "";
        Assert.AreEqual("", x.Value);
        Assert.AreEqual(0, x.Subfields.Count);
        Assert.AreEqual(true, x.HasValue);
        Assert.AreEqual(false, x.HasSubfields);
    }

    [TestMethod]
    public void Field_SubfieldsOverwriteValue1() {
        var x = new Field("Test", "Value");
        x.Subfields.Clear();
        Assert.AreEqual(null, x.Value);
        Assert.AreEqual(0, x.Subfields.Count);
        Assert.AreEqual(false, x.HasValue);
        Assert.AreEqual(true, x.HasSubfields);
    }

    [TestMethod]
    public void Field_SubfieldsOverwriteValue2() {
        var x = new Field("Test", "Value");
        x.Subfields.Add(new Field("Test"));
        Assert.AreEqual(null, x.Value);
        Assert.AreEqual(1, x.Subfields.Count);
        Assert.AreEqual(false, x.HasValue);
        Assert.AreEqual(true, x.HasSubfields);
    }

    [TestMethod]
    public void Field_Clone1() {
        var s = new Field("Name", "Value");
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.Clone();
        s.Name = "NewName";
        s.Value = "NewValue";
        s.Subfields.Clear();
        s.Tags.Clear();

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual("Value", x.Value);
        Assert.AreEqual(true, x.HasValue);
        Assert.AreEqual(false, x.HasSubfields);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }

    [TestMethod]
    public void Field_Clone2() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.Clone();
        s.Name = "NewName";
        s.Subfields.Clear();
        s.Tags.Clear();
        s.Value = "NewValue";

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual(false, x.HasValue);
        Assert.AreEqual(true, x.HasSubfields);
        Assert.AreEqual(2, x.Subfields.Count);
        Assert.AreEqual("F1", x.Subfields[0].Name);
        Assert.AreEqual("V1", x.Subfields[0].Value);
        Assert.AreEqual("F2", x.Subfields[1].Name);
        Assert.AreEqual(false, x.Subfields[1].HasValue);
        Assert.AreEqual(true, x.Subfields[1].HasSubfields);
        Assert.AreEqual(2, x.Subfields[1].Subfields.Count);
        Assert.AreEqual("F21", x.Subfields[1].Subfields[0].Name);
        Assert.AreEqual("V21", x.Subfields[1].Subfields[0].Value);
        Assert.AreEqual("F22", x.Subfields[1].Subfields[1].Name);
        Assert.AreEqual(false, x.Subfields[1].Subfields[1].HasValue);
        Assert.AreEqual(true, x.Subfields[1].Subfields[1].HasSubfields);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }


    [TestMethod]
    public void Field_AsReadOnly1() {
        var s = new Field("Name", "Value");
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        s.Name = "NewName";
        s.Value = "NewValue";
        s.Subfields.Clear();
        s.Tags.Clear();

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual("Value", x.Value);
        Assert.AreEqual(true, x.HasValue);
        Assert.AreEqual(false, x.HasSubfields);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }

    [TestMethod]
    public void Field_AsReadOnly2() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        s.Name = "NewName";
        s.Subfields.Clear();
        s.Tags.Clear();
        s.Value = "NewValue";

        Assert.AreEqual("Name", x.Name);
        Assert.AreEqual(false, x.HasValue);
        Assert.AreEqual(true, x.HasSubfields);
        Assert.AreEqual(2, x.Subfields.Count);
        Assert.AreEqual("F1", x.Subfields[0].Name);
        Assert.AreEqual("V1", x.Subfields[0].Value);
        Assert.AreEqual("F2", x.Subfields[1].Name);
        Assert.AreEqual(false, x.Subfields[1].HasValue);
        Assert.AreEqual(true, x.Subfields[1].HasSubfields);
        Assert.AreEqual(2, x.Subfields[1].Subfields.Count);
        Assert.AreEqual("F21", x.Subfields[1].Subfields[0].Name);
        Assert.AreEqual("V21", x.Subfields[1].Subfields[0].Value);
        Assert.AreEqual("F22", x.Subfields[1].Subfields[1].Name);
        Assert.AreEqual(false, x.Subfields[1].Subfields[1].HasValue);
        Assert.AreEqual(true, x.Subfields[1].Subfields[1].HasSubfields);
        Assert.AreEqual("M1", x.Tags[0].Name);
        Assert.AreEqual("M2", x.Tags[1].Name);
        Assert.AreEqual(true, x.Tags[0].State);
        Assert.AreEqual(false, x.Tags[1].State);
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change1() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Name = "Test";
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change2() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Value = "Test";
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change3() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Subfields.Add(new Field("Test"));
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change4() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Tags[0].State = true;
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change5() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Subfields[0].Value = "x";
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void Field_AsReadOnly_Change6() {
        var s = new Field("Name");
        s.Subfields.Add(new Field("F1", "V1"));
        s.Subfields.Add(new Field("F2"));
        s.Subfields[1].Subfields.Add(new Field("F21", "V21"));
        s.Subfields[1].Subfields.Add(new Field("F22"));
        s.Tags.Add(new Tag("M1", true));
        s.Tags.Add(new Tag("M2", false));

        var x = s.AsReadOnly();
        x.Subfields[1].Subfields.Add(new Field("Test"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Field_SubfieldsCannotBeUsedTwice() {
        var f1 = new Field("Test");
        var f2 = new Field("Test");
        var sf1 = new Field("A", "1");
        var sf2 = new Field("B", "2");
        var sf3 = new Field("C", "3");

        f1.Subfields.Add(sf1);
        f1.Subfields.Add(sf2);
        f1.Subfields.Add(sf3);

        f2.Subfields.Add(sf2);
    }

    [TestMethod]
    public void Field_SubfieldsRelease() {
        var f1 = new Field("Test");
        var f2 = new Field("Test");
        var sf1 = new Field("A", "1");
        var sf2 = new Field("B", "2");
        var sf3 = new Field("C", "3");

        f1.Subfields.Add(sf1);
        f1.Subfields.Add(sf2);
        f1.Subfields.Add(sf3);
        f1.Subfields.Remove(sf2);

        f2.Subfields.Add(sf2);
    }

    [TestMethod]
    public void Field_SubieldReleaseDueToClear() {
        var f1 = new Field("Test");
        var f2 = new Field("Test");
        var sf1 = new Field("A", "1");
        var sf2 = new Field("B", "2");
        var sf3 = new Field("C", "3");

        f1.Subfields.Add(sf1);
        f1.Subfields.Add(sf2);
        f1.Subfields.Add(sf3);
        f1.Subfields.Clear();

        f2.Subfields.Add(sf1);
        f2.Subfields.Add(sf2);
        f2.Subfields.Add(sf3);
    }


    #region Value conversions

    [TestMethod]
    public void Field_Value_Byte() {
        {
            var x = new Field("Test");
            x.ValueAsByte = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual((byte)42, x.ValueAsByte);
        }
        {
            var x = new Field("Test", "0x00");
            Assert.AreEqual(Byte.MinValue, x.ValueAsByte);
        }
        {
            var x = new Field("Test", "0xFF");
            Assert.AreEqual(Byte.MaxValue, x.ValueAsByte);
        }
    }

    [TestMethod]
    public void Field_Value_ByteHex() {
        var x = new Field("Test", "0x2A");
        Assert.AreEqual((byte)42, x.ValueAsByte);
    }

    [TestMethod]
    public void Field_Value_ByteNot() {
        {
            var x = new Field("Test", "");
            Assert.AreEqual(null, x.ValueAsByte);
        }
        {
            var x = new Field("Test", "256");
            Assert.AreEqual(null, x.ValueAsByte);
        }
    }


    [TestMethod]
    public void Field_Value_Int16() {
        {
            var x = new Field("Test");
            x.ValueAsInt16 = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual(42, x.ValueAsInt16);
        }
        {
            var x = new Field("Test", "0x8000");
            Assert.AreEqual(Int16.MinValue, x.ValueAsInt16);
        }
        {
            var x = new Field("Test", "0x7FFF");
            Assert.AreEqual(Int16.MaxValue, x.ValueAsInt16);
        }
    }

    [TestMethod]
    public void Field_Value_Int16Hex() {
        {
            var x = new Field("Test", "0x2A");
            Assert.AreEqual((Int16)42, x.ValueAsInt16);
        }
        {
            var x = new Field("Test", "0xFFD6");
            Assert.AreEqual(-(Int16)42, x.ValueAsInt16);
        }
    }

    [TestMethod]
    public void Field_Value_Int16Not() {
        {
            var x = new Field("Test", "");
            Assert.AreEqual(null, x.ValueAsInt16);
        }
        {
            var x = new Field("Test", ((UInt32)Int16.MaxValue + 1).ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual(null, x.ValueAsInt16);
        }
    }


    [TestMethod]
    public void Field_Value_Int32() {
        {
            var x = new Field("Test");
            x.ValueAsInt32 = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual(42, x.ValueAsInt32);
        }
        {
            var x = new Field("Test", "0x80000000");
            Assert.AreEqual(Int32.MinValue, x.ValueAsInt32);
        }
        {
            var x = new Field("Test", "0x7FFFFFFF");
            Assert.AreEqual(Int32.MaxValue, x.ValueAsInt32);
        }
    }

    [TestMethod]
    public void Field_Value_Int32Hex() {
        {
            var x = new Field("Test", "0x2A");
            Assert.AreEqual(42, x.ValueAsInt32);
        }
        {
            var x = new Field("Test", "0xFFFFFFD6");
            Assert.AreEqual(-42, x.ValueAsInt32);
        }
    }

    [TestMethod]
    public void Field_Value_Int32Not() {
        {
            var x = new Field("Test", "");
            Assert.AreEqual(null, x.ValueAsInt32);
        }
        {
            var x = new Field("Test", ((UInt32)Int32.MaxValue + 1).ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual(null, x.ValueAsInt32);
        }
    }


    [TestMethod]
    public void Field_Value_Int64() {
        {
            var x = new Field("Test");
            x.ValueAsInt64 = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual(42, x.ValueAsInt64);
        }
        {
            var x = new Field("Test", "0x8000000000000000");
            Assert.AreEqual(Int64.MinValue, x.ValueAsInt64);
        }
        {
            var x = new Field("Test", "0x7FFFFFFFFFFFFFFF");
            Assert.AreEqual(Int64.MaxValue, x.ValueAsInt64);
        }
    }

    [TestMethod]
    public void Field_Value_Int64Hex() {
        {
            var x = new Field("Test", "0x2A");
            Assert.AreEqual((Int64)42, x.ValueAsInt64);
        }
        {
            var x = new Field("Test", "0xFFFFFFFFFFFFFFD6");
            Assert.AreEqual(-(Int64)42, x.ValueAsInt64);
        }
    }

    [TestMethod]
    public void Field_Value_Int64Not() {
        {
            var x = new Field("Test", "");
            Assert.AreEqual(null, x.ValueAsInt64);
        }
        {
            var x = new Field("Test", "9223372036854775808");
            Assert.AreEqual(null, x.ValueAsInt64);
        }
    }


    [TestMethod]
    public void Field_Value_Single() {
        {
            var x = new Field("Test");
            x.ValueAsSingle = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual((Single)42, x.ValueAsSingle);
        }
        {
            var x = new Field("Test", "-3.40282347E+38");
            Assert.AreEqual(Single.MinValue, x.ValueAsSingle);
        }
        {
            var x = new Field("Test", "3.40282347E+38");
            Assert.AreEqual(Single.MaxValue, x.ValueAsSingle);
        }
        {
            var x = new Field("Test", "1.401298E-45");
            Assert.AreEqual(Single.Epsilon, x.ValueAsSingle);
        }
    }

    [TestMethod]
    public void Field_Value_SingleNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsSingle);
    }


    [TestMethod]
    public void Field_Value_Double() {
        {
            var x = new Field("Test");
            x.ValueAsDouble = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual(42.0, x.ValueAsDouble);
        }
        {
            var x = new Field("Test", "-1.7976931348623157E+308");
            Assert.AreEqual(Double.MinValue, x.ValueAsDouble);
        }
        {
            var x = new Field("Test", "1.7976931348623157E+308");
            Assert.AreEqual(Double.MaxValue, x.ValueAsDouble);
        }
        {
            var x = new Field("Test", "4.94065645841247E-324");
            Assert.AreEqual(Double.Epsilon, x.ValueAsDouble);
        }
    }

    [TestMethod]
    public void Field_Value_DoubleNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsDouble);
    }


    [TestMethod]
    public void Field_Value_Boolean() {
        {
            var x = new Field("Test");
            x.ValueAsBoolean = true;
            Assert.AreEqual("True", x.Value);
            Assert.AreEqual(true, x.ValueAsBoolean);
        }
        {
            var x = new Field("Test", "FALSE");
            Assert.AreEqual(false, x.ValueAsBoolean);
        }
        {
            var x = new Field("Test", "TRUE");
            Assert.AreEqual(true, x.ValueAsBoolean);
        }
    }

    [TestMethod]
    public void Field_Value_BooleanNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsBoolean);
    }


    [TestMethod]
    public void Field_Value_Decimal() {
        {
            var x = new Field("Test");
            x.ValueAsDecimal = 42;
            Assert.AreEqual("42", x.Value);
            Assert.AreEqual((decimal)42, x.ValueAsDecimal);
        }
        {
            var x = new Field("Test", "-79228162514264337593543950335");
            Assert.AreEqual(Decimal.MinValue, x.ValueAsDecimal);
        }
        {
            var x = new Field("Test", "79228162514264337593543950335");
            Assert.AreEqual(Decimal.MaxValue, x.ValueAsDecimal);
        }
        {
            var x = new Field("Test", "1E-28");
            Assert.AreEqual(new Decimal(1, 0, 0, false, 28), x.ValueAsDecimal);
        }
    }

    [TestMethod]
    public void Field_Value_DecimalNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsDecimal);
    }


    [TestMethod]
    public void Field_Value_Time() {
        {
            var now = DateTime.UtcNow;
            var x = new Field("Test");
            x.ValueAsTime = now;
            Assert.AreEqual(now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK", CultureInfo.InvariantCulture), x.Value);
            Assert.AreEqual(now, x.ValueAsTime);
        }
        {
            var now = DateTime.Now;
            var x = new Field("Test");
            x.ValueAsTime = now;
            Assert.AreEqual(now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK", CultureInfo.InvariantCulture), x.Value);
            Assert.AreEqual(now, x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28");
            Assert.AreEqual(new DateTime(1979, 1, 28, 0, 0, 0, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28Z");
            Assert.AreEqual(new DateTime(1979, 1, 28, 0, 0, 0, DateTimeKind.Utc), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28 17:45");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28  17:45Z");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, DateTimeKind.Utc), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28 17:45:00");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28  17:45:00Z");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, DateTimeKind.Utc), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28 17:45:00.123");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, 123, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28 17:45:00.123Z");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, 123, DateTimeKind.Utc), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28T17:45:00.123");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, 123, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28T17:45:00.123Z");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, 123, DateTimeKind.Utc), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "1979-01-28 17:45+01");
            Assert.AreEqual(new DateTime(1979, 1, 28, 8, 45, 0, DateTimeKind.Local), x.ValueAsTime);
        }
        {
            var x = new Field("Test", "286393500");
            Assert.AreEqual(new DateTime(1979, 1, 28, 17, 45, 0, DateTimeKind.Utc), x.ValueAsTime);
        }
    }

    [TestMethod]
    public void Field_Value_TimeNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsTime);
    }


    [TestMethod]
    public void Field_Value_IPAddress() {
        {
            var x = new Field("Test");
            x.ValueAsIPAddress = IPAddress.Parse("127.0.0.1");
            Assert.AreEqual("127.0.0.1", x.Value);
            Assert.AreEqual(IPAddress.Parse("127.0.0.1"), x.ValueAsIPAddress);
        }
        {
            var x = new Field("Test", "192.168.0.1");
            Assert.AreEqual(IPAddress.Parse("192.168.0.1"), x.ValueAsIPAddress);
        }
        {
            var x = new Field("Test");
            x.ValueAsIPAddress = IPAddress.Parse("::1");
            Assert.AreEqual("::1", x.Value);
            Assert.AreEqual(IPAddress.Parse("::1"), x.ValueAsIPAddress);
        }
        {
            var x = new Field("Test", "FF01:0:0:0:0:0:0:152");
            Assert.AreEqual(IPAddress.Parse("FF01::152"), x.ValueAsIPAddress);
        }
    }

    [TestMethod]
    public void Field_Value_IPAddressNot() {
        var x = new Field("Test", "");
        Assert.AreEqual(null, x.ValueAsIPAddress);
    }

    #endregion

}
