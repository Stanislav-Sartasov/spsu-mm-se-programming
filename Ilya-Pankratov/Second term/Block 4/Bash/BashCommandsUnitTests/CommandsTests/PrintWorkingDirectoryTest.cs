using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class PrintWorkingDirectoryTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new PrintWorkingDirectoryCommand();
            Assert.AreEqual("PrintWorkingDirectory", command.FullName);
            Assert.AreEqual("pwd", command.ShortName);
        }

        [Test]
        public void ExecuteTest()
        {
            ICommand command = new PrintWorkingDirectoryCommand();
            var commandResult = command.Execute(null);

            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual(Directory.GetCurrentDirectory(), commandResult.First());
        }

        [Test]
        public void InvalidArgumentsTest()
        {
            ICommand command = new PrintWorkingDirectoryCommand();
            var commandResult = command.Execute(new List<string>());
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(1, commandResult.Count());
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }
    }
}