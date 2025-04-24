namespace AASeqCli;
using System;
using System.Reflection;
using AASeq;

internal static partial class App {

    public static void Version(bool isVerbose) {
        var assembly = Assembly.GetEntryAssembly();
        var name = assembly?.GetName().Name ?? string.Empty;
        var version = assembly?.GetName().Version ?? new Version();
        Console.WriteLine($"{name} {version.Major}.{version.Minor} {version.Revision}");

        if (isVerbose) {
            var manager = PluginManager.Instance;  // this loads plugins

            var hadEndpoints = false;
            Console.Write("Endpoints: ");
            foreach (var plugin in manager.EndpointPlugins) {
                if (hadEndpoints) { Console.Write(" "); }
                Console.Write(plugin.Name);
                hadEndpoints = true;
            }
            if (!hadEndpoints) { Console.Write("(none)"); }
            Console.WriteLine();

            var hadCommands = false;
            Console.Write("Commands : ");
            foreach (var plugin in manager.CommandPlugins) {
                if (hadCommands) { Console.Write(" "); } else { hadCommands = true; }
                Console.Write(plugin.Name);
            }
            if (!hadCommands) { Console.Write("(none)"); }
            Console.WriteLine();
        }
    }

}
