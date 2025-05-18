namespace AASeq.Plugins.Standard;
using System;
using System.Globalization;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

/// <summary>
/// RANDOM variable.
/// </summary>
internal sealed class Random : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        var parameters = argument.Split(new[] { ',' }, StringSplitOptions.TrimEntries);
        long min;
        long max;
        if (parameters.Length == 0) {
            min = 0;
            max = 32767;
        } else if ((parameters.Length == 1)
                && long.TryParse(parameters[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out max)
                && (max > 0)) {
            min = 0;
        } else if ((parameters.Length == 2)
                && long.TryParse(parameters[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out min)
                && long.TryParse(parameters[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out max)
                && (min < max)) {
        } else {
            logger.LogWarning($"Unknown argument '{argument}'.");
            min = 0;
            max = 32767;
        }
        var bytes = new byte[16];
        Rnd.GetBytes(bytes);
        var value = BitConverter.ToUInt128(bytes, 0);
        var range = (UInt128)((Int128)max - (Int128)min + 1);
        var result = (long)((Int128)(value % range) + min);
        return (result.ToString("0", CultureInfo.InvariantCulture));
    }

    private static readonly RandomNumberGenerator Rnd = RandomNumberGenerator.Create();

}
