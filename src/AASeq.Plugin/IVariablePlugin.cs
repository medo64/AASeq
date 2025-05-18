namespace AASeq;
using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// Variable plugin interface marker.
/// Use is optional since plugin loading is based on method signatures.
/// </summary>
public interface IVariablePlugin {

    /// <summary>
    /// Gets value of the variable.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="argument">Argument.</param>
    public static abstract string? GetVariableValue(ILogger logger, string argument);

}
