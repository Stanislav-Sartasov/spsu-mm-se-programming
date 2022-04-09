using System;
using NUnit.Framework;
using BotPluginsLib;

namespace BotPluginsTest
{
    public class PluginLoaderTest
    {
        private const String TestPluginsDirectory = "..\\..\\..\\..\\TestFiles";
        private const Int64 StartBalance = 1000;

        [Test]
        public void TestPluginBotLoader()
        {
            BotPluginLoader loader = new BotPluginLoader(TestPluginsDirectory, StartBalance);
            Assert.AreEqual(3, loader.LoadedBots.Count);
        }
    }
}