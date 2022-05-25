using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Bash
{
    public class BashEmulator
    {
        private string exitCode;
        private string currentPath;
        private readonly string homeDirectory;
        private readonly LocalVariablesStorage storage;

        public BashEmulator(string exitCode)
        {
            currentPath = homeDirectory = GetHomeDiretory();
            storage = new LocalVariablesStorage();
            this.exitCode = exitCode;
            storage.Add("HOME", homeDirectory, true);
        }

        public List<string> Execute(string? userInput)
        {
            var oneCommand = new List<string>();
            var previousCommandResultFlag = false;
            List<string>? commandResult = null;
            string[] parsedInput;

            if (userInput == null || userInput.Length == 0)
            {
                return new List<string> { String.Empty };
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
                    if (previousCommandResultFlag && commandResult != null)
                    {
                        oneCommand.AddRange(commandResult);
                    }
                    else
                    {
                        previousCommandResultFlag = true;
                    }

                    commandResult = ExecuteCommand(oneCommand.ToArray());
                    oneCommand.Clear();
                }
            }

            if (previousCommandResultFlag && commandResult != null)
            {
                oneCommand.AddRange(commandResult);
            }

            commandResult = ExecuteCommand(oneCommand.ToArray());
            return commandResult;
        }

        private List<string> ExecuteCommand(string[] command)
        {
            if (command[0].Length > 0 && command[0][0] == '$')
            {
                return AddLocalVariable(command);
            }
            else if (command[0].Length == 2 && Regex.IsMatch(command[0], ".:"))
            {
                return ChangeDisk(command);
            }

            List<string> commandResult;

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
                    commandResult = new List<string>() { String.Empty };
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

        private static List<string> StartSystemProcess(string[] command)
        {
            string processResult;

            using (var process = new Process())
            {
                process.StartInfo.FileName = command[0];
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                foreach (var arg in command[1..])
                {
                    process.StartInfo.ArgumentList.Add(arg);
                }

                try
                {
                    process.Start();
                    processResult = process.StandardOutput.ReadToEnd();
                }
                catch (Exception)
                {
                    processResult = $"Command {command[0]} was not found";
                }
            }
               
            return new List<string>() { processResult };
        }

        private List<string> ChangeCurrentDirectory(string[] directory)
        {
            if (directory.Length != 2)
            {
                return new List<string>() { "Invalid arguments" };
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
                    return new List<string>() { "Such directory does not exist" };
                }
                else
                {
                    currentPath = newCurrentPath;
                }
            }

            return new List<string>() { String.Empty };
        }

        private List<string> ChangeDisk(string[] command)
        {
            if (command.Length != 1)
            {
                return new List<string>() { "Invalid arguments" };
            }

            var newPath = command[0] + '\\';

            if (Directory.Exists(newPath))
            {
                currentPath = newPath;
            }
            else
            {
                return new List<string>() { "Such disk does not exist" };
            }
            
            return new List<string>() { String.Empty };
        }

        private List<string> Concatenate(string[] fileNames)
        {
            var oneArgumentResult = new StringBuilder();
            var result = new List<string>();

            for (int i = 1; i < fileNames.Length; i++)
            {
                fileNames[i] = AnalizeArgument(fileNames[i]);
                var filePath = currentPath + '\\' + fileNames[i];

                if (!File.Exists(filePath))
                {
                    oneArgumentResult.Append($"The file {fileNames[i]} does not exist");
                }
                else
                {
                    oneArgumentResult.Append($"filename: {fileNames[i]}");
                    oneArgumentResult.Append('\n');
                    var lines = File.ReadAllLines(filePath);

                    for (int j = 0; j < lines.Length; j++)
                    {
                        oneArgumentResult.Append(lines[j]);

                        if (j != lines.Length - 1)
                        {
                            oneArgumentResult.Append('\n');
                        }
                    }
                }

                result.Add(oneArgumentResult.ToString());
                oneArgumentResult.Clear();
            }

            return result;
        }

        private List<string> Echo(string[] printedText)
        {
            var result = new List<string>();

            for (int i = 1; i < printedText.Length; i++)
            {
                result.Add(AnalizeArgument(printedText[i]));
            }

            return result;
        }

        private List<string> ListFiles(string[] command)
        {
            if (command.Length != 1)
            {
                return new List<string>() { "Invalid arguments" };
            }

            var oneArgumentResult = new StringBuilder();

            foreach (var entity in Directory.EnumerateDirectories(currentPath))
            {
                oneArgumentResult.Append(entity.Split("\\").Last());
                oneArgumentResult.Append('\t');
            }

            foreach (var entity in Directory.EnumerateFiles(currentPath))
            {
                oneArgumentResult.Append(entity.Split("\\").Last());
                oneArgumentResult.Append('\t');
            }

            return new List<string>() { oneArgumentResult.ToString() };
        }

        private List<string> PrintWorkingDirectory(string[] command)
        {
            if (command.Length != 1)
            {
                return  new List<string>() { "Invalid arguments" };
            }

            return new List<string>() { currentPath };
        }

        private List<string> WordCount(string[] command)
        {
            if (command.Length == 1)
            {
                return new List<string>() { "Invalid arguments" };
            }

            var oneArgumentResult = new StringBuilder();
            var result = new List<string>();

            for (int i = 1; i < command.Length; i++)
            {
                command[i] = AnalizeArgument(command[i]);
                var filePath = currentPath + '\\' + command[i];

                if (!File.Exists(filePath))
                {
                    oneArgumentResult.Append($"The file {command[i]} does not exist");
                }
                else
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        int wordCouner = 0;
                        int lineCounter = 0;
                        var byteCounter = new FileInfo(filePath).Length.ToString();

                        while (!reader.EndOfStream)
                        {
                            lineCounter++;
                            wordCouner += reader.ReadLine().Split(" ").Count(x => x != " " && x != "" && x != "\n" && x != "\r" && x != "\t");
                        }

                        oneArgumentResult.Append($"filename: {command[i]}\nlines {lineCounter}\nwords {wordCouner}\nbytes {byteCounter}");
                    }
                    
                }

                result.Add(oneArgumentResult.ToString());
                oneArgumentResult.Clear();
            }

            return result;
        }

        private List<string> Exit(string[] command)
        {
            if (command.Length != 1)
            {
                return new List<string>() { "Invalid arguments" };
            }

            return new List<string>() { exitCode };
        }

        private List<string> AddLocalVariable(string[] command)
        {
            List<string> commandResult;

            if (command.Length != 1 || !command[0].Contains('='))
            {
                commandResult = new List<string>() {"Invalid command"};
            }
            else
            {
                var parsedAssignment = command[0].Split('=');
                storage.Add(parsedAssignment[0][1..], parsedAssignment[1], false);
                commandResult = new List<string>() { String.Empty };
            }

            return commandResult;
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
                return storage.Get(argument[1..]);
            }

            return argument;
        }

        private static string GetHomeDiretory()
        {
            var splitedPath = Directory.GetCurrentDirectory().Split("\\");
            var builder = new StringBuilder();
            string homeDirectory;

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

            return homeDirectory;
        }
    }
}