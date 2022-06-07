using System.Text;
using Bash.Commands;

namespace Bash
{
    public class BashCore
    {
        private VariableStorage locals;
        private List<ICommand> container = IoCContainer.GetCommands().ToList();

        public BashCore()
        {
            locals = new VariableStorage();
        }

        public void Start()
        {
            new Help().Execute(new string[0]);

            while (true)
            {
                Console.Write($"{Environment.UserName}:~$ ");
                string[] result = ProcessCommands(Console.ReadLine());
                PrintResult(result);
            }
        }

        private string[] ProcessCommands(string? userInput)
        {
            List<string[]>? commands = Parser.GetCommands(userInput);
            string[] previousResult = new string[0];

            if (commands is null)
            {
                return new string[] { "Incorrect input, failed to recognize commands"};
            }

            foreach (string[] command in commands)
            {
                if (command[0][0] == '$')
                {
                    previousResult = AddVariables(command);
                }
                else
                {
                    string[] normalizedCommand = RestoreVariables(command[1..]);
                    string[]? result;
                    string[] args = normalizedCommand.Concat(previousResult).ToArray();
                    bool success;
                    (result, success) = ExecuteCommand(command[0], args);
                    if (!success)
                    {
                        args = RestoreVariables(command).Concat(previousResult).ToArray();
                        (result, success) = ExecuteCommand("np", args);
                    }

                    previousResult = result ?? new string[0];
                }
            }

            return previousResult;
        }

        private string[] RestoreVariables(string[] instructions)
        {
            List<string> result = new List<string>();
            StringBuilder builderInstruction = new StringBuilder();
            StringBuilder builderVariable = new StringBuilder();

            foreach (var item in instructions)
            {
                bool variableFlag = false;
                char previousSymb = '!';
                string instruction = item.ToString() + " ";
                foreach (var symb in instruction)
                {
                    if (symb == ' ')
                    {
                        builderInstruction.Append(locals.Get(builderVariable.ToString()));
                        builderVariable.Clear();
                        builderInstruction.Append(symb);
                        variableFlag = false;
                    }
                    else if (!variableFlag && (previousSymb == '\\' || symb != '$'))
                    {
                        if (symb == '$')
                        {
                            builderInstruction.Remove(builderInstruction.Length - 1, 1);
                        }
                        builderInstruction.Append(symb);
                    }
                    else if (variableFlag)
                    {
                        builderVariable.Append(symb);
                    }
                    else
                    {
                        variableFlag = true;
                    }

                    previousSymb = symb;
                }

                builderInstruction.Remove(builderInstruction.Length - 1, 1);
                result.Add(builderInstruction.ToString());
                builderInstruction.Clear();
                builderVariable.Clear();
            }

            return result.ToArray();
        }

        private string[] AddVariables(string[] command)
        {
            List<string> result = new List<string>();

            foreach (var instruction in command)
            {
                int nameIndexEnd = instruction.IndexOf('=');
                int valueIndex = nameIndexEnd + 1;

                if (instruction.Length < 4 || instruction[0] != '$' || Math.Abs(nameIndexEnd) == 1)
                {
                    result.Add($"Command {instruction} not found.");
                }
                else
                {
                    locals.Add(instruction[1..nameIndexEnd], instruction[valueIndex..]);
                }
            }

            return result.ToArray();
        }

        private void PrintResult(string[] result)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        private (string[]?, bool) ExecuteCommand(string nameCommand, string[] args)
        {
            foreach (var item in container)
            {
                if (nameCommand == item.Name)
                {
                    return (item.Execute(args), true);
                }
            }

            return (null, false);
        }
    }
}