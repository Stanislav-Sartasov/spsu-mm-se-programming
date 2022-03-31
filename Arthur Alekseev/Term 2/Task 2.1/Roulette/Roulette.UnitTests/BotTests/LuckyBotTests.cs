using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.BotTests
{
	public class LuckyBotTests
	{
		private const int maxAttempts = 50;

		[Test]
		public void CreateTest()
		{
			var bot = new LuckyBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual("Lucky Bot", bot.Name);
			Assert.IsNotNull(bot.Description);

			Assert.IsNotNull(bot.ToString());

			Assert.Pass();
		}

		[Test]
		public void CorrectBetTest()
		{
			var bot = new LuckyBot(900);

			Assert.IsNotNull(bot.MakeBets());

			Assert.Pass();
		}

		[Test]
		public void TakeMoneyTest()
		{
			var bot = new LuckyBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual(500, bot.TakeMoney(500));
			Assert.AreEqual(400, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void TakeAllMoneyTest()
		{
			var bot = new LuckyBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual(900, bot.TakeMoney(1500));
			Assert.AreEqual(0, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void GiveMoneyTest()
		{
			var bot = new LuckyBot(900);

			Assert.AreEqual(900, bot.Money);
			bot.GiveMoney(100);
			Assert.AreEqual(1000, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void StopPlayingTest()
		{
			var bot = new LuckyBot(900);

			var attempt = 0;
			while (bot.MakeBets().Count != 0 && attempt < maxAttempts)
				attempt++;

			Assert.AreEqual(0, bot.MakeBets().Count);
			Assert.AreEqual(13, attempt);

			Assert.Pass();
		}

		[Test]
		public void CorrectNumberBetTest()
		{
			var bot = new LuckyBot(900);
			var bet = bot.MakeBets()[0];

			// Fields of bet are private, so I will check if bet gives the bot money
			bet.Play(new Field(13, Color.Black));
			Assert.IsTrue(bot.Money > 900);

			Assert.Pass();
		}
	}
}