using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BotsPluginManagement;

namespace BotsPluginManagementTests
{
    [TestClass]
    public class BotsManagerTests
    {
        private const string pathForTests = @"..\..\..\..\..\Bots";

        [TestMethod]
        public void NullPathTest()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(
                () =>
                {
                    BotsManager botsManager = new BotsManager(null, 100);
                }
                );
        }

        [TestMethod]
        public void InvalidPathTest()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(
                () =>
                {
                    BotsManager botsManager = new BotsManager(@"O:\NotExistingFolder\OneMore", 200);
                }
                );
        }

        [TestMethod]
        public void GetBotByTypeNameTest()
        {
            BotsManager botsManager = new BotsManager(pathForTests, 200);
            var baseStrategyBot = botsManager.GetBotByTypeName("BaseStrategyBot");

            Assert.IsNotNull(baseStrategyBot);
        }

        [TestMethod]
        public void UpdateBotsTest()
        {
            BotsManager botsManager = new BotsManager(pathForTests, 200);
            
            foreach (var bot in botsManager.Bots)
            {
                Assert.AreEqual(200, bot.Balance);
            }

            botsManager.UpdateBots(1000);

            foreach (var bot in botsManager.Bots)
            {
                Assert.AreEqual(1000, bot.Balance);
            }
        }
    }
}