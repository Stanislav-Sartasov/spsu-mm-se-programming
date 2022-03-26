using System;
using System.Collections.Generic;
using System.Linq;
using Roulette.Bot;

namespace Roulette
{
	public class StatisticCollector
	{
		private const int BotAmount = 500;
		private const int BotTypeAmount = 500;
		private const int BotMoney = 10000;
		private const int TurnAmount = 40;
		private readonly Common.GamePlay.Roulette _roulette;
		private readonly List<Bot.Bot> _players;

		public StatisticCollector()
		{
			_players = new List<Bot.Bot>();
			_roulette = new Common.GamePlay.Roulette();

			FillPlayers();
		}

		private void FillPlayers()
		{
			for (var i = 0; i < BotAmount; i++)
			{
				_players.Add(new RandomBot(BotMoney));
				_players.Add(new AllInBot(BotMoney));
				_players.Add(new IntelligentBot(BotMoney));
				_players.Add(new CautiousBot(BotMoney));
				_players.Add(new AllInOneWinBot(BotMoney));
				_players.Add(new LuckyBot(BotMoney));
				_players.Add(new RedBetBot(BotMoney));
				_players.Add(new BlackBetBot(BotMoney));
			}
		}

		public void CollectStatistics()
		{
			for (var i = 0; i < TurnAmount; i++)
				PlayTurn();
		}

		private void PlayTurn()
		{
			foreach (var bet in _players.SelectMany(player => player.MakeBets())) bet.Play(_roulette.GetRandomField());
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
				var average = (int) _players.FindAll(b => b.Name == bot.Name).Select(b => b.Money).Average();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(average, BotMoney);
				StatisticsLogger.LogNewLine();
			}

			// Best results
			StatisticsLogger.LogHeader("Best bot results:");
			foreach (var bot in types)
			{
				var maximum = _players.FindAll(b => b.Name == bot.Name).Select(b => b.Money).Max();
				StatisticsLogger.LogBotName(bot.Name);
				StatisticsLogger.LogMessageNoNewLine(": ");
				StatisticsLogger.LogMoney(maximum, BotMoney);
				StatisticsLogger.LogNewLine();
			}
		}
	}
}