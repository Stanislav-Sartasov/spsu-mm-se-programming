using Enumerations;

namespace Task_1
{
    public class Analyzer
    {
        private List<(string str, TypeOfParsedStr type)> data;
        private List<Command> listOfCommands;
        private Queue<(string name, string value)> localVar;
        private Queue<string> arguments;
        public bool IsAnalyzed { get; private set; }
        public string ErrorMessenge { get; private set; }

        public Analyzer(List<(string str, TypeOfParsedStr type)> data)
        {
            this.data = data;
            IsAnalyzed = false;
            listOfCommands = new List<Command>();
            localVar = new Queue<(string name, string value)>();
            arguments = new Queue<string>();
            ErrorMessenge = "Analyzer was not called";
        }

        public void Analyze()
        {
            var listOfCmds = new List<Command>();
            var localV = new Queue<(string name, string value)>();
            var lastIsPipeline = true;
            var lastIsCommand = false;
            IsAnalyzed = true;

            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].type == TypeOfParsedStr.Argument)
                {
                    if (!lastIsCommand || lastIsPipeline)
                    {
                        IsAnalyzed = false;
                        ErrorMessenge = data[i].str + ": command not found";
                        break;
                    }

                    else
                    {
                        arguments.Enqueue(data[i].str);
                        var lastCommand = listOfCmds.Last();
                        listOfCmds = listOfCmds.SkipLast(1).ToList();
                        listOfCmds.Add(Command.SetArgument);
                        listOfCmds.Add(lastCommand);
                        lastIsCommand = false;
                        lastIsPipeline = false;
                    }
                }

                else if (data[i].type == TypeOfParsedStr.PipeLine)
                {
                    if (lastIsPipeline)
                    {
                        IsAnalyzed = false;
                        ErrorMessenge = "Two | in a row";
                        break;
                    }

                    else
                    {
                        listOfCmds.Add(Command.Pipeline);
                        lastIsPipeline = true;
                        lastIsCommand = false;
                    }
                }

                else if (data[i].type == TypeOfParsedStr.DeclaringLocalVariable)
                {
                    if (!lastIsPipeline || lastIsCommand)
                    {
                        IsAnalyzed = false;
                        ErrorMessenge = "Two commands in a row";
                        break;
                    }

                    else
                    {
                        lastIsPipeline = false;
                        lastIsCommand = true;
                        var command = data[i].str.Split('=');
                        listOfCmds.Add(Command.SetLocalVar);
                        localV.Enqueue((command.First(), command.Last()));
                    }
                }

                else if (data[i].type == TypeOfParsedStr.Command)
                {
                    if (!lastIsPipeline)
                    {
                        IsAnalyzed = false;
                        ErrorMessenge = "Two commands in a row";
                        break;
                    }

                    else
                    {
                        lastIsPipeline= false;
                        lastIsCommand= true;
                        listOfCmds.Add(GetCommand(data[i].str));
                    }
                }
            }

            if (IsAnalyzed)
            {
                listOfCommands = listOfCmds;
                localVar = localV;
            }
        }

        public Command GetCommand(string command)
        {
            switch (command)
            {
                case "exit":
                    return Command.Exit;
                case "echo":
                    return Command.Echo;
                case "pwd":
                    return Command.Pwd;
                case "cat":
                    return Command.Cat;
                case "cd":
                    return Command.Cd;
                case "wc":
                    return Command.Wc;
                case "whoami":
                    return Command.Whoami;
                case "|":
                    return Command.Pipeline;
                case "$":
                    return Command.SetLocalVar;
                case "clear":
                    return Command.Clear;
                default:
                    return Command.Exit;
            }

        }

        public List<Command> GetCommands()
        {
            return new List<Command>(listOfCommands);
        }

        public Queue<(string name, string value)> GetArgumentsForSetLocalVar()
        {
            return new Queue<(string name, string value)>(localVar);
        }

        public Queue<string> GetArguments()
        {
            return new Queue<string>(arguments);
        }
    }
}
