using System;
using System.Diagnostics;

namespace Clamito.Gui {
    internal static class Log {

        private static readonly TraceSource Source = new TraceSource("Clamito.Gui");

        [Conditional("TRACE")]
        public static void WriteVerbose(string format, params object[] args) {
            Source.TraceEvent(TraceEventType.Verbose, 0, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteInformation(string format, params object[] args) {
            Source.TraceEvent(TraceEventType.Information, 0, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteWarning(string format, params object[] args) {
            Source.TraceEvent(TraceEventType.Warning, 0, format, args);
        }

        [Conditional("TRACE")]
        public static void WriteError(string format, params object[] args) {
            Source.TraceEvent(TraceEventType.Error, 0, format, args);
        }


        [Conditional("TRACE")]
        public static void WriteException(string prefix, Exception ex) {
            Source.TraceEvent(TraceEventType.Error, 0, "{0}: Unhandled exception - {1}", prefix, ex.Message);
            Source.TraceEvent(TraceEventType.Verbose, 0, "{0}: Unhandled exception - {1}{2}{3}", prefix, ex.Message, Environment.NewLine, ex.StackTrace);
        }

        [Conditional("TRACE")]
        public static void WriteCriticalException(Exception ex) {
            Source.TraceEvent(TraceEventType.Critical, 0, "Unhandled exception - {0}", ex.Message);
            Source.TraceEvent(TraceEventType.Verbose, 0, "Unhandled exception - {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);

            Trace.WriteLine("EXCEPTION: " + ex.GetType().Name + ": " + ex.Message);
            Trace.WriteLine("EXCEPTION: " + ex.StackTrace);
        }

    }
}
