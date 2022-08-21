using System;
using System.Diagnostics;

namespace AASeq;

internal class Log : LogBase {

    public static Log Write = new Log();


    public void Custom(TraceEventType type, string text) {
        base.TraceEvent(type, "CUSTOM", text.Replace("{", "").Replace("}", ""));
    }

}
