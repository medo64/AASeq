using System;
using System.Diagnostics;
using System.Globalization;

namespace Clamito {
    internal class Log : LogBase {

        public static Log Write = new Log();


        [Conditional("TRACE")]
        public void DocumentLoad(long elapsedMilliseconds) {
            base.TraceEvent(TraceEventType.Information, "Document.Load", "Loaded in {0} ms.", elapsedMilliseconds);
        }

        [Conditional("TRACE")]
        public void DocumentSave(long elapsedMilliseconds) {
            base.TraceEvent(TraceEventType.Information, "Document.Save", "Saved in {0} ms.", elapsedMilliseconds);
        }

    }
}
