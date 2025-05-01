namespace WiresharkDictImport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using AASeqPlugin;
using static System.Net.Mime.MediaTypeNames;

internal class WiresharkDictionaryLookup {

    public WiresharkDictionaryLookup(string fileName) {
        using var stream = File.OpenRead(fileName);
        var dirName = Path.GetDirectoryName(fileName);

        var doc = new XmlDocument();
        doc.XmlResolver = new XmlResourceResolver(dirName);
        doc.Load(stream);
        if (doc.DocumentElement is null) { throw new InvalidOperationException("Cannot load XML document."); }

        var nodes = doc.DocumentElement.ChildNodes;
        XmlNode iRootNode = doc.SelectSingleNode("/dictionary") ?? throw new InvalidOperationException("Cannot load XML document.");
        if (iRootNode == null) { throw new FormatException("Invalid root element."); }

        var vendorsByCode = new Dictionary<UInt32, VendorDictionaryEntry>();
        var vendorsById = new Dictionary<String, VendorDictionaryEntry>();
        var applicationsById = new Dictionary<UInt32, ApplicationDictionaryEntry>();
        var applicationsByName = new Dictionary<String, ApplicationDictionaryEntry>();
        var commandsByCode = new Dictionary<UInt32, CommandDictionaryEntry>();
        var commandsByName = new Dictionary<String, CommandDictionaryEntry>();
        var avpsByCode = new Dictionary<(UInt32, UInt32), AvpDictionaryEntry>();
        var avpsByName = new Dictionary<String, AvpDictionaryEntry>();

        ParseVendorEntries(iRootNode.ChildNodes, iRootNode.LocalName, vendorsByCode, vendorsById);  // doing vendors first to avoid XML ordering errors
        ParseApplicationEntries(iRootNode.ChildNodes, iRootNode.LocalName, applicationsById, applicationsByName, avpsByCode, avpsByName, vendorsById);
        ParseCommandEntries(iRootNode.ChildNodes, iRootNode.LocalName, commandsByCode, commandsByName, vendorsById);
        ParseAvpEntries(iRootNode.ChildNodes, iRootNode.LocalName, avpsByCode, avpsByName, vendorsById);

        var vendors = new List<VendorDictionaryEntry>(vendorsById.Values);
        vendors.Sort((a, b) => a.Code.CompareTo(b.Code));
        Vendors = vendors.AsReadOnly();

        var applications = new List<ApplicationDictionaryEntry>(applicationsById.Values);
        applications.Sort((a, b) => a.Id.CompareTo(b.Id));
        Applications = applications.AsReadOnly();

        var commands = new List<CommandDictionaryEntry>(commandsByCode.Values);
        commands.Sort((a, b) => a.Code.CompareTo(b.Code));
        Commands = commands.AsReadOnly();

        var avps = new List<AvpDictionaryEntry>(avpsByCode.Values);
        avps.Sort((a, b) => {
            var vendorA = a.Vendor?.Code ?? 0;
            var vendorB = b.Vendor?.Code ?? 0;
            if (vendorA == vendorB) {
                return a.Code.CompareTo(b.Code);
            } else {
                return vendorA.CompareTo(vendorB);
            }
        });
        Avps = avps.AsReadOnly();
    }

    public IList<VendorDictionaryEntry> Vendors { get; }
    public IList<ApplicationDictionaryEntry> Applications { get; }
    public IList<CommandDictionaryEntry> Commands { get; }
    public IList<AvpDictionaryEntry> Avps { get; }


    public static void Load(string fileName) {
    }


