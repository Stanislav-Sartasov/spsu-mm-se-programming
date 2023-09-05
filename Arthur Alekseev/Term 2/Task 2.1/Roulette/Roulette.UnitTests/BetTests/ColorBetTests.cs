using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.BetTests
{
	public class ColorBetTests
	{
		private LuckyBot _testBot;

		[SetUp]
		public void SetUp()
		{
			_testBot = new LuckyBot(1250);
		}

		[Test]
		public void CreateTest()
		{
			var bet = new ColorBet(250, _testBot, Color.Red);

			Assert.Pass();
		}

		[Test]
		public void SuccessPlayTest()
		{
			var bet = new ColorBet(250, _testBot, Color.Red);
			bet.Play(new Field(2, Color.Red));

			Assert.AreEqual(1500, _testBot.Money);

			Assert.Pass();
		}

		[Test]
		public void FailPlayTest()
		{
			var bet = new ColorBet(250, _testBot, Color.Red);
			bet.Play(new Field(2, Color.Green));

			Assert.AreEqual(1000, _testBot.Money);

			Assert.Pass();
		}
	}
}