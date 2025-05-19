namespace AASeq.Plugins.Standard;
using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// ENV variable.
/// </summary>
internal sealed class Env : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        if (argument.Length == 0) {
            logger.LogWarning($"Unknown argument '{argument}'.");
            return null;
        }

        if (Environment.GetEnvironmentVariable(argument) is string envValue) {
            return envValue;
        } else {  // make it case insensitive on Linux too
            foreach (var key in Environment.GetEnvironmentVariables().Keys) {
                var varName = key.ToString();
                if (argument.Equals(varName, StringComparison.OrdinalIgnoreCase)) {
                    return Environment.GetEnvironmentVariable(varName);
                }
            }
        }

        return null;
    }

}
