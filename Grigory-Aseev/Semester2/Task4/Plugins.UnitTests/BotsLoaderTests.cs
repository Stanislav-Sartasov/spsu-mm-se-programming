using NUnit.Framework;
using PlayerStructure;
using System.Collections.Generic;

namespace Plugins.UnitTests
{
    public class BotsLoaderTests
    {
        [Test]
        public void PluginConstructorTest()
        {
            BotsLoader loader = new BotsLoader();
            Assert.AreEqual(0, loader.SuccessfullyLoadedBots);
            Assert.AreEqual(0, loader.NumberOfBots);
        }

        [Test]
        public void LoadWithGoodPathTest()
        {
            BotsLoader loader = new BotsLoader();
            Assert.AreEqual(3, loader.Load("../../../../Plugins/Dlls/Bots.dll"));
            Assert.AreEqual(3, loader.SuccessfullyLoadedBots);
            Assert.AreEqual(3, loader.NumberOfBots);

        }

        [Test]
        public void LoadWithBadPathTest()
        {
            BotsLoader loader = new BotsLoader();
            Assert.AreEqual(0, loader.Load("../../../../Plugins"));
            Assert.AreEqual(0, loader.SuccessfullyLoadedBots);
            Assert.AreEqual(0, loader.NumberOfBots);
        }

        [Test]
        public void TakeBotsWellTest()
        {
            BotsLoader loader = new BotsLoader();
            loader.Load("../../../../Plugins/Dlls/Bots.dll");
            (List<IPlayer>, List<string>) bots = loader.TakeBots();

            Assert.AreEqual(3, bots.Item1.Count);
            Assert.AreEqual(3, bots.Item2.Count);
            Assert.IsTrue(bots.Item2.Contains("HalvesBot"));
            Assert.IsTrue(bots.Item2.Contains("PlusMinusBot"));
            Assert.IsTrue(bots.Item2.Contains("StandartBot"));
        }

        [Test]
        public void TakeBotsBadlyTest()
        {
            BotsLoader loader = new BotsLoader();
            loader.Load("../../../../Plugins");
            (List<IPlayer>, List<string>) bots = loader.TakeBots();

            Assert.AreEqual(0, bots.Item1.Count);
            Assert.AreEqual(0, bots.Item2.Count);
            Assert.IsFalse(bots.Item2.Contains("HalvesBot"));
            Assert.IsFalse(bots.Item2.Contains("PlusMinusBot"));
            Assert.IsFalse(bots.Item2.Contains("StandartBot"));
        }
    }
}