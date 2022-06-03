using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class CommandApplier
    {
        private BashSimulator simulator;
        private DirectoryHelper dirHelper;
        private FileHelper fileHelper;

        public CommandApplier()
        {
            simulator = new BashSimulator();
            dirHelper = new DirectoryHelper();
            fileHelper = new FileHelper();
        }

        public string ReadCommands(List<string> commands=null)
        {
            string result = "";
            if (commands == null)
            {
                string command = Console.ReadLine();
                while (command != "exit")
                {
                    command = simulator.InsertVariables(command);
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
                    command = simulator.InsertVariables(command);
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
            int i = 0;
            string subString = "";
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

            else if (command.Length >= 3 && command.Substring(0) == "pwd")
            {
                return dirHelper.PrintDirectoryInfo();
            }

            else if (command.Length >= 5 && command.Substring(0, 5) == "echo ")
            {
                return simulator.EchoCommand(subString);
            }

            else if (command.Length >= 4 && command.Substring(0, 4) == "cat ")
            {
                return fileHelper.CatCommand(@command.Substring(4));
            }

            else if (command.Length >= 3 && command.Substring(0, 3) == "wc ")
            {
                return fileHelper.WcCommand(@command.Substring(3));
            }

            else
            {
                if (i == command.Length - 1 && command[i] == '"')
                {
                    return fileHelper.OtherCommand(subString);
                }
                else if (i >= command.Length)
                {
                    return "Can't recognize the command or name of program\nPlease use quotes to set a program name";
                }
                else
                {
                    return fileHelper.OtherCommand(subString, command.Substring(i + 2));
                }
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
