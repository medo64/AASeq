namespace AASeq;
using System;

/// <summary>
/// Main execution engine.
/// </summary>
public sealed class Engine : IDisposable {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="document">Document.</param>
    public Engine(AASeqDocument document) {
        Document = document;
    }


    /// <summary>
    /// Gets root document.
    /// </summary>
    public AASeqDocument Document { get; }


    #region IDisposable

    /// <summary>
    /// Disposes the engine.
    /// </summary>
    public void Dispose() {
    }

    #endregion IDisposable

}
