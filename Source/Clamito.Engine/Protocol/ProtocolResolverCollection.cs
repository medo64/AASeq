using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Clamito {
    /// <summary>
    /// Collection of protocol plugins.
    /// </summary>
    [DebuggerDisplay("{Count} protocol plugins")]
    public sealed class ProtocolResolverCollection : IReadOnlyList<ProtocolResolver> {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "LoadFile is intentionaly called because given assembly has to be executable.")]
        internal ProtocolResolverCollection() {
            var path = new FileInfo((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location).DirectoryName;
            var pluginRoot = new DirectoryInfo(path);

            Log.WriteVerbose("Searching for plugins in {0}...", pluginRoot.FullName);

            var protocolInterface = typeof(ProtocolPlugin);
            foreach (var file in pluginRoot.GetFiles("*.dll")) {
                Log.WriteVerbose("Checking file {0}...", file.Name);
                var assembly = Assembly.LoadFile(file.FullName);

                foreach (var type in assembly.GetTypes()) {
                    Log.WriteVerbose("Checking type {0}...", type.Name);

                    if (!type.IsClass) { //must be class
                        Log.WriteVerbose("Type {0} is not a class.", type.Name);
                        continue;
                    }

                    if (!protocolInterface.IsAssignableFrom(type)) {
                        Log.WriteVerbose("Type {0} does not implement an ProtocolPlugin.", type.Name);
                        continue;
                    }

                    var protocolAttributes = type.GetCustomAttributes(typeof(ProtocolAttribute), true);
                    if (protocolAttributes.Length == 0) {
                        Log.WriteVerbose("Type {0} does not have a Protocol attribute.", type.Name);
                        continue;
                    }

                    var protocolConstructor = type.GetConstructor(new Type[] { });
                    if (protocolConstructor == null) {
                        Log.WriteVerbose("Type {0} does not have a parameterless constructor.", type.Name);
                        continue;
                    }


                    Log.WriteVerbose("Loading plugin type {0}...", type.Name);
                    ProtocolResolver protocol;
                    try {
                        protocol = new ProtocolResolver(type);
                    } catch (ArgumentException ex) {
                        Log.WriteWarning("Cannot load plugin type {0} ({1})!", type.Name, ex.Message.Split(new char[] { '\r', '\n' })[0]);
                        continue;
                    }

                    if (this.Contains(protocol.Name)) {
                        Log.WriteWarning("Duplicate plugin name {0}!", protocol.Name);
                        continue;
                    }

                    this.BaseCollection.Add(protocol);
                    this.LookupByName.Add(protocol.Name, protocol);
                    Log.WriteInformation("Loaded plugin {0}.", protocol.Name);
                }
            }

            this.BaseCollection.Sort(
                delegate (ProtocolResolver protocol1, ProtocolResolver protocol2) {
                    return string.CompareOrdinal(protocol1.DisplayName, protocol2.DisplayName);
                }
            );

            Log.WriteVerbose("Found total of {0} plugin(s).", this.BaseCollection.Count);
        }


        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly List<ProtocolResolver> BaseCollection = new List<ProtocolResolver>();
        private readonly Dictionary<string, ProtocolResolver> LookupByName = new Dictionary<string, ProtocolResolver>(ProtocolResolver.NameComparer);


        #region IReadOnlyList

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Index is less than 0. -or- Index is equal to or greater than collection count. -or- Duplicate name in collection. -or- Item cannot be in other collection.</exception>
        public ProtocolResolver this[int index] {
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
        public IEnumerator<ProtocolResolver> GetEnumerator() {
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
        public ProtocolResolver this[string name] {
            get {
                if (name != null) {
                    ProtocolResolver value;
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
