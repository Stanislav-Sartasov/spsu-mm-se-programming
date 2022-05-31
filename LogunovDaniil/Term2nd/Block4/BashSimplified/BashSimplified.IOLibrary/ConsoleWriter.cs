namespace BashSimplified.IOLibrary
{
	public class ConsoleWriter : IWriter
	{
		public void WriteLine(string output)
		{
			Console.WriteLine(output);
		}
	}
}
