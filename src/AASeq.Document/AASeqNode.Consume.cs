namespace AASeq;
using System;
using System.Collections.Generic;

public sealed partial class AASeqNode {

    /// <summary>
    /// Returns the property value or null if no value is present.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    public string? ConsumeProperty(string propertyName) {
        ArgumentNullException.ThrowIfNull(propertyName);
        if (Properties.Remove(propertyName, out var value)) {
            return value;
        } else {
            return null;
        }
    }

}
