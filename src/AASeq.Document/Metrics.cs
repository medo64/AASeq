namespace AASeq;
using System.Diagnostics.Metrics;

internal static class Metrics {

    private static readonly Meter Meter = new("AASeq.Document");

    /// <summary>
    /// Document creation count.
    /// </summary>
    public static Counter<long> DocumentNewCount = Meter.CreateCounter<long>("aaseq.document.new.count");

    /// <summary>
    /// Document load count.
    /// </summary>
    public static Counter<long> DocumentLoadCount = Meter.CreateCounter<long>("aaseq.document.load.count");

    /// <summary>
    /// Document save count.
    /// </summary>
    public static Counter<long> DocumentSaveCount = Meter.CreateCounter<long>("aaseq.document.save.count");

    /// <summary>
    /// Document clone count.
    /// </summary>
    public static Counter<long> DocumentCloneCount = Meter.CreateCounter<long>("aaseq.document.clone.count");


    /// <summary>
    /// Time needed for document load.
    /// </summary>
    public static Histogram<long> DocumentLoadMilliseconds = Meter.CreateHistogram<long>("aaseq.document.load.milliseconds");

    /// <summary>
    /// Time needed for document save.
    /// </summary>
    public static Histogram<long> DocumentSaveMilliseconds = Meter.CreateHistogram<long>("aaseq.document.save.milliseconds");

    /// <summary>
    /// Time needed for document clone.
    /// </summary>
    public static Histogram<long> DocumentCloneMilliseconds = Meter.CreateHistogram<long>("aaseq.document.clone.milliseconds");

}
