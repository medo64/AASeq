namespace AASeq.Plugins.Standard;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

/// <summary>
/// RANDOM variable.
/// </summary>
internal sealed class RandomHex : IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static string? GetVariableValue(ILogger logger, string argument) {
        int count;
        if (argument.Length == 0) {
            count = 16;
        } else if (!int.TryParse(argument, NumberStyles.Integer, CultureInfo.InvariantCulture, out count)) {
            logger.LogWarning($"Unknown argument '{argument}'.");
            count = 16;
        }

        var bytes = new byte[count];
        Rnd.GetBytes(bytes);
        var sb = new StringBuilder(count);
        for (var i = 0; i < count; i++) {
            sb.Append((bytes[i] & 0x0F).ToString("0", CultureInfo.InvariantCulture));
        }
        return sb.ToString();
    }

    private static readonly RandomNumberGenerator Rnd = RandomNumberGenerator.Create();

}
