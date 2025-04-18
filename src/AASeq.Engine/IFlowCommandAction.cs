namespace AASeq;

/// <summary>
/// Command flow action.
/// </summary>
public interface IFlowCommandAction : IFlowAction {

    /// <summary>
    /// Gets command name.
    /// </summary>
    public string CommandName { get; }

}
