using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.BetTests
{
	public class DozenBetTests
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
			var bet = new DozenBet(250, _testBot, 0);

			Assert.Pass();
		}

		[Test]
		public void SuccessPlayTest()
		{
			var bet = new DozenBet(250, _testBot, 1);
			bet.Play(new Field(13, Color.Red));

			Assert.AreEqual(1750, _testBot.Money);

			Assert.Pass();
		}

		[Test]
		public void FailPlayTest()
		{
			var bet = new DozenBet(250, _testBot, 2);
			bet.Play(new Field(13, Color.Green));

			Assert.AreEqual(1000, _testBot.Money);

			Assert.Pass();
		}
	}
}