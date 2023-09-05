using BashCommands;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace BashCommandsUnitTests
{
    public class ConcatenateUnitTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new ConcatenateCommand();
            Assert.AreEqual("Concatenate", command.FullName);
            Assert.AreEqual("cat", command.ShortName);
        }

        [Test]
        public void ExecuteTest()
        {
            ICommand command = new ConcatenateCommand();
            var pathToTestDir = TestTools.FindPathToTestFiles();
            Assert.IsTrue(pathToTestDir != "TestFile directory is missing");

            // single argument test
            var arguments = new List<string>()
            {
                Path.Combine(pathToTestDir, "FirstTestFile.txt")
            };

            var commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 1);

            var linesFromCommand = commandResult.First().Split('\n');
            var firstFileLines = File.ReadAllLines(arguments[0]);
            Assert.AreEqual(linesFromCommand.Length, firstFileLines.Length);

            for (int i = 0; i < firstFileLines.Length; i++)
            {
                Assert.AreEqual(firstFileLines[i], linesFromCommand[i]);
            }

            // multi argument test
            arguments.Add(Path.Combine(pathToTestDir, "SecondTestFile.txt"));
            commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 2);

            linesFromCommand = commandResult.First().Split('\n');
            Assert.AreEqual(linesFromCommand.Length, firstFileLines.Length);

            for (int i = 0; i < firstFileLines.Length; i++)
            {
                Assert.AreEqual(firstFileLines[i], linesFromCommand[i]);
            }

            linesFromCommand = commandResult.Last().Split('\n');
            var secondFileLines = File.ReadAllLines(arguments.Last());
            Assert.AreEqual(linesFromCommand.Length, secondFileLines.Length);

            for (int i = 0; i < secondFileLines.Length; i++)
            {
                Assert.AreEqual(secondFileLines[i], linesFromCommand[i]);
            }
        }

        [Test]
        public void InvalidArgumentsTest()
        {
            ICommand command = new ConcatenateCommand();
            var commandResult = command.Execute(null);

            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }

        [Test]
        public void FileDoesNotExistTest()
        {
            ICommand command = new ConcatenateCommand();
            var fileName = "TheInvalidFile.txt";
            var commandResult = command.Execute(new List<string>() { fileName });

            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual($"The file {fileName} does not exist", commandResult.First());
        }
    }
}
