using System.Text;

namespace Commands.BashCommands;

public class Echo : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }

    public Echo()
    {
        Name = "echo";
        Description = "Print arguments in terminal";
        Launched = false;
        Output = CommandOutput.EmptyBashCommand;
    }

    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        
        var builder = new StringBuilder();
        
        foreach (var param in parameters)
        {
            builder.Append(param);
            if (param != parameters.Last())
                builder.Append(' ');
        }
        Output.AddRecord(new Record(builder.ToString(), false, String.Empty));
        return CommandResult.Success;
    }
}