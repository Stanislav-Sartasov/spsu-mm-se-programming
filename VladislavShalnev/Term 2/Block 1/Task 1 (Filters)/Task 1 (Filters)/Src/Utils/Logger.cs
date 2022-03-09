
namespace Utils.Logger
{
	public static class Logger
	{
		public static void Log(string text, ConsoleColor color = ConsoleColor.White)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}

		public static void Error(string text) => Log(text, ConsoleColor.Red);

		public static void Success(string text) => Log(text, ConsoleColor.Green);
	}
}
