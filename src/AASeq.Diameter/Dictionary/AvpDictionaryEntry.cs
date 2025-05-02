namespace AASeq.Diameter;
using System;
using System.Collections.Generic;

internal record AvpDictionaryEntry(String Name, UInt32 Code, AvpBitState MandatoryBit, VendorDictionaryEntry? Vendor, AvpType AvpType) {

    public AvpDictionaryEntry(String name, UInt32 code, AvpBitState mandatoryBit, VendorDictionaryEntry? vendor, IEnumerable<AvpEnumDictionaryEntry> enums)
        : this(name, code, mandatoryBit, vendor, AvpType.Enumerated) {
        Enums = new List<AvpEnumDictionaryEntry>(enums).AsReadOnly();
        foreach (var e in Enums) {
            try {
                EnumsByName.Add(e.Name, e);
                EnumsByCode.Add(e.Code, e);
            } catch (ArgumentException ex) {
                throw new InvalidOperationException($"Cannot add enum {e.Code}:{e.Name} for AVP {name} .", ex);
            }
        }
    }

    public IReadOnlyList<AvpEnumDictionaryEntry> Enums { get; init; } = [];

    private readonly Dictionary<String, AvpEnumDictionaryEntry> EnumsByName = new(StringComparer.CurrentCultureIgnoreCase);
    private readonly Dictionary<Int32, AvpEnumDictionaryEntry> EnumsByCode = [];

    public AvpEnumDictionaryEntry? FindEnumByCode(Int32 code) {
        return EnumsByCode.TryGetValue(code, out var e) ? e : null;
    }

    public AvpEnumDictionaryEntry? FindEnumByName(String name) {
        return EnumsByName.TryGetValue(name, out var e) ? e : null;
    }

}
