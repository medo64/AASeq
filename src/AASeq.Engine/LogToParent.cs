namespace AASeq;
using System;
using Microsoft.Extensions.Logging;

internal class LogToParent : ILogger {

    public LogToParent(ILogger parentLogger, string prefix, LogToFile fileLog) {
        ParentLogger = parentLogger;
        Prefix = "[" + prefix + "] ";
        FileLog = fileLog;
    }

    private readonly ILogger ParentLogger;
    private readonly string Prefix;
    private readonly LogToFile FileLog;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        return ParentLogger.BeginScope(state);
    }

    public bool IsEnabled(LogLevel logLevel) {
        return ParentLogger.IsEnabled(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
        ParentLogger.Log(logLevel, eventId, state, exception, (s, e) => Prefix + formatter(s, e));

        if ((logLevel >= LogLevel.Warning) && FileLog.IsEnabled) {
            switch (logLevel) {
                case LogLevel.Trace: FileLog.WriteLog("V: " + (Prefix ?? "") + formatter(state, exception)); break;
                case LogLevel.Debug: FileLog.WriteLog("D: " + (Prefix ?? "") + formatter(state, exception)); break;
                case LogLevel.Information: FileLog.WriteLog("I: " + (Prefix ?? "") + formatter(state, exception)); break;
                case LogLevel.Warning: FileLog.WriteLog("W: " + (Prefix ?? "") + formatter(state, exception)); break;
                case LogLevel.Error: FileLog.WriteLog("E: " + (Prefix ?? "") + formatter(state, exception)); break;
                case LogLevel.Critical: FileLog.WriteLog("C: " + (Prefix ?? "") + formatter(state, exception)); break;
                default: break;
            }
        }
    }

}
