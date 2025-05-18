namespace AASeqCli;
using System;
using System.Diagnostics;
using System.Reflection;
using AASeq;

internal static partial class App {

    public static void Version(bool debug, bool verbose) {
        var assembly = Assembly.GetEntryAssembly();
        var name = assembly?.GetName().Name ?? string.Empty;
        var version = assembly?.GetName().Version ?? new Version();
        Console.WriteLine($"{name} {version.Major}.{version.Minor} {version.Revision}");

        var logger = Logger.GetInstance(debug, verbose);

        if (verbose) {
            var manager = new PluginManager(logger);  // this loads plugins

            var hadCommands = false;
            Console.Write("Commands : ");
            foreach (var plugin in manager.CommandPlugins) {
                if (hadCommands) { Console.Write(" "); } else { hadCommands = true; }
                Console.Write(plugin.Name);
            }
            if (!hadCommands) { Console.Write("(none)"); }
            Console.WriteLine();

            var hadEndpoints = false;
            Console.Write("Endpoints: ");
            foreach (var plugin in manager.EndpointPlugins) {
                if (hadEndpoints) { Console.Write(" "); }
                Console.Write(plugin.Name);
                hadEndpoints = true;
            }
            if (!hadEndpoints) { Console.Write("(none)"); }
            Console.WriteLine();


            var hadVariables = false;
            Console.Write("Variables: ");
            foreach (var plugin in manager.VariablePlugins) {
                if (hadVariables) { Console.Write(" "); } else { hadVariables = true; }
                Console.Write(plugin.Name);
            }
            if (!hadVariables) { Console.Write("(none)"); }
            Console.WriteLine();
        }
    }

}
