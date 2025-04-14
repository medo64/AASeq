namespace AASeq;
using System;

public sealed partial class AASeqProperties {

    /// <summary>
    /// Finds value of a property.
    /// If property is not found, null is returned.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    public String? GetPropertyValue(string propertyName) {
        ArgumentNullException.ThrowIfNull(propertyName);
        return TryGetValue(propertyName, out var propertyValue) ? propertyValue : null;
    }

    /// <summary>
    /// Finds value of a property.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="defaultValue">Default value.</param>
    public String GetPropertyValue(string propertyName, String defaultValue) {
        return GetPropertyValue(propertyName) ?? defaultValue;
    }

}
