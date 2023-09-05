namespace Bash.Commands
{
    public interface ICommand
    {
        string Name { get; }

        string[]? Execute(string[] args);
    }
}
