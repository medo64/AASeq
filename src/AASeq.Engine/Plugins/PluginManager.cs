namespace AASeq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

/// <summary>
/// Plugin management.
/// </summary>
internal sealed class PluginManager {

    private PluginManager() {
        var sw = Stopwatch.StartNew();
        try {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var exePath = Environment.ProcessPath ?? throw new InvalidOperationException("Cannot determine process path,");

            var exeDir = new DirectoryInfo(Path.GetDirectoryName(exePath)!);
            foreach (var file in exeDir.GetFiles("*.dll")) {
                Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{file}'");

                Assembly assembly;
                if (file.Name.Equals("AASeq.Engine.dll", StringComparison.OrdinalIgnoreCase)) {
                    assembly = Assembly.GetExecutingAssembly();
                    Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{assembly.FullName}'");
                } else {
                    assembly = Assembly.LoadFile(file.FullName);
                    Metrics.PluginFileLoadCount.Add(1);
                    Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{assembly.FullName}' ({file.Name})");
                }
                Metrics.PluginFileCheckCount.Add(1);

                foreach (var type in assembly.GetTypes()) {
                    if (!type.IsClass) { continue; }

                    Debug.WriteLine($"[AASeq.Engine] Checking for plugins in '{type.Name}' ({assembly.FullName})");

                    var endpointPlugin = GetEndpointPlugin(type);
                    if (endpointPlugin is not null) {
                        if (endpointPlugin.Name.Equals("Me", StringComparison.OrdinalIgnoreCase)) { continue; }
                        EndpointPluginsByName.Add(endpointPlugin.Name, endpointPlugin);
                        Debug.WriteLine($"[AASeq.Engine] Found endpoint plugin '{endpointPlugin.Name}' in '{type.Name}' ({assembly.FullName})");
                    }

                    var commandPlugin = GetCommandPlugin(type);
                    if (commandPlugin is not null) {
                        CommandPluginsByName.Add(commandPlugin.Name, commandPlugin);
                        Debug.WriteLine($"[AASeq.Engine] Found command plugin '{commandPlugin.Name}' in '{type.Name}' ({assembly.FullName})");
                    }

                }
            }

            CommandPlugins = new ReadOnlyCollection<CommandPlugin>([.. CommandPluginsByName.Values]);
            EndpointPlugins = new ReadOnlyCollection<EndpointPlugin>([.. EndpointPluginsByName.Values]);
        } finally {
            Debug.WriteLine($"[AASeqEngine] Init: {sw.ElapsedMilliseconds} ms");
            Metrics.PluginFileLoadMilliseconds.Record(sw.ElapsedMilliseconds);
        }
    }


    private readonly SortedDictionary<string, EndpointPlugin> EndpointPluginsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly SortedDictionary<string, CommandPlugin> CommandPluginsByName = new(StringComparer.OrdinalIgnoreCase);


    /// <summary>
    /// Gets all command plugins.
    /// </summary>
    public IReadOnlyCollection<CommandPlugin> CommandPlugins { get; }

    /// <summary>
    /// Gets all endpoint plugins.
    /// </summary>
    public IReadOnlyCollection<EndpointPlugin> EndpointPlugins { get; }


    /// <summary>
    /// Gets plugin loader instance.
    /// </summary>
    public static PluginManager Instance { get; } = new PluginManager();


    /// <summary>
    /// Finds a command plugin by name.
    /// </summary>
    /// <param name="pluginName">Plugin name.</param>
    public static CommandPlugin? FindCommandPlugin(string pluginName) {
        return Instance.CommandPluginsByName.TryGetValue(pluginName, out var plugin) ? plugin : null;
    }

    /// <summary>
    /// Finds a endpoint plugin by name.
    /// </summary>
    /// <param name="pluginName">Plugin name.</param>
    public static EndpointPlugin? FindEndpointPlugin(string pluginName) {
        return Instance.EndpointPluginsByName.TryGetValue(pluginName, out var plugin) ? plugin : null;
    }


    #region Helpers

    private static CommandPlugin? GetCommandPlugin(Type type) {
        if (!type.IsClass) { return null; }

        var mCreateInstance = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (mCreateInstance is null) { return null; }
        if (!mCreateInstance.ReturnType.IsAssignableTo(typeof(object))) { return null; }

        var mExecute = type.GetMethod("TryExecute", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(AASeqNodes), typeof(CancellationToken)]);
        if (mExecute is null) { return null; }
        if (!mExecute.ReturnType.Equals(typeof(bool))) { return null; }

        return new CommandPlugin(type, mCreateInstance, mExecute);
    }

    private static EndpointPlugin? GetEndpointPlugin(Type type) {
        if (!type.IsClass) { return null; }

        var mCreateInstance = type.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (mCreateInstance is null) { return null; }
        if (!mCreateInstance.ReturnType.IsAssignableTo(typeof(object))) { return null; }

        var mValidaateConfiguration = type.GetMethod("ValidateConfiguration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, [typeof(AASeqNodes)]);
        if (mValidaateConfiguration is null) { return null; }
        if (!mValidaateConfiguration.ReturnType.Equals(typeof(AASeqNodes))) { return null; }

        var mSend = type.GetMethod("TrySend", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(Guid), typeof(String), typeof(AASeqNodes), typeof(CancellationToken)]);
        if (mSend is null) { return null; }
        if (!mSend.ReturnType.Equals(typeof(bool))) { return null; }

        var mReceive = type.GetMethod("TryReceive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, [typeof(Guid), typeof(string).MakeByRefType(), typeof(AASeqNodes).MakeByRefType(), typeof(CancellationToken)]);
        if (mReceive is null) { return null; }
        if (!mReceive.ReturnType.Equals(typeof(bool))) { return null; }

        return new EndpointPlugin(type, mCreateInstance, mValidaateConfiguration, mSend, mReceive);
    }

    #endregion Helpers

}
