using System;
using System.Collections.Generic;
using System.Linq;
using Roulette.Bot;
using Roulette.Common.GamePlay;

namespace Roulette
{
	public class StatisticCollector
	{
		private const int BotAmount = 500;
		private const int BotTypeAmount = 500;
		private const int BotMoney = 10000;
		private const int TurnAmount = 40;

		private readonly Game _game;

		public StatisticCollector()
		{
			_game = new Game();
			FillPlayers();
		}

		private void FillPlayers()
		{
			for (var i = 0; i < BotAmount; i++)
			{
				_game.AddPlayer(new RandomBot(BotMoney));
				_game.AddPlayer(new AllInBot(BotMoney));
				_game.AddPlayer(new IntelligentBot(BotMoney));
				_game.AddPlayer(new CautiousBot(BotMoney));
				_game.AddPlayer(new AllInOneWinBot(BotMoney));
				_game.AddPlayer(new LuckyBot(BotMoney));
				_game.AddPlayer(new RedBetBot(BotMoney));
				_game.AddPlayer(new BlackBetBot(BotMoney));
			}
		}

		public void CollectStatistics()
		{
			_game.PlayGame(TurnAmount);
		}

		public void LogStatistics()
		{
			var types = new List<Bot.Bot>();

			types.Add(new RandomBot(BotMoney));
			types.Add(new AllInBot(BotMoney));
			types.Add(new IntelligentBot(BotMoney));
			types.Add(new CautiousBot(BotMoney));
			types.Add(new AllInOneWinBot(BotMoney));
			types.Add(new LuckyBot(BotMoney));
			types.Add(new RedBetBot(BotMoney));
			types.Add(new BlackBetBot(BotMoney));

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
				var average = (int) _game.Players.FindAll(b => ((Bot.Bot) b).Name == bot.Name)
					.Select(b => ((Bot.Bot) b).Money).Average();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(average, BotMoney);
				StatisticsLogger.LogNewLine();
			}

			// Best results
			StatisticsLogger.LogHeader("Best bot results:");
			foreach (var bot in types)
			{
				var maximum = _game.Players.FindAll(b => ((Bot.Bot) b).Name == bot.Name)
					.Select(b => ((Bot.Bot) b).Money).Max();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(maximum, BotMoney);
				StatisticsLogger.LogNewLine();
			}
		}
	}
}