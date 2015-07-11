using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public override ResultCollection Execute(FieldCollection data) {
            if (data == null) { throw new ArgumentNullException(nameof(data), "Data cannot be null."); }
            var interval = data["Interval"];

            int milliseconds;
            if (interval == null) {
                Thread.Sleep(1000);
                return true;
            } else if (TryParseTime(interval, out milliseconds)) {
                Thread.Sleep(milliseconds);
                return true;
            } else {
                Thread.Sleep(1000);
                return ErrorResult.NewWarning("Cannot parse interval {0}.", interval);
            }
        }

        #endregion


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

    }
}
