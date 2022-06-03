using System.Collections.Generic;
using System.Linq;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class ExitTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new ExitCommand();
            Assert.AreEqual("Exit", command.FullName);
            Assert.AreEqual("exit", command.ShortName);
        }

        [Test]
        public void InvalidArgumentTest()
        {
            ICommand command = new ExitCommand();
            var commandResult = command.Execute(new List<string>() { "Some argument" });
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(1, commandResult.Count());
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }
    }
}
