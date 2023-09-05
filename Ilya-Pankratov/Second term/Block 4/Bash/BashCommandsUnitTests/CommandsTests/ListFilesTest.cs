using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class ListFilesTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new ListFilesCommand();
            Assert.AreEqual("ListFiles", command.FullName);
            Assert.AreEqual("ls", command.ShortName);
        }

        [Test]
        public void ExecuteTest()
        {
            ICommand command = new ListFilesCommand();
            var commandResult = command.Execute(null);
            var fileEntiry = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory());
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(fileEntiry.Length, commandResult.Count());

            foreach (var entiry in fileEntiry)
            {
               Assert.IsTrue(commandResult.Contains(entiry.Split('\\').Last())); 
            }
        }

        [Test]
        public void InvalidArgumentsTest()
        {
            ICommand command = new ListFilesCommand();
            var commandResult = command.Execute(new List<string>());
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(1, commandResult.Count());
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }
    }
}
