using System.Linq;
using BaseBot;
using KellyBot;
using NUnit.Framework;
using Plugins;
using ThorpBot;

namespace PluginsTests
{
    public class BotsLoaderTests
    {
        [Test]
        public void GoodPathLoadTest()
        {
            var bots = BotsLoader.LoadBots("../../../../../BlackJack/Plugins/BotsDlls");

            Assert.AreEqual(3, bots.Count);
            Assert.AreEqual(1, bots.Count(x => x is BaseStrategyBot));
            Assert.AreEqual(1, bots.Count(x => x is ThorpSystemBot));
            Assert.AreEqual(1, bots.Count(x => x is KellyCriterionBot));

            foreach (var bot in bots)
            {
                Assert.AreEqual(1000, bot.Balance);
            }
        }

        [Test]
        public void BadPathsLoadTest()
        {
            var bots = BotsLoader.LoadBots("../../../../BotsDlls");

            Assert.IsEmpty(bots);

            bots = BotsLoader.LoadBots("../../../../../BlackJack/Plugins");

            Assert.IsEmpty(bots);
        }
    }
}