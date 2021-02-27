using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Clamito {

    /// <summary>
    /// Dummy protocol.
    /// </summary>
    public sealed class LogCommand : CommandPlugin {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public LogCommand() { }


        #region Definition

        /// <summary>
        /// Gets unique name for command.
        /// </summary>
        public override string Name { get { return "Log"; } }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public override string Description { get { return "Custom log message."; } }

        #endregion


        #region Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="data">Command data.</param>
        public override IEnumerable<Failure> Execute(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            var level = data["Level"] ?? "";
            var text = data["Text"] ?? "";

            if (level.Equals("Critical", StringComparison.OrdinalIgnoreCase) || level.StartsWith("C", StringComparison.OrdinalIgnoreCase)) {
                Log.Write.Custom(TraceEventType.Critical, text);
            } else if (level.Equals("Error", StringComparison.OrdinalIgnoreCase) || level.StartsWith("E", StringComparison.OrdinalIgnoreCase)) {
                Log.Write.Custom(TraceEventType.Error, text);
            } else if (level.Equals("Warning", StringComparison.OrdinalIgnoreCase) || level.StartsWith("W", StringComparison.OrdinalIgnoreCase)) {
                Log.Write.Custom(TraceEventType.Warning, text);
            } else if (level.Equals("Information", StringComparison.OrdinalIgnoreCase) || level.StartsWith("I", StringComparison.OrdinalIgnoreCase)) {
                Log.Write.Custom(TraceEventType.Information, text);
            } else if (level.Equals("Verbose", StringComparison.OrdinalIgnoreCase) || level.StartsWith("V", StringComparison.OrdinalIgnoreCase)) {
                Log.Write.Custom(TraceEventType.Verbose, text);
            } else {
                Log.Write.Custom(TraceEventType.Verbose, text);
                yield return Failure.NewWarning("Cannot determine log level based on '{0}'.", level);
            }
        }

        #endregion


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public override FieldCollection GetDefaultData() {
            return new FieldCollection(new Field[] { new Field("Text", ""), new Field("Level", "Verbose") });
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public override IEnumerable<Failure> ValidateData(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            foreach (var path in data.AllPaths) {
                if (!path.Path.Equals("Text") && !path.Path.Equals("Level")) {
                    yield return Failure.NewWarning("Unknown field: {0}.", path);
                }
            }
        }

        #endregion

    }
}
