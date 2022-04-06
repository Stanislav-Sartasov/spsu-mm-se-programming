using System.Collections.Generic;
using Blackjack;
using Blackjack.Cards;
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

		Program.SubscribeToEvents(game);

		foreach (Player bot in game.Players)
		{
			bot.Hit(new Card(CardSuit.Diamonds, CardType.Ace));
			bot.Stand();
			bot.Kick();
		}
	}
}