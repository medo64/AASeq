namespace AASeqPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

internal sealed partial class Log {

    [LoggerMessage(LogLevel.Error, "[Diameter] {endpoint}: {message}")]
    public static partial void ReadError(ILogger logger, IPEndPoint endpoint, Exception exception, string message);

    [LoggerMessage(LogLevel.Error, "[Diameter] {endpoint}: {message}")]
    public static partial void ConnectionError(ILogger logger, IPEndPoint endpoint, Exception exception, string message);

    [LoggerMessage(LogLevel.Information, "[Diameter] {endpoint}: Connected")]
    public static partial void Connected(ILogger logger, IPEndPoint endpoint);

    [LoggerMessage(LogLevel.Information, "[Diameter] {endpoint}: >{messageName}")]
    public static partial void MessageOut(ILogger logger, IPEndPoint endpoint, string messageName);

    [LoggerMessage(LogLevel.Information, "[Diameter] {endpoint}: <{messageName}")]
    public static partial void MessageIn(ILogger logger, IPEndPoint endpoint, string messageName);

}
