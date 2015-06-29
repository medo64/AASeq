using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Clamito {
    /// <summary>
    /// Collection of protocols.
    /// </summary>
    [DebuggerDisplay("{Count} protocols")]
    public sealed class ProtocolCollection : IReadOnlyList<ProtocolBase> {


        private static readonly ProtocolCollection _instance = new ProtocolCollection();
        /// <summary>
        /// Loaded protocols.
        /// </summary>
        public static ProtocolCollection Instance { get { return _instance; } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "LoadFile is intentionaly called because given assembly has to be executable.")]
        internal ProtocolCollection() {
            var path = new FileInfo((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location).DirectoryName;
            var root = new DirectoryInfo(path);

            Log.Write.Verbose("ProtocolCollection", "Searching for protocols in {0}...", root.FullName);

            foreach (var file in root.GetFiles("*.dll")) {
                Log.Write.Verbose("ProtocolCollection", "Checking file {0}...", file.Name);
                var assembly = Assembly.LoadFile(file.FullName);

                foreach (var type in assembly.GetTypes()) {
                    Log.Write.Verbose("ProtocolCollection", "Checking type {0}...", type.Name);

                    if (!type.IsClass) { //must be class
                        Log.Write.Verbose("ProtocolCollection", "Type {0} is not a class.", type.Name);
                        continue;
                    }

                    if (!typeof(ProtocolBase).IsAssignableFrom(type)) {
                        Log.Write.Verbose("ProtocolCollection", "Type {0} does not implement ProtocolBase.", type.Name);
                        continue;
                    }

                    var protocolConstructor = type.GetConstructor(new Type[] { });
                    if (protocolConstructor == null) {
                        Log.Write.Verbose("ProtocolCollection", "Type {0} does not have a parameterless constructor.", type.Name);
                        continue;
                    }

                    Log.Write.Verbose("ProtocolCollection", "Loading protocol type {0}...", type.Name);
                    try {
                        var protocol = (ProtocolBase)Activator.CreateInstance(type);

                        if (this.Contains(protocol.Name)) {
                            Log.Write.Warning("ProtocolCollection", "Duplicate protocol name {0}!", protocol.Name);
                            continue;
                        }

                        this.BaseCollection.Add(protocol);
                        this.LookupByName.Add(protocol.Name, protocol);
                        Log.Write.Information("ProtocolCollection", "Loaded protocol {0}.", protocol.Name);
                    } catch (ArgumentException ex) {
                        Log.Write.Warning("ProtocolCollection", "Cannot load protocol type {0} ({1})!", type.Name, ex.Message.Split(new char[] { '\r', '\n' })[0]);
                        continue;
                    }
                }
            }

            this.BaseCollection.Sort(
                delegate (ProtocolBase protocol1, ProtocolBase protocol2) {
                    return string.CompareOrdinal(protocol1.DisplayName, protocol2.DisplayName);
                }
            );

            Log.Write.Verbose("ProtocolCollection", "Found total of {0} protocol(s).", this.BaseCollection.Count);
        }


        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<ProtocolBase> BaseCollection = new List<ProtocolBase>();
        private readonly Dictionary<string, ProtocolBase> LookupByName = new Dictionary<string, ProtocolBase>(StringComparer.OrdinalIgnoreCase);


        #region IReadOnlyList

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        public ProtocolBase this[int index] {
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
        public IEnumerator<ProtocolBase> GetEnumerator() {
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
        public ProtocolBase this[string name] {
            get {
                if (name != null) {
                    ProtocolBase value;
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
