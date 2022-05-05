using System;

namespace Roulette
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var botLibraryPath = "../../../../BotLib/Roulette.Bot.dll";
			try
			{
				var statisticCollector = new StatisticCollector(botLibraryPath);
				statisticCollector.CollectStatistics();
				statisticCollector.LogStatistics();
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Something went wrong during the bot library loading.\nMake sure path is correct.");
				Console.WriteLine(ex.Message);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
}