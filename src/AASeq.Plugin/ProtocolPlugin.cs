using System;
using System.Collections.Generic;
using System.Globalization;

namespace AASeq;

/// <summary>
/// Protocol interface.
/// </summary>
public abstract class ProtocolPlugin : PluginBase {

    #region Definition

    /// <summary>
    /// Gets protocol behaviour model.
    /// </summary>
    public abstract ProtocolPluginModel Model { get; }

    /// <summary>
    /// Gets display name for protocol.
    /// </summary>
    public override string DisplayName {
        get { return Name + " " + Model.ToString().ToLower(CultureInfo.CurrentCulture); }
    }

    #endregion


    #region Instance

    /// <summary>
    /// Creates new instance of current class.
    /// </summary>
    public ProtocolPlugin CreateInstance() {
        return (ProtocolPlugin)Activator.CreateInstance(GetType());
    }

    #endregion


    #region Setup

    /// <summary>
    /// Starts protocol and allocates all needed resources.
    /// </summary>
    /// <param name="data">Protocol data.</param>
    public abstract IEnumerable<Failure> Initialize(FieldCollection data);

    /// <summary>
    /// Stops protocol and releases all resources.
    /// </summary>
    public virtual IEnumerable<Failure> Terminate() {
        Dispose();
        yield break;
    }

    #endregion


    #region Execute

    /// <summary>
    /// Sends message.
    /// </summary>
    /// <param name="data">Message data.</param>
    public abstract IEnumerable<Failure> Send(FieldCollection data);

    /// <summary>
    /// Returns received message or null if timeout occurred.
    /// </summary>
    /// <param name="receivedData">Message content. Must be empty; will be filled by function.</param>
    public abstract IEnumerable<Failure> Receive(FieldCollection receivedData);

    #endregion


    #region Data

    /// <summary>
    /// Returns default data fields.
    /// </summary>
    public virtual FieldCollection GetDefaultData() {
        return null;
    }

    /// <summary>
    /// Returns data errors.
    /// </summary>
    /// <param name="data">Data fields to validate.</param>
    public virtual IEnumerable<Failure> ValidateData(FieldCollection data) {
        yield break;
    }

    #endregion

}
