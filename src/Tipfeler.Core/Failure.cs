using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tipfeler {
    /// <summary>
    /// Error/warning result.
    /// </summary>
    public class Failure {

        private Failure(int line, string format, params object[] args) {
            Line = line;
            Text = String.Format(CultureInfo.InvariantCulture, format, args);
        }

        private Failure(int line, string text, bool isWarning) { //used for clone
            Line = line;
            Text = text;
            IsWarning = isWarning;
        }


        /// <summary>
        /// Gets error text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets line where error occurred. Value will be 0 if line number is not known.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets if result is warning.
        /// </summary>
        public bool IsWarning { get; private set; }

        /// <summary>
        /// Gets if result is error.
        /// </summary>
        public bool IsError { get { return !IsWarning; } }


        #region Create

        /// <summary>
        /// Returns new warning.
        /// </summary>
        /// <param name="format">Warning text as composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static Failure NewWarning(string format, params object[] args) {
            return new Failure(0, format, args) { IsWarning = true };
        }

        /// <summary>
        /// Returns new warning.
        /// </summary>
        /// <param name="line">Warning line.</param>
        /// <param name="format">Warning text as composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public static Failure NewWarning(int line, string format, params object[] args) {
            return new Failure(line, format, args) { IsWarning = true };
        }


        /// <summary>
        /// Returns new error.
        /// </summary>
        /// <param name="format">Error text as composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static Failure NewError(string format, params object[] args) {
            return new Failure(0, format, args) { IsWarning = false };
        }

        /// <summary>
        /// Returns new error.
        /// </summary>
        /// <param name="line">Error line.</param>
        /// <param name="format">Error text as composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public static Failure NewError(int line, string format, params object[] args) {
            return new Failure(line, format, args) { IsWarning = false };
        }

        #endregion


        /// <summary>
        /// Creates a copy of the result.
        /// </summary>
        public Failure Clone() {
            return new Failure(Line, Text, IsWarning, IsError);
        }

        /// <summary>
        /// Creates a copy of the result.
        /// </summary>
        /// <param name="newPrefix">New prefix for all text elements.</param>
        /// <exception cref="System.ArgumentNullException">New prefix cannot be null.</exception>
        public Failure Clone(string newPrefix) {
            if (newPrefix == null) { throw new ArgumentNullException(nameof(newPrefix), "New prefix cannot be null."); }
            return new Failure(Line, newPrefix + Text, IsWarning);
        }

    }
}
