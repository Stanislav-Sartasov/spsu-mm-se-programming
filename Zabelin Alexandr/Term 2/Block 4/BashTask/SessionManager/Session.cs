using System.Diagnostics;
using Commands;
using VarManager;

namespace SessionManager
{
    public class Session
    {
        private SessionVars localVars = new SessionVars();
        private ICommand Cat = new Cat();
        private ICommand Echo = new Echo();
        private ICommand Pwd = new Pwd();
        private ICommand Wc = new Wc();
        private ICommand SystemProcess = new SystemProcess();
        private ICommand Exit = new Exit();


        public void Start()
        {
            while (true)
            {
                string? commandLine = this.ReadCommand();

                if (commandLine == null)
                {
                    continue;
                }

                if (commandLine.Contains('|'))
                {
                    this.ProcessPipelineCommands(commandLine);
                }
                else
                {
                    this.ProcessUsualCommands(commandLine);
                }
            }
        }

        private int ProcessPipelineCommands(string commandLine)
        {
            if (this.GetVarAssignment(commandLine) != null)
            {
                Console.WriteLine("Var assignment is not available in pipeline mode");

                return 0;
            }

            commandLine = this.localVars.GetInterpolatedString(commandLine);

            string[] commands = commandLine.Split(" | ");
            string[] startCommandWithArgs = commands[0].Split(" ");
            string commandName = startCommandWithArgs[0];
            ICommand command = this.GetCommand(commandName);
            string[] commandArgs =  startCommandWithArgs.Length == 2 && command.RequiredArgs ? new string[] { startCommandWithArgs[1] } : new string[1] {""};
            
            try
            {
                for (int i = 0; i < commands.Length - 1; i++)
                {
                    if (command.IsSystem)
                    {
                        commandArgs = new string[] { commandName, commandArgs[0] };
                    }

                    commandArgs = command.Run(commandArgs);
                    command = this.GetCommand(commands[i + 1]);
                }

                if (command.IsSystem)
                {
                    commandArgs = new string[] { commandName, commandArgs[0] };
                }

                if (command.NeedToBePrinted)
                {
                    this.Echo.Run(command.Run(commandArgs));
                } 
                else
                {
                    command.Run(commandArgs);
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong. Please try again");
            }

            return 0;
        }

        private int ProcessUsualCommands(string commandLine)
        {
            IVar? var = this.GetVarAssignment(commandLine);

            if (var != null)
            {
                this.localVars.SetValue(var.Key, var.Value);

                return 0;
            }

            commandLine = commandLine.Replace("\"", String.Empty);
            commandLine = this.localVars.GetInterpolatedString(commandLine);

            string commandName = commandLine.Split(' ')[0];
            ICommand command = this.GetCommand(commandName);
            string[] args = command.RequiredArgs && commandName.Length + 1 < commandLine.Length ? new string[] { commandLine.Substring(commandName.Length + 1) } : new string[] {""};

            if (command.IsSystem)
            {
                args = new string[] { commandName, args[0] };
            }

            try
            {
                if (command.NeedToBePrinted)
                {
                    this.Echo.Run(command.Run(args));
                }
                else
                {
                    command.Run(args);
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong. Please try again");
            }

            return 0;
        }

        private IVar? GetVarAssignment(string commandLine)
        {
            try
            {
                string[] commands = commandLine.Split(' ');

                foreach (string command in commands)
                {
                    if (command.Contains('=') && command[0] == '$')
                    {
                        string[] varInfo = command.Replace("$", String.Empty).Split('=');
                        IVar var = new Var(varInfo[0], varInfo[1]);

                        return var;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }

            
        }

        private string? ReadCommand()
        {
            try
            {
                string directory = Directory.GetCurrentDirectory();

                Console.Write($"\n{directory}: ");
            }
            catch { }

            return Console.ReadLine();
        }

        private ICommand GetCommand(string commandName)
        {
            switch (commandName)
            {
                case "echo":
                    return this.Echo;
                case "pwd":
                    return this.Pwd;
                case "cat":
                    return this.Cat;
                case "wc":
                    return this.Wc;
                case "exit":
                    return this.Exit;
                default:
                    return this.SystemProcess;
            }
        }
    }
}