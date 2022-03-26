using NUnit.Framework;
using Roulette.Bot;

namespace Roulette.UnitTests.BotTests
{
	public class AllInBotTests
	{
		private const int maxAttempts = 50;

		[Test]
		public void CreateTest()
		{
			var bot = new AllInBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual("All In Bot", bot.Name);
			Assert.IsNotNull(bot.Description);

			Assert.IsNotNull(bot.ToString());

			Assert.Pass();
		}

		[Test]
		public void CorrectBetTest()
		{
			var bot = new AllInBot(900);

			Assert.IsNotNull(bot.MakeBets());

			Assert.Pass();
		}

		[Test]
		public void TakeMoneyTest()
		{
			var bot = new AllInBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual(500, bot.TakeMoney(500));
			Assert.AreEqual(400, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void TakeAllMoneyTest()
		{
			var bot = new AllInBot(900);

			Assert.AreEqual(900, bot.Money);
			Assert.AreEqual(900, bot.TakeMoney(1500));
			Assert.AreEqual(0, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void GiveMoneyTest()
		{
			var bot = new AllInBot(900);

			Assert.AreEqual(900, bot.Money);
			bot.GiveMoney(100);
			Assert.AreEqual(1000, bot.Money);

			Assert.Pass();
		}

		[Test]
		public void StopPlayingTest()
		{
			var bot = new AllInBot(900);

			var attempt = 0;
			while (bot.MakeBets().Count != 0 && attempt < maxAttempts)
				attempt++;

			if (bot.MakeBets().Count == 0 && attempt <= 40)
				Assert.Pass();

			Assert.Fail();
		}
	}
}