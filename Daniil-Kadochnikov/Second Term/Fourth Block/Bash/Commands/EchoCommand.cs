namespace Commands
{
	public class EchoCommand : ICommand
	{
		public string Name { get; } = "echo";

		public string[]? Execute(string[] arguments)
		{
			return new string[] { String.Join(' ', arguments) };
		}
	}
}