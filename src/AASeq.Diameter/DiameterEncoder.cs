namespace AASeq.Diameter;
using System;
using System.Collections.Generic;
using System.Buffers.Binary;
using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using AASeq;

/// <summary>
/// Diameter encoder/decoder.
/// </summary>
public static class DiameterEncoder {

    /// <summary>
    /// Encodes AASeqNodes into a diameter message.
    /// </summary>
    /// <param name="messageName">Message name.</param>
    /// <param name="nodes">Nodes.</param>
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
            if (node.Name.Equals(".HopByHop", StringComparison.OrdinalIgnoreCase)) { continue; }
            if (node.Name.Equals(".EndToEnd", StringComparison.OrdinalIgnoreCase)) { continue; }
            if (node.Name.Equals(".Flags", StringComparison.OrdinalIgnoreCase)) { continue; }
            var avpName = node.Name;
            var avpEntry = DictionaryLookup.Instance.FindAvpByName(avpName) ?? throw new InvalidOperationException($"Cannot find AVP '{avpName}' in dictionary.");
            if (avpEntry.AvpType != AvpType.Grouped) {
                if (node.Nodes.Count > 0) { throw new InvalidOperationException($"AVP '{avpEntry.Name}' cannot have children."); }
            } else {
                if (!node.Value.IsNull) { throw new InvalidOperationException($"AVP '{avpEntry.Name}' cannot have value."); }
            }
            avps.Add(GetAvp(avpEntry, node));
        }

        var hopByHopId = nodes[".HopByHop"].AsUInt32();
        var endToEnd = nodes[".EndToEnd"].AsUInt32();

        var flagNode = nodes.FindNode(".Flags");
        var flags = flagNode?.Value.AsByte() ?? 0x00;
        if (isRequest) { flags |= 0x80; } else { flags &= 0x7F; }
        if ("true".Equals(flagNode?.GetPropertyValue("proxiable"))) { flags |= 0x40; }
        if ("true".Equals(flagNode?.GetPropertyValue("error"))) { flags |= 0x20; }
        if ("true".Equals(flagNode?.GetPropertyValue("retransmitted"))) { flags |= 0x10; }

        return new DiameterMessage(flags, commandEntry.Code, applicationEntry.Id, hopByHopId, endToEnd, avps);
    }

    /// <summary>
    /// Decodes a diameter message into AASeqNodes.
    /// </summary>
    /// <param name="message">Diameter message.</param>
    /// <param name="messageName">Message name.</param>
    /// <returns></returns>
    public static AASeqNodes Decode(DiameterMessage message, out string messageName) {
        return Decode(message, includeAllFlags: true, out messageName);
    }

    /// <summary>
    /// Decodes a diameter message into AASeqNodes.
    /// </summary>
    /// <param name="message">Diameter message.</param>
    /// <param name="includeAllFlags">If true, flags are included even when they match AVP definition.</param>
    /// <param name="messageName">Message name.</param>
    /// <returns></returns>
    public static AASeqNodes Decode(DiameterMessage message, bool includeAllFlags, out string messageName) {
        var applicationEntry = DictionaryLookup.Instance.FindApplicationById(message.ApplicationId);
        var commandEntry = DictionaryLookup.Instance.FindCommandByCode(message.CommandCode);

        messageName = ($"{applicationEntry?.Name ?? message.ApplicationId.ToString(CultureInfo.InvariantCulture)}:{commandEntry?.Name ?? message.CommandCode.ToString(CultureInfo.InvariantCulture)}") + (message.HasRequestFlag ? "-Request" : "-Answer");

        var nodes = new AASeqNodes();
        nodes.Add(new AASeqNode(".HopByHop", message.HopByHopIdentifier));
        nodes.Add(new AASeqNode(".EndToEnd", message.EndToEndIdentifier));
        var flags = new AASeqNode(".Flags", new byte[] { message.Flags });
        flags.Properties.Add("proxiable", message.HasProxiableFlag);
        flags.Properties.Add("error", message.HasErrorFlag);
        flags.Properties.Add("retransmitted", message.HasRetransmittedFlag);
        nodes.Add(flags);

        foreach (var avp in message.Avps) {
            var avpEntry = DictionaryLookup.Instance.FindAvpByCode(avp.VendorCode ?? 0, avp.Code);
            AASeqNode node;
            if (avpEntry is not null) {
                node = GetNode(avpEntry, avp.GetData());
            } else {
                var avpName = ((avp.VendorCode != null) ? avp.VendorCode.Value.ToString(CultureInfo.InvariantCulture) + ":" : "") + avp.Code.ToString(CultureInfo.InvariantCulture);
                node = new AASeqNode(avpName, avp.GetData());
            }
            if (includeAllFlags | (avp.Flags != avpEntry.DefaultFlags)) {
                node.Properties.Add("flags", "0x" + avp.Flags.ToString("X2", CultureInfo.InvariantCulture));
                node.Properties.Add("mandatory", avp.HasMandatoryFlag);
                node.Properties.Add("vendor", (avp.VendorCode != null) ? avp.VendorCode.Value.ToString(CultureInfo.InvariantCulture) : "false");
            }
            nodes.Add(node);
        }
        return nodes;
    }


    #region Helpers

    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    private static DiameterAvp GetAvp(AvpDictionaryEntry avpEntry, AASeqNode node) {
        var avp = avpEntry.AvpType switch {
            AvpType.Address => new DiameterAvp(avpEntry, GetAddressBytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Address.")),
            AvpType.DiameterIdentity => new DiameterAvp(avpEntry, Utf8.GetBytes(node.Value.AsString(""))),
            AvpType.DiameterURI => throw new NotImplementedException("Uri"),
            AvpType.Enumerated => new DiameterAvp(avpEntry, GetEnumBytes(avpEntry, node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Enumerated.")),
            AvpType.Float32 => throw new NotImplementedException("Float32"),
            AvpType.Grouped => GetGroupedAvp(avpEntry, node.Nodes) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Grouped."),
            AvpType.Integer32 => new DiameterAvp(avpEntry, GetInt32Bytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Integer32.")),
            AvpType.Integer64 => new DiameterAvp(avpEntry, GetInt64Bytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Integer64.")),
            AvpType.IPFilterRule => throw new NotImplementedException("IPFilterRule"),
            AvpType.OctetString => new DiameterAvp(avpEntry, node.Value.AsByteArray() ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an OctetString.")),
            AvpType.QoSFilterRule => throw new NotImplementedException("QoSFilterRule"),
            AvpType.RawAddress => new DiameterAvp(avpEntry, GetRawAddressBytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to RawAddress.")),
            AvpType.Time => new DiameterAvp(avpEntry, GetTimeBytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to Time.")),
            AvpType.Unsigned32 => new DiameterAvp(avpEntry, GetUInt32Bytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Unsigned32.")),
            AvpType.Unsigned64 => new DiameterAvp(avpEntry, GetUInt64Bytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an Unsigned64.")),
            AvpType.UTF8String => new DiameterAvp(avpEntry, GetUtf8StringBytes(node.Value) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} to an UTF8String.")),
            _ => throw new NotImplementedException(),  // should never happen
        };

        var flagsProp = node.GetPropertyValue("flags");
        byte flags;
        if (byte.TryParse(flagsProp, NumberStyles.HexNumber | NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out flags)) {
            if (avp.HasVendorFlag) { flags |= 0x80; }
        } else {
            flags = avpEntry.DefaultFlags;
        }
        if ("true".Equals(node.GetPropertyValue("mandatory"))) {
            flags |= 0b01000000;
        } else if ("false".Equals(node.GetPropertyValue("mandatory"))) {
            flags &= 0b10111111;
        }
        if (avp.Flags != flags) { avp = avp with { Flags = flags }; }

        return avp;
    }

    private static AASeqNode GetNode(AvpDictionaryEntry avpEntry, byte[] data) {
        return avpEntry.AvpType switch {
            AvpType.Address => new AASeqNode(avpEntry.Name, GetAddress(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an IPAddress.")),
            AvpType.DiameterIdentity => new AASeqNode(avpEntry.Name, Utf8.GetString(data)),
            AvpType.DiameterURI => throw new NotImplementedException("DiameterURI"),
            AvpType.Enumerated => new AASeqNode(avpEntry.Name, GetEnum(avpEntry, data)),
            AvpType.Float32 => throw new NotImplementedException("Float32"),
            AvpType.Grouped => new AASeqNode(avpEntry.Name, GetGrouped(data)),
            AvpType.Integer32 => new AASeqNode(avpEntry.Name, GetInt32(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an Integer32.")),
            AvpType.Integer64 => new AASeqNode(avpEntry.Name, GetInt64(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an Integer64.")),
            AvpType.IPFilterRule => throw new NotImplementedException("IPFilterRule"),
            AvpType.OctetString => new AASeqNode(avpEntry.Name, data),
            AvpType.QoSFilterRule => throw new NotImplementedException("QoSFilterRule"),
            AvpType.RawAddress => new AASeqNode(avpEntry.Name, GetRawAddress(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an IPAddress.")),
            AvpType.Time => new AASeqNode(avpEntry.Name, GetTime(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from a Time.")),
            AvpType.Unsigned32 => new AASeqNode(avpEntry.Name, GetUInt32(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an Unsigned32.")),
            AvpType.Unsigned64 => new AASeqNode(avpEntry.Name, GetUInt64(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an Unsigned64.")),
            AvpType.UTF8String => new AASeqNode(avpEntry.Name, GetUtf8String(data) ?? throw new InvalidOperationException($"Cannot convert {avpEntry.Name} from an UTF8String.")),
            _ => throw new NotImplementedException(),  // should never happen
        };
    }


    private static DiameterAvp GetGroupedAvp(AvpDictionaryEntry avpEntry, AASeqNodes children) {
        var totalLen = 0;
        var avps = new List<DiameterAvp>();
        foreach (var child in children) {
            var subName = child.Name;
            var subEntry = DictionaryLookup.Instance.FindAvpByName(subName) ?? throw new InvalidOperationException($"Cannot find AVP '{subName}' in dictionary.");
            var sub = GetAvp(subEntry, child);
            totalLen += sub.LengthWithPadding;
            avps.Add(sub);
        }
        var bytes = new byte[totalLen];
        var offset = 0;
        foreach (var avp in avps) {
            avp.WriteTo(bytes.AsSpan(offset));
            offset += avp.LengthWithPadding;
        }
        return new DiameterAvp(avpEntry, bytes);
    }

    private static AASeqNodes GetGrouped(byte[] bytes) {
        var nodes = new AASeqNodes();
        var offset = 0;
        while (offset < bytes.Length) {
            var avp = DiameterAvp.ReadFrom(bytes.AsSpan(offset));
            offset += avp.LengthWithPadding;

            var avpEntry = DictionaryLookup.Instance.FindAvpByCode(avp.VendorCode ?? 0, avp.Code);
            AASeqNode node;
            if (avpEntry is not null) {
                node = GetNode(avpEntry, avp.GetData());
            } else {
                var avpName = ((avp.VendorCode != null) ? avp.VendorCode.Value.ToString(CultureInfo.InvariantCulture) + ":" : "") + avp.Code.ToString(CultureInfo.InvariantCulture);
                node = new AASeqNode(avpName, avp.GetData());
            }
            node.Properties.Add("flags", "0x" + avp.Flags.ToString("X2", CultureInfo.InvariantCulture));
            node.Properties.Add("mandatory", avp.HasMandatoryFlag);
            node.Properties.Add("vendor", avp.HasVendorFlag);
            nodes.Add(node);
        }
        return nodes;
    }

    private static byte[]? GetAddressBytes(AASeqValue value) {
        var ip = value.AsIPAddress();
        if (ip is null) { return value.AsByteArray(); }
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
        if (number is null) { return value.AsByteArray(); }
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
        if (number is null) { return value.AsByteArray(); }
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


    private static byte[]? GetRawAddressBytes(AASeqValue value) {
        var ip = value.AsIPAddress();
        if (ip is null) { return value.AsByteArray(); }
        if (ip.AddressFamily == AddressFamily.InterNetwork) {
            return ip.GetAddressBytes();
        } else if (ip.AddressFamily == AddressFamily.InterNetworkV6) {
            return ip.GetAddressBytes();
        }
        return null;
    }

    private static object GetRawAddress(byte[] bytes) {
        if (bytes.Length == 4) {
            return new IPAddress(bytes);
        } else if (bytes.Length == 16) {
            return new IPAddress(bytes);
        }
        return bytes;
    }


    private readonly static DateTimeOffset DiameterEpoch = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static byte[]? GetTimeBytes(AASeqValue value) {
        var dto = value.AsDateTimeOffset();
        if (dto is null) { return value.AsByteArray(); }
        var seconds = (uint)(dto.Value - DiameterEpoch).TotalSeconds;
        var bytes = new byte[4];
        bytes[0] = (byte)((seconds >> 24) & 0xFF);
        bytes[1] = (byte)((seconds >> 16) & 0xFF);
        bytes[2] = (byte)((seconds >> 8) & 0xFF);
        bytes[3] = (byte)(seconds & 0xFF);
        return bytes;
    }

    private static object GetTime(byte[] bytes) {
        if (bytes.Length != 4) { return bytes; }
        var seconds = BinaryPrimitives.ReadUInt32BigEndian(bytes);
        var dto = DiameterEpoch.AddSeconds(seconds);
        return dto;
    }


    private static byte[]? GetUInt32Bytes(AASeqValue value) {
        var number = value.AsUInt32();
        if (number is null) { return value.AsByteArray(); }
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
        if (number is null) { return value.AsByteArray(); }
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


    private static byte[]? GetUtf8StringBytes(AASeqValue value) {
        if (value.RawValue is byte[] bytes) { return bytes; }
        var text = value.AsString("");
        return Utf8.GetBytes(text);
    }

    private static object GetUtf8String(byte[] bytes) {
        return Utf8.GetString(bytes);
    }

    #endregion Helpers

}
