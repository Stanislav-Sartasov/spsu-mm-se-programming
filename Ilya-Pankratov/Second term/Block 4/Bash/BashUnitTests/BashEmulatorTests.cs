using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class BashEmulatorTests
    {
        private readonly string homeDir = GetHomeDir();
        private readonly string exitCode = new Guid().ToString();

        [Test]
        public void PrintWorkingDirectoryTest()
        {
            var bash = new BashEmulator(exitCode);
            var testCommand = "pwd";
            var commandResult = bash.Execute(testCommand);
            var expectedResults = homeDir;

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResults, commandResult[0]);
        }

        [Test]
        public void ChangeDirectoryTest()
        {
            // first case - go to root directory
            var bash = new BashEmulator(exitCode);
            var testCommand = "cd";
            var argument = "..";

            var commandResult = bash.Execute(testCommand + ' ' + argument);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            commandResult = bash.Execute("pwd");
            var splittedHomeDir = homeDir.Split('\\')[..^1];
            string expectedResult = UnitedPath(splittedHomeDir);

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResult, commandResult[0]);

            // second case - go back to previous directory
            argument = homeDir.Split('\\')[^1];
            commandResult = bash.Execute(testCommand + ' ' + argument);

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            commandResult = bash.Execute("pwd");
            expectedResult += $"\\{argument}";

            Assert.NotNull(commandResult);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResult, commandResult[0]);
        }

        [Test]
        public void ListFilesTest()
        {
            var bash = new BashEmulator(exitCode);
            var testCommand = "ls";

            var commandResult = bash.Execute(testCommand);
            var fileEntities = Directory.GetFileSystemEntries(homeDir);
            Assert.IsTrue(commandResult.Count == fileEntities.Length);

            foreach (var entry in fileEntities)
            {
                Assert.IsTrue(commandResult.Contains(entry.Split("\\")[^1]));
            }
        }

        [Test]
        public void ExitTest()
        {
            var bash = new BashEmulator(exitCode);
            var testCommand = "exit";

            var commandResult = bash.Execute(testCommand);
            var expectedResult = exitCode;

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResult, commandResult[0]);
        }

        [Test]
        public void EchoTest()
        {
            // single argument test
            var bash = new BashEmulator(exitCode);
            var testCommand = "echo";
            var arguments = new List<string>()
            {
                "firstArg"
            };

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            var commandResult = bash.Execute(command);
            var expectedResult = arguments[0];

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResult, commandResult[0]);

            // multi argument test
            arguments = new List<string>()
            {
                "firstArg", "secondArg", "ThirdArg"
            };

            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command);

            Assert.IsTrue(commandResult.Count == arguments.Count);

            for (int i = 0; i < arguments.Count; i++)
            {
                Assert.AreEqual(arguments[i], commandResult[i]);
            }
        }

        [Test]
        public void ConcatenateTest()
        {
            // single argument test
            var bash = new BashEmulator(exitCode);
            var testCommand = "cat";
            var arguments = new List<string>()
            {
                "FirstTestFile.txt"
            };

            var commandResult = bash.Execute("cd TestFiles");
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 1);
            var splittedCommandResult = commandResult[0].Split('\n');
            var firstFileLines = File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt");
            
            Assert.AreEqual(splittedCommandResult.Length - 1, firstFileLines.Length);

            for (int i = 0; i < firstFileLines.Length; i++)
            {
                Assert.AreEqual(firstFileLines[i], splittedCommandResult[i + 1]);
            }

            // multi argument test
            arguments.Add("SecondTestFile.txt");
            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command);
            Assert.AreEqual(commandResult.Count, 2);
            splittedCommandResult = commandResult[0].Split('\n');
            var secondFileLines = File.ReadAllLines(homeDir + @"\TestFiles\SecondTestFile.txt");
            Assert.AreEqual(splittedCommandResult.Length - 1, firstFileLines.Length);

            for (int i = 0; i < firstFileLines.Length; i++)
            {
                Assert.AreEqual(firstFileLines[i], splittedCommandResult[i + 1]);
            }

            splittedCommandResult = commandResult[1].Split('\n');
            Assert.AreEqual(splittedCommandResult.Length - 1, secondFileLines.Length);

            for (int i = 0; i < secondFileLines.Length; i++)
            {
                Assert.AreEqual(secondFileLines[i], splittedCommandResult[i + 1]);
            }
        }

        [Test]
        public void WordCountTest()
        {
            // single argument test
            var bash = new BashEmulator(exitCode);
            var testCommand = "wc";
            var arguments = new List<string>()
            {
                "FirstTestFile.txt"
            };

            var commandResult = bash.Execute("cd TestFiles");
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 1);
            var allLinesFromFirstFile = File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt");

            foreach (var line in commandResult[0].Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line));
            }

            // multi argument test
            arguments.Add("SecondTestFile.txt");
            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 2);
            var allLinesFromSecondFile = File.ReadAllLines(homeDir + @"\TestFiles\SecondTestFile.txt");

            foreach (var line in commandResult[0].Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line) || allLinesFromSecondFile.Contains(line));
            }
        }

        [Test]
        public void LocalVariablesTest()
        {
            var bash = new BashEmulator(exitCode);
            var varName = "myVar";
            var varValue = "testValue";

            var testCommand = $"${varName}={varValue}";
            var commandResult = bash.Execute(testCommand);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            testCommand = $"echo ${varName}";
            commandResult = bash.Execute(testCommand);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(varValue, commandResult[0]);
        }

        [Test]
        public void EchoPlusLocalVariableTest()
        {
            var bash = new BashEmulator(exitCode);
            var firstVarName = "firstVar";
            var firstVarValue = "firstValue";
            var secondVarName = "secondVar";
            var secondVarValue = "secondValue";
            var firstTestCommand = $"${firstVarName}={firstVarValue}";
            var secondTestCommand = $"${secondVarName}={secondVarValue}";

            var commandResult = bash.Execute(firstTestCommand);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            commandResult = bash.Execute(secondTestCommand);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            var testCommand = $"echo \"${firstVarName} first case\" \"{secondVarValue}\"";
            commandResult = bash.Execute(testCommand);

            Assert.IsTrue(commandResult.Count == 2);
            Assert.AreEqual(firstVarValue + " first case", commandResult[0]);
            Assert.AreEqual(secondVarValue, commandResult[1]);
        }

        [Test]
        public void ChangeDiskTest()
        {
            var bash = new BashEmulator(exitCode);
            var drives = DriveInfo.GetDrives();
            var currentDrive = homeDir[..2];
            var newDrive = String.Empty;
            var userHasSingleDrive = true;

            foreach (var drive in drives)
            {
                if (drive.Name[..2] != currentDrive)
                {
                    userHasSingleDrive = false;
                    newDrive = drive.Name[..2];
                }
            }

            if (userHasSingleDrive)
            {
                var result = bash.Execute(currentDrive);
                Assert.IsTrue(result.Count == 1);
                Assert.AreEqual(String.Empty, result);
                return;
            }

            var commandResult = bash.Execute(newDrive);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            commandResult = bash.Execute("pwd");
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(newDrive + '\\', commandResult[0]);
        }

        [Test]
        public void PipelineTest()
        {
            // first case
            var bash = new BashEmulator(exitCode);
            var commandResult = bash.Execute("cd TestFiles");
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            var varName = "myVar";
            var varValue = "FirstTestFile.txt";

            var command = $"${varName}={varValue}";
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);

            command = $"echo ${varName} | cat";
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 1);
            var allLinesFromFirstFile = File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt");

            foreach (var line in commandResult[0].Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line) || line == $"The file {varValue}");
            }

            // second case
            command = $"wc {varValue} | echo";
            commandResult = bash.Execute(command);
            Assert.IsTrue(commandResult.Count == 1);

            foreach (var line in commandResult[0].Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line));
            }
        }

        [Test]
        public void InvalidBashCommand()
        {
            var bash = new BashEmulator(exitCode);
            var lines = File.ReadAllLines(homeDir + @"\TestFiles\ExceptionTests.txt");

            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                var commandResult = bash.Execute(lines[i]);
                Assert.IsTrue(commandResult.Count == 1);
                Assert.AreEqual(lines[i + 1], commandResult[0]);
            }
        }

        [Test]
        public void InvalidsystemCommand()
        {
            var bash = new BashEmulator(exitCode);
            var command = "doNothing firstUselessArgumet secondUselessArgument";
            var commandResult = bash.Execute(command);
            var dirPath = homeDir + "\\BashUnitTests\\bin\\Debug";

            var expectedResult = $"Command {command.Split(' ')[0]} was not found";

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(expectedResult, commandResult[0]);
        }

        [Test]
        public void EmpetyUserInputTest()
        {
            var bash = new BashEmulator(exitCode);
            var commandResult = bash.Execute(String.Empty);

            Assert.IsTrue(commandResult.Count == 1);
            Assert.AreEqual(String.Empty, commandResult[0]);
        }

        private static string UnitedPath(string[] path)
        {
            var result = new StringBuilder();

            for (int i = 0; i < path.Length - 1; i++)
            {
                result.Append(path[i]);
                result.Append('\\');
            }

            return result.Append(path[^1]).ToString();
        }

        private static string GetHomeDir()
        {
            var path = Directory.GetCurrentDirectory();
            var strBuilder = new StringBuilder();
            var currentProjectName = Assembly.GetCallingAssembly().GetName().Name;
            var splitedPath = path.Split('\\');

            for (int i = 0; i < splitedPath.Length; i++)
            {
                strBuilder.Append(splitedPath[i]);

                if (splitedPath[i + 1] == currentProjectName)
                {
                    break;
                }
                else
                {
                    strBuilder.Append('\\');
                }
            }

            path = strBuilder.ToString();

            if (!Directory.Exists(path))
            {
                path = "Failed to find home directory";
            }

            return path;
        }

        private static string ConcatenateCommandAndAgruments(string command, IEnumerable<string> arguments, string concatenatingSymbol)
        {
            var strBuilder = new StringBuilder();

            if (command != "")
            {
                strBuilder.Append(command);
                strBuilder.Append(' ');
            }
            
            var args = arguments.ToArray();

            for (int i = 0; i < args.Length; i++)
            {
                strBuilder.Append(args[i]);

                if (i != args.Length - 1)
                {
                    strBuilder.Append(concatenatingSymbol);
                }
            }

            return strBuilder.ToString();
        }
    }
}