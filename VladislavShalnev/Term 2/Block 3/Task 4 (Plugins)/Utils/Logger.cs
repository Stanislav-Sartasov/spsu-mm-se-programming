namespace Utils;

public static class Logger
{
	public static void Log(
		string text,
		ConsoleColor color = ConsoleColor.White,
		string end = "\n"
	)
	{
		Console.ForegroundColor = color;
		Console.Write(text + end);
		Console.ResetColor();
	}

	public static void Error(string text, string end = "\n") =>
		Log(text, ConsoleColor.Red, end);

	public static void Success(string text, string end = "\n") =>
		Log(text, ConsoleColor.Green, end);
}