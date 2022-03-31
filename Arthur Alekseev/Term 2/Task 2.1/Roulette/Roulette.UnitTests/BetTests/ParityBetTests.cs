using NUnit.Framework;
using Roulette.Bot;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.BetTests
{
	public class ParityBetTests
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
			var bet = new ParityBet(250, _testBot, Parity.Even);

			Assert.Pass();
		}

		[Test]
		public void SuccessPlayTest()
		{
			var bet = new ParityBet(250, _testBot, Parity.Odd);
			bet.Play(new Field(13, Color.Red));

			Assert.AreEqual(1500, _testBot.Money);

			Assert.Pass();
		}

		[Test]
		public void FailPlayTest()
		{
			var bet = new ParityBet(250, _testBot, Parity.Even);
			bet.Play(new Field(13, Color.Green));

			Assert.AreEqual(1000, _testBot.Money);

			Assert.Pass();
		}
	}
}