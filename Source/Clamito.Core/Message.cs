using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Clamito {

    /// <summary>
    /// Message.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public sealed class Message : Interaction {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Message name.</param>
        /// <param name="source">Source endpoint.</param>
        /// <param name="destination">Destination endpoint.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null. -or- Source cannot be null. -or- Destination cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters. -or- Source and destination cannot be the same.</exception>
        public Message(string name, Endpoint source, Endpoint destination)
            : this(name, source, destination, null) {
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="name">Message name.</param>
        /// <param name="source">Source endpoint.</param>
        /// <param name="destination">Destination endpoint.</param>
        /// <param name="fields">Fields.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null. -or- Source cannot be null. -or- Destination cannot be null. -or- Fields cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Name contains invalid characters. -or- Source and destination cannot be the same.</exception>
        public Message(string name, Endpoint source, Endpoint destination, IEnumerable<Field> fields)
            : this(name, source, destination, null) {
            this.Data.AddRange(fields);
        }

        private Message(string name, Endpoint source, Endpoint destination, FieldCollection fields)
            : base(name, fields) {
            this.ReplaceEndpoints(source, destination);
        }


        /// <summary>
        /// Gets source.
        /// </summary>
        public Endpoint Source { get; private set; }

        /// <summary>
        /// Gets destination.
        /// </summary>
        public Endpoint Destination { get; private set; }


        /// <summary>
        /// Sets endpoints.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Source cannot be null. -or- Destination cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Source and destination cannot be the same.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public void ReplaceEndpoints(Endpoint source, Endpoint destination) {
            if (source == null) { throw new ArgumentNullException(nameof(source), "Source cannot be null."); }
            if (destination == null) { throw new ArgumentNullException(nameof(destination), "Destination cannot be null."); }
            if (source.Equals(destination)) { throw new ArgumentOutOfRangeException(nameof(source), "Source and destination cannot be the same."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }

            this.Source = source;
            this.Destination = destination;
            this.OnChanged(new EventArgs());
        }


        /// <summary>
        /// Gets interaction kind.
        /// </summary>
        public override InteractionKind Kind { get { return InteractionKind.Message; } }


        #region Clone

        /// <summary>
        /// Creates a copy of the message.
        /// </summary>
        public override Interaction Clone() {
            var message = new Message(this.Name, this.Source.Clone(), this.Destination.Clone(), this.Data.Clone()) { Description = this.Description };
            return message;
        }

        /// <summary>
        /// Creates a read-only copy of the message.
        /// </summary>
        public override Interaction AsReadOnly() {
            var message = new Message(this.Name, this.Source.AsReadOnly(), this.Destination.AsReadOnly(), this.Data.AsReadOnly()) { Description = this.Description };
            message.IsReadOnly = true;
            return message;
        }

        #endregion

    }
}
