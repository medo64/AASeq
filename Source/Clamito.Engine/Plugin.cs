using System;
using System.IO;
using System.Reflection;

namespace Clamito {

    /// <summary>
    /// Plugin handling.
    /// </summary>
    public static class Plugin {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Classes initialized here are needed through whole application lifetime.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Initialization must be guaranteed to occur before a static method of the type is called.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "LoadFile is intentionaly called because given assembly has to be executable.")]
        static Plugin() {
            Plugin.Protocols = new PluginCollection<ProtocolPlugin>();
            Plugin.Protocols.Add(new DummyProtocol());

            var path = new FileInfo((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location).DirectoryName;
            var root = new DirectoryInfo(path);

            Log.Write.Verbose("Plugin.Initialize", "Searching for protocols in {0}...", root.FullName);

            foreach (var file in root.GetFiles("*.dll")) {
                Log.Write.Verbose("Plugin.Initialize", "Checking file {0}...", file.Name);

                //ignore basic dlls
                if (file.Name.Equals("Clamito.Core.dll")) { continue; }
                if (file.Name.Equals("Clamito.Engine.dll")) { continue; }
                if (file.Name.Equals("Clamito.Plugin.dll")) { continue; }

                //load assembly
                var assembly = Assembly.LoadFile(file.FullName);

                foreach (var type in assembly.GetTypes()) {
                    Log.Write.Verbose("Plugin.Initialize", "Checking type {0}...", type.Name);

                    if (!type.IsClass) { //must be class
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} is not a class.", type.Name);
                        continue;
                    }

                    if (!typeof(ProtocolPlugin).IsAssignableFrom(type)) {
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} does not implement ProtocolBase.", type.Name);
                        continue;
                    }

                    var protocolConstructor = type.GetConstructor(new Type[] { });
                    if (protocolConstructor == null) {
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} does not have a parameterless constructor.", type.Name);
                        continue;
                    }

                    Log.Write.Verbose("Plugin.Initialize", "Loading protocol type {0}...", type.Name);
                    try {
                        var protocol = (ProtocolPlugin)Activator.CreateInstance(type);

                        if (Plugin.Protocols[protocol.Name] != null) {
                            Log.Write.Warning("Plugin.Initialize", "Duplicate protocol name {0}!", protocol.Name);
                            continue;
                        }

                        Plugin.Protocols.Add(protocol);
                        Log.Write.Information("Plugin.Initialize", "Loaded protocol {0}.", protocol.Name);
                    } catch (ArgumentException ex) {
                        Log.Write.Warning("Plugin.Initialize", "Cannot load protocol type {0} ({1})!", type.Name, ex.Message.Split(new char[] { '\r', '\n' })[0]);
                        continue;
                    }
                }
            }

            Plugin.Protocols.Sort();

            Log.Write.Verbose("Plugin.Initialize", "Found total of {0} protocol(s).", Plugin.Protocols.Count);
        }


        /// <summary>
        /// Loaded protocols.
        /// </summary>
        public static PluginCollection<ProtocolPlugin> Protocols { get; }


    }
}
