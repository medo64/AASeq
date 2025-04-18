namespace AASeq;

/// <summary>
/// Incoming message flow action.
/// </summary>
public interface IFlowMessageInAction : IFlowAction {

    /// <summary>
    /// Gets source name.
    /// </summary>
    public string SourceName { get; }

    /// <summary>
    /// Gets message name.
    /// </summary>
    public string MessageName { get; }

}
