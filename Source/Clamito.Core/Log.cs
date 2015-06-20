using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Clamito {

    /// <summary>
    /// Basic logging.
    /// </summary>
    public sealed class Log : IDisposable {

        private Log(String name) {
            this.Name = name;
            this.NamePadded = ((name.Length > 20) ? name.Substring(0, 20) : name).PadRight(20);
            this.TraceSource = new TraceSource(name);
        }

        private readonly string Name;
        private readonly string NamePadded;
        private readonly TraceSource TraceSource;
        private readonly Object SyncRoot = new object();


        /// <summary>
        /// Writes Debug log entry.
        /// To be used for writing out detailed debug operation.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("DEBUG")]
        public void Debug(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(null, "DEBUG", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Debug log entry.
        /// To be used for writing out a detailed debug operation.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("DEBUG")]
        public void Debug(String text, params Object[] args) {
            this.Debug(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Debug log entry.
        /// To be used for writing out a detailed debug operation.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("DEBUG")]
        public void Debug(Int32 eventId, String text, params Object[] args) {
            this.Debug(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Debug log entry.
        /// To be used for writing out a detailed debug operation.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("DEBUG")]
        public void Debug(Guid uniqueId, String text, params Object[] args) {
            this.Debug(0, uniqueId, text, args);
        }


        /// <summary>
        /// Writes Verbose log entry.
        /// To be used for writing out details for an operation.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Verbose(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(TraceEventType.Verbose, "VERBOSE", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Verbose log entry.
        /// To be used for writing out details for an operation.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Verbose(String text, params Object[] args) {
            this.Verbose(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Verbose log entry.
        /// To be used for writing out details for an operation.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Verbose(Int32 eventId, String text, params Object[] args) {
            this.Verbose(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Verbose log entry.
        /// To be used for writing out details for an operation.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Verbose(Guid uniqueId, String text, params Object[] args) {
            this.Verbose(0, uniqueId, text, args);
        }


        /// <summary>
        /// Writes Information log entry.
        /// To be used for general information.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Information(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(TraceEventType.Information, "INFO", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Information log entry.
        /// To be used for general information.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Information(String text, params Object[] args) {
            this.Information(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Information log entry.
        /// To be used for general information.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Information(Int32 eventId, String text, params Object[] args) {
            this.Information(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Information log entry.
        /// To be used for general information.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Information(Guid uniqueId, String text, params Object[] args) {
            this.Information(0, uniqueId, text, args);
        }


        /// <summary>
        /// Writes Warning log entry.
        /// To be used for recoverable errors.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Warning(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(TraceEventType.Warning, "WARNING", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Warning log entry.
        /// To be used for recoverable errors.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Warning(String text, params Object[] args) {
            this.Warning(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Warning log entry.
        /// To be used for recoverable errors.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Warning(Int32 eventId, String text, params Object[] args) {
            this.Warning(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Warning log entry.
        /// To be used for recoverable errors.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Warning(Guid uniqueId, String text, params Object[] args) {
            this.Warning(0, uniqueId, text, args);
        }


        /// <summary>
        /// Writes Error log entry.
        /// To be used when serious error occurs.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Error(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(TraceEventType.Error, "ERROR", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Error log entry.
        /// To be used when serious error occurs.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Error(String text, params Object[] args) {
            this.Error(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Error log entry.
        /// To be used when serious error occurs.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Error(Int32 eventId, String text, params Object[] args) {
            this.Error(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Error log entry.
        /// To be used when serious error occurs.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Error(Guid uniqueId, String text, params Object[] args) {
            this.Error(0, uniqueId, text, args);
        }


        /// <summary>
        /// Writes Critical log entry.
        /// To be used when operation fails in such manner that further execution is impeeded.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Critical(Int32 eventId, Guid uniqueId, String text, params Object[] args) {
            WriteTraceLine(TraceEventType.Critical, "CRITICAL", eventId, uniqueId, text, args);
        }

        /// <summary>
        /// Writes Critical log entry.
        /// To be used when operation fails in such manner that further execution is impeeded.
        /// </summary>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Critical(String text, params Object[] args) {
            this.Critical(0, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Critical log entry.
        /// To be used when operation fails in such manner that further execution is impeeded.
        /// </summary>
        /// <param name="eventId">Event ID.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Critical(Int32 eventId, String text, params Object[] args) {
            this.Critical(eventId, Guid.Empty, text, args);
        }

        /// <summary>
        /// Writes Critical log entry.
        /// To be used when operation fails in such manner that further execution is impeeded.
        /// </summary>
        /// <param name="uniqueId">Unique ID that has same value for related operations. Can be null.</param>
        /// <param name="text">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        [Conditional("TRACE")]
        public void Critical(Guid uniqueId, String text, params Object[] args) {
            this.Critical(0, uniqueId, text, args);
        }


        private static readonly String Indent100 = new String(' ', 100);
        private readonly ThreadLocal<StringBuilder> StringBuilders = new ThreadLocal<StringBuilder>(() => { return new StringBuilder(); });

        [Conditional("TRACE")]
        private void WriteTraceLine(TraceEventType? severity, String severityText, Int32 eventId, Guid uniqueId, String format, params Object[] args) {
            try {
                if (severity != null) {
                    foreach (var line in this.GetTraceLines(severityText, eventId, uniqueId, format, args)) {
                        this.TraceSource.TraceEvent(severity.Value, eventId, line);
                    }
                }
                WriteDebugLine(severityText[0], format, args); //executed only if DEBUG is set
            } catch (Exception ex) {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        private IEnumerable<String> GetTraceLines(String severityText, Int32 eventId, Guid uniqueId, String format, params Object[] args) {
            if (eventId < 0) { eventId = 0; }
            if (eventId > 65535) { eventId = 65535; }

            var sb = this.StringBuilders.Value;
            sb.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
            sb.Append(" ");
            sb.Append(this.NamePadded);
            sb.Append(" ");
            sb.Append(severityText.PadRight(8));
            sb.Append(" ");
            sb.Append(((eventId != 0) ? "#" + eventId.ToString(CultureInfo.InvariantCulture) : "").PadRight(6));
            sb.Append(" ");
            sb.Append(((uniqueId != Guid.Empty) ? uniqueId.ToString("B", CultureInfo.InvariantCulture) : "").PadRight(38));
            sb.Append(" ");

            lock (this.SyncRoot) { //to ensure that multiple lines go one after other
                foreach (var line in String.Format(CultureInfo.InvariantCulture, format, args).Split(new String[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (sb.Length > 0) {
                        yield return sb.ToString() + line;
                        sb.Length = 0;
                    } else {
                        yield return Log.Indent100 + line;
                    }
                }
            }
        }


        [Conditional("DEBUG")]
        private void WriteDebugLine(Char severity, String format, params Object[] args) {
            foreach (var line in this.GetDebugLines(severity, format, args)) {
                System.Diagnostics.Debug.WriteLine(line);
            }
        }

        private IEnumerable<String> GetDebugLines(Char severity, String format, params Object[] args) {
            var sb = new StringBuilder();
            sb.Append(severity);
            sb.Append(": ");
            sb.Append(this.Name);
            sb.Append(": ");
            var indentCount = sb.Length;
            lock (this.SyncRoot) { //to ensure that multiple lines go one after other
                foreach (var line in String.Format(CultureInfo.InvariantCulture, format, args).Split(new String[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (sb.Length > 0) {
                        yield return sb.ToString() + line;
                        sb.Length = 0;
                    } else {
                        yield return new String(' ', indentCount) + line;
                    }
                }
            }
        }


        #region Static

        private static readonly object SyncRootLogDictionary = new object();
        private static readonly Dictionary<string, Log> LogDictionary = new Dictionary<string, Log>();


        /// <summary>
        /// Returns logging class based on calling assembly name.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Log GetLog() {
            return GetLog(Assembly.GetCallingAssembly().GetName().Name);
        }

        /// <summary>
        /// Returns logging class for specified log name.
        /// </summary>
        /// <param name="name">Name of the log.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Log GetLog(String name) {
            if (name == null) { name = Assembly.GetCallingAssembly().GetName().Name; }
            lock (Log.SyncRootLogDictionary) {
                if (LogDictionary.ContainsKey(name)) {
                    return LogDictionary[name];
                } else {
                    var newLog = new Log(name);
                    LogDictionary.Add(name, newLog);
                    return newLog;
                }
            }
        }


        /// <summary>
        /// Returns new GUID that can be used as an unique ID.
        /// </summary>
        public static Guid NewUniqueId() {
            return Guid.NewGuid();
        }

        #endregion


        /// <summary>
        /// Releases used resources.
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases used resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        void Dispose(bool disposing) {
            if (disposing) {
                this.StringBuilders.Dispose();
            }
        }

    }
}
