namespace AASeqPlugin;
using System;
using System.Collections.Generic;
using System.Buffers.Binary;
using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
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
            AvpType.Address => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetAddressBytes(value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Address.")),
            AvpType.DiameterIdentity => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, Utf8.GetBytes(value.AsString(""))),
            AvpType.DiameterURI => throw new NotImplementedException("Uri"),
            AvpType.Enumerated => new DiameterAvp(avpEntry.Code, GetDefaultFlags(avpEntry), avpEntry.Vendor?.Code, GetEnumBytes(avpEntry, value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Enumerated.")),
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
            AvpType.Address => GetAddress(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to IPAddress."),
            AvpType.DiameterIdentity => Utf8.GetString(data),
            AvpType.DiameterURI => throw new NotImplementedException("DiameterURI"),
            AvpType.Enumerated => GetEnum(avpEntry, data),
            AvpType.Float32 => throw new NotImplementedException("Float32"),
            AvpType.Grouped => throw new NotImplementedException("Grouped"),
            AvpType.Integer32 => GetInt32(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Int32."),
            AvpType.Integer64 => GetInt64(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Int64."),
            AvpType.IPFilterRule => throw new NotImplementedException("IPFilterRule"),
            AvpType.OctetString => data,
            AvpType.QoSFilterRule => throw new NotImplementedException("QoSFilterRule"),
            AvpType.Time => throw new NotImplementedException("Time"),
            AvpType.Unsigned32 => GetUInt32(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to UInt32."),
            AvpType.Unsigned64 => GetUInt64(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to UInt64."),
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
        return flags;
    }


    private static byte[]? GetAddressBytes(AASeqValue value) {
        var ip = value.AsIPAddress();
        if (ip is null) { return null; }
        if (ip.AddressFamily == AddressFamily.InterNetwork) {
            var ipBytes = new byte[6];
            ipBytes[0] = 0x00;
            ipBytes[1] = 0x01;
            Buffer.BlockCopy(ip.GetAddressBytes(), 0, ipBytes, 2, 4);
            return ipBytes;
        } else if (ip.AddressFamily == AddressFamily.InterNetworkV6) {
            var ipBytes = new byte[18];
            ipBytes[0] = 0x00;
            ipBytes[1] = 0x02;
            Buffer.BlockCopy(ip.GetAddressBytes(), 0, ipBytes, 2, 16);
            return ipBytes;
        }
        return null;
    }

    private static object GetAddress(byte[] bytes) {
        if ((bytes.Length == 6) && (bytes[0] == 0x00) && (bytes[1] == 0x01)) {
            var ipBytes = new byte[4];
            Buffer.BlockCopy(bytes, 2, ipBytes, 0, 4);
            return new IPAddress(ipBytes);
        } else if ((bytes.Length == 18) && (bytes[0] == 0x00) && (bytes[1] == 0x02)) {
            var ipBytes = new byte[16];
            Buffer.BlockCopy(bytes, 2, ipBytes, 0, 16);
            return new IPAddress(ipBytes);
        }
        return bytes;
    }


    private static byte[]? GetEnumBytes(AvpDictionaryEntry avpEntry, AASeqValue value) {
        var enumName = value.AsString();
        if (enumName is null) { return null; }
        var enumCode = avpEntry.FindEnumByName(enumName)?.Code;
        if (enumCode is null) {
            if (!Int32.TryParse(enumName, NumberStyles.Integer, CultureInfo.InvariantCulture, out var code)) { return null; }
            enumCode = code;
        }
        var number = enumCode.Value;
        var bytes = new byte[4];
        bytes[0] = (byte)((number >> 24) & 0xFF);
        bytes[1] = (byte)((number >> 16) & 0xFF);
        bytes[2] = (byte)((number >> 8) & 0xFF);
        bytes[3] = (byte)(number & 0xFF);
        return bytes;
    }

    private static object GetEnum(AvpDictionaryEntry avpEntry, byte[] bytes) {
        if (bytes.Length != 4) { return bytes; }
        var enumCode = BinaryPrimitives.ReadInt32BigEndian(bytes);
        if (avpEntry.AvpType == AvpType.Enumerated) {
            var enumName = avpEntry.FindEnumByCode(enumCode)?.Name;
            if (enumName is not null) { return enumName; }
        }
        return enumCode.ToString(CultureInfo.InvariantCulture);
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

    private static object GetInt32(byte[] bytes) {
        if (bytes.Length != 4) { return bytes; }
        return BinaryPrimitives.ReadInt32BigEndian(bytes);
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

    private static object GetInt64(byte[] bytes) {
        if (bytes.Length != 8) { return bytes; }
        return BinaryPrimitives.ReadInt64BigEndian(bytes);
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

    private static object GetUInt32(byte[] bytes) {
        if (bytes.Length != 4) { return bytes; }
        return BinaryPrimitives.ReadUInt32BigEndian(bytes);
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

    private static object GetUInt64(byte[] bytes) {
        if (bytes.Length != 8) { return bytes; }
        return BinaryPrimitives.ReadUInt64BigEndian(bytes);
    }

    #endregion Helpers

}
