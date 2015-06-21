using System;

namespace Clamito {
    /// <summary>
    /// Event arguments for a completed step.
    /// </summary>
    public class StepCompletedEventArgs : EventArgs {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="results">Result collection.</param>
        public StepCompletedEventArgs(ResultCollection results) {
            if (results == null) { throw new ArgumentNullException("results", "Result is null."); }
            this.Results = results;
        }


        /// <summary>
        /// Gets result collection.
        /// </summary>
        public ResultCollection Results { get; private set; }


        /// <summary>
        /// Gets if result was success.
        /// </summary>
        public bool IsSuccess {
            get { return this.Results.IsSuccess; }
        }

    }
}
