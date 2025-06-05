namespace AASeqPlugin;

using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using AASeq;
using AASeq.Diameter;

/// <summary>
/// DIAMETER_END_TO_END variable.
/// </summary>
internal sealed class DiameterEndToEnd : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        if (!string.IsNullOrEmpty(argument)) { logger.LogWarning("Unknown argument '{0}'", argument); }
        return DiameterMessage.GetNewEndToEndIdentifier().ToString(CultureInfo.InvariantCulture);
    }

}
