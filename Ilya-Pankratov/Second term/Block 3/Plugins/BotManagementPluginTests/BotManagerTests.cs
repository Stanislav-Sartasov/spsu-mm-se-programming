using System;
using System.IO;
using BotManagementPlugin;
using NUnit.Framework;

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

            try
            {
                manager.Do();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}