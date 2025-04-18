namespace AASeqCli;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;

internal static partial class App {

    internal static void Main(string[] args) {
        var verboseOption = new Option<bool>(
            aliases: ["--verbose", "-v"],
            description: "Enable verbose output") {
            Arity = ArgumentArity.Zero,
        };

        var fileArgument = new Argument<FileInfo>(
            name: "file",
            description: "File to use"
        ) {
            Arity = ArgumentArity.ExactlyOne
        };
        fileArgument.AddValidator(commandResult => {
            if (commandResult.GetValueOrDefault() is FileInfo source) {
                if (!source.Exists) {
                    commandResult.ErrorMessage = $"File \"{source.FullName}\" doesn't exist";
                }
            } else {
                commandResult.ErrorMessage = "Must specify file";
            }
        });

        // Default command
        var rootCommand = new RootCommand("Protocol simulator") {
            verboseOption,
            fileArgument,
        };
        rootCommand.SetHandler(App.Default, fileArgument);
        rootCommand.AddArgument(fileArgument);

        // Command: lint
        var lintCommand = new Command("lint", "Parse file and display it") {
            verboseOption,
            fileArgument,
        };
        lintCommand.SetHandler(App.Lint, fileArgument);
        rootCommand.Add(lintCommand);

        // Command: new
        var newCommand = new Command("new", "Creates a new file") {
            verboseOption,
            fileArgument,
        };
        newCommand.SetHandler(App.New, fileArgument);
        rootCommand.Add(newCommand);

        // Command: run
        var runCommand = new Command("run", "Execute flows in the file") {
            verboseOption,
            fileArgument,
        };
        runCommand.SetHandler(App.Run, fileArgument);
        rootCommand.Add(runCommand);

        // Command: version
        var versionCommand = new Command("version", "Shows current version") {
            verboseOption,
        };
        versionCommand.SetHandler(App.Version, verboseOption);
        rootCommand.Add(versionCommand);


        // Done
        var cliBuilder = new CommandLineBuilder(rootCommand);
        cliBuilder.UseHelp();
        var cliParser = cliBuilder.Build();
        cliParser.Invoke(args, new SystemConsole());
    }

}
