using Bots;
using NUnit.Framework;

namespace Blackjack.UnitTests;

public class GameTests
{
	[Test]
	public void KickTest()
	{
		Game game = new Game();
		SillyBot bot = new SillyBot("Silly", 0);
		game.Players.Add(bot);
		
		game.Start();
		
		Assert.AreEqual(0, game.Players.Count);
	}
}