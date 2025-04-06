namespace AASeq;
using System.Diagnostics.Metrics;

internal static class Metrics {

    private static readonly Meter Meter = new("AASeq.Engine");

    /// <summary>
    /// Number of files checked for plugins.
    /// </summary>
    public static Counter<long> PluginFileCheckCount = Meter.CreateCounter<long>("aaseq.engine.plugin.check.count");

    /// <summary>
    /// Number of files loaded for plugins.
    /// </summary>
    public static Counter<long> PluginFileLoadCount = Meter.CreateCounter<long>("aaseq.engine.plugin.load.count");


    /// <summary>
    /// Time needed for plugin load.
    /// </summary>
    public static Histogram<long> PluginFileLoadMilliseconds = Meter.CreateHistogram<long>("aaseq.engine.plugin.load.milliseconds");

}
