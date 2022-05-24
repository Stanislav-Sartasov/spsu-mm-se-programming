using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class BashEmulatorTests
    {
        private readonly string homeDir = GetHomeDir();

        [Test]
        public void PrintWorkingDirectoryTest()
        {
            var bash = new BashEmulator();
            var testCommand = "pwd";
            var commandResult = bash.Execute(testCommand).Trim('\n');
            var expectedResults = homeDir;

            Assert.AreEqual(expectedResults, commandResult);
        }

        [Test]
        public void ChangeDirectoryTest()
        {
            // first case - go to root directory
            var bash = new BashEmulator();
            var testCommand = "cd";
            var argument = "..";

            var commandResult = bash.Execute(testCommand + ' ' + argument).Trim('\n');
            Assert.AreEqual(commandResult, String.Empty);

            commandResult = bash.Execute("pwd").Trim('\n');
            var splittedHomeDir = homeDir.Split('\\')[..^1];
            string expectedResult = UnitedPath(splittedHomeDir);

            Assert.AreEqual(expectedResult, commandResult);

            // second case - go back to previous directory
            argument = homeDir.Split('\\')[^1];

            commandResult = bash.Execute(testCommand + ' ' + argument).Trim('\n');
            Assert.AreEqual(commandResult, String.Empty);

            commandResult = bash.Execute("pwd").Trim('\n');
            expectedResult += $"\\{argument}";

            Assert.AreEqual(expectedResult, commandResult);
        }

        [Test]
        public void ListFilesTest()
        {
            var bash = new BashEmulator();
            var testCommand = "ls";

            var commandResult = bash.Execute(testCommand).Trim('\n');

            foreach (var entry in Directory.GetFileSystemEntries(homeDir))
            {
                Assert.IsTrue(commandResult.Split('\t').Contains(entry.Split("\\")[^1]));
            }
        }

        [Test]
        public void ExitTest()
        {
            var bash = new BashEmulator();
            var testCommand = "exit";

            var commandResult = bash.Execute(testCommand);
            var expectedResult = "exit";

            Assert.AreEqual(expectedResult, commandResult);
        }

        [Test]
        public void EchoTest()
        {
            // single argument test
            var bash = new BashEmulator();
            var testCommand = "echo";
            var arguments = new List<string>()
            {
                "firstArg"
            };

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            var commandResult = bash.Execute(command).Trim('\n');
            var expectedResult = arguments[0];
            Assert.AreEqual(expectedResult, commandResult);

            // multi argument test
            arguments = new List<string>()
            {
                "firstArg", "secondArg", "ThirdArg"
            };

            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command).Trim('\n');
            expectedResult = ConcatenateCommandAndAgruments("", arguments, "\n");

            Assert.AreEqual(expectedResult, commandResult);
        }

        [Test]
        public void ConcatenateTest()
        {
            // single argument test
            var bash = new BashEmulator();
            var testCommand = "cat";
            var arguments = new List<string>()
            {
                "FirstTestFile.txt"
            };

            var commandResult = bash.Execute("cd TestFiles");
            Assert.AreEqual(commandResult, String.Empty);

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command).Trim('\n');

            foreach (var line in File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt"))
            {
                Assert.IsTrue(commandResult.Contains(line));
            }

            // multi argument test
            arguments.Add("SecondTestFile.txt");
            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command).Trim('\n');

            foreach (var line in File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt"))
            {
                Assert.IsTrue(commandResult.Contains(line));
            }

            foreach (var line in File.ReadAllLines(homeDir + @"\TestFiles\SecondTestFile.txt"))
            {
                Assert.IsTrue(commandResult.Contains(line));
            }
        }

        [Test]
        public void WordCountTest()
        {
            // single argument test
            var bash = new BashEmulator();
            var testCommand = "wc";
            var arguments = new List<string>()
            {
                "FirstTestFile.txt"
            };

            var commandResult = bash.Execute("cd TestFiles");
            Assert.AreEqual(commandResult, String.Empty);

            var command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command).Trim('\n');
            var allLinesFromFirstFile = File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt");

            foreach (var line in commandResult.Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line));
            }

            // multi argument test
            arguments.Add("SecondTestFile.txt");
            command = ConcatenateCommandAndAgruments(testCommand, arguments, " ");
            commandResult = bash.Execute(command).Trim('\n');
            var allLinesFromSecondFile = File.ReadAllLines(homeDir + @"\TestFiles\SecondTestFile.txt");

            foreach (var line in commandResult.Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line) || allLinesFromSecondFile.Contains(line));
            }
        }

        [Test]
        public void LocalVariablesTest()
        {
            var bash = new BashEmulator();
            var varName = "myVar";
            var varValue = "testValue";
            var testCommand = $"${varName}={varValue}";

            Assert.AreEqual(String.Empty, bash.Execute(testCommand));
            Assert.AreEqual(varValue, bash.Execute($"echo ${varName}").Trim('\n'));
        }

        [Test]
        public void EchoPlusLocalVariableTest()
        {
            var bash = new BashEmulator();
            var firstVarName = "firstVar";
            var firstVarValue = "firstValue";
            var secondVarName = "secondVar";
            var secondVarValue = "secondValue";
            var firstTestCommand = $"${firstVarName}={firstVarValue}";
            var secondTestCommand = $"${secondVarName}={secondVarValue}";

            Assert.AreEqual(String.Empty, bash.Execute(firstTestCommand));
            Assert.AreEqual(String.Empty, bash.Execute(secondTestCommand));

            var expecteResult = "firstValue first case\nsecondValue";
            var testCommand = $"echo \"${firstVarName} first case\" \"{secondVarValue}\"";
            Assert.AreEqual(expecteResult, bash.Execute(testCommand).Trim('\n'));
        }

        [Test]
        public void ChangeDiskTest()
        {
            var bash = new BashEmulator();
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
                var expectedResult = "Such disk does not exist";
                Assert.AreEqual(expectedResult, bash.Execute(newDrive).Trim('\n'));
                return;
            }

            Assert.AreEqual(String.Empty, bash.Execute(newDrive));
            Assert.AreEqual(newDrive + '\\', bash.Execute("pwd").Trim('\n'));
        }

        [Test]
        public void PipelineTest()
        {
            // first case
            var bash = new BashEmulator();
            var commandResult = bash.Execute("cd TestFiles");
            Assert.AreEqual(String.Empty, commandResult);

            var varName = "myVar";
            var varValue = "FirstTestFile.txt";

            var command = $"${varName}={varValue}";
            commandResult = bash.Execute(command).Trim('\n');
            Assert.AreEqual(String.Empty, commandResult);

            command = $"echo ${varName} | cat";
            commandResult = bash.Execute(command).Trim('\n');
            var allLinesFromFirstFile = File.ReadAllLines(homeDir + @"\TestFiles\FirstTestFile.txt");

            foreach (var line in commandResult.Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line) || line == $"The file {varValue}");
            }

            // second case
            command = $"wc {varValue} | echo";
            commandResult = bash.Execute(command);

            foreach (var line in commandResult.Split('\n'))
            {
                Assert.IsTrue(allLinesFromFirstFile.Contains(line));
            }
        }

        [Test]
        public void InvalidBashCommand()
        {
            var bash = new BashEmulator();
            var lines = File.ReadAllLines(homeDir + @"\TestFiles\ExceptionTests.txt");

            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                var commandResult = bash.Execute(lines[i]).Trim('\n');
                Assert.AreEqual(lines[i + 1], commandResult);
            }
        }

        [Test]
        public void InvalidsystemCommand()
        {
            var bash = new BashEmulator();
            var command = "doNothing firstUselessArgumet secondUselessArgument";
            var commandResult = bash.Execute(command).Trim('\n');
            var dirPath = homeDir + "\\BashUnitTests\\bin\\Debug";

            if (Directory.Exists(dirPath))
            {
                var flag = true;

                foreach (var dir in Directory.GetDirectories(dirPath))
                {
                    if (Regex.IsMatch(dir, "net"))
                    {
                        dirPath = dir;
                        flag = false;
                    }
                }

                if (flag)
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

            var expectedResult = "An error occurred trying to start process 'doNothing' with working directory " +
                                 $"'{dirPath}'. The system cannot find the file specified.";

            Assert.AreEqual(expectedResult, commandResult);
        }

        [Test]
        public void EmpetyUserInputTest()
        {
            var bash = new BashEmulator();
            Assert.AreEqual(String.Empty, bash.Execute(String.Empty).Trim('\n'));
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