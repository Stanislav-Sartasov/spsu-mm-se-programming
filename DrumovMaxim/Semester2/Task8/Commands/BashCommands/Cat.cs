namespace Commands.BashCommands;

public class Cat : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }
    
    public Cat()
    {
        Name = "cat";
        Description = "Print content of the file";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        var finalResult = CommandResult.Success;
        foreach (var param in parameters)
        {
            var result = ProcessFile(param);
            if (finalResult != CommandResult.Failed) finalResult = result;
        }

        return finalResult;
    }
    
    private CommandResult ProcessFile(string pathToFile)
    {
        if (File.Exists(pathToFile))
        {
            Output.AddRecord(GetInformationFormFile(pathToFile));
            return CommandResult.Success;
        }
        else
        {
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), pathToFile);
            if (File.Exists(newPath))
            {
                Output.AddRecord(GetInformationFormFile(pathToFile));
                return CommandResult.Success;
            }
            else
            {
                Output.AddRecord(Record.NoSuchFile(pathToFile.Split('/').Last()));
                return CommandResult.Failed;
            }
        }
    }

    private Record GetInformationFormFile(string pathToFile)
    {
        return new Record(File.ReadAllText(pathToFile), false, String.Empty);
    }
    
}