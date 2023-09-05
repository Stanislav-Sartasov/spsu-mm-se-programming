using System;
using System.Linq;
using Roulette.Common.GamePlay;
using Roulette.BotLoader;
using System.Collections.Generic;
using Roulette.Common;

namespace Roulette
{
	public class StatisticCollector
	{
		private const int BotAmount = 500;
		private const int BotTypeAmount = 500;
		private const int BotMoney = 10000;
		private const int TurnAmount = 40;

		private readonly Game _game;
		private readonly BotLoader.BotLoader loader;

		public StatisticCollector(string botLibraryPath)
		{
			_game = new Game();
			loader = new BotLoader.BotLoader(botLibraryPath);
			
			FillPlayers();
		}

		private void FillPlayers()
		{
			for (var i = 0; i < BotAmount; i++)
			{
				foreach (var name in loader.BotNames)
				{
					IPlayer bot = loader.GetBot(name, BotMoney);
					if (bot!= null)
						_game.Players.Add(bot);
				}
			}
		}

		public void CollectStatistics()
		{
			_game.PlayGame(TurnAmount);
		}

		public void LogStatistics()
		{
			var types = new List<IPlayer>();

			foreach (var name in loader.BotNames)
				types.Add(loader.GetBot(name, BotMoney));

			StatisticsLogger.LogHeader("Experimental data");
			StatisticsLogger.LogMessage("Turn amount: " + Convert.ToString(TurnAmount));
			StatisticsLogger.LogMessage("Bot amount: " + Convert.ToString(BotAmount));
			StatisticsLogger.LogMessage("Starting money: " + Convert.ToString(BotMoney));
			StatisticsLogger.LogMessage("Bot types count:" + Convert.ToString(BotTypeAmount));

			StatisticsLogger.LogHeader("Bot Descriptions");
			foreach (var bot in types)
			{
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogNewLine();
				StatisticsLogger.LogMessage(bot.Description);
				StatisticsLogger.LogNewLine();
			}

			// Averages
			StatisticsLogger.LogHeader("Average bot money:");

			foreach (var bot in types)
			{
				var average = (int) _game.Players.FindAll(b => b.Name == bot.Name)
					.Select(b => b.Money).Average();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(average, BotMoney);
				StatisticsLogger.LogNewLine();
			}

			// Best results
			StatisticsLogger.LogHeader("Best bot results:");
			foreach (var bot in types)
			{
				var maximum = _game.Players.FindAll(b => b.Name == bot.Name)
					.Select(b => b.Money).Max();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(maximum, BotMoney);
				StatisticsLogger.LogNewLine();
			}
		}
	}
}