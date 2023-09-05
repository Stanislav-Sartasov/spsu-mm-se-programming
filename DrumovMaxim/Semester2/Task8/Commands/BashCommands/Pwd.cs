namespace Commands.BashCommands;

public class Pwd : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }

    public Pwd()
    {
        Name = "pwd";
        Description = "Print working directory";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }

    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        Output.AddRecord(new Record(Directory.GetCurrentDirectory(), false, String.Empty));
        return CommandResult.Success;
    }
}