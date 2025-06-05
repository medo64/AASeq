namespace AASeqPlugin;

using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using AASeq;
using AASeq.Diameter;

/// <summary>
/// DIAMETER_SESSION_ID variable.
/// </summary>
internal sealed class DiameterSessionId : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        var identity = string.IsNullOrEmpty(argument) ? Environment.MachineName : argument.Trim();
        return DiameterMessage.GetNewSessionId(identity);
    }

}
