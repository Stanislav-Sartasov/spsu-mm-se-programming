using Bash.Utils;

namespace Bash.Commands;

internal class EchoCommand : ICommand
{
	public string Trigger => "echo";

	private IIOManager _io;

	public EchoCommand(IIOManager io) =>
		_io = io;
	
	public string[]? Run(string[] args)
	{
		_io.Write(string.Join(' ', args.Select(arg => arg.Replace("\"", ""))));

		return null;
	}
}