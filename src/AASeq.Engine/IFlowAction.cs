namespace AASeq;

/// <summary>
/// Flow action.
/// </summary>
public interface IFlowAction {

    /// <summary>
    /// Returns the action definition.
    /// </summary>
    public AASeqNode DefinitionNode { get; }

    /// <summary>
    /// Gets if action is a command
    /// </summary>
    public bool IsCommand {
        get { return this is FlowCommand; }
    }

    /// <summary>
    /// Gets if action is an outgoing message.
    /// </summary>
    public bool IsMessageOut {
        get { return this is FlowMessageOut; }
    }

    /// <summary>
    /// Gets if action is an incoming message.
    /// </summary>
    public bool IsMessageIn {
        get { return this is FlowMessageIn; }
    }

}
