using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;

namespace Task_2
{
    public class CommandApplier
    {
        private BashSimulator simulator;
        private Dictionary<string, ICommand> commands;

        public CommandApplier()
        {
            simulator = new BashSimulator();
            commands = new Dictionary<string, ICommand>();
            CatCommand catCommand = new CatCommand();
            commands[catCommand.Name] = catCommand;
            WcCommand wcCommand = new WcCommand();
            commands[wcCommand.Name] = wcCommand;
            PwdCommand pwdCommand = new PwdCommand();
            commands[pwdCommand.Name] = pwdCommand;
            AnotherCommand anotherCommand = new AnotherCommand();
            commands[anotherCommand.Name] = anotherCommand;
            EchoCommand echoCommand = new EchoCommand();
            commands[echoCommand.Name] = echoCommand;
        }

        public string ReadCommands(List<string> commands=null)
        {
            string result = "";
            if (commands == null)
            {
                string command = Console.ReadLine();
                while (command != "exit")
                {
                    string commandResult = ApplyComplexCommand(command, "", true);
                    Console.WriteLine(commandResult);
                    result += commandResult;
                    command = Console.ReadLine();
                }
            }
            else
            {
                int i = 0;
                string command = commands[i];
                i++;
                while (command != "exit")
                {
                    string commandResult = ApplyComplexCommand(command, "", true);
                    Console.WriteLine(commandResult);
                    result += commandResult;
                    command = commands[i];
                    i++;
                }
            }
            return result;
        }

        public string ApplyComplexCommand(string command, string prevResult, bool firstFlag)
        {
            int i = 0;
            bool operatorFlag = false;

            while (i < command.Length)
            {
               if (command[i] == '|')
               {
                    operatorFlag = true;
                    break;
               }
               else
                    i++;
            }

            string commandResult = "";

            if (operatorFlag)
            {
                if (firstFlag)
                    commandResult = ApplySimpleCommand(command.Substring(0, i - 1));
                else
                {
                    if (command.Substring(0, i - 1) == "echo")
                    {
                        commandResult = ApplySimpleCommand(command.Substring(0, i - 1) + ' ' + '"' + prevResult + '"');
                    }
                    else
                        commandResult = ApplySimpleCommand(command.Substring(0, i - 1) + " " + prevResult);
                }
                return ApplyComplexCommand(command.Substring(i + 2), commandResult, false);
            }

            else
            {
                if (firstFlag)
                    return commandResult = ApplySimpleCommand(command.Substring(0, i));
                else
                    if (command == "echo")
                        return commandResult = ApplySimpleCommand(command + ' ' + '"' + prevResult + '"');
                    else
                        return commandResult = ApplySimpleCommand(command + ' ' + prevResult);
            }

        }

        public string ApplySimpleCommand(string command)
        {
            command = simulator.InsertVariables(command);
            int i = 0;
            string subString = "";
            string[] arguments = new string[2] { "", "" };
            while (i < command.Length && command[i] != '"')
            {
                i++;
            }
            i++;
            while (i < command.Length &&  command[i] != '"')
            {
                subString += command[i];
                i++;
            }
            if (command[0] == '$')
            {
                SetVariable(command);
                return "";
            }

            foreach (var commandPair in commands)
            {
                if (command.Length >= commandPair.Key.Length &&
                    command.Substring(0, commandPair.Key.Length) == commandPair.Key
                    && commandPair.Key != "another")
                {
                    if (commandPair.Key == "echo ")
                    {
                        arguments[0] = subString;
                    }
                    else
                    {
                        arguments[0] = @command.Substring(commandPair.Key.Length);
                    }
                    return commandPair.Value.ApplyCommand(arguments);
                }
            }

            if (i == command.Length - 1 && command[i] == '"')
            {
                arguments[0] = subString;
                return commands["another"].ApplyCommand(arguments);
            }
            else if (i >= command.Length)
            {
                return "Can't recognize the command or name of program\nPlease use quotes to set a program name";
            }
            else
            {
                arguments[0] = subString;
                arguments[1] = command.Substring(i + 2);
                return commands["another"].ApplyCommand(arguments);
            }
        }

        public void SetVariable(string command)
        {
            int i = 1;
            string variable = "";
            string value = "";

            while (command[i] != '=')
            {
                variable += command[i];
                i++;
            }

            i++;

            while (i != command.Length)
            {
                value += command[i];
                i++;
            }

            simulator.SetVariable(variable, value);
        }
    }
}
