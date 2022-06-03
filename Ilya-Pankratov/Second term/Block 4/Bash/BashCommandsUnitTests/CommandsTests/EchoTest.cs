using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class EchoTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new EchoCommand();
            Assert.AreEqual("Echo", command.FullName);
            Assert.AreEqual("echo", command.ShortName);
        }

        [Test]
        public void ExecuteTest()
        {
            ICommand command = new EchoCommand();

            // single argument test
            var arguments = new List<string>() { "Something" };
            var commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 1);
            Assert.AreEqual("Something", commandResult.First());

            // multi argiment test
            arguments.Add("Something more");
            commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 2);
            Assert.AreEqual("Something", commandResult.First());
            Assert.AreEqual("Something more", commandResult.Last());
        }

        [Test]
        public void InvalidArgumentsTest()
        {
            ICommand command = new EchoCommand();
            var commandResult = command.Execute(null);

            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 1);
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }
    }
}
