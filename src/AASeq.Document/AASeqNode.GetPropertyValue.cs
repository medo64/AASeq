namespace AASeq;
using System;

public sealed partial class AASeqNode {

    /// <summary>
    /// Returns the property value or null if no value is present.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    public string? GetPropertyValue(string propertyName) {
        ArgumentNullException.ThrowIfNull(propertyName);
        if (Properties.TryGetValue(propertyName, out var value)) {
            return value;
        } else {
            return null;
        }
    }

    /// <summary>
    /// Returns the property value or default if no value is present.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="defaultValue">Default property value.</param>
    public string GetPropertyValue(string propertyName, string defaultValue) {
        return GetPropertyValue(propertyName) ?? defaultValue;
    }

}
