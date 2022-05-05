using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.BetTests
{
	public class NumberBetTests
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
			var bet = new NumberBet(250, _testBot, 13);

			Assert.Pass();
		}

		[Test]
		public void SuccessPlayTest()
		{
			var bet = new NumberBet(250, _testBot, 13);
			bet.Play(new Field(13, Color.Red));

			Assert.AreEqual(10000, _testBot.Money);

			Assert.Pass();
		}

		[Test]
		public void FailPlayTest()
		{
			var bet = new NumberBet(250, _testBot, 13);
			bet.Play(new Field(2, Color.Green));

			Assert.AreEqual(1000, _testBot.Money);

			Assert.Pass();
		}
	}
}