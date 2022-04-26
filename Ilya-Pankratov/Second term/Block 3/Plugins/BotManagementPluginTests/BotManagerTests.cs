using System;
using System.IO;
using NUnit.Framework;
using BotManagementPlugin;

namespace BotManagementPluginTests
{
    public class Tests
    {
        [Test]
        public void DoTest()
        {
            var manager = new BotManager();
            var input = new StringReader("D:\\GitHub works SPBU\\spsu-mm-se-programming\\Ilya-Pankratov\\Second term\\Block 3\\Plugins\\Extensions");
            Console.SetIn(input);
            Assert.DoesNotThrow(() => manager.Do());
        }
    }
}