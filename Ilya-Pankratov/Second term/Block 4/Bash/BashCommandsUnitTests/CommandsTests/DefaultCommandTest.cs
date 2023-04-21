using System.Collections.Generic;
using System.Linq;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class DefaultCommandTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new DefaultCommand();
            Assert.AreEqual("DefaultCommand", command.FullName);
            Assert.AreEqual("defcmd", command.ShortName);
        }

        [Test]
        public void InvalidCommandTest()
        {
            ICommand command = new DefaultCommand();
            var arguments = new List<string>()
            {
                "someUnknownCommand",
                "SomeArgument"
            };

            var commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual($"Command {arguments.First()} was not found", commandResult.First());
        }
    }
}
