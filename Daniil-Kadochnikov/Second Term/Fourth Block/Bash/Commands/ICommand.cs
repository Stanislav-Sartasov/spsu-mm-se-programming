namespace Commands
{
	public interface ICommand
	{
		public string Name { get; }
		public string[]? Execute(string[] arguments);
	}
}
