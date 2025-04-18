namespace AASeq;

/// <summary>
/// Outgoing message flow action.
/// </summary>
public interface IFlowMessageOutAction : IFlowAction {

    /// <summary>
    /// Gets destination name.
    /// </summary>
    public string DestinationName { get; }

    /// <summary>
    /// Gets message name.
    /// </summary>
    public string MessageName { get; }

}
