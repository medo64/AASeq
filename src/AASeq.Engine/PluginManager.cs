namespace AASeq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Plugin management.
/// </summary>
internal sealed class PluginManager {

    private PluginManager() {
        EndpointPluginsByName.Add("Me", new EndpointPlugin(typeof(EndpointPlugins.Me)));  // this one is special

        foreach (var type in new Type[] { typeof(EndpointPlugins.Dummy) }) {  // TODO: remove hardcoding
            var plugin = GetEndpointPlugin(type);
            if (plugin is not null) {
                EndpointPluginsByName.Add(plugin.Name, plugin);
            }
        }

        foreach (var type in new Type[] { typeof(CommandPlugins.Wait) }) {  // TODO: remove hardcoding
            var plugin = GetCommandPlugin(type);
            if (plugin is not null) {
                CommandPluginsByName.Add(plugin.Name, plugin);
            }
        }

        CommandPlugins = new ReadOnlyCollection<CommandPlugin>([.. CommandPluginsByName.Values]);
        EndpointPlugins = new ReadOnlyCollection<EndpointPlugin>([.. EndpointPluginsByName.Values]);
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
        var constructor = type.GetConstructor([typeof(AASeqNodes)]);
        if (constructor is null) { return null; }

        return new CommandPlugin(type);
    }

    private static EndpointPlugin? GetEndpointPlugin(Type type) {
        var constructor = type.GetConstructor([typeof(AASeqNodes)]);
        if (constructor is null) { return null; }

        return new EndpointPlugin(type);
    }

    #endregion Helpers
}
