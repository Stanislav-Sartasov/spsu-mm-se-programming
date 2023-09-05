namespace Commands;

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; }
    public CommandOutput Output { get;  }
    public CommandResult Launch(IList<String> parameters);
}