namespace Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AASeq;

[TestClass]
public class VariablesTests {

    [TestMethod]
    public void Variables_Basic() {
        var vars = new Variables();
        vars["a"] = "a1";
        vars["A"] = "a2";
        Assert.AreEqual("a2", vars["a"]);
    }

    [TestMethod]
    public void Variables_Environment() {
        var vars = new Variables(Environment.GetEnvironmentVariables());
        Assert.IsTrue(vars["PATH"].Length > 0);
    }


    [TestMethod]
    public void Variables_SimpleReplacement() {  // $parameter
        var vars = new Variables();
        vars["Var"] = "Test";
        var nodes = AASeqNodes.Parse("A { B $VAR }");
        Assert.AreEqual("A { B Test }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_ParameterLength() {  // ${#parameter}
        var vars = new Variables();
        vars["Var"] = "Test";
        var nodes = AASeqNodes.Parse("A { B \"N${#VAR}\" }");
        Assert.AreEqual("A { B N4 }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_ReplaceDefault() {  // ${parameter:-word}
        var vars = new Variables();
        vars["Var"] = "Test";

        {
            var nodes = AASeqNodes.Parse("A { B \"${VAR:-unset}\" }");
            Assert.AreEqual("A { B Test }", vars.GetExpanded(nodes).ToString());
        }
        {
            var nodes = AASeqNodes.Parse("A { B \"${NVAR:-unset}\" }");
            Assert.AreEqual("A { B unset }", vars.GetExpanded(nodes).ToString());
        }
    }


    [TestMethod]
    public void Variables_AssignDefault() {  // ${parameter:=word}
        var vars = new Variables();
        vars["Var"] = "Test";

        {
            var nodes = AASeqNodes.Parse("A { B \"${VAR:=unset}\" }");
            Assert.AreEqual("A { B Test }", vars.GetExpanded(nodes).ToString());
        }
        {
            var nodes = AASeqNodes.Parse("A { B \"${NVAR:=unset}\" }");
            Assert.AreEqual("A { B unset }", vars.GetExpanded(nodes).ToString());
        }

        Assert.AreEqual("Test", vars["VAR"]);
        Assert.AreEqual("unset", vars["NVAR"]);
    }

    [TestMethod]
    public void Variables_NoReplaceDefault() {  // ${parameter:+word}
        var vars = new Variables();
        vars["Var"] = "Test";

        {
            var nodes = AASeqNodes.Parse("A { B \"${VAR:+unset}\" }");
            Assert.AreEqual("A { B unset }", vars.GetExpanded(nodes).ToString());
        }
        {
            var nodes = AASeqNodes.Parse("A { B \"${NVAR:+unset}\" }");
            Assert.AreEqual("A { B \"\" }", vars.GetExpanded(nodes).ToString());
        }

        Assert.AreEqual("Test", vars["VAR"]);
    }

    [TestMethod]
    public void Variables_ToUppercase() {  // ${parameter@U}
        var vars = new Variables();
        vars["Var"] = "tesT";

        var nodes = AASeqNodes.Parse("A { B \"${VAR@U}\" }");
        Assert.AreEqual("A { B TEST }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_ToTitlecase() {  // ${parameter@u}
        var vars = new Variables();
        vars["Var"] = "tesT";

        var nodes = AASeqNodes.Parse("A { B \"${VAR@u}\" }");
        Assert.AreEqual("A { B TesT }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_ToLowercase() {  // ${parameter@L}
        var vars = new Variables();
        vars["Var"] = "tesT";

        var nodes = AASeqNodes.Parse("A { B \"${VAR@L}\" }");
        Assert.AreEqual("A { B test }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_Substring() {  // ${parameter:offset}
        var vars = new Variables();
        vars["Var"] = "tesT";

        var nodes = AASeqNodes.Parse("A { B \"${VAR:2}\" }");
        Assert.AreEqual("A { B sT }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_Substring2() {  // ${parameter:offset:length}
        var vars = new Variables();
        vars["Var"] = "tesT";

        var nodes = AASeqNodes.Parse("A { B \"${VAR:2:1}\" }");
        Assert.AreEqual("A { B s }", vars.GetExpanded(nodes).ToString());
    }

    [TestMethod]
    public void Variables_Indirect() {  // ${!parameter}
        var vars = new Variables();
        vars["Var"] = "Test";
        vars["Indi"] = "Var";

        var nodes = AASeqNodes.Parse("A { B \"${!INDI}\" }");
        Assert.AreEqual("A { B Test }", vars.GetExpanded(nodes).ToString());
    }

}
