using BashCommands;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace BashCommandsUnitTests
{
    public class WordCountTest
    {
        [Test]
        public void AssignmentTest()
        {
            ICommand command = new WordCount();
            Assert.AreEqual("WordCount", command.FullName);
            Assert.AreEqual("wc", command.ShortName);
        }

        [Test]
        public void ExecuteTest()
        {
            ICommand command = new WordCount();
            var a = Directory.GetCurrentDirectory();
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

            foreach (var line in linesFromCommand)
            {
                Assert.IsTrue(firstFileLines.Contains(line));
            }

            // multi argument test
            arguments.Add(Path.Combine(pathToTestDir, "SecondTestFile.txt"));
            commandResult = command.Execute(arguments);
            Assert.IsNotNull(commandResult);
            Assert.AreEqual(commandResult.Count(), 2);

            linesFromCommand = commandResult.First().Split('\n');

            foreach (var line in linesFromCommand)
            {
                Assert.IsTrue(firstFileLines.Contains(line));
            }

            linesFromCommand = commandResult.Last().Split('\n');
            var secondFileLines = File.ReadAllLines(arguments.Last());

            foreach (var line in linesFromCommand)
            {
                Assert.IsTrue(secondFileLines.Contains(line));
            }
        }

        [Test]
        public void InvalidArgumentsTest()
        {
            ICommand command = new WordCount();
            var commandResult = command.Execute(null);

            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual("Invalid arguments", commandResult.First());
        }

        [Test]
        public void FileDoesNotExistTest()
        {
            ICommand command = new WordCount();
            var fileName = "TheInvalidFile.txt";
            var commandResult = command.Execute(new List<string>() { fileName });

            Assert.IsNotNull(commandResult);
            Assert.IsTrue(commandResult.Count() == 1);
            Assert.AreEqual($"The file {fileName} does not exist", commandResult.First());
        }
    }
}
