using Bash.Exceptions;

namespace Bash.Commands;

public class ExitCommand : ICommand
{
	public string Trigger => "exit";
	
	private Action<int> _exit;

	public ExitCommand(Action<int> exit) =>
		_exit = exit;
	
	public string[] Run(string[] args)
	{
		_exit(args.Length != 0 ? int.Parse(args[0]) : 0);

		throw new ExitException();
	}
}