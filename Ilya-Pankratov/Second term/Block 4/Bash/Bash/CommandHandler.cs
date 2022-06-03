using System.Text;
using BashCommands;

namespace Bash
{
    public class CommandHandler
    {
        private VariableManager variableManager;
        private List<ICommand> availableCommands;
        private CommandManager commmandManager;

        public CommandHandler()
        {
            commmandManager = new CommandManager();
            availableCommands = commmandManager.GetCommands().ToList();
            variableManager = new VariableManager();
        }

        public IEnumerable<string> Execute(IEnumerable<string> userInput)
        {
            var commands = CreateCommandSequence(userInput);
            IEnumerable<string>? commandResult = null;

            foreach (var command in commands)
            {
                if (commandResult != null)
                {
                    command.AddArguments(commandResult.ToList());
                }

                if (command.ShortName.StartsWith('$'))
                {
                    var assignmentCoomand = command.ShortName.Split('=');

                    if (assignmentCoomand.Length == 2)
                    {
                        variableManager.AddVariable(assignmentCoomand[0], assignmentCoomand[1]);
                    }
                    else
                    {
                        commandResult = null;
                    }
                }
                else if (command.ShortName.StartsWith('&'))
                {
                    commmandManager.AddCommandsFromAssembly(command.ShortName[1..]);
                    availableCommands.AddRange(commmandManager.GetCommands());
                }
                else if (command.ShortName == String.Empty)
                {
                    return new List<string> { String.Empty };
                }
                else
                {
                    commandResult = ExecuteSingleCommand(command);
                }
            }

            if (commandResult == null || !commandResult.Any())
            {
                return new List<string>() { String.Empty };
            }

            return commandResult;
        }       

        private IEnumerable<string>? ExecuteSingleCommand(CommandInfo command)
        {
            var invokedCommand = availableCommands.Find((x) => x.ShortName == command.ShortName);

            if (invokedCommand == default(ICommand))
            {
                invokedCommand = availableCommands.Find(x => x.ShortName == "defcmd");
            }

            if (invokedCommand == default(ICommand))
            {
                return new List<string>() { "Unpredictable exception" };
            }
            else if (invokedCommand.ShortName == "defcmd")
            {
                command.InsertArgumentFirst(command.ShortName);
            }

            return invokedCommand.Execute(command.Arguments);
        }

        private List<CommandInfo> CreateCommandSequence(IEnumerable<string> userInput)
        {
            var isCommandEnded = true;
            var commands = new List<CommandInfo>();
            var commandArguments = new List<string>();
            var commandName = String.Empty;

            foreach (var part in userInput)
            {
                if (part == "|")
                {
                    isCommandEnded = true;

                    if (commandArguments.Count == 0)
                    {
                        commands.Add(new CommandInfo(commandName, null));
                    }
                    else
                    {
                        commands.Add(new CommandInfo(commandName, commandArguments));
                    }

                    isCommandEnded = true;
                    commandArguments.Clear();
                    commandName = String.Empty;
                }
                else if (isCommandEnded)
                {
                    commandName = part;
                    isCommandEnded = false;
                }
                else
                {
                    var argument = part;

                    if (part.Contains('$'))
                    {
                        argument = AnalizeArgument(part);
                    }

                    commandArguments.Add(argument);
                }
            }

            if (commandArguments.Count == 0)
            {
                commands.Add(new CommandInfo(commandName, null));
            }
            else
            {
                commands.Add(new CommandInfo(commandName, commandArguments));
            }

            return commands;
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
                return variableManager.GetVariable(argument);
            }

            return argument;
        }
    }
}
