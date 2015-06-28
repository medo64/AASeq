using System;
using System.Diagnostics;
using System.Globalization;

namespace Clamito.Gui {
    internal static class Log {

        [Conditional("TRACE")]
        public static void WriteVerbose(string format, params object[] args) {
            TraceEvent(TraceEventType.Verbose, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteInformation(string format, params object[] args) {
            TraceEvent(TraceEventType.Information, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteWarning(string format, params object[] args) {
            TraceEvent(TraceEventType.Warning, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteError(string format, params object[] args) {
            TraceEvent(TraceEventType.Error, format, args);
        }


        [Conditional("TRACE")]
        public static void WriteException(string prefix, Exception ex) {
            TraceEvent(TraceEventType.Error, "{0}: Unhandled exception - {1}", prefix, ex.Message);
            TraceEvent(TraceEventType.Verbose, "{0}: Unhandled exception - {1}{2}{3}", prefix, ex.Message, Environment.NewLine, ex.StackTrace);
        }

        [Conditional("TRACE")]
        public static void WriteCriticalException(Exception ex) {
            TraceEvent(TraceEventType.Critical, "Unhandled exception - {0}", ex.Message);
            TraceEvent(TraceEventType.Verbose, "Unhandled exception - {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);

            Trace.WriteLine("EXCEPTION: " + ex.GetType().Name + ": " + ex.Message);
            Debug.WriteLine("EXCEPTION: " + ex.StackTrace);
        }


        private static readonly TraceSource Source = new TraceSource("Clamito.Gui");

        private static void TraceEvent(TraceEventType type, string format, params object[] args) {
#if DEBUG
            Debug.WriteLine(type.ToString().Substring(0, 1) + ": " + string.Format(CultureInfo.InvariantCulture, format, args));
#endif
            Source.TraceEvent(type, 0, format, args);
        }

    }
}
