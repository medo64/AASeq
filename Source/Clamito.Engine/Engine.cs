using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Clamito {
    /// <summary>
    /// Main execution engine.
    /// </summary>
    public sealed class Engine : IDisposable {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="document">Document.</param>
        public Engine(Document document) {
            this.Document = document;
        }


        /// <summary>
        /// Gets document.
        /// </summary>
        public Document Document { get; private set; }

        private readonly Dictionary<String, ProtocolPlugin> Endpoints = new Dictionary<string, ProtocolPlugin>();

        #region Setup

        private bool WasInitialized;
        /// <summary>
        /// Gets if engine is initialized.
        /// </summary>
        public Boolean IsInitialized { get; private set; }

        /// <summary>
        /// Initializes engine.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Initialization can be done only once.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposal of proxies is handled in Engine's Dispose object.")]
        public ResultCollection Initialize() {
            if (this.WasInitialized) { throw new NotSupportedException("Initialization can be done only once."); }
            this.WasInitialized = true;

            var errorList = new List<ErrorResult>();

            foreach (var endpoint in this.Document.Endpoints) {
                if (endpoint.ProtocolName == null) { continue; }
                var protocol = Plugin.Protocols[endpoint.ProtocolName];
                if (protocol == null) { return ErrorResult.NewError("Protocol '{0}' not found.", endpoint.ProtocolName); }
                var proxy = protocol.CreateInstance();
                proxy.Initialize(endpoint.Properties);
                this.Endpoints.Add(endpoint.Name, proxy);
            }

            this.CancelEvent = new ManualResetEvent(false);
            this.StepEvent = new CountdownEvent(0);
            this.CanStopEvent = new ManualResetEvent(true);

            this.Thread = new Thread(Run);
            this.Thread.Name = "Engine";
            this.Thread.IsBackground = true;
            this.Thread.CurrentCulture = CultureInfo.InvariantCulture;
            this.Thread.Start();

            this.IsInitialized = true;

            return new ResultCollection(errorList);
        }

        /// <summary>
        /// Terminates engine.
        /// </summary>
        public ResultCollection Terminate() {
            this.IsInitialized = false;
            this.Dispose();
            return true;
        }

        #endregion


        #region Execution

        /// <summary>
        /// Gets whether engine is currently execuring.
        /// </summary>
        public Boolean IsRunning { get { return (this.StepEvent.CurrentCount > 0); } }

        /// <summary>
        /// Gets number of executed steps so far.
        /// </summary>
        public Int32 StepCount { get { return this.CurrentStepCount; } }

        /// <summary>
        /// Starts running the engine.
        /// </summary>
        public void Start() {
            if (!this.IsInitialized) { this.Initialize(); }

            this.StepEvent.Reset(0);
            this.CanStopEvent.WaitOne();
            this.OnStarted(new EventArgs());
            this.StepEvent.Reset(int.MaxValue);
        }

        /// <summary>
        /// Performs a single step.
        /// </summary>
        public void Step() {
            if (!this.IsInitialized) { this.Initialize(); }

            this.StepEvent.Reset(1);
        }

        /// <summary>
        /// Stops the engine.
        /// </summary>
        public void Stop() {
            if (!this.IsInitialized) { this.Initialize(); }

            this.StepEvent.Reset(0);
            this.CanStopEvent.WaitOne();
            this.OnStopped(new EventArgs());
        }

        #endregion


        #region Events

        /// <summary>
        /// Occurs when engine has started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Raises Started event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnStarted(EventArgs e) {
            var eh = this.Started;
            if (eh != null) { eh.Invoke(this, e); }
        }


        /// <summary>
        /// Occurs when engine is preparing to perform a single step.
        /// </summary>
        public event EventHandler StepStarted;

        /// <summary>
        /// Raises StepStarted event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnStepStarted(EventArgs e) {
            var eh = this.StepStarted;
            if (eh != null) { eh.Invoke(this, e); }
        }


        /// <summary>
        /// Occurs when engine has performed a single step.
        /// </summary>
        public event EventHandler<StepCompletedEventArgs> StepCompleted;

        /// <summary>
        /// Raises StepCompleted event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnStepCompleted(StepCompletedEventArgs e) {
            var eh = this.StepCompleted;
            if (eh != null) { eh.Invoke(this, e); }
        }


        /// <summary>
        /// Occurs when engine has stopped.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Raises Stopped event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnStopped(EventArgs e) {
            var eh = this.Stopped;
            if (eh != null) { eh.Invoke(this, e); }
        }

        #endregion


        #region IDisposable

        /// <summary>
        /// Releases all allocated resources.
        /// </summary>
        public void Dispose() {
            if (this.WasInitialized) {
                if (this.Thread != null) {
                    this.CancelEvent.Set();
                    Thread.Sleep(100);
                    if (this.Thread.IsAlive) {
                        this.Thread.Abort();
                    }
                    this.Thread = null;
                }

                if (this.CancelEvent != null) {
                    this.CancelEvent.Dispose();
                    this.CancelEvent = null;
                }

                if (this.StepEvent != null) {
                    this.StepEvent.Dispose();
                    this.StepEvent = null;
                }

                if (this.CanStopEvent != null) {
                    this.CanStopEvent.Dispose();
                    this.CanStopEvent = null;
                }

                foreach (var proxy in this.Endpoints) { //dispose proxies
                    proxy.Value.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }

        #endregion


        #region Thread

        private Thread Thread;
        private ManualResetEvent CancelEvent;
        private CountdownEvent StepEvent;
        private ManualResetEvent CanStopEvent;
        private int CurrentStepCount;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Errors during send/receive are handled by the engine logging.")]
        private void Run() {
            try {
                bool wasRunning = false;

                while (!this.CancelEvent.WaitOne(0, false)) {
                    bool doStep = !this.StepEvent.IsSet;
                    if (doStep) {
                        this.OnStepStarted(new EventArgs());

                        wasRunning = true;
                        this.CanStopEvent.Reset();
                        this.StepEvent.Signal();
                        Interlocked.Increment(ref this.CurrentStepCount);

                        var errors = new List<ErrorResult>();

                        var interactionIndex = 0;
                        foreach (var interaction in this.Document.Interactions) {
                            interactionIndex++;

                            switch (interaction.Kind) {
                                case InteractionKind.Message:
                                    {
                                        var message = (Message)interaction;
                                        var endpointSrc = message.Source;
                                        var endpointDst = message.Destination;
                                        var protocolSrc = endpointSrc.ProtocolName;
                                        var protocolDst = endpointDst.ProtocolName;

                                        if ((protocolSrc == null) && (protocolDst == null)) { //ignore communication
                                        } else if (protocolDst != null) { //sending
                                            try {
                                                ProtocolPlugin protocolProxy;
                                                if (this.Endpoints.TryGetValue(endpointDst.Name, out protocolProxy)) {
                                                    var content = message.Fields.AsReadOnly(); //TODO: resolve constants
                                                    var protocolErrors = protocolProxy.Send(content);
                                                    if (protocolErrors.Count > 0) {
                                                        errors.AddRange(protocolErrors.Clone(string.Format(CultureInfo.InvariantCulture, "{0} {1}: ", interactionIndex, interaction.Name)));
                                                    }
                                                } else {
                                                    errors.Add(ErrorResult.NewError("{0} {1}: Cannot find proxy for {2}.", interactionIndex, interaction.Name, endpointDst.Name));
                                                }
                                            } catch (Exception ex) {
                                                errors.Add(ErrorResult.NewError("{0} {1}: Exception sending message to {2}: {3}.", interactionIndex, interaction.Name, endpointDst.Name, ex.Message));
                                            }
                                        } else if (protocolSrc != null) { //receiving
                                            try {
                                                ProtocolPlugin protocol;
                                                if (this.Endpoints.TryGetValue(endpointSrc.Name, out protocol)) {
                                                    var content = message.Fields.AsReadOnly(); //TODO: resolve constants

                                                    var dummyProtocol = protocol as DummyProtocol;
                                                    if (dummyProtocol != null) {
                                                        dummyProtocol.PokeReceive(content);
                                                    }

                                                    FieldCollection receivedContent;
                                                    var protocolErrors = protocol.Receive(out receivedContent);
                                                    if (protocolErrors.Count > 0) {
                                                        errors.AddRange(protocolErrors.Clone(string.Format(CultureInfo.InvariantCulture, "{0} {1}: ", interactionIndex, interaction.Name)));
                                                    }
                                                } else {
                                                    errors.Add(ErrorResult.NewError("{0} {1}: Cannot find proxy for {2}.", interactionIndex, interaction.Name, endpointSrc.Name));
                                                }
                                            } catch (Exception ex) {
                                                errors.Add(ErrorResult.NewError("{0} {1}: Exception receiving message from {2}: {3}.", interactionIndex, interaction.Name, endpointSrc.Name, ex.Message));
                                            }
                                        } else { //ignore communication between two protocols
                                        }

                                    }
                                    break;

                                case InteractionKind.Command:
                                    {
                                        var command = (Command)interaction;
                                        if (command.Name == "Wait") {
                                            int milliseconds;
                                            if (TryParseTime(command.Parameters, out milliseconds)) {
                                                Thread.Sleep(milliseconds);
                                            } else {
                                                errors.Add(ErrorResult.NewWarning("{0} {1}: Cannot parse parameter {2}.", interactionIndex, interaction.Name, command.Parameters));
                                                Thread.Sleep(1000);
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        errors.Add(ErrorResult.NewError("{0} {1}: Unknown interaction {2}.", interactionIndex, interaction.Name, interaction.Kind));
                                    }
                                    break;

                            }
                        }

                        this.CanStopEvent.Set();

                        this.OnStepCompleted(new StepCompletedEventArgs(new ResultCollection(errors)));
                    } else if (wasRunning) {
                        wasRunning = false;
                        this.OnStopped(new EventArgs());
                    }

                    Thread.Sleep(100);
                }
            } catch (ThreadAbortException) { }
        }

        #endregion


        #region Command parameters

        private static bool TryParseTime(string parameters, out int milliseconds) {
            parameters = parameters.Trim();
            if (parameters.EndsWith("ms", StringComparison.OrdinalIgnoreCase)) {
                if (int.TryParse(parameters.Substring(0, parameters.Length - 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out milliseconds)) {
                    return true;
                }
            } else if (parameters.EndsWith("s", StringComparison.OrdinalIgnoreCase)) {
                if (int.TryParse(parameters.Substring(0, parameters.Length - 2), NumberStyles.Integer, CultureInfo.InvariantCulture, out milliseconds)) {
                    milliseconds *= 1000;
                    return true;
                }
            }

            milliseconds = 1000;
            return false;
        }

        #endregion

    }
}
