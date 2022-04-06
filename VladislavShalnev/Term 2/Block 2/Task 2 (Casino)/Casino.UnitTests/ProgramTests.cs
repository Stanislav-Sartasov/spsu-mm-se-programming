using System.Collections.Generic;
using Blackjack;
using Blackjack.Players;
using Bots;
using NUnit.Framework;

namespace Casino.UnitTests;

public class ProgramTests
{
	[Test]
	public void SubscribeToEventsTest()
	{
		Game game = new Game();
		game.Players = new List<Player>()
		{
			new MadBot("Mad", 1000),
			new RiskyBot("Risky", 1000),
			new SillyBot("Silly", 1000)
		};
		
		foreach (Player bot in game.Players)
		{
			Assert.IsNull(bot.OnLoss);
			Assert.IsNull(bot.OnWin);
			Assert.IsNull(bot.OnTie);
			Assert.IsNull(bot.OnHit);
			Assert.IsNull(bot.OnStand);
			Assert.IsNull(bot.OnKick);
			Assert.IsNull(bot.OnBet);
		}
		
		Assert.IsNull(game.OnStart);
		Assert.IsNull(game.OnEnd);
		
		Program.SubscribeToEvents(game);

		foreach (Player bot in game.Players)
		{
			Assert.IsNotNull(bot.OnLoss);
			Assert.IsNotNull(bot.OnWin);
			Assert.IsNotNull(bot.OnTie);
			Assert.IsNotNull(bot.OnHit);
			Assert.IsNotNull(bot.OnStand);
			Assert.IsNotNull(bot.OnKick);
			Assert.IsNotNull(bot.OnBet);
		}
		
		Assert.IsNotNull(game.OnStart);
		Assert.IsNotNull(game.OnEnd);
		
		foreach (Player bot in game.Players)
		{
			bot.OnLoss?.Invoke();
			bot.OnWin?.Invoke();
			bot.OnTie?.Invoke();
			bot.OnHit?.Invoke();
			bot.OnStand?.Invoke();
			bot.OnKick?.Invoke();
			bot.OnBet?.Invoke();
		}
		
		game.OnStart?.Invoke();
		game.OnEnd?.Invoke();
	}
}