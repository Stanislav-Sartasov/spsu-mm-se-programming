using System;
using System.IO;
using NUnit.Framework;
using BotManagementPlugin;

namespace BotManagementPluginTests
{
    public class BotManagerTests
    {
        [Test]
        public void DoTest()    
        {
            var manager = new BotManager();
            var input = new StringReader("..\\..\\..\\..\\Extensions");
            Console.SetIn(input);
            Assert.DoesNotThrow(() => manager.Do());
        }

        [Test]
        public void DoExitTest()
        {
            var manager = new BotManager();
            var input = new StringReader("Exit");
            Console.SetIn(input);
            Assert.DoesNotThrow(() => manager.Do());
        }
    }
}