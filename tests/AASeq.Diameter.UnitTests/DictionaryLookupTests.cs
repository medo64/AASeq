namespace Tests;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AASeq.Diameter;

[TestClass]
public sealed class DictionaryLookupTests {

    [TestMethod]
    public void DictionaryLookup_VendorByCode() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindVendorByCode(10415));
        Assert.IsNull(DictionaryLookup.Instance.FindVendorByCode(5104));
    }

    [TestMethod]
    public void DictionaryLookup_VendorByName() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindVendorByName("3GPP"));
        Assert.IsNull(DictionaryLookup.Instance.FindVendorByName("Unknown"));
    }


    [TestMethod]
    public void DictionaryLookup_ApplicationById() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindApplicationById(0));
        Assert.IsNull(DictionaryLookup.Instance.FindApplicationById(5104));
    }

    [TestMethod]
    public void DictionaryLookup_ApplicationByName() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindApplicationByName("Common"));
        Assert.IsNotNull(DictionaryLookup.Instance.FindApplicationByName("Gx"));
        Assert.IsNull(DictionaryLookup.Instance.FindApplicationByName("Unknown"));
    }


    [TestMethod]
    public void DictionaryLookup_CommandById() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindCommandByCode(257));
        Assert.IsNull(DictionaryLookup.Instance.FindCommandByCode(5104));
    }

    [TestMethod]
    public void DictionaryLookup_CommandByName() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindCommandByName("Capabilities-Exchange"));
        Assert.IsNotNull(DictionaryLookup.Instance.FindCommandByName("Push-Notification"));
        Assert.IsNull(DictionaryLookup.Instance.FindCommandByName("Unknown"));
    }


    [TestMethod]
    public void DictionaryLookup_AvpByCode() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindAvpByCode(0, 1));
        Assert.IsNotNull(DictionaryLookup.Instance.FindAvpByCode(10415, 100));
        Assert.IsNull(DictionaryLookup.Instance.FindAvpByCode(5104, 5104));
    }

    [TestMethod]
    public void DictionaryLookup_AvpByName() {
        Assert.IsNotNull(DictionaryLookup.Instance.FindAvpByName("Authentication-Information-SIM"));
        Assert.IsNotNull(DictionaryLookup.Instance.FindAvpByName("Framed-Route"));
        Assert.IsNull(DictionaryLookup.Instance.FindAvpByName("Unknown"));
    }

}
