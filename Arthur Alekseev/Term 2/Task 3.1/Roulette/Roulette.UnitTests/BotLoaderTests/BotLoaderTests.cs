using NUnit.Framework;
using Roulette.BotLoader;
using System;

namespace Roulette.UnitTests.BotLoaderTests
{
	public class BotLoaderTests
	{
		private string _correctPath;
		private string _incorrectPath;

		[SetUp]
		public void SetUp()
		{
			_correctPath = "../../../../BotLib/Roulette.Bot.dll";
			_incorrectPath = "something else/Roulette.Bot.dll";
		}

		[Test]
		public void BotLoaderCreateCorrectPathTest()
		{
			BotLoader.BotLoader loader = new BotLoader.BotLoader(_correctPath);
			Assert.Pass();
		}

		[Test]
		public void BotLoaderCreateIncorrectPathTest()
		{
			try
			{
				BotLoader.BotLoader loader = new BotLoader.BotLoader(_incorrectPath);
			}
			catch (Exception ex)
			{
				Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void BotLoaderCorrectBotAmountTest()
		{
			BotLoader.BotLoader loader = new BotLoader.BotLoader(_correctPath);

			Assert.AreEqual(8, loader.BotNames.Count);

			Assert.Pass();
		}

		[Test]
		public void ValidNameBotCreationTest()
		{
			BotLoader.BotLoader loader = new BotLoader.BotLoader(_correctPath);

			var result = loader.GetBot("AllInBot", 100);

			Assert.IsNotNull(result);
		}

		[Test]
		public void InvalidNameBotCreationTest()
		{
			BotLoader.BotLoader loader = new BotLoader.BotLoader(_correctPath);

			var result = loader.GetBot("Invalid Bot Name", 100);

			Assert.IsNull(result);
		}
	}
}
