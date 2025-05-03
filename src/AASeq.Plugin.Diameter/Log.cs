namespace AASeqPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

internal sealed partial class Log {

    [LoggerMessage(LogLevel.Error, "{message}")]
    public static partial void ReadError(ILogger logger, Exception exception, string message);

    [LoggerMessage(LogLevel.Error, "{message}")]
    public static partial void ConnectionError(ILogger logger, Exception exception, string message);

    [LoggerMessage(LogLevel.Information, "Connected ({endpoint})")]
    public static partial void Connected(ILogger logger, IPEndPoint endpoint);

    [LoggerMessage(LogLevel.Information, ">{messageName}")]
    public static partial void MessageOut(ILogger logger, string messageName);

    [LoggerMessage(LogLevel.Information, "<{messageName}")]
    public static partial void MessageIn(ILogger logger, string messageName);

}
