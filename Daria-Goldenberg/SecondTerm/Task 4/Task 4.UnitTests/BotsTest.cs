using NUnit.Framework;
using BlackJack;
using Bots;

namespace Task_4.UnitTests
{
	public class BotsTest
	{

		[TestCase(1000, 950)]
		[TestCase(20, 15)]
		public void FirstBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}

		[TestCase(1000, 875)]
		[TestCase(800, 750)]
		[TestCase(204, 153)]
		[TestCase(198, 99)]
		[TestCase(37, 27)]
		public void SecondBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();

			SecondBot bot = new SecondBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}

		[TestCase(1000, 880)]
		[TestCase(700, 640)]
		[TestCase(500, 470)]
		[TestCase(300, 285)]
		[TestCase(100, 95)]
		public void ThirdBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();

			ThirdBot bot = new ThirdBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}
	}
}
