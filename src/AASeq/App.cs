namespace AASeqCli;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;

internal static class App {

    internal static void Main(string[] args) {
        var versionOption = new Option<bool>(
            aliases: ["--version", "-V"],
            description: "Show version information") {
            Arity = ArgumentArity.Zero,
            IsRequired = true,
        };

        var verboseOption = new Option<bool>(
            aliases: ["--verbose", "-v"],
            description: "Enable verbose output") {
            Arity = ArgumentArity.Zero,
        };

        var rootCommand = new RootCommand("Protocol simulator") {
            verboseOption,
            versionOption,
        };
        rootCommand.SetHandler(AppExec.Base, versionOption, verboseOption);

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
        rootCommand.AddArgument(fileArgument);

        var newCommand = new Command("new", "Creates a new file") {
            verboseOption,
            versionOption,
            fileArgument,
        };
        newCommand.SetHandler(AppExec.New, fileArgument);
        rootCommand.Add(newCommand);

        var lintCommand = new Command("lint", "Parse file and display it") {
            verboseOption,
            versionOption,
            fileArgument,
        };
        lintCommand.SetHandler(AppExec.Lint, fileArgument);
        rootCommand.Add(lintCommand);

        var runCommand = new Command("run", "Execute flows in the file") {
            verboseOption,
            versionOption,
            fileArgument,
        };
        runCommand.SetHandler(AppExec.Run, fileArgument);
        rootCommand.Add(runCommand);

        var cliBuilder = new CommandLineBuilder(rootCommand);
        cliBuilder.UseHelp();
        var cliParser = cliBuilder.Build();
        cliParser.Invoke(args, new SystemConsole());
    }

}
