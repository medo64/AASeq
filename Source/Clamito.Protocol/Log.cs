using System;
using System.Diagnostics;
using System.Globalization;

namespace Clamito {
    internal static class Log {

        [Conditional("TRACE")]
        public static void WriteVerbose(string format, params object[] args) {
            Trace(TraceEventType.Verbose, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteInformation(string format, params object[] args) {
            Trace(TraceEventType.Information, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteWarning(string format, params object[] args) {
            Trace(TraceEventType.Warning, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteError(string format, params object[] args) {
            Trace(TraceEventType.Error, format, args);
        }


        [Conditional("TRACE")]
        public static void WriteException(string prefix, Exception ex) {
            Trace(TraceEventType.Error, "{0}: Unhandled exception - {1}", prefix, ex.Message);
        }


        private static readonly TraceSource Source = new TraceSource("Clamito.Protocol");

        private static void Trace(TraceEventType type, string format, params object[] args) {
#if DEBUG
            Debug.WriteLine(type.ToString().Substring(0, 1) + ": " + string.Format(CultureInfo.InvariantCulture, format, args));
#endif
            Source.TraceEvent(type, 0, format, args);
        }

    }
}