namespace AASeq;
using System;

/// <summary>
/// Endpoint storage class.
/// </summary>
internal sealed record EndpointStore : IEndpoint {

    public EndpointStore(string name, AASeqNodes configuration, EndpointPlugin plugin, EndpointInstance instance) {
        Name = name;
        Configuration = configuration;
        Plugin = plugin;
        Instance = instance;
    }


    internal string Name { get; }
    internal AASeqNodes Configuration { get; }
    internal EndpointPlugin Plugin { get; }
    internal EndpointInstance Instance { get; }


    public AASeqNode DefinitionNode {
        get {
            return Name.Equals(Instance.PluginName, StringComparison.OrdinalIgnoreCase)
                 ? new AASeqNode('@' + Instance.PluginName, AASeqValue.Null, Configuration)
                 : new AASeqNode('@' + Name, Instance.PluginName, Configuration);
        }
    }
}
