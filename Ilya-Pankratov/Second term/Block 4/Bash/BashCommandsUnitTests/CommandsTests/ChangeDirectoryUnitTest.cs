using System.Collections.Generic;
using System.IO;
using System.Linq;
using BashCommands;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class Tests
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new ChangeDirectoryCommand();
            Assert.AreEqual(command.FullName, "ChangeDirectory");
            Assert.AreEqual(command.ShortName, "cd");
        }

        [Test]
        public void ExecuteGoToRootDirectoryTest()
        {
            var initualDir = Directory.GetCurrentDirectory();
            ICommand command = new ChangeDirectoryCommand();
            var arguments = new List<string>() { ".." };
            var newDirectory = Directory.GetParent(initualDir);
            string newDirectoryPath;

            if (newDirectory == null)
            {
                newDirectoryPath = initualDir;
            }
            else
            {
                newDirectoryPath = newDirectory.FullName;
            }

            command.Execute(arguments);

            Assert.AreEqual(newDirectoryPath, Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(initualDir);
        }

        [Test]
        public void ExecuteAbsolutePathTest()
        {
            var initualDir = Directory.GetCurrentDirectory();
            ICommand command = new ChangeDirectoryCommand();
            var newDirectory = Directory.GetParent(initualDir);
            string newDirectoryPath;

            if (newDirectory == null)
            {
                newDirectoryPath = Directory.GetCurrentDirectory();
            }
            else
            {
                newDirectoryPath = newDirectory.FullName;
            }

            var arguments = new List<string>() { newDirectoryPath };
            command.Execute(arguments);

            Assert.AreEqual(newDirectoryPath, Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(initualDir);
        }

        [Test]
        public void ExecuteMoveToTheSpecifiedDirectory()
        {
            var initualDir = Directory.GetCurrentDirectory();
            ICommand command = new ChangeDirectoryCommand();
            var arguments = new List<string>() { ".." };
            var newDirectoryPath = Directory.GetCurrentDirectory();
            var newDirectory = newDirectoryPath.Split('\\').Last();

            command.Execute(arguments);

            arguments = new List<string>() { newDirectory };
            command.Execute(arguments);

            Assert.AreEqual(newDirectoryPath, Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(initualDir);
        }

        [Test]
        public void ExecuteChangeDriveTest()
        {
            var initualDir = Directory.GetCurrentDirectory();
            ICommand command = new ChangeDirectoryCommand();
            string newDirectoryPath = DriveInfo.GetDrives().First().Name;
            var arguments = new List<string>() { newDirectoryPath };

            command.Execute(arguments);
            Assert.AreEqual(newDirectoryPath, Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory(initualDir);
        }

        [Test]
        public void ExecuteInvalidArgumentsTest()
        {
            ICommand command = new ChangeDirectoryCommand();
            string newDirectoryPath = "Invalid arguments";
            var commandResult = command.Execute(null);
            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual(newDirectoryPath, commandResult.First());
        }
    }
}