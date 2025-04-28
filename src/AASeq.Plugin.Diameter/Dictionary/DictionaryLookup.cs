namespace AASeqPlugin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using AASeq;

/// <summary>
/// Diameter dictionary lookup.
/// </summary>
internal class DictionaryLookup {

    private DictionaryLookup() {
        foreach (var resName in GetDictionaryResourceNames()) {
            using var stream = GetStream(resName);
            var doc = AASeqNodes.Load(stream);
            foreach (var rootNode in doc) {
                if (rootNode.Name.Equals("DiameterDictionary", StringComparison.OrdinalIgnoreCase)) {
                    foreach (var node in rootNode.Nodes) {
                        if (node.Name.Equals("Vendor", StringComparison.OrdinalIgnoreCase)) {
                            var name = node.Value.AsString() ?? throw new NotSupportedException("No vendor name.");
                            var id = node.GetPropertyValue("id") ?? throw new NotSupportedException($"No vendor ID ({name}).");
                            var codeVal = node.GetPropertyValue("code") ?? throw new NotSupportedException($"No vendor code ({name}).");
                            if (!uint.TryParse(codeVal, NumberStyles.Integer, CultureInfo.InvariantCulture, out var code)) { throw new NotSupportedException($"Unknown vendor code ({name})."); }
                            var entry = new VendorDictionaryEntry(name, id, code);
                            VendorsByCode.Add(entry.Code, entry);
                            VendorsById.Add(entry.Id, entry);
                            VendorsByName.Add(entry.Name, entry);
                        } else if (node.Name.Equals("Application", StringComparison.OrdinalIgnoreCase)) {
                            var name = node.Value.AsString() ?? throw new NotSupportedException("No application name.");
                            var idVal = node.GetPropertyValue("id") ?? throw new NotSupportedException($"No application ID ({name}).");
                            if (!uint.TryParse(idVal, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)) { throw new NotSupportedException($"Unknown application ID ({name})."); }
                            var entry = new ApplicationDictionaryEntry(name, id);
                            ApplicationsById.Add(entry.Id, entry);
                            ApplicationsByName.Add(entry.Name, entry);
                        } else if (node.Name.Equals("Command", StringComparison.OrdinalIgnoreCase)) {
                            var name = node.Value.AsString() ?? throw new NotSupportedException("No command name.");
                            var abbrev = node.GetPropertyValue("abbrev");
                            var codeVal = node.GetPropertyValue("code") ?? throw new NotSupportedException($"No command Code ({name}).");
                            if (!uint.TryParse(codeVal, NumberStyles.Integer, CultureInfo.InvariantCulture, out var code)) { throw new NotSupportedException($"Unknown command code ({name})."); }
                            var proxiableBit = node.GetPropertyValue("proxiableBit")?.ToUpperInvariant() switch {
                                "MUSTNOT" => AvpBitState.MustNot,
                                "MAY" => AvpBitState.May,
                                "MUST" => AvpBitState.Must,
                                _ => throw new NotSupportedException($"Unknown proxiableBit parameter ({name}).")
                            };
                            var vendorId = node.GetPropertyValue("vendorId");
                            var vendor = (vendorId is null) ? null : (FindVendorById(vendorId) ?? throw new NotSupportedException($"Unknown command vendor ({name})."));
                            var entry = new CommandDictionaryEntry(name, code, proxiableBit, vendor);
                            CommandsByCode.Add(entry.Code, entry);
                            CommandsByName.Add(entry.Name, entry);
                            if (!string.IsNullOrEmpty(abbrev) && !abbrev.Equals(entry.Name, StringComparison.Ordinal)) { CommandsByName.Add(abbrev, entry); }
                        } else if (node.Name.Equals("AVP", StringComparison.OrdinalIgnoreCase)) {
                            var name = node.Value.AsString() ?? throw new NotSupportedException("No avp name.");
                            var codeVal = node.GetPropertyValue("code") ?? throw new NotSupportedException($"No avp code ({name}).");
                            if (!uint.TryParse(codeVal, NumberStyles.Integer, CultureInfo.InvariantCulture, out var code)) { throw new NotSupportedException($"Unknown avp code ({name})."); }
                            var mandatoryBit = node.GetPropertyValue("mandatoryBit")?.ToUpperInvariant() switch {
                                "MUSTNOT" => AvpBitState.MustNot,
                                "MAY" => AvpBitState.May,
                                "MUST" => AvpBitState.Must,
                                _ => throw new NotSupportedException($"Unknown mandatoryBit parameter ({name}).")
                            };
                            var protectedBit = node.GetPropertyValue("protectedBit")?.ToUpperInvariant() switch {
                                "MUSTNOT" => AvpBitState.MustNot,
                                "MAY" => AvpBitState.May,
                                "MUST" => AvpBitState.Must,
                                _ => throw new NotSupportedException($"Unknown protectedBit parameter ({name}).")
                            };
                            var mayEncrypt = node.GetPropertyValue("mayEncrypt")?.ToUpperInvariant() switch {
                                "NO" => false,
                                "YES" => true,
                                _ => throw new NotSupportedException($"Unknown mayEnrypt parameter ({name}).")
                            };
                            var vendorId = node.GetPropertyValue("vendorId");
                            var vendor = (vendorId is null) ? null : (FindVendorById(vendorId) ?? throw new NotSupportedException($"Unknown AVP vendor ({name})."));
                            var avpType = node.GetPropertyValue("type")?.ToUpperInvariant() switch {
                                "ADDRESS" => AvpType.Address,
                                "DIAMETERIDENTITY" => AvpType.DiameterIdentity,
                                "DIAMETERURI" => AvpType.DiameterURI,
                                "ENUMERATED" => AvpType.Enumerated,
                                "FLOAT32" => AvpType.Float32,
                                "GROUPED" => AvpType.Grouped,
                                "INTEGER32" => AvpType.Integer32,
                                "INTEGER64" => AvpType.Integer64,
                                "IPFILTERRULE" => AvpType.IPFilterRule,
                                "OCTETSTRING" => AvpType.OctetString,
                                "QOSFILTERRULE" => AvpType.QoSFilterRule,
                                "TIME" => AvpType.Time,
                                "UNSIGNED32" => AvpType.Unsigned32,
                                "UNSIGNED64" => AvpType.Unsigned64,
                                "UTF8STRING" => AvpType.UTF8String,
                                _ => throw new NotSupportedException($"Unknown type parameter ({name}).")
                            };
                            var entry = new AvpDictionaryEntry(name, code, mandatoryBit, protectedBit, mayEncrypt, vendor, avpType);
                            AvpsByCode.Add((entry.Vendor?.Code ?? 0, entry.Code), entry);
                            AvpsByName.Add(entry.Name, entry);
                        }
                    }
                }
            }
        }
    }


    private readonly Dictionary<uint, VendorDictionaryEntry> VendorsByCode = [];
    private readonly Dictionary<string, VendorDictionaryEntry> VendorsById = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, VendorDictionaryEntry> VendorsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<uint, ApplicationDictionaryEntry> ApplicationsById = [];
    private readonly Dictionary<string, ApplicationDictionaryEntry> ApplicationsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<uint, CommandDictionaryEntry> CommandsByCode = [];
    private readonly Dictionary<string, CommandDictionaryEntry> CommandsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<(uint, uint), AvpDictionaryEntry> AvpsByCode = [];
    private readonly Dictionary<string, AvpDictionaryEntry> AvpsByName = new(StringComparer.OrdinalIgnoreCase);


    /// <summary>
    /// Returns vendor, if found; null otherwise.
    /// </summary>
    /// <param name="code">Vendor code.</param>
    public VendorDictionaryEntry? FindVendorByCode(uint code) {
        return VendorsByCode.TryGetValue(code, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns vendor, if found; null otherwise.
    /// </summary>
    /// <param name="id">Vendor ID.</param>
    public VendorDictionaryEntry? FindVendorById(string id) {
        return VendorsById.TryGetValue(id, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns vendor, if found; null otherwise.
    /// </summary>
    /// <param name="name">Vendor name.</param>
    public VendorDictionaryEntry? FindVendorByName(string name) {
        return VendorsByName.TryGetValue(name, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns application, if found; null otherwise.
    /// </summary>
    /// <param name="id">Application ID.</param>
    public ApplicationDictionaryEntry? FindApplicationById(uint id) {
        return ApplicationsById.TryGetValue(id, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns application, if found; null otherwise.
    /// </summary>
    /// <param name="name">Application name.</param>
    public ApplicationDictionaryEntry? FindApplicationByName(string name) {
        return ApplicationsByName.TryGetValue(name, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns command, if found; null otherwise.
    /// </summary>
    /// <param name="code">Command code.</param>
    public CommandDictionaryEntry? FindCommandByCode(uint code) {
        return CommandsByCode.TryGetValue(code, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns command, if found; null otherwise.
    /// </summary>
    /// <param name="name">Command name.</param>
    public CommandDictionaryEntry? FindCommandByName(string name) {
        return CommandsByName.TryGetValue(name, out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns avp, if found; null otherwise.
    /// </summary>
    /// <param name="vendorCode">Vendor code.</param>
    /// <param name="code">AVP code.</param>
    public AvpDictionaryEntry? FindAvpByCode(uint vendorCode, uint code) {
        return AvpsByCode.TryGetValue((vendorCode, code), out var entry) ? entry : null;
    }

    /// <summary>
    /// Returns avp, if found; null otherwise.
    /// </summary>
    /// <param name="name">AVP name.</param>
    public AvpDictionaryEntry? FindAvpByName(string name) {
        return AvpsByName.TryGetValue(name, out var entry) ? entry : null;
    }


    private static readonly DictionaryLookup _Instance = new();
    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static DictionaryLookup Instance => _Instance;


    #region Loading

    private static IEnumerable<string> GetDictionaryResourceNames() {
        foreach (var resName in Assembly.GetExecutingAssembly().GetManifestResourceNames()) {
            if (resName.StartsWith("AASeqPlugin.Assets.Dictionary.", StringComparison.Ordinal) && resName.EndsWith(".aaseq", StringComparison.Ordinal)) {
                yield return resName;
            }
        }
    }

    private static MemoryStream GetStream(string streamName) {
        var assembly = Assembly.GetExecutingAssembly();
        var resStream = assembly.GetManifestResourceStream(streamName);
        var buffer = new byte[(int)resStream!.Length];
        resStream.ReadExactly(buffer);
        return new MemoryStream(buffer) { Position = 0 };
    }

    #endregion Loading

}
