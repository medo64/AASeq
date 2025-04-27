namespace AASeqPlugin;

internal record AvpDictionaryEntry (string Name, uint Code, AvpBitState MandatoryBit, AvpBitState ProtectedBit, bool MayEncrypt, VendorDictionaryEntry? Vendor, AvpType AvpType) {

}
