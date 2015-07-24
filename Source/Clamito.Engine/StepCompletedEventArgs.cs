using System;
using System.Collections.Generic;

namespace Clamito {
    /// <summary>
    /// Event arguments for a completed step.
    /// </summary>
    public class StepCompletedEventArgs : EventArgs {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="results">Result collection.</param>
        public StepCompletedEventArgs(IEnumerable<Failure> results) {
            if (results == null) { throw new ArgumentNullException("results", "Result is null."); }
            this.Results = results;
        }


        /// <summary>
        /// Gets result collection.
        /// </summary>
        public IEnumerable<Failure> Results { get; private set; }


        /// <summary>
        /// Gets if result was success.
        /// </summary>
        public bool IsSuccess {
            get {
                foreach (var result in this.Results) {
                    if (result.IsError) { return false; }
                }
                return true;
            }
        }

    }
}
