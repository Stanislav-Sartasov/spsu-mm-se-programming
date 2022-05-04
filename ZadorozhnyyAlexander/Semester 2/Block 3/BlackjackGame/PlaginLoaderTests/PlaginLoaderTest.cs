using System;
using NUnit.Framework;
using PlaginLoaderLibrary;

namespace PlaginLoaderTests
{
    public class PlaginLoaderTest
    {
        private const string rightDirectory = "..\\..\\..\\..\\BotDllFiles";
        private const string fakeDirectory = "..\\..\\..\\..\\NothingHere";
        private const int startMoney = 10000;
        private const int startRate = 500;

        [Test]
        public void RightPluginLoaderTest()
        {
            PlaginLoader loader = new PlaginLoader(rightDirectory, startMoney, startRate);
            Assert.AreEqual(3, loader.AllBots.Count);
        }

        [Test]
        public void FakePluginLoaderTest()
        {
            PlaginLoader loader = new PlaginLoader(fakeDirectory, startMoney, startRate);
            Assert.IsNull(loader.AllBots);
        }
    }
}