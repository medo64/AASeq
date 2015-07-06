using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Clamito {
    /// <summary>
    /// Collection of protocols.
    /// </summary>
    [DebuggerDisplay("{Count} protocols")]
    public sealed class ProtocolPluginCollection : IReadOnlyList<ProtocolPlugin> {

        internal ProtocolPluginCollection() {
        }


        internal void Add(ProtocolPlugin protocol) {
            if (protocol == null) { throw new ArgumentNullException(nameof(protocol), "Protocol cannot be null."); }
            if (this.Contains(protocol.Name)) { throw new ArgumentOutOfRangeException(nameof(protocol), "Protocol " + protocol.Name + " already exists."); }
            this.BaseCollection.Add(protocol);
            this.LookupByName.Add(protocol.Name, protocol);
        }

        internal void Sort() {
            this.BaseCollection.Sort(
                delegate (ProtocolPlugin protocol1, ProtocolPlugin protocol2) {
                    return string.CompareOrdinal(protocol1.DisplayName, protocol2.DisplayName);
                }
            );
        }


        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<ProtocolPlugin> BaseCollection = new List<ProtocolPlugin>();
        private readonly Dictionary<string, ProtocolPlugin> LookupByName = new Dictionary<string, ProtocolPlugin>(StringComparer.OrdinalIgnoreCase);


        #region IReadOnlyList

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        public ProtocolPlugin this[int index] {
            get { return this.BaseCollection[index]; }
        }

        /// <summary>
        /// Gets the number of protocols contained in the collection.
        /// </summary>
        public int Count {
            get { return this.BaseCollection.Count; }
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
        /// </summary>
        public IEnumerator<ProtocolPlugin> GetEnumerator() {
            return this.BaseCollection.GetEnumerator();
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a non-generic collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.BaseCollection.GetEnumerator();
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets item based on a name or null if item cannot be found.
        /// </summary>
        /// <param name="name">Name.</param>
        public ProtocolPlugin this[string name] {
            get {
                if (name != null) {
                    ProtocolPlugin value;
                    return this.LookupByName.TryGetValue(name, out value) ? value : null;
                } else {
                    return null;
                }
            }
        }


        /// <summary>
        /// Determines whether the collection contains a specific protocol.
        /// </summary>
        /// <param name="name">Name of the item to locate.</param>
        /// <exception cref="System.ArgumentNullException">Name cannot be null.</exception>
        public bool Contains(string name) {
            return (name == null) ? false : this.LookupByName.ContainsKey(name);
        }

        #endregion

    }
}
