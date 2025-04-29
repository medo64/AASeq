namespace DiamDictCheck;
using System;
using System.Collections.Generic;
using System.IO;
using AASeq;

internal static class App {

    internal static void Main(string[] args) {
        try {
            var countVendor = 0;
            var countApplication = 0;
            var countCommand = 0;
            var countAvp = 0;

            var vendorsPerName = new Dictionary<string, object?>();
            var vendorsPerCode = new Dictionary<int, object?>();
            var applicationsPerName = new Dictionary<string, object?>();
            var applicationsPerId = new Dictionary<uint, object?>();
            var commandsPerName = new Dictionary<string, object?>();
            var commandsPerCode = new Dictionary<int, object?>();
            var commandsPerAbbrev = new Dictionary<string, object?>();
            var avpsPerVendorAndName = new Dictionary<(string, string), object?>();
            var avpsPerVendorAndCode = new Dictionary<(string, int), object?>();

            foreach (var file in Directory.GetFiles("../../src/AASeq.Plugin.Diameter/Assets/", "*.aaseq")) {
                Console.WriteLine($"Processing {file}...");
                var doc = AASeqNodes.Load(file);

                if ((doc.Count != 1) || (!"DiameterDictionary".Equals(doc[0].Name))) {
                    WriteError("Expecting single node named DiameterDictionary.");
                }

                foreach (var node in doc[0].Nodes) {
                    node.Save(Console.Out);

                    if (node.Name.Equals("Vendor")) {
                        countVendor++;

                        if (node.Value.RawValue is not string) { WriteError($"Node '{node.Value} is not string."); }
                        if (!int.TryParse(node.Properties["code"], out var vendorCode)) { WriteError($"Unexpected code for '{node.Value}."); }
                        if (node.Properties.Count != 1) { WriteError($"Unexpected property count for '{node.Value}."); }

                        vendorsPerName.Add(node.Value.AsString(""), null);
                        vendorsPerCode.Add(vendorCode, null);

                    } else if (node.Name.Equals("Application")) {
                        countApplication++;

                        if (node.Value.RawValue is not string) { WriteError($"Node '{node.Value} is not string."); }
                        if (!uint.TryParse(node.Properties["id"], out var appId)) { WriteError($"Unexpected id for '{node.Value}."); }
                        if (node.Properties.Count != 1) { WriteError($"Unexpected property count for '{node.Value}."); }
                        if (node.Nodes.Count != 0) { WriteError($"Unexpected subnode for '{node.Value}."); }

                        applicationsPerId.Add(appId, null);
                        applicationsPerName.Add(node.Value.AsString(""), null);

                    } else if (node.Name.Equals("Command")) {
                        countCommand++;

                        if (node.Value.RawValue is not string) { WriteError($"Node '{node.Value} is not string."); }
                        if (!int.TryParse(node.Properties["code"], out var commandCode)) { WriteError($"Unexpected code for '{node.Value}."); }
                        if (node.Properties.Count != 2) {
                            WriteError($"Unexpected property count for '{node.Value}.");
                        }
                        if (node.Nodes.Count != 0) { WriteError($"Unexpected subnode for '{node.Value}."); }

                        commandsPerCode.Add(commandCode, null);
                        commandsPerName.Add(node.Value.AsString(""), null);

                        var abbrev = node.Properties.GetPropertyValue("abbrev", "");
                        if (!string.IsNullOrEmpty(abbrev)) {
                            commandsPerAbbrev.Add(abbrev, null);
                        }

                    } else if (node.Name.Equals("Avp")) {
                        countAvp++;

                        if (node.Value.RawValue is not string) { WriteError($"Node '{node.Value} is not string."); }
                        if (!int.TryParse(node.Properties["code"], out var avpCode)) { WriteError($"Unexpected code for '{node.Value}."); }
                        if (!"must".Equals(node.Properties["mandatoryBit"]) && !"may".Equals(node.Properties["mandatoryBit"]) && !"mustnot".Equals(node.Properties["mandatoryBit"])) { WriteError($"Unexpected mandatory={node.Properties["mandatoryBit"]} for '{node.Value}."); }
                        if (node.Properties.Count == 3) {
                        } else if (node.Properties.Count == 4) {
                            if (!vendorsPerName.TryGetValue(node.Properties["vendor"], out var _)) { WriteError($"Unexpected vendor='{node.Properties["vendor"]}' for '{node.Value}."); }
                        } else {
                            WriteError($"Unexpected property count for '{node.Value}.");
                        }

                        switch (node.Properties["type"]) {
                            case "OctetString":
                            case "Integer32":
                            case "Integer64":
                            case "Unsigned64":
                            case "Float32":
                            case "Float64":
                            case "Address":
                            case "Time":
                            case "UTF8String":
                            case "DiameterIdentity":
                            case "DiameterURI":
                            case "IPFilterRule":
                            case "QoSFilterRule":
                                if (node.Nodes.Count != 0) { WriteError($"Unexpected subnode for '{node.Value}."); }
                                break;
                            case "Unsigned32":
                                if (node.Nodes.Count != 0) {
                                    foreach (var subnode in node.Nodes) {
                                        if (subnode.Name.Equals("Bitmask") || subnode.Name.Equals("Enum")) {
                                            if (!int.TryParse(subnode.Properties["code"], out var _)) { WriteError($"Unexpected subcode for '{node.Value}."); }
                                            if (subnode.Properties.Count != 1) { WriteError($"Unexpected subproperty count for '{node.Value}."); }
                                        } else {
                                            WriteError($"Subnode '{subnode.Value}' of '{node.Value} is not Bitmask or Enum.");
                                        }
                                    }
                                }
                                break;
                            case "Grouped":
                                foreach (var subnode in node.Nodes) {
                                    if (!subnode.Name.Equals("Avp")) { WriteError($"Subnode '{subnode.Value}' of '{node.Value} is not Avp."); }
                                    if (subnode.Nodes.Count > 0) { WriteError($"Unexpected subnode count for '{node.Value}."); }
                                }
                                break;
                            case "Enumerated":
                                foreach (var subnode in node.Nodes) {
                                    if (!subnode.Name.Equals("Enum")) { WriteError($"Subnode '{subnode.Value}' of '{node.Value} is not Enum."); }
                                    if (subnode.Nodes.Count > 0) { WriteError($"Unexpected subnode count for '{node.Value}."); }
                                }
                                break;
                            default:
                                WriteError($"Unexpected type={node.Properties["type"]} for '{node.Value}.");
                                break;
                        }

                        avpsPerVendorAndCode.Add((node.Properties.GetPropertyValue("vendor", ""), avpCode), null);
                        avpsPerVendorAndName.Add((node.Properties.GetPropertyValue("vendor", ""), node.Value.AsString("")), null);

                    } else {
                        WriteError($"Unrecognized node type '{node.Name}'.");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to save file");
                Console.ReadLine();
                doc.Save(file);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Processed {countVendor} vendors, {countApplication} applications, {countCommand} commands, {countAvp} AVPs");
            Console.ResetColor();
        } catch (Exception ex) {
            WriteError(ex.Message);
        }
    }

    private static void WriteError(string text) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
        Environment.Exit(1);
    }

}
