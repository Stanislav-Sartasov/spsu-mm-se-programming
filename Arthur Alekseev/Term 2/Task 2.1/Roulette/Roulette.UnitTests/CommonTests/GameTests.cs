using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.CommonTests
{
	public class GameTests
	{
		[Test]
		public void CreationTest()
		{
			var game = new Game();
			Assert.IsNotNull(game);
			Assert.AreEqual(0, game.Players.Count);
			Assert.Pass();
		}

		[Test]
		public void BotAddTest()
		{
			var bot = new LuckyBot(700);

			var game = new Game();

			game.AddPlayer(bot);
			Assert.AreEqual(1, game.Players.Count);
			Assert.AreEqual(bot, game.Players[0]);

			Assert.Pass();
		}

		[Test]
		public void PlayGameTest()
		{
			var redBetBot = new RedBetBot(700);
			var blackBetBot = new BlackBetBot(700);

			var game = new Game();

			game.AddPlayer(redBetBot);
			game.AddPlayer(blackBetBot);

			// Play 1 game, 1 of bots should lose
			game.PlayGame(1);

			if (((Bot.Bot) game.Players[0]).Money < 700 || ((Bot.Bot) game.Players[1]).Money < 700) Assert.Pass();

			Assert.Fail();
		}
	}
}