using BotLibrary;
using NUnit.Framework;
using System;
using Moq;
using BasicLibrary;
using System.Collections.Generic;
using System.Reflection;
using Task4;
using System.IO;

namespace Task4.UnitTests
{
    public class NewUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }
     
        [Test]
        public void LoaderTest()
        {
            var loader = new PluginLoader();


            string[] paths = new string[4] { "..\\..\\..\\..\\BotLibrary",
                "..\\..\\..\\..\\BasicLibrary",
                "..\\..\\..\\..\\Task4",
                "..\\..\\..\\..\\SomeNotExistingFolder"};

            var plugins = loader.GetPlugins(paths);

            if (plugins.Count != 1) Assert.Fail();
            if (loader.GetAsm("BotPlugin") == null) Assert.Fail();
            for (int i = 1; i < 3; i++)
            {
                if (loader.GetAsm(paths[i]) == null) Assert.Fail();
            }
            try
            {
                loader.GetAsm(paths[3]);
                Assert.Fail();
            }
            catch (Exception)
            {
            }

            Assert.Pass();

        }
        [Test]

        public void GamblerTest()
        {
            var mock = new Mock<Confirmer>();
            Dealer dealer = new Dealer(22);
            mock.Setup(x => x.GetCorectAnswer<PlayerMove>()).Returns(PlayerMove.Pass);
            mock.Setup(x => x.GetCorectInt(It.IsAny<int>(), It.IsAny<int>())).Returns(100);
            Gambler man = new Gambler(10000);
            man.confirmer = mock.Object;
            Game game = new Game(new List<Gamester>() { man });
            FieldInfo fi = typeof(Game).GetField("gameDealer", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(game, dealer);
            game.Start(10);
            FieldInfo field = typeof(Gambler).GetField("bank", BindingFlags.Instance | BindingFlags.NonPublic);
            if ((int)field.GetValue(man) < 10000) Assert.Fail();
            Assert.Pass();
        }

        [Test]
        public void BotPluginTest()
        {
            PluginLoader loader = new PluginLoader();
            BotPlugin bp = new BotPlugin();
            loader.GetPlugins(new string[] { "..\\..\\..\\..\\BotLibrary" });
            var bots = bp.LoadBots(loader.GetAsm("BotPlugin"));
            if (bots.Count != 9) Assert.Fail();
            Assert.Pass();
        }
    }
}
