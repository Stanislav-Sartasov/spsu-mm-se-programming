using NUnit.Framework;

namespace Plugin.UnitTest
{
	public class Tests
	{
		[Test]
		public void LoadLibraryTest()
		{
			BotLoader.LoadBots("../../../../Bots.dll", 500);
			Assert.AreEqual(0, BotLoader.Bots.Count);

			BotLoader.LoadBots("../../../../Bots/Bots.dll", 500);
			Assert.AreEqual(3, BotLoader.Bots.Count);

			Assert.Pass();
		}
	}
}
