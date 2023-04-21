using NUnit.Framework;

namespace BotLoader.UnitTests
{
    public class LoaderTests
    {
        [Test]
        public void LoadBotTest()
        {
            Loader loader = new Loader();
            var bots = loader.LoadBot("../../../../TestBotsDll/TestBots.dll");
            Assert.AreEqual(3, bots.Count);
            var nullBots = loader.LoadBot("../../../../TestBotsDll/NotATestBots.dll");
            Assert.IsNull(nullBots);

            Assert.Pass();
        }

        [Test]
        public void LoadAllBotsFromLoadAllBotsFromDirectoryTest()
        {
            Loader loader = new Loader();
            var bots = loader.LoadAllBotsFromDirectory("../../../../TestBotsDll");
            Assert.AreEqual(6, bots.Count);
            var nullBots = loader.LoadBot("../../../../ThereAreNoBotsHere");
            Assert.IsNull(nullBots);

            Assert.Pass();
        }
    }
}