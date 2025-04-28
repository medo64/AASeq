namespace AASeqPlugin;
using System;
using System.Collections.Generic;
using System.Buffers.Binary;
using System.Text;
using System.Globalization;
using Microsoft.Extensions.Logging;
using AASeq;

internal static class DiameterEncoder {

    public static DiameterMessage Encode(string messageName, AASeqNodes nodes) {
        var isRequest = messageName.EndsWith("-Request", StringComparison.OrdinalIgnoreCase);
        var nameParts = messageName.Split(':', 2, StringSplitOptions.None);
        var applicationIdPart = (nameParts.Length == 1) ? "Common" : nameParts[0];
        var commandCodePart = ((nameParts.Length == 1) ? nameParts[0] : nameParts[1]);
        commandCodePart = commandCodePart[0..(commandCodePart.LastIndexOf('-'))];

        var applicationEntry = DictionaryLookup.Instance.FindApplicationByName(applicationIdPart) ?? throw new InvalidOperationException($"Cannot find application '{applicationIdPart}' in dictionary.");
        var commandEntry = DictionaryLookup.Instance.FindCommandByName(commandCodePart) ?? throw new InvalidOperationException($"Cannot find command '{commandCodePart}' in dictionary.");

        var avps = new List<DiameterAvp>();
        foreach (var node in nodes) {
            var avpName = node.Name;
            var avpEntry = DictionaryLookup.Instance.FindAvpByName(avpName) ?? throw new InvalidOperationException($"Cannot find AVP '{avpName}' in dictionary.");
            if (avpEntry.AvpType != AvpType.Grouped) {
                if (node.Nodes.Count > 0) { throw new InvalidOperationException($"AVP '{avpEntry.Name}' cannot have children."); }
                avps.Add(GetAvp(avpEntry, node.Value));
            } else {  // grouped AVPs get special processing
                if (!node.Value.IsNull) { throw new InvalidOperationException($"AVP '{avpEntry.Name}' cannot have value."); }
                avps.Add(GetGroupedAvp(avpEntry, node.Value));
            }
        }
        return new DiameterMessage((byte)(isRequest ? 0x80 : 0x00), commandEntry.Code, applicationEntry.Id, avps);
    }

    public static AASeqNodes Decode(ILogger logger, DiameterMessage message, out string messageName) {
        var applicationEntry = DictionaryLookup.Instance.FindApplicationById(message.ApplicationId);
        var commandEntry = DictionaryLookup.Instance.FindCommandByCode(message.CommandCode);

        messageName = ($"{applicationEntry?.Name ?? message.ApplicationId.ToString(CultureInfo.InvariantCulture)}:{commandEntry?.Name ?? message.CommandCode.ToString(CultureInfo.InvariantCulture)}") + (message.HasRequestFlag ? "-Request" : "-Answer");
        var nodes = new AASeqNodes();
        foreach (var avp in message.Avps) {
            var avpEntry = DictionaryLookup.Instance.FindAvpByCode(avp.VendorId ?? 0, avp.Code);
            if (avpEntry is not null) {
                nodes.Add(GetNode(avpEntry, avp.GetData()));
            } else {
                var avpName = ((avp.VendorId != null) ? avp.VendorId.Value.ToString(CultureInfo.InvariantCulture) + ":" : "") + avp.Code.ToString(CultureInfo.InvariantCulture);
                nodes.Add(new AASeqNode(avpName, avp.GetData()));
            }
        }
        return nodes;
    }


    #region Helpers

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    private static DiameterAvp GetAvp(AvpDictionaryEntry avpEntry, AASeqValue value) {
        return avpEntry.AvpType switch {
            AvpType.Address => throw new NotImplementedException("Address"),
            AvpType.DiameterIdentity => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, Utf8.GetBytes(value.AsString(""))),
            AvpType.DiameterURI => throw new NotImplementedException("Uri"),
            AvpType.Enumerated => throw new NotImplementedException("Enumerated"),
            AvpType.Float32 => throw new NotImplementedException("Float32"),
            AvpType.Grouped => throw new NotImplementedException("Grouped"),
            AvpType.Integer32 => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetInt32Bytes(value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Int32.")),
            AvpType.Integer64 => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetInt64Bytes(value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Int64.")),
            AvpType.IPFilterRule => throw new NotImplementedException("IPFilterRule"),
            AvpType.OctetString => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, value.AsByteArray() ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to byte array.")),
            AvpType.QoSFilterRule => throw new NotImplementedException("QoSFilterRule"),
            AvpType.Time => throw new NotImplementedException("Time"),
            AvpType.Unsigned32 => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetUInt32Bytes(value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to UInt32.")),
            AvpType.Unsigned64 => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetUInt64Bytes(value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to UInt64.")),
            AvpType.UTF8String => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, Utf8.GetBytes(value.AsString(""))),
            _ => throw new NotImplementedException(),  // should never happen
        };
    }

    private static AASeqNode GetNode(AvpDictionaryEntry avpEntry, byte[] data) {
        object value = avpEntry.AvpType switch {
            AvpType.Address => throw new NotImplementedException("Address"),
            AvpType.DiameterIdentity => Utf8.GetString(data),
            AvpType.DiameterURI => throw new NotImplementedException("DiameterURI"),
            AvpType.Enumerated => GetUInt32(data),  // TODO
            AvpType.Float32 => throw new NotImplementedException("Float32"),
            AvpType.Grouped => throw new NotImplementedException("Grouped"),
            AvpType.Integer32 => GetInt32(data),
            AvpType.Integer64 => GetInt64(data),
            AvpType.IPFilterRule => throw new NotImplementedException("IPFilterRule"),
            AvpType.OctetString => data,
            AvpType.QoSFilterRule => throw new NotImplementedException("QoSFilterRule"),
            AvpType.Time => throw new NotImplementedException("Time"),
            AvpType.Unsigned32 => GetUInt32(data),
            AvpType.Unsigned64 => GetUInt64(data),
            AvpType.UTF8String => Utf8.GetString(data),
            _ => throw new NotImplementedException(),  // should never happen
        };
        return new AASeqNode(avpEntry.Name, value);
    }

    private static DiameterAvp GetGroupedAvp(AvpDictionaryEntry avpEntry, AASeqValue value) {
        throw new NotImplementedException();
    }

    private static byte GetDefaultFlags(AvpDictionaryEntry avpEntry) {
        var flags = (byte)0;
        if (avpEntry.Vendor is not null) { flags |= 0x80; }
        if (avpEntry.MandatoryBit == AvpBitState.Must) { flags |= 0x40; }
        if (avpEntry.ProtectedBit == AvpBitState.Must) { flags |= 0x20; }
        return flags;
    }

    private static byte[]? GetInt32Bytes(AASeqValue value) {
        var number = value.AsInt32();
        if (number is null) { return null; }
        var bytes = new byte[4];
        bytes[0] = (byte)((number >> 24) & 0xFF);
        bytes[1] = (byte)((number >> 16) & 0xFF);
        bytes[2] = (byte)((number >> 8) & 0xFF);
        bytes[3] = (byte)(number & 0xFF);
        return bytes;
    }

    private static byte[]? GetInt64Bytes(AASeqValue value) {
        var number = value.AsInt64();
        if (number is null) { return null; }
        var bytes = new byte[8];
        bytes[0] = (byte)((number >> 56) & 0xFF);
        bytes[1] = (byte)((number >> 48) & 0xFF);
        bytes[2] = (byte)((number >> 40) & 0xFF);
        bytes[3] = (byte)((number >> 32) & 0xFF);
        bytes[4] = (byte)((number >> 24) & 0xFF);
        bytes[5] = (byte)((number >> 16) & 0xFF);
        bytes[6] = (byte)((number >> 8) & 0xFF);
        bytes[7] = (byte)(number & 0xFF);
        return bytes;
    }

    private static byte[]? GetUInt32Bytes(AASeqValue value) {
        var number = value.AsUInt32();
        if (number is null) { return null; }
        var bytes = new byte[4];
        bytes[0] = (byte)((number >> 24) & 0xFF);
        bytes[1] = (byte)((number >> 16) & 0xFF);
        bytes[2] = (byte)((number >> 8) & 0xFF);
        bytes[3] = (byte)(number & 0xFF);
        return bytes;
    }

    private static byte[]? GetUInt64Bytes(AASeqValue value) {
        var number = value.AsUInt64();
        if (number is null) { return null; }
        var bytes = new byte[8];
        bytes[0] = (byte)((number >> 56) & 0xFF);
        bytes[1] = (byte)((number >> 48) & 0xFF);
        bytes[2] = (byte)((number >> 40) & 0xFF);
        bytes[3] = (byte)((number >> 32) & 0xFF);
        bytes[4] = (byte)((number >> 24) & 0xFF);
        bytes[5] = (byte)((number >> 16) & 0xFF);
        bytes[6] = (byte)((number >> 8) & 0xFF);
        bytes[7] = (byte)(number & 0xFF);
        return bytes;
    }

    private static Int32 GetInt32(byte[] value) {
        return BinaryPrimitives.ReadInt32BigEndian(value);
    }

    private static UInt32 GetUInt32(byte[] value) {
        return BinaryPrimitives.ReadUInt32BigEndian(value);
    }

    private static Int64 GetInt64(byte[] value) {
        return BinaryPrimitives.ReadInt64BigEndian(value);
    }

    private static UInt64 GetUInt64(byte[] value) {
        return BinaryPrimitives.ReadUInt64BigEndian(value);
    }

    #endregion Helpers

}
