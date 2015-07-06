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
            this.Fields.AddRange(fields);
        }

        private Message(string name, Endpoint source, Endpoint destination, FieldCollection fields)
            : base(name) {

            this.ReplaceEndpoints(source, destination);
            this.Fields = (fields != null) ? fields : new FieldCollection();
            this.Fields.Changed += delegate (Object sender, EventArgs e) {
                this.OnChanged(new EventArgs());
            };
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
        /// Gets fields.
        /// </summary>
        public FieldCollection Fields { get; private set; }

        /// <summary>
        /// Gets if any fields are present.
        /// </summary>
        public bool HasFields {
            get { return (this.Fields.Count > 0); }
        }


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
        /// Sets Field collections.
        /// </summary>
        /// <param name="fields">Fields.</param>
        /// <exception cref="System.ArgumentNullException">Fields cannot be null.</exception>
        /// <exception cref="System.NotSupportedException">Object is read-only.</exception>
        public void ReplaceFields(FieldCollection fields) {
            if (fields == null) { throw new ArgumentNullException(nameof(fields), "Fields cannot be null."); }
            if (this.IsReadOnly) { throw new NotSupportedException("Object is read-only."); }

            this.Fields.Clear(); //to release old stuff

            this.Fields = fields;
            this.Fields.Changed += delegate (Object sender, EventArgs e) {
                this.OnChanged(new EventArgs());
            };
        }


        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString() {
            return this.Name;
        }

        #endregion


        /// <summary>
        /// Gets interaction kind.
        /// </summary>
        public override InteractionKind Kind { get { return InteractionKind.Message; } }


        #region Clone

        /// <summary>
        /// Creates a copy of the message.
        /// </summary>
        public override Interaction Clone() {
            var message = new Message(this.Name, this.Source.Clone(), this.Destination.Clone(), this.Fields.Clone()) { Description = this.Description };
            return message;
        }

        /// <summary>
        /// Creates a read-only copy of the message.
        /// </summary>
        public override Interaction AsReadOnly() {
            var message = new Message(this.Name, this.Source.AsReadOnly(), this.Destination.AsReadOnly(), this.Fields.AsReadOnly()) { Description = this.Description };
            message.IsReadOnly = true;
            return message;
        }

        #endregion

    }
}
