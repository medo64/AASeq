using System.CommandLine;
using System.IO;

namespace AASeqCli;

internal static class App {

    internal static void Main(string[] args) {
        var rootCommand = new RootCommand("Protocol simulator");

        var verboseOption = new Option<bool>(
            name: "--verbose",
            description: "Each time it's specified, additional verbose output is shown") {
            Arity = ArgumentArity.Zero,
        };
        verboseOption.AddAlias("-v");
        rootCommand.AddGlobalOption(verboseOption);

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

        var newCommand = new Command("new", "Creates a new file");
        lintCommand.AddAlias("init");
        newCommand.AddArgument(fileArgument);
        newCommand.SetHandler(AppExec.New, fileArgument);
        rootCommand.Add(newCommand);

        var lintCommand = new Command("lint", "Parse file and display it");
        lintCommand.AddAlias("show");
        lintCommand.AddArgument(fileArgument);
        lintCommand.SetHandler(AppExec.Lint, fileArgument);
        rootCommand.Add(lintCommand);

        var runCommand = new Command("run", "Execute flows in the file");
        runCommand.AddArgument(fileArgument);
        runCommand.SetHandler(AppExec.Run, fileArgument);
        rootCommand.Add(runCommand);

        rootCommand.SetHandler(AppExec.Run, fileArgument);

        rootCommand.Invoke(args);
    }

}
