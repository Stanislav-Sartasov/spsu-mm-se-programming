namespace PluginLoader.UnitTests;
using NUnit.Framework;
using PluginLoader;
using Roulette;

public class BotLoaderTests
{
    [Test]
    public void NonExistentPathTest()
    {
        string nonExistentPath = "Folder/";

        Casino casino = new Casino(200, 20000);
        const int startAmountOfMoney = 12000;
        object[] parameters = { startAmountOfMoney, casino };

        Assert.AreEqual(null, BotLoader.LoadBots(nonExistentPath, parameters));
    }

    [Test]
    public void NoDllFilesTest()
    {
        string path = "../../../../BotLoaderTestFolder/";

        Casino casino = new Casino(200, 20000);
        const int startAmountOfMoney = 12000;
        object[] parameters = { startAmountOfMoney, casino };

        Assert.AreEqual(null, BotLoader.LoadBots(path, parameters));
    }

    [Test]
    public void LoadBotsTest()
    {
        string path = "../../../../Plugins/";

        Casino casino = new Casino(200, 20000);
        const int startAmountOfMoney = 12000;
        object[] parameters = { startAmountOfMoney, casino };

        var bots = BotLoader.LoadBots(path, parameters);

        if (bots == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(5, bots.Length);
        }
    }
}
