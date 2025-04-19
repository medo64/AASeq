namespace AASeq;
using System.Diagnostics.Metrics;

internal static class Metrics {

    private static readonly Meter Meter = new("AASeq.Document");

    /// <summary>
    /// Document Load count.
    /// </summary>
    public static Counter<long> NodesLoadCount = Meter.CreateCounter<long>("aaseq.nodes.load.count");

    /// <summary>
    /// Time needed for document Load operation.
    /// </summary>
    public static Histogram<long> NodesLoadMilliseconds = Meter.CreateHistogram<long>("aaseq.nodes.load.milliseconds");


    /// <summary>
    /// Document Save count.
    /// </summary>
    public static Counter<long> NodesSaveCount = Meter.CreateCounter<long>("aaseq.nodes.save.count");

    /// <summary>
    /// Time needed for document Save operation.
    /// </summary>
    public static Histogram<long> NodesSaveMilliseconds = Meter.CreateHistogram<long>("aaseq.nodes.save.milliseconds");


    /// <summary>
    /// Document Clone count.
    /// </summary>
    public static Counter<long> NodesCloneCount = Meter.CreateCounter<long>("aaseq.nodes.clone.count");


    /// <summary>
    /// Time needed for document Clone operation.
    /// </summary>
    public static Histogram<long> NodesCloneMilliseconds = Meter.CreateHistogram<long>("aaseq.nodes.clone.milliseconds");


    /// <summary>
    /// Document Validate count.
    /// </summary>
    public static Counter<long> NodesValidateCount = Meter.CreateCounter<long>("aaseq.nodes.validate.count");


    /// <summary>
    /// Time needed for document Validate operation.
    /// </summary>
    public static Histogram<long> NodesValidateMilliseconds = Meter.CreateHistogram<long>("aaseq.nodes.validate.milliseconds");

}
