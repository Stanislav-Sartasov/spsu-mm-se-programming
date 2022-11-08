using NUnit.Framework;

namespace LibraryLoader.Tests
{
	public class LoaderTests
	{
		[Test]
		public void LoadBotsTest()
		{
			Loader load = new();
			var bots = load.LoadBots("../../../../TestBotsDll");
			Assert.AreEqual(3, bots.Count);
			foreach (var bot in bots)
				Assert.AreEqual(100000, bot.GetBalance());

			Assert.Pass();
		}
	}
}