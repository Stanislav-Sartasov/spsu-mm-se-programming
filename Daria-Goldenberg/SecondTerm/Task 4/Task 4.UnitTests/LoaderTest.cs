using System.Collections.Generic;
using NUnit.Framework;
using BlackJack;
using Bots;
using LibraryLoader;

namespace Task_4.UnitTests
{
	public class LoaderTest
	{
		private string path;

		[SetUp]
		public void Setup()
		{
			path = "../../../../Plugins/Bots.dll";
		}

		[Test]
		public void LoadBotsTest()
		{
			Game game = new Game();
			List<Player> bots = Loader.LoadBots(path, game);
			Assert.AreEqual(3, bots.Count);

			Assert.Pass();
		}
	}
}
