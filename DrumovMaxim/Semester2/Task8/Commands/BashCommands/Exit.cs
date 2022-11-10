namespace Commands.BashCommands;

public class Exit : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }
    
    public Exit()
    {
        Name = "exit";
        Description = "Exit from terminal";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        Environment.Exit(0);
        return CommandResult.Success;
    }
}