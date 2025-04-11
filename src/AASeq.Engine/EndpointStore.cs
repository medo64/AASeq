namespace AASeq;
using System;

/// <summary>
/// Endpoint storage class.
/// </summary>
internal record EndpointStore : IEndpoint {

    public EndpointStore(string name, EndpointInstance instance) {
        Name = name;
        Instance = instance;
    }


    internal string Name { get; }
    internal EndpointInstance Instance { get; }


    public AASeqNode GetDefinitionNode() {
        var definitionNode = Name.Equals(Instance.PluginName, StringComparison.OrdinalIgnoreCase)
                           ? new AASeqNode('@' + Instance.PluginName)
                           : new AASeqNode('@' + Name, Instance.PluginName);
        foreach (var configNode in Instance.GetConfiguration()) {
            definitionNode.Nodes.Add(configNode);
        }
        return definitionNode;
    }
}
