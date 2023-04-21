using BotManagementPlugin;
using NUnit.Framework;

namespace BotManagerTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DoTest()
        {
            BotManager manager = new BotManager();
            manager.Do();
            Assert.Pass();
        }
    }
}