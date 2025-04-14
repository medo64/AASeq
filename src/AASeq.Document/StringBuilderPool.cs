namespace AASeq;
using System;
using System.Text;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

internal static class StringBuilderPool {

    private static readonly ConcurrentQueue<StringBuilder> Pool = new();
    private static readonly int MaxPoolSize = Math.Max(Environment.ProcessorCount, 8);
    private const int MaximumRetainedCapacity = 65536;


    /// <summary>
    /// Gets a StringBuilder from the pool if one is available, otherwise creates one.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder Get() {
        return Pool.TryDequeue(out StringBuilder? sb) ? sb : new StringBuilder(4096);
    }

    /// <summary>
    /// Return a StringBuilder to the pool.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Return(StringBuilder sb) {
        if (sb.Capacity > MaximumRetainedCapacity) { return false; }  // don't pool if too large
        if (Pool.Count >= MaxPoolSize) { return false; }

        sb.Length = 0;
        Pool.Enqueue(sb);
        return true;
    }

}
