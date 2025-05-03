namespace AASeq;
using System;
using Microsoft.Extensions.Logging;

internal class Logger : ILogger {

    public Logger(ILogger rootLogger, string prefix) {
        RootLogger = rootLogger;
        Prefix = "[" + prefix + "] ";
    }

    private readonly ILogger RootLogger;
    private readonly string Prefix;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        return RootLogger.BeginScope(state);
    }

    public bool IsEnabled(LogLevel logLevel) {
        return RootLogger.IsEnabled(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
         RootLogger.Log(logLevel, eventId, state, exception, (s, e) => Prefix + formatter(s, e));
    }
}
