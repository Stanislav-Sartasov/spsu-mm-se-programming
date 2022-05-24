using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Bash
{
    public class BashEmulator
    {
        private string currentPath;
        private readonly string homeDirectory;
        private readonly Dictionary<string, string> localVariables;

        public BashEmulator()
        {
            var splitedPath = Directory.GetCurrentDirectory().Split("\\");
            var builder = new StringBuilder();

            if (splitedPath[^3] == "bin")
            {
                for (int i = 0; i < splitedPath.Length - 4; i++)
                {
                    builder.Append(splitedPath[i]);
                    if (i != splitedPath.Length - 5)
                    {
                        builder.Append('\\');
                    }
                }

                homeDirectory = builder.ToString();
            }
            else
            {
                homeDirectory = Directory.GetCurrentDirectory();
            }

            currentPath = homeDirectory;
            localVariables = new Dictionary<string, string>();
            localVariables.Add("HOME", homeDirectory);
        }

        public string Execute(string? userInput)
        {
            var oneCommand = new List<string>();
            var previousCommandResultFlag = false;
            string commandResult = String.Empty;
            string[] parsedInput;

            if (userInput == null || userInput.Length == 0)
            {
                parsedInput = new string[] { "" };
            }
            else
            {
                parsedInput = CommandParser.Parse(userInput);
            }

            for(int i = 0; i < parsedInput.Length; i++)
            {
                if (parsedInput[i] != "|")
                {
                    oneCommand.Add(parsedInput[i]);
                }
                else
                {
                    if (previousCommandResultFlag)
                    {
                        oneCommand.Add(commandResult);
                    }
                    else
                    {
                        previousCommandResultFlag = true;
                    }

                    commandResult = ExecuteCommand(oneCommand.ToArray()).Trim('\n');
                    oneCommand.Clear();
                }
            }

            if (previousCommandResultFlag)
            {
                oneCommand.Add(commandResult);
            }

            commandResult = ExecuteCommand(oneCommand.ToArray());

            return commandResult;
        }

        private string ExecuteCommand(string[] command)
        {
            if (command[0].Length > 0 && command[0][0] == '$')
            {
                return AddLocalVariable(command);
            }
            else if (Regex.IsMatch(command[0], ".:"))
            {
                return ChangeDisk(command);
            }

            string commandResult;

            switch (command[0])
            {
                case "cd":
                {
                    commandResult = ChangeCurrentDirectory(command);
                    break;
                }
                case "ls":
                {
                    commandResult = ListFiles(command);
                    break;
                }
                case "pwd":
                {
                    commandResult = PrintWorkingDirectory(command);
                    break;
                }
                case "cat":
                {
                    commandResult = Concatenate(command);
                    break;
                }
                case "echo":
                {
                    commandResult = Echo(command);
                    break;
                }
                case "wc":
                {
                    commandResult = WordCount(command);
                    break;
                }
                case "exit":
                {
                    commandResult = Exit(command);
                    break;
                }
                case "":
                {
                    commandResult = command[0];
                    break;
                }
                default:
                {
                    commandResult = StartSystemProcess(command);
                    break;
                }
            }

            return commandResult;
        }

        private static string StartSystemProcess(string[] command)
        {
            var processResult = String.Empty;

            var process = new Process();
            process.StartInfo.FileName = command[0];
            
            foreach (var arg in command[1..])
            {
                process.StartInfo.ArgumentList.Add(arg);
            }

            try
            {
                process.Start();
            }
            catch(Exception e)
            {
                processResult = e.Message;
            }

            process.Dispose();
            return processResult + "\n\n";
        }

        private string ChangeCurrentDirectory(string[] directory)
        {
            if (directory.Length != 2)
            {
                return "Invalid arguments\n\n";
            }

            if (directory[1] == "..")
            {
                currentPath = ReturnToPreviousDirectory();
            }
            else
            {
                directory[1] = AnalizeArgument(directory[1]);
                var newCurrentPath = GoToNextDirectory(directory[1]);

                if (directory[1] == "~" || directory[1] == homeDirectory)
                {
                    currentPath = homeDirectory;
                }
                else if (currentPath == newCurrentPath)
                {
                    return "Such directory does not exist\n\n";
                }
                else
                {
                    currentPath = newCurrentPath;
                }
            }

            return String.Empty;
        }

        private string ChangeDisk(string[] command)
        {
            if (command.Length != 1)
            {
                return "Invalid arguments\n\n";
            }

            var newPath = command[0] + '\\';

            if (Directory.Exists(newPath))
            {
                currentPath = newPath;
            }
            else
            {
                return "Such disk does not exist\n\n";
            }
            
            return String.Empty;
        }

        private string Concatenate(string[] fileNames)
        {
            var result = new StringBuilder();

            for (int i = 1; i < fileNames.Length; i++)
            {
                if (i != 1)
                {
                    result.Append('\n');
                }

                fileNames[i] = AnalizeArgument(fileNames[i]);
                var filePath = currentPath + '\\' + fileNames[i];

                if (!File.Exists(filePath))
                {
                    result.Append($"The file {fileNames[i]} does not exist");
                    result.Append('\n');
                }
                else
                {
                    result.Append($"filename: {fileNames[i]}");
                    result.Append('\n');

                    foreach (var str in File.ReadAllLines(filePath))
                    {
                        result.Append(str);
                        result.Append('\n');
                    }
                }
            }

            result.Append('\n');

            return result.ToString();
        }

        private string Echo(string[] printedText)
        {
            var builder = new StringBuilder();

            for (int i = 1; i < printedText.Length; i++)
            {
                builder.Append(AnalizeArgument(printedText[i]));

                if (i != printedText.Length - 1)
                {
                    builder.Append('\n');
                }
            }

            if (builder.Length != 0 && builder[^2] != '\n' && builder[^1] != '\n')
            {
                builder.Append("\n\n");
            }
            else if (builder[^1] != '\n')
            {
                builder.Append('\n');
            }

            return builder.ToString();
        }

        private string ListFiles(string[] command)
        {
            if (command.Length != 1)
            {
                return "Invalid arguments\n\n";
            }

            var result = new StringBuilder();

            foreach (var entity in Directory.EnumerateDirectories(currentPath))
            {
                result.Append(entity.Split("\\").Last());
                result.Append('\t');
            }

            foreach (var entity in Directory.EnumerateFiles(currentPath))
            {
                result.Append(entity.Split("\\").Last());
                result.Append('\t');
            }

            result.Append("\n\n");

            return result.ToString();
        }

        private string PrintWorkingDirectory(string[] command)
        {
            if (command.Length != 1)
            {
                return "Invalid arguments\n\n";
            }

            return currentPath + "\n\n";
        }

        private string WordCount(string[] command)
        {
            var result = new StringBuilder();

            for (int i = 1; i < command.Length; i++)
            {
                if (i != 1)
                {
                    result.Append("\n\n");
                }

                command[i] = AnalizeArgument(command[i]);
                var filePath = currentPath + '\\' + command[i];

                if (!File.Exists(filePath))
                {
                    result.Append($"The file {command[i]} does not exist\n\n");
                }
                else
                {
                    var reader = new StreamReader(filePath);

                    int wordCouner = 0;
                    int lineCounter = 0;
                    var byteCounter = new FileInfo(filePath).Length.ToString();

                    while (!reader.EndOfStream)
                    {
                        lineCounter++;
                        wordCouner += reader.ReadLine().Split(" ").Count(x => x != " ");
                    }

                    reader.Dispose();
                    result.Append($"filename: {command[i]}\nlines {lineCounter}\nwords {wordCouner}\nbytes {byteCounter}\n");

                    if (command.Length == 2)
                    {
                        result.Append('\n');
                    }

                }
            }

            return result.ToString();
        }

        private string AddLocalVariable(string[] command)
        {
            string commandResult;

            if (command.Length != 1 || !command[0].Contains('='))
            {
                commandResult = "Invalid command\n";
            }
            else
            {
                var parsedAssignment = command[0].Split('=');

                if (localVariables.ContainsKey(parsedAssignment[0][1..]))
                {
                    localVariables[parsedAssignment[0][1..]] = parsedAssignment[1];
                }
                else
                {
                    localVariables.Add(parsedAssignment[0][1..], parsedAssignment[1]);
                }

                commandResult = String.Empty;
            }

            return commandResult;
        }

        private string GetLocalVariable(string variableName)
        {
            if (localVariables.ContainsKey(variableName))
            {
                return localVariables[variableName];
            }
            else
            {
                return String.Empty;
            }
        }

        private static string Exit(string[] command)
        {
            if (command.Length != 1)
            {
                return "Invalid arguments\n\n";
            }

            return "exit";
        }

        private string ReturnToPreviousDirectory()
        {
            var newPath = new StringBuilder();
            var path = currentPath.Split('\\');

            for (int i = 0; i < path.Length - 1; i++)
            {
                newPath.Append(path[i]);

                if (i != path.Length - 2 || path[i][path[i].Length - 1] == ':')
                {
                    newPath.Append('\\');
                }

            }

            return newPath.ToString();
        }

        private string GoToNextDirectory(string directoryName)
        {
            string newCurrentPath = currentPath + '\\' + directoryName;

            if (Directory.Exists(newCurrentPath))
            {
                return newCurrentPath;
            }

            return currentPath;
        }

        private string AnalizeArgument(string argument)
        {
            var builder = new StringBuilder();
            var splitedAtgument = argument.Split(" ");

            for (int i = 0; i < splitedAtgument.Length; i++)
            {
                builder.Append(ProcessArgument(splitedAtgument[i]));

                if (i != splitedAtgument.Length - 1)
                {
                    builder.Append(' ');
                }
            }

            return builder.ToString();
        }

        private string ProcessArgument(string argument)
        {
            if (argument.IndexOf('$') == 0)
            {
                return GetLocalVariable(argument[1..]);
            }

            return argument;
        }
    }
}