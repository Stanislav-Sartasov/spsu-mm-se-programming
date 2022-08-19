namespace Commands.BashCommands;

public class Cd : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; private set; }

    public Cd()
    {
        Name = "cd";
        Description = "Change current directory to the specified directory";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        if (parameters.Count != 1)
        {
            if (parameters.Count > 1)
            {
                Output = new CommandOutput(true, parameters.Count, Record.ManyArguments);
                return CommandResult.TooManyArguments;
            }
            else
            {
                Output = new CommandOutput(true, parameters.Count, Record.NoArguments);
                return CommandResult.NoArguments;
            }
        }

        var newPath = parameters.First();

        if (newPath == "..")
        {
            return ChangeDirectory(newPath);
        }
        else if (Path.IsPathFullyQualified(newPath))
        {
            return ChangeDirectory(newPath);
        }
        else
        {
            return ChangeDirectory(Path.Combine(Directory.GetCurrentDirectory(), newPath));
        }
    }

    private CommandResult ChangeDirectory(string fullPath)
    {
        if (Directory.Exists(fullPath))
        {
            Output = Output = new CommandOutput(true, Record.SuccessEmpty);
            Directory.SetCurrentDirectory(fullPath);
            return CommandResult.Success;
        }
        else
        {
            Output = new CommandOutput(true, Record.NoSuchDirectory(fullPath.Split('/').Last()));  
            return CommandResult.Failed;
        }
    }

    /*private CommandResult MoveBack()
    {
        var curDir = Directory.GetCurrentDirectory().Split('/');

        if (curDir.Length != 1)
        {
            Directory.SetCurrentDirectory(String.Join('/', curDir.Skip(curDir.Length - 1)));
        }

        return CommandResult.Success;
    }*/
}