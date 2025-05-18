namespace AASeq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Command plugin.
/// </summary>
internal sealed class VariablePlugin : PluginBase {

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="type">Plugin reflection type.</param>
    /// <param name="getVariableValueMethodInfo">Reflection data for GetVariables method.</param>
    public VariablePlugin(Type type, MethodInfo getVariableValueMethodInfo)
        : base(type) {
        GetVariableValueMethodInfo = getVariableValueMethodInfo;

        var sbPluginName = new StringBuilder();
        var chPluginName = new Queue<char>(base.Name);
        while (chPluginName.Count > 0) {
            var ch = chPluginName.Dequeue();
            if (char.IsUpper(ch)) {  // uppercase followed by a non-uppercase starts the new word
                if (!char.IsUpper(chPluginName.Peek()) && (sbPluginName.Length > 0)) {
                    sbPluginName.Append('_');
                }
                sbPluginName.Append(ch);
            } else if (char.IsLower(ch) || char.IsNumber(ch)) {
                sbPluginName.Append(char.ToUpperInvariant(ch));
            } else {
                if (sbPluginName[^1] is not '_') { sbPluginName.Append('_'); }
            }
        }
        Name = sbPluginName.ToString();
    }

    /// <summary>
    /// Gets variable name.
    /// </summary>
    public override string Name { get; }

    private readonly MethodInfo GetVariableValueMethodInfo;


    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public string? GetVariableValue(ILogger logger, string argument) {
        return GetVariableValueMethodInfo.Invoke(null, [logger, argument]) as string;
    }

}
