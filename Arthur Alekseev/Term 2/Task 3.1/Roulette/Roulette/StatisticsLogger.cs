using System;

namespace Roulette
{
	public static class StatisticsLogger
	{
		public static void LogMessage(object message)
		{
			Console.WriteLine(message);
		}

		public static void LogNewLine()
		{
			Console.WriteLine();
		}

		public static void LogMessageNoNewLine(object message)
		{
			Console.Write(message);
		}

		public static void LogHeader(string header)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n" + header);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void LogBotName(object name)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(name);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void LogMoney(int money, int avarage)
		{
			if (money > avarage)
				Console.ForegroundColor = ConsoleColor.Green;
			if (money < avarage)
				Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(money);
			Console.Write(" (" + Convert.ToString(100 * money / avarage) + "%)");
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}