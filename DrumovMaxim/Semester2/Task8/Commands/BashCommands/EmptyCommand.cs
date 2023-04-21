namespace Commands.BashCommands;

public class EmptyCommand : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }

    public EmptyCommand()
    {
        Name = "empty";
        Description = "Does nothing";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        return CommandResult.Success;
    }
}