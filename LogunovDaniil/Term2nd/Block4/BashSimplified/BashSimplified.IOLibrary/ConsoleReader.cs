namespace BashSimplified.IOLibrary
{
	public class ConsoleReader : IReader
	{
		public string GetLine()
		{
			var line = Console.ReadLine();
			return line != null ? line : "";
		}
	}
}
