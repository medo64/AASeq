using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Clamito {

    /// <summary>
    /// Dummy protocol.
    /// </summary>
    public sealed class WaitCommand : CommandPlugin {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public WaitCommand() { }


        #region Definition

        /// <summary>
        /// Gets unique name for command.
        /// </summary>
        public override string Name { get { return "Wait"; } }

        /// <summary>
        /// Gets protocol description.
        /// </summary>
        public override string Description { get { return "Delay handling command."; } }

        #endregion


        #region Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="data">Command data.</param>
        public override IEnumerable<Failure> Execute(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            var interval = data["Interval"];

            int milliseconds;
            if (interval == null) {
                Thread.Sleep(1000);
                yield break;
            } else if (TryParseTime(interval, out milliseconds)) {
                Thread.Sleep(milliseconds);
                yield break;
            } else {
                Thread.Sleep(1000);
                yield return Failure.NewWarning("Cannot parse interval {0}.", interval);
            }
        }

        private static bool TryParseTime(string parameters, out int milliseconds) {
            parameters = parameters.Trim();
            if (parameters.EndsWith("ms", StringComparison.OrdinalIgnoreCase)) {
                if (int.TryParse(parameters.Substring(0, parameters.Length - 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out milliseconds)) {
                    return true;
                }
            } else if (parameters.EndsWith("s", StringComparison.OrdinalIgnoreCase)) {
                if (int.TryParse(parameters.Substring(0, parameters.Length - 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out milliseconds)) {
                    milliseconds *= 1000;
                    return true;
                }
            }

            milliseconds = 1000;
            return false;
        }

        #endregion


        #region Data

        /// <summary>
        /// Returns default data fields.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Calling the method two times in succession creates different results.")]
        public override FieldCollection GetDefaultData() {
            return new FieldCollection(new Field[] { new Field("Interval", "1s") });
        }

        /// <summary>
        /// Returns data errors.
        /// </summary>
        /// <param name="data">Data fields to validate.</param>
        public override IEnumerable<Failure> ValidateData(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            var errors = new List<Failure>();
            foreach (var path in data.AllPaths) {
                if (!path.Path.Equals("Interval")) {
                    yield return Failure.NewWarning("Unknown field: {0}.", path);
                }
            }
        }

        #endregion

    }
}
