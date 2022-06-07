using LibraryLoader;
using Game.Players;

namespace Task4.Tests
{
    public class LoaderTest
    {
        string path;

        [Test]
        public void LoadBotsTest()
        {
            path = "../../../../Bots/Bots.dll";
            List<Player> bots = Loader.LoadBots(path);
            Assert.That(bots.Count, Is.EqualTo(3));

            Assert.Pass();
        }
    }
}
