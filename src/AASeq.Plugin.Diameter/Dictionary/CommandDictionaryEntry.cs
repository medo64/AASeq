namespace AASeqPlugin;

internal record CommandDictionaryEntry (string Name, uint Code, AvpBitState ProxiableBit, VendorDictionaryEntry? Vendor) {
}
