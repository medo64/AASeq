using System;
using System.IO;
using System.Reflection;

namespace AASeq;

/// <summary>
/// Plugin handling.
/// </summary>
public static class Plugin {

    private static bool IsInitialized = false;
    /// <summary>
    /// Initializes the plugins.
    /// </summary>
    public static void Initialize() {
        if (Plugin.IsInitialized) { throw new NotSupportedException("Plugin system has already been initialized."); }

        var protocols = new PluginCollection<ProtocolPlugin> {
                new DummyProtocol()
            };

        var commands = new PluginCollection<CommandPlugin> {
                new LogCommand(),
                new WaitCommand()
            };


        var path = new FileInfo((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location).DirectoryName;
        var root = new DirectoryInfo(path);

        Log.Write.Verbose("Plugin.Initialize", "Searching for protocols in {0}...", root.FullName);

        foreach (var file in root.GetFiles("*.dll")) {
            Log.Write.Verbose("Plugin.Initialize", "Checking file {0}...", file.Name);

            //ignore basic dlls
            if (file.Name.Equals("AASeq.Core.dll")) { continue; }
            if (file.Name.Equals("AASeq.Engine.dll")) { continue; }
            if (file.Name.Equals("AASeq.Plugin.dll")) { continue; }

            //load assembly
            try {
                var assembly = Assembly.LoadFile(file.FullName);


                foreach (var type in assembly.GetTypes()) {
                    Log.Write.Verbose("Plugin.Initialize", "Checking type {0}...", type.Name);

                    if (!type.IsClass) { //must be class
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} is not a class.", type.Name);
                        continue;
                    }

                    var protocolConstructor = type.GetConstructor(Array.Empty<Type>());
                    if (protocolConstructor == null) {
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} does not have a parameterless constructor.", type.Name);
                        continue;
                    }

                    if (typeof(ProtocolPlugin).IsAssignableFrom(type)) {

                        Log.Write.Verbose("Plugin.Initialize", "Loading protocol type {0}...", type.Name);
                        try {
                            var protocol = (ProtocolPlugin)Activator.CreateInstance(type);

                            if (protocols[protocol.Name] != null) {
                                Log.Write.Warning("Plugin.Initialize", "Duplicate protocol name {0}!", protocol.Name);
                                continue;
                            }

                            protocols.Add(protocol);
                            Log.Write.Information("Plugin.Initialize", "Loaded protocol {0}.", protocol.Name);
                        } catch (ArgumentException ex) {
                            Log.Write.Warning("Plugin.Initialize", "Cannot load protocol type {0} ({1})!", type.Name, ex.Message.Split(new char[] { '\r', '\n' })[0]);
                            continue;
                        }

                    } else if (typeof(CommandPlugin).IsAssignableFrom(type)) {

                        Log.Write.Verbose("Plugin.Initialize", "Loading command type {0}...", type.Name);
                        try {
                            var command = (CommandPlugin)Activator.CreateInstance(type);

                            if (commands[command.Name] != null) {
                                Log.Write.Warning("Plugin.Initialize", "Duplicate command name {0}!", command.Name);
                                continue;
                            }

                            commands.Add(command);
                            Log.Write.Information("Plugin.Initialize", "Loaded command {0}.", command.Name);
                        } catch (ArgumentException ex) {
                            Log.Write.Warning("Plugin.Initialize", "Cannot load command type {0} ({1})!", type.Name, ex.Message.Split(new char[] { '\r', '\n' })[0]);
                            continue;
                        }

                    } else {
                        Log.Write.Verbose("Plugin.Initialize", "Type {0} does not implement PluginBase.", type.Name);
                        continue;
                    }
                }

            } catch (ReflectionTypeLoadException ex) {
                Log.Write.Information("Plugin.Initialize", "Cannot load potential plugin assembly {0}.", file.Name);
                Log.Write.Verbose("Plugin.Initialize", "Cannot load potential plugin assembly: {0}.", ex.Message);
            } catch (TypeLoadException ex) {
                Log.Write.Information("Plugin.Initialize", "Cannot check potential plugin assembly {0}.", file.Name);
                Log.Write.Verbose("Plugin.Initialize", "Cannot load potential plugin assembly: {0}.", ex.Message);
            } catch (Exception ex) {
                Log.Write.Error("Plugin.Initialize", ex);
                throw;
            }
        }

        protocols.Sort();
        commands.Sort();

        Plugin.Protocols = protocols;
        Plugin.Commands = commands;

        Plugin.IsInitialized = true;
        Log.Write.Verbose("Plugin.Initialize", "Found total of {0} protocol(s) and {1} command(s).", Plugin.Protocols.Count, Plugin.Commands.Count);
    }


    private static PluginCollection<ProtocolPlugin> _protocols;
    /// <summary>
    /// Loaded protocols.
    /// </summary>
    /// <exception cref="System.NotSupportedException">Plugin system not initialized.</exception>
    public static PluginCollection<ProtocolPlugin> Protocols {
        get {
            if (!Plugin.IsInitialized) { throw new NotSupportedException("Plugin system not initialized."); }
            return Plugin._protocols;
        }
        private set { Plugin._protocols = value; }
    }

    private static PluginCollection<CommandPlugin> _commands;
    /// <summary>
    /// Loaded commands.
    /// </summary>
    /// <exception cref="System.NotSupportedException">Plugin system not initialized.</exception>
    public static PluginCollection<CommandPlugin> Commands {
        get {
            if (!Plugin.IsInitialized) { throw new NotSupportedException("Plugin system not initialized."); }
            return Plugin._commands;
        }
        private set { Plugin._commands = value; }
    }

}
