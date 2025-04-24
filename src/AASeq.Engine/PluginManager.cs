namespace AASeq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Plugin management.
/// </summary>
public sealed class PluginManager {

    private PluginManager() {
        var sw = Stopwatch.StartNew();
        try {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var exePath = Environment.ProcessPath ?? throw new InvalidOperationException("Cannot determine process path,");

            var ownAssembly = Assembly.GetExecutingAssembly();
            Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{ownAssembly.FullName}'");
            CheckAssembly(ownAssembly);

            var exeDir = new DirectoryInfo(Path.GetDirectoryName(exePath)!);
            foreach (var file in exeDir.GetFiles("*.dll")) {
                if (file.Name.Equals("AASeq.Engine.dll", StringComparison.OrdinalIgnoreCase)) { continue; }

                Debug.WriteLine($"[AASeq.Engine] Loading file '{file}'");
                var loadContext = new AssemblyLoadContext(file.FullName, isCollectible: true);
                var assembly = loadContext.LoadFromAssemblyPath(file.FullName);
                Metrics.PluginFileLoadCount.Add(1);

                Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{assembly.FullName}' ({file.Name})");
                if (CheckAssembly(assembly)) {
                    PluginLoadContexts.Add(loadContext);
                } else {
                    loadContext.Unload();
                }
                Metrics.PluginFileCheckCount.Add(1);
            }

            CommandPlugins = new ReadOnlyCollection<CommandPlugin>([.. CommandPluginsByName.Values]);
            EndpointPlugins = new ReadOnlyCollection<EndpointPlugin>([.. EndpointPluginsByName.Values]);
        } finally {
            Debug.WriteLine($"[AASeq.Engine] Init: {sw.ElapsedMilliseconds} ms");
            Metrics.PluginFileLoadMilliseconds.Record(sw.ElapsedMilliseconds);
        }
    }

    private bool CheckAssembly(Assembly assembly) {
        var anyFound = false;
        foreach (var type in assembly.GetTypes()) {
            if (!type.IsClass) { continue; }

            Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{type.Name}' ({assembly.FullName})");

            var endpointPlugin = GetEndpointPlugin(type);
            if (endpointPlugin is not null) {
                anyFound = true;
                if (endpointPlugin.Name.Equals("Me", StringComparison.OrdinalIgnoreCase)) { continue; }
                EndpointPluginsByName.Add(endpointPlugin.Name, endpointPlugin);
                Debug.WriteLine($"[AASeq.Engine] Found endpoint plugin '{endpointPlugin.Name}' in '{type.Name}' ({assembly.FullName})");
            }

            var commandPlugin = GetCommandPlugin(type);
            if (commandPlugin is not null) {
                anyFound = true;
                CommandPluginsByName.Add(commandPlugin.Name, commandPlugin);
                Debug.WriteLine($"[AASeq.Engine] Found command plugin '{commandPlugin.Name}' in '{type.Name}' ({assembly.FullName})");
            }

        }
        return anyFound;
    }

    private static readonly List<AssemblyLoadContext> PluginLoadContexts = [];  // just to keep them around
    private readonly SortedDictionary<string, EndpointPlugin> EndpointPluginsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly SortedDictionary<string, CommandPlugin> CommandPluginsByName = new(StringComparer.OrdinalIgnoreCase);


    /// <summary>
    /// Gets all command plugins.
    /// </summary>
    public IReadOnlyCollection<IPluginDefinition> CommandPlugins { get; }

    /// <summary>
    /// Gets all endpoint plugins.
    /// </summary>
    public IReadOnlyCollection<IPluginDefinition> EndpointPlugins { get; }


    /// <summary>
    /// Gets plugin loader instance.
    /// </summary>
    public static PluginManager Instance { get; } = new PluginManager();


    /// <summary>
    /// Finds a command plugin by name.
    /// </summary>
    /// <param name="pluginName">Plugin name.</param>
    internal static CommandPlugin? FindCommandPlugin(string pluginName) {
        return Instance.CommandPluginsByName.TryGetValue(pluginName, out var plugin) ? plugin : null;
    }

    /// <summary>
    /// Finds a endpoint plugin by name.
    /// </summary>
    /// <param name="pluginName">Plugin name.</param>
    internal static EndpointPlugin? FindEndpointPlugin(string pluginName) {
        return Instance.EndpointPluginsByName.TryGetValue(pluginName, out var plugin) ? plugin : null;
    }


    #region Helpers

    private static CommandPlugin? GetCommandPlugin(Type type) {
        if (!type.IsClass) { return null; }

        var mCreateInstance = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (mCreateInstance is null) { return null; }
        if (!mCreateInstance.ReturnType.IsAssignableTo(typeof(object))) { return null; }

        var mExecute = type.GetMethod("ExecuteAsync", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(AASeqNodes), typeof(CancellationToken)]);
        if (mExecute is null) { return null; }
        if (!mExecute.ReturnType.Equals(typeof(Task))) { return null; }

        return new CommandPlugin(type, mCreateInstance, mExecute);
    }

    private static EndpointPlugin? GetEndpointPlugin(Type type) {
        if (!type.IsClass) { return null; }

        var mCreateInstance = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (mCreateInstance is null) { return null; }
        if (!mCreateInstance.ReturnType.IsAssignableTo(typeof(object))) { return null; }

        var mSend = type.GetMethod("SendAsync", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(Guid), typeof(String), typeof(AASeqNodes), typeof(CancellationToken)]);
        if (mSend is null) { return null; }
        if (!mSend.ReturnType.Equals(typeof(Task))) { return null; }

        var mReceive = type.GetMethod("ReceiveAsync", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(Guid), typeof(string), typeof(AASeqNodes), typeof(CancellationToken)]);
        if (mReceive is null) { return null; }
        if (!mReceive.ReturnType.Equals(typeof(Task<Tuple<string, AASeqNodes>>))) { return null; }

        return new EndpointPlugin(type, mCreateInstance, mSend, mReceive);
    }

    #endregion Helpers

}
