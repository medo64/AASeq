namespace AASeqCli;

using System;
using Microsoft.Extensions.Logging;

internal sealed class Logger : ILogger {

    internal Logger(LogLevel minimumLogLevel) {
        MinimumLogLevel = minimumLogLevel;
    }

    public LogLevel MinimumLogLevel { get; }


    #region ILogger

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel) {
        return (logLevel >= LogLevel.Information);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
        if (logLevel < MinimumLogLevel) { return; }

        switch (logLevel) {
            case LogLevel.Trace:
                Output.WriteVerbose(formatter.Invoke(state, exception).ToString());
                return;
            case LogLevel.Debug:
                Output.WriteDebug(formatter.Invoke(state, exception).ToString());
                return;
            case LogLevel.Information:
                Output.WriteInfo(formatter.Invoke(state, exception).ToString());
                return;
            case LogLevel.Warning:
                Output.WriteWarning(formatter.Invoke(state, exception).ToString());
                return;
            case LogLevel.Error:
                Output.WriteError(formatter.Invoke(state, exception).ToString());
                return;
            case LogLevel.Critical:
                Output.WriteError(formatter.Invoke(state, exception).ToString());
                break;
        }

        var message = formatter.Invoke(state, exception).ToString();
        Output.WriteError(message);
    }

    #endregion ILogger


    public static Logger Default { get; } = new Logger(LogLevel.Warning);

    public static Logger GetInstance(bool debug, bool verbose) {
        return new Logger(debug ? LogLevel.Trace : verbose ? LogLevel.Debug : LogLevel.Information);
    }

}
