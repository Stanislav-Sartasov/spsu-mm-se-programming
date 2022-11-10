using NUnit.Framework;
using Task4;
using GameTable.BotStructure;

namespace Plugin.UnitTests
{
    public class PluginTests
    {

        [Test]
        public void BadPathTest()
        {
            int startBalance = 10000, startCounterGame = 40;
            string name = "";
            object[] launchArgs = { name, startBalance, startCounterGame };
            var bots = new BotLoader().BotsLoader("../../Plugin/", launchArgs);
            Assert.AreEqual(null, bots);
        }
        [Test]
        public void BotsLoaderTest()
        {
            int startBalance = 10000, startCounterGame = 40;
            string name = "";
            object[] launchArgs = { name, startBalance, startCounterGame };
            var bots = new BotLoader().BotsLoader("../../../../Plugin/Plugins/", launchArgs);
            
            Assert.IsNotNull(bots);
            Assert.AreEqual(3, bots.Count);
            
            Assert.AreEqual(bots[0].Name, "ProgressionSeriesBot");
            Assert.AreEqual(bots[1].Name, "ThomasDonaldBot");
            Assert.AreEqual(bots[2].Name, "WideStrideBot");

            Assert.AreEqual(bots[0].Balance, 10000);
            Assert.AreEqual(bots[1].Balance, 10000);
            Assert.AreEqual(bots[2].Balance, 10000);

            Assert.AreEqual(bots[0].GameCounter, 40);
            Assert.AreEqual(bots[1].GameCounter, 40);
            Assert.AreEqual(bots[2].GameCounter, 40);

            StartCasino casino;

            if (bots is not null)
            {
                for (int i = 0; i < bots.Count; i++)
                {
                    casino = new StartCasino(bots[i]);
                    casino.Launch();
                    Assert.AreEqual(casino.bot, bots[i]);
                    Assert.AreEqual(bots[i].Gain, bots[i].Balance - startBalance);
                    if (bots[i].GameCounter + 1 == 0)
                    {
                        Assert.AreEqual(bots[i].GameCounter + 1, 0);
                    }
                    Assert.AreEqual(bots[i].State, BotState.Stop);
                }
            }
        }

    }
}