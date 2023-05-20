namespace AASeq;
using System;
using System.Diagnostics;

internal static class Helpers {

    #region Exceptions

    [StackTraceHidden]
    public static void ThrowIfArgumentNull(object? argumentValue, string argumentName, string exceptionMessage) {
        if (argumentValue == null) {
            throw new ArgumentNullException(argumentName, exceptionMessage);
        }
    }

    [StackTraceHidden]
    public static void ThrowIfArgumentFalse(bool value, string argumentName, string exceptionMessage) {
        if (value == false) {
            throw new ArgumentOutOfRangeException(argumentName, exceptionMessage);
        }
    }

    [StackTraceHidden]
    public static void ThrowArgumentOutOfRange(string argumentName, string exceptionMessage) {
        throw new ArgumentOutOfRangeException(argumentName, exceptionMessage);
    }

    #endregion Exceptions

}
