namespace WiresharkDictImport;
using System;
using System.Globalization;
using System.IO;
using AASeq;
using AASeqPlugin;

internal static class App {

    internal static void Main(string[] args) {
        var doc = new WiresharkDictionaryLookup(File.OpenRead("../../tools/WiresharkDictImport/data/wireshark-4.4.6/dictionary.xml"));

        Console.WriteLine();
        Console.WriteLine($"Vendors ....: {doc.Vendors.Count,4}");
        Console.WriteLine($"Applications: {doc.Applications.Count,4}");
        Console.WriteLine($"Commands ...: {doc.Commands.Count,4}");
        Console.WriteLine($"AVPs .......: {doc.Avps.Count,4}");

        var newDoc = new AASeqNodes();
        var rootNode = new AASeqNode("DiameterDictionary");
        newDoc.Add(rootNode);

        foreach (var vendor in doc.Vendors) {
            var vendorNode = new AASeqNode("Vendor", vendor.Name);
            vendorNode.Properties.Add("code", vendor.Code.ToString(CultureInfo.InvariantCulture));
            rootNode.Nodes.Add(vendorNode);
        }

        foreach (var application in doc.Applications) {
            var applicationNode = new AASeqNode("Application", application.Name);
            applicationNode.Properties.Add("id", application.Id.ToString(CultureInfo.InvariantCulture));
            rootNode.Nodes.Add(applicationNode);
        }

        foreach (var command in doc.Commands) {
            var commandNode = new AASeqNode("Command", command.Name);
            commandNode.Properties.Add("code", command.Code.ToString(CultureInfo.InvariantCulture));
            rootNode.Nodes.Add(commandNode);
        }

        foreach (var avp in doc.Avps) {
            var avpNode = new AASeqNode("Avp", avp.Name);
            avpNode.Properties.Add("code", avp.Code.ToString(CultureInfo.InvariantCulture));
            if (avp.Vendor is not null) { avpNode.Properties.Add("vendor", avp.Vendor.Name); }
            switch (avp.MandatoryBit) {
                case AvpBitState.Must: avpNode.Properties.Add("mandatoryBit", "must"); break;
                case AvpBitState.May: avpNode.Properties.Add("mandatoryBit", "may"); break;
                case AvpBitState.MustNot: avpNode.Properties.Add("mandatoryBit", "mustnot"); break;
            }
            avpNode.Properties.Add("type", avp.AvpType.ToString());
            if (avp.AvpType == AvpType.Enumerated) {
                foreach (var e in avp.Enums) {
                    var enumNode = new AASeqNode("Enum", e.Name);
                    enumNode.Properties.Add("code", e.Code.ToString(CultureInfo.InvariantCulture));
                    avpNode.Nodes.Add(enumNode);
                }
            }
            rootNode.Nodes.Add(avpNode);
        }

        newDoc.Save(File.OpenWrite("../../tools/WiresharkDictImport/data/DiameterDictionary.aaseq"));
    }
}
