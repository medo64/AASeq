namespace AASeq.Plugins.Standard;
using System;
using System.Globalization;
using Microsoft.Extensions.Logging;

/// <summary>
/// SYSINFO variable.
/// </summary>
internal sealed class Sysinfo : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        switch (argument.Trim().ToUpperInvariant()) {
            case "HOSTNAME": return Environment.MachineName;
            case "PROCESSORS": return Environment.ProcessorCount.ToString(CultureInfo.InvariantCulture);
            case "UPTIME": return (Environment.TickCount64 / 1000).ToString(CultureInfo.InvariantCulture);
            case "UPTIME_MS": return Environment.TickCount64.ToString(CultureInfo.InvariantCulture);
            case "USERNAME": return Environment.UserName;
            default:
                logger.LogWarning($"Cannot parse SYSINFO argument '{argument}'.");
                return null;
        }
    }

}
