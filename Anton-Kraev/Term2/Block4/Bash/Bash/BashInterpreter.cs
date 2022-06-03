using Bash.BashComponents;
using Bash.LocalVariables;
using Bash.Exceptions;
using Bash.Interfaces;

namespace Bash
{
    public class BashInterpreter
    {
        private IExecuter executer;
        private IVariableManager variableManager;
        private ILogger logger;

        public BashInterpreter()
        {
            executer = new Executer();
            variableManager = new VariableManager();
            logger = new Logger();
        }

        public void Run()
        {
            while (true)
            {
                string result = "";
                try
                {
                    result = ExecuteCommands(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType().Name + "!");
                    Console.WriteLine(ex.Message);
                }
                logger.Log(result);
            }
        }

        private string ExecuteCommands(string input)
        {
            var actions = input.Split('|');
            var previousArgs = "";
            foreach(var action in actions)
            {
                var parsed = Parse(action.Trim());
                if (parsed[0] == "") continue;
                previousArgs = executer.Execute(parsed[0], parsed[1] + previousArgs) ?? throw new BadRequestException("Unknown command");
            }
            return previousArgs;
        }
        
        private string[] Parse(string? action)
        {
            var command = "";
            var args = "";

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new BadRequestException("Empty request");
            }

            if (action.StartsWith('$'))
            {
                if (action.Count(c => c.Equals('=')) != 1)
                {
                    throw new BadRequestException("Error creating a local variable, variable assignment pattern: $var=value");
                }
                variableManager.SetVariableValue(action.Split('=')[0].Trim().Replace("$", ""), action.Split('=')[1].Trim().Replace("\"", ""));
                return new string[] { command, args };
            }

            command = action.Split()[0];
            if (!char.IsLetter(command[0]))
            {
                throw new BadRequestException("Invalid name, the command must start with a letter");
            }  

            bool inQuotes = false;
            foreach (var arg in action.Split()[1..])
            {
                if (inQuotes)
                {
                    if (!arg.EndsWith("\""))
                    {
                        args += arg;
                    }
                    else
                    {
                        args += arg.Replace("\"", "");
                        inQuotes = !inQuotes;
                    }
                }
                else
                {
                    if (arg.StartsWith("\"") && arg.EndsWith("\""))
                    {
                        if (arg.Length != 1)
                            args += arg.Replace("\"", "");
                    }
                    else if (arg.StartsWith("\""))
                    {
                        args += arg.Replace("\"", "");
                        inQuotes = !inQuotes;
                    }
                    else if (arg.StartsWith("$"))
                    {
                        args += variableManager.GetVariableValue(arg.Replace("$", ""));
                    }
                    else
                    {
                        args += arg;
                    }
                }
                args += " ";
            }
            args.TrimEnd();

            if (inQuotes)
            {
                throw new BadRequestException("Missing closing quotation mark");
            }

            return new string[] { command, args };
        }

    }
}