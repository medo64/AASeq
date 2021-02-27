using System;
using System.Diagnostics;
using System.Globalization;

namespace Clamito {

    /// <summary>
    /// Base class for logging.
    /// </summary>
    public abstract class LogBase {

        /// <summary>
        /// Writes Verbose log.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Format cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        [Conditional("TRACE")]
        public void Verbose(string source, string format, params object[] args) {
            TraceEvent(TraceEventType.Verbose, source, format, args);
        }

        /// <summary>
        /// Writes Information log.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Format cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        [Conditional("TRACE")]
        public void Information(string source, string format, params object[] args) {
            TraceEvent(TraceEventType.Information, source, format, args);
        }

        /// <summary>
        /// Writes Warning log.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Format cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        [Conditional("TRACE")]
        public void Warning(string source, string format, params object[] args) {
            TraceEvent(TraceEventType.Warning, source, format, args);
        }

        /// <summary>
        /// Writes Error log.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Format cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        [Conditional("TRACE")]
        public void Error(string source, string format, params object[] args) {
            TraceEvent(TraceEventType.Error, source, format, args);
        }


        /// <summary>
        /// Writes Exception log.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="exception">Exception.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Exception cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        [Conditional("TRACE")]
        public void Error(string source, Exception exception) {
            if (exception == null) { throw new ArgumentNullException(nameof(exception), "Exception cannot be null."); }
            TraceEvent(TraceEventType.Error, source, "Unhandled Exception: {0}", source, exception.Message);
            TraceEvent(TraceEventType.Verbose, source, "Unhandled Exception: {0}{1}{2}", source, exception.Message, Environment.NewLine, exception.StackTrace);
        }



        private readonly TraceSource Source = new TraceSource("Clamito.Core");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Event type.</param>
        /// <param name="source">Source.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Format cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source cannot contain curly brackets.</exception>
        protected void TraceEvent(TraceEventType type, string source, string format, params object[] args) {
            if (source == null) { throw new ArgumentNullException(nameof(source), "Source cannot be null."); }
            if (source.IndexOfAny(new char[] { '{', '}' }) >= 0) { throw new ArgumentOutOfRangeException(nameof(source), "Source cannot contain curly brackets."); }
            if (format == null) { throw new ArgumentNullException(nameof(format), "Format cannot be null."); }
#if DEBUG
            Debug.WriteLine(type.ToString().Substring(0, 1) + ": " + this.Source.Name + ":" + source + ": " + string.Format(CultureInfo.InvariantCulture, format, args));
#endif
            this.Source.TraceEvent(type, 0, source + ": " + format, args);
        }

    }
}