    private static void ParseVendorEntries(XmlNodeList nodes, string rootNodeLocalName, Dictionary<UInt32, VendorDictionaryEntry> vendorsByCode, IDictionary<String, VendorDictionaryEntry> vendorsById) {
        foreach (XmlNode node in nodes) {
            switch (node.LocalName) {
                case "vendor": {
                        var vendorIdStr = GetAttributeValue(node, "vendor-id");
                        var codeStr = GetAttributeValue(node, "code");
                        if ("None".Equals(vendorIdStr)) { continue; }

                        var code = UInt32.Parse(codeStr, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        var name = vendorIdStr.Trim().Replace("_", "").Trim();

                        var vendor = new VendorDictionaryEntry(name, code);
                        Console.WriteLine($"VENDOR      {vendor.Code,10} {vendor.Name}");

                        vendorsByCode.Add(vendor.Code, vendor);
                        vendorsById.Add(vendor.Name, vendor);
                    }
                    break;

                case "base":
                    ParseVendorEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, vendorsByCode, vendorsById);
                    break;

                default: {
                        if (node is XmlEntityReference) {
                            ParseVendorEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, vendorsByCode, vendorsById);
                        }
                    }
                    break;
            }
        }
    }


    private static void ParseApplicationEntries(XmlNodeList nodes, string rootNodeLocalName, IDictionary<UInt32, ApplicationDictionaryEntry> applicationById, IDictionary<String, ApplicationDictionaryEntry> applicationByName, IDictionary<(UInt32, UInt32), AvpDictionaryEntry> avpsByCode, IDictionary<String, AvpDictionaryEntry> avpsByName, Dictionary<String, VendorDictionaryEntry> vendorsById) {
        foreach (XmlNode node in nodes) {
            switch (node.LocalName) {
                case "application": {
                        var idStr = GetAttributeValue(node, "id");
                        var nameStr = GetAttributeValue(node, "name", "");

                        var id = UInt32.Parse(idStr, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        var name = nameStr.Trim();

                        var application = new ApplicationDictionaryEntry(name, id);
                        Console.WriteLine($"APPLICATION {application.Id,10} {application.Name}");

                        if (applicationByName.ContainsKey(application.Name)) {
                            var oldApplication = applicationByName[application.Name];
                            applicationById.Remove(oldApplication.Id);
                            applicationByName.Remove(oldApplication.Name);
                            if (oldApplication.Id > application.Id) {  // higher number keeps its name
                                application = new ApplicationDictionaryEntry(application.Name + "#" + application.Id.ToString(CultureInfo.InvariantCulture), application.Id);
                                applicationById.Add(application.Id, application);
                                applicationByName.Add(application.Name, application);
                            } else {  // lower number gets its name changed
                                oldApplication = new ApplicationDictionaryEntry(oldApplication.Name + "#" + oldApplication.Id.ToString(CultureInfo.InvariantCulture), oldApplication.Id);
                                applicationById.Add(oldApplication.Id, oldApplication);
                                applicationByName.Add(oldApplication.Name, oldApplication);
                                applicationById.TryAdd(application.Id, application);
                                applicationByName.Add(application.Name, application);
                            }

                        } else {
                            applicationById.TryAdd(application.Id, application);
                            applicationByName.Add(application.Name, application);
                        }
                        ParseAvpEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, avpsByCode, avpsByName, vendorsById);
                    }
                    break;

                case "base":
                    ParseApplicationEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, applicationById, applicationByName, avpsByCode, avpsByName, vendorsById);
                    break;

                default: {
                        if (node is XmlEntityReference) {
                            ParseApplicationEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, applicationById, applicationByName, avpsByCode, avpsByName, vendorsById);
                        }
                    }
                    break;
            }
        }
    }


    private static void ParseCommandEntries(XmlNodeList nodes, string rootNodeLocalName, IDictionary<UInt32, CommandDictionaryEntry> commandsByCode, IDictionary<String, CommandDictionaryEntry> commandsByName, Dictionary<String, VendorDictionaryEntry> vendorsById) {
        foreach (XmlNode node in nodes) {
            switch (node.LocalName) {
                case "command": {
                        var codeStr = GetAttributeValue(node, "code");
                        var nameStr = GetAttributeValue(node, "name");
                        var vendorIdStr = GetAttributeValue(node, "vendor-id", "");
                        if ("None".Equals(vendorIdStr)) { vendorIdStr = ""; }

                        var code = UInt32.Parse(codeStr, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        var name = nameStr.Trim().Replace(' ', '-');
                        var vendorId = vendorIdStr.Trim();
                        var vendor = string.IsNullOrEmpty(vendorId) ? null : vendorsById[vendorId];

                        var command = new CommandDictionaryEntry(name, code);
                        Console.WriteLine($"COMMAND     {command.Code,10} {command.Name}");
                        commandsByCode.Add(command.Code, command);
                        commandsByName.Add(command.Name, command);
                    }
                    break;

                case "base":
                    ParseCommandEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, commandsByCode, commandsByName, vendorsById);
                    break;

                default: {
                        if (node is XmlEntityReference) {
                            ParseCommandEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, commandsByCode, commandsByName, vendorsById);
                        }
                    }
                    break;
            }
        }
    }


    private static void ParseAvpEntries(XmlNodeList nodes, string rootNodeLocalName, IDictionary<(UInt32, UInt32), AvpDictionaryEntry> avpsByCode, IDictionary<String, AvpDictionaryEntry> avpsByName, Dictionary<String, VendorDictionaryEntry> vendorsById) {
        foreach (XmlNode node in nodes) {
            switch (node.LocalName) {
                case "avp": {
                        var codeStr = GetAttributeValue(node, "code");
                        var nameStr = GetAttributeValue(node, "name", "");
                        var vendorStr = GetAttributeValue(node, "vendor-bit");
                        var mandatoryStr = GetAttributeValue(node, "mandatory");
                        var protectedStr = GetAttributeValue(node, "protected");
                        var mayEncryptStr = GetAttributeValue(node, "may-encrypt");
                        var vendorIdStr = GetAttributeValue(node, "vendor-id", "");
                        if ("None".Equals(vendorIdStr)) { vendorIdStr = ""; }

                        var avpEnumDict = new Dictionary<String, AvpEnumDictionaryEntry>();
                        var typeNameStr = "";
                        foreach (XmlNode avpNode in node.ChildNodes) {
                            switch (avpNode.LocalName) {
                                case "type":
                                    if (string.IsNullOrEmpty(typeNameStr)) {
                                        typeNameStr = GetAttributeValue(avpNode, "type-name");
                                    }
                                    break;

                                case "enum":
                                    typeNameStr = "Enumerated";
                                    var avpEnumNameStr = GetAttributeValue(avpNode, "name");
                                    var avpEnumCodeStr = GetAttributeValue(avpNode, "code");
                                    if ("Undefined".Equals(avpEnumNameStr, StringComparison.Ordinal)) { continue; }
                                    if ("Reserved".Equals(avpEnumNameStr, StringComparison.Ordinal)) { continue; }

                                    var avpEnumName = avpEnumNameStr.Trim().Replace(' ', '_').Replace('-', '_').ToUpperInvariant();
                                    int avpEnumCode;
                                    if (!Int32.TryParse(avpEnumCodeStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out avpEnumCode)) {
                                        avpEnumCode = (Int32)(UInt32.Parse(avpEnumCodeStr, NumberStyles.Integer, CultureInfo.InvariantCulture));
                                    }

                                    if (avpEnumDict.ContainsKey(avpEnumName)) {
                                        var avpEnum = new AvpEnumDictionaryEntry(avpEnumName + "#" + avpEnumCode.ToString(CultureInfo.InvariantCulture), avpEnumCode);
                                        avpEnumDict.Add(avpEnum.Name, avpEnum);
                                    } else {
                                        var avpEnum = new AvpEnumDictionaryEntry(avpEnumName, avpEnumCode);
                                        avpEnumDict.Add(avpEnum.Name, avpEnum);
                                    }
                                    break;

                                case "grouped":
                                    typeNameStr = "Grouped";
                                    break;
                            }
                        }

                        var code = UInt32.Parse(codeStr, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        var name = nameStr.Trim();
                        var vendorId = vendorIdStr.Trim();
                        var vendor = string.IsNullOrEmpty(vendorId) ? null : vendorsById[vendorId];
                        var mandatoryBit = string.Equals(mandatoryStr, "must", StringComparison.OrdinalIgnoreCase) ? AvpBitState.Must : string.Equals(mandatoryStr, "mustnot", StringComparison.OrdinalIgnoreCase) ? AvpBitState.MustNot : AvpBitState.May;
                        var protectedBit = string.Equals(protectedStr, "must", StringComparison.OrdinalIgnoreCase) ? AvpBitState.Must : string.Equals(protectedStr, "mustnot", StringComparison.OrdinalIgnoreCase) ? AvpBitState.MustNot : AvpBitState.May;
                        var mayEncrypt = string.Equals(mayEncryptStr, "true", StringComparison.OrdinalIgnoreCase);
                        var avpType = GetAvpType(typeNameStr);

                        AvpDictionaryEntry avp;
                        if (avpType == AvpType.Enumerated) {
                            var avpEnumList = new List<AvpEnumDictionaryEntry>(avpEnumDict.Values);
                            avpEnumList.Sort((a, b) => a.Code.CompareTo(b.Code));
                            avp = new AvpDictionaryEntry(name, code, mandatoryBit, vendor, avpEnumList);
                        } else {
                            avp = new AvpDictionaryEntry(name, code, mandatoryBit, vendor, avpType);
                        }
                        if (avp.Vendor is null) {
                            Console.WriteLine($"AVP         {avp.Code,10} {avp.Name} ({avp.AvpType})");
                        } else {
                            Console.WriteLine($"AVP         {avp.Code,10} {avp.Name} ({avp.AvpType}) [{avp.Vendor.Name}]");
                        }
                        if (avpType == AvpType.Enumerated) {
                            foreach (var avpEnum in avp.Enums) {
                                Console.WriteLine($"            {avpEnum.Code,10} {avpEnum.Name}");
                            }
                        }
                        avpsByCode.Add((avp.Vendor?.Code ?? 0, avp.Code), avp);
                        avpsByName.Add(avp.Name, avp);
                    }
                    break;

                case "base":
                    ParseAvpEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, avpsByCode, avpsByName, vendorsById);
                    break;

                default: {
                        if (node is XmlEntityReference) {
                            ParseAvpEntries(node.ChildNodes, rootNodeLocalName + "/" + node.LocalName, avpsByCode, avpsByName, vendorsById);
                        }
                    }
                    break;
            }
        }
    }


    private static string GetAttributeValue(XmlNode node, string attributeName, string defaultValue = "") {
        if (node is null) { return defaultValue; }
        if (node.Attributes is null) { return defaultValue; }
        var attr = node.Attributes[attributeName];
        return (attr != null) ? attr.Value : defaultValue;
    }

    private static AvpType GetAvpType(string typeName) {
        return typeName.Trim().ToUpperInvariant() switch {
            "ADDRESS" => AvpType.Address,
            "APPID" => AvpType.Unsigned32,
            "DIAMETERIDENTITY" => AvpType.DiameterIdentity,
            "DIAMETERURI" => AvpType.DiameterURI,
            "ENUMERATED" => AvpType.Enumerated,
            "FLOAT32" => AvpType.Float32,
            "GROUPED" => AvpType.Grouped,
            "INTEGER32" => AvpType.Integer32,
            "INTEGER64" => AvpType.Integer64,
            "IPADDRESS" => AvpType.Address,
            "IPFILTERRULE" => AvpType.IPFilterRule,
            "OCTETSTRING" => AvpType.OctetString,
            "OCTETSTRINGORUTF8" => AvpType.OctetString,
            "QOSFILTERRULE" => AvpType.QoSFilterRule,
            "TIME" => AvpType.Time,
            "UNSIGNED32" => AvpType.Unsigned32,
            "UNSIGNED64" => AvpType.Unsigned64,
            "UTF8STRING" => AvpType.UTF8String,
            "VENDORID" => AvpType.Unsigned32,
            _ => throw new FormatException("Unknown AVP type: " + typeName),
        };
    }

}
