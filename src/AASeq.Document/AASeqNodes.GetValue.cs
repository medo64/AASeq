namespace AASeq;
using System;
using System.Net;
using System.Text.RegularExpressions;

public sealed partial class AASeqNodes {

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public String GetValue(string nodePath, String defaultValue) {
        return FindValue(nodePath)?.AsString() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Boolean GetValue(string nodePath, Boolean defaultValue) {
        return FindValue(nodePath)?.AsBoolean() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public SByte GetValue(string nodePath, SByte defaultValue) {
        return FindValue(nodePath)?.AsSByte() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Byte GetValue(string nodePath, Byte defaultValue) {
        return FindValue(nodePath)?.AsByte() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Int16 GetValue(string nodePath, Int16 defaultValue) {
        return FindValue(nodePath)?.AsInt16() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public UInt16 GetValue(string nodePath, UInt16 defaultValue) {
        return FindValue(nodePath)?.AsUInt16() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Int32 GetValue(string nodePath, Int32 defaultValue) {
        return FindValue(nodePath)?.AsInt32() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public UInt32 GetValue(string nodePath, UInt32 defaultValue) {
        return FindValue(nodePath)?.AsUInt32() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Int64 GetValue(string nodePath, Int64 defaultValue) {
        return FindValue(nodePath)?.AsInt64() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public UInt64 GetValue(string nodePath, UInt64 defaultValue) {
        return FindValue(nodePath)?.AsUInt64() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Int128 GetValue(string nodePath, Int128 defaultValue) {
        return FindValue(nodePath)?.AsInt128() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public UInt128 GetValue(string nodePath, UInt128 defaultValue) {
        return FindValue(nodePath)?.AsUInt128() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Half GetValue(string nodePath, Half defaultValue) {
        return FindValue(nodePath)?.AsHalf() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Single GetValue(string nodePath, Single defaultValue) {
        return FindValue(nodePath)?.AsSingle() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Double GetValue(string nodePath, Double defaultValue) {
        return FindValue(nodePath)?.AsDouble() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Decimal GetValue(string nodePath, Decimal defaultValue) {
        return FindValue(nodePath)?.AsDecimal() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public DateTimeOffset GetValue(string nodePath, DateTimeOffset defaultValue) {
        return FindValue(nodePath)?.AsDateTimeOffset() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public DateOnly GetValue(string nodePath, DateOnly defaultValue) {
        return FindValue(nodePath)?.AsDateOnly() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public TimeOnly GetValue(string nodePath, TimeOnly defaultValue) {
        return FindValue(nodePath)?.AsTimeOnly() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public TimeSpan GetValue(string nodePath, TimeSpan defaultValue) {
        return FindValue(nodePath)?.AsTimeSpan() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public IPAddress GetValue(string nodePath, IPAddress defaultValue) {
        return FindValue(nodePath)?.AsIPAddress() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Regex GetValue(string nodePath, Regex defaultValue) {
        return FindValue(nodePath)?.AsRegex() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Uri GetValue(string nodePath, Uri defaultValue) {
        return FindValue(nodePath)?.AsUri() ?? defaultValue;
    }

    /// <summary>
    /// Finds value belonging to a node at a given path.
    /// It will not create new nodes.
    /// If there are any duplicates, along the path, only the last one will remain.
    /// </summary>
    /// <param name="nodePath">Path to the node.</param>
    /// <param name="defaultValue">Default value.</param>
    public Guid GetValue(string nodePath, Guid defaultValue) {
        return FindValue(nodePath)?.AsGuid() ?? defaultValue;
    }

}
