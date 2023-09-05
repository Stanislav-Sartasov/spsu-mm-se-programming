namespace Bash.Commands;

public interface ICommand
{
	public string Trigger { get; }
	
	public string[]? Run(string[] args);
}