namespace Bash.App.Output
{
	public class Logger : ILogger
	{
		public void Log(string arg)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(arg);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public string Read()
		{
			Console.Write(Directory.GetCurrentDirectory() + "> ");
			return Console.ReadLine();
		}
	}
}