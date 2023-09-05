using System.Runtime.CompilerServices;
using Bash.Commands;
using Bash.Exceptions;
using Bash.Utils;

[assembly: InternalsVisibleTo("Bash.UnitTests")]
namespace Bash.Core;

public class Bash
{
	private readonly IIOManager _io;
	private readonly VariablesManager _variablesManager;
	private readonly List<ICommand> _commands;
	private readonly Func<string, IEnumerable<string>, object?> _startProcess;

	public Bash(
		IIOManager io,
		Action<int> exit,
		Func<string, IEnumerable<string>, object?> startProcess
	)
	{ 
		_io = io;
		_variablesManager = new VariablesManager();
		_startProcess = startProcess;

		_commands = new List<ICommand>
		{
			new EchoCommand(io),
			new ExitCommand(exit),
			new PwdCommand(),
			new CatCommand(),
			new WcCommand()
		};
	}

	public void Run()
	{
		while (true)
		{
			try
			{
				string input = _io.Read();
				_variablesManager.ReadVariables(input);

				List<CommandObject> commands = CommandsParser.Parse(
					_variablesManager.ReplaceVariables(
						_variablesManager.ReplaceVariablesAssignments(input)
					)
				);

				string[]? previousResult = null;

				foreach (var command in commands)
				{
					if (previousResult is not null)
						command.Args.AddRange(previousResult);

					previousResult = Execute(command);
				}

				// Standard output
				if (previousResult is not null)
					foreach (var result in previousResult)
						_io.Write(result);
			}
			catch (ExitException)
			{
				return;
			}
			catch (Exception ex)
			{
				_io.Write(ex.Message);
			}
		}
	}

	private string[]? Execute(CommandObject command)
	{
		if (command.Name.Length == 0) // For variable assignments
			return null;
		
		ICommand? executableCommand = _commands.Find(current =>
			command.Name == current.Trigger
		);

		if (executableCommand is not null)
			return executableCommand.Run(command.Args.ToArray());

		_startProcess(command.Name, command.Args);
		return null;
	}
		
}