namespace Commands.BashCommands;

public class Wc : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }
    
    public Wc()
    {
        Name = "wc";
        Description = "Print the number of lines, words, and bytes in a file";
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
            Output.AddRecord(GetInformationFormFile(pathToFile, pathToFile));
            return CommandResult.Success;
        }
        else
        {
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), pathToFile);
            if (File.Exists(newPath))
            {
                Output.AddRecord(GetInformationFormFile(newPath, pathToFile));
                return CommandResult.Success;
            }
            else
            {
                Output.AddRecord(Record.NoSuchFile(pathToFile.Split('/').Last()));
                return CommandResult.Failed;
            }
        }
    }

    private Record GetInformationFormFile(string pathToFile, string calledFileName)
    {
        var reader = new StreamReader(pathToFile);
        var words = 0;
        var lines = 0;
        var bytes = new FileInfo(pathToFile).Length.ToString();
        
        while (!reader.EndOfStream)
        {
            lines++;
            words += reader.ReadLine().Trim().Split(" ").Count(x => x != " " && x != "" && x != "\n" && x != "\r" && x != "\t");
        }
        
        reader.Dispose();
        return new Record($"{words} {lines} {bytes} {calledFileName}", false, String.Empty);
    }
}