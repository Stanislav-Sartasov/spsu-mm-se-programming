namespace Bash.Commands
{
    internal class Help : ICommand
    {
        public string Name { get; private set; }

        public Help()
        {
            Name = "help";
        }

        public string[]? Execute(string[] args)
        {
            Console.WriteLine("Hello ^_^, this is a bash simulation, call <help [command]> and it will output information about this command\nif there are no parameters, it will output information about all its commands\nBut if the command is not recognized or there are many arguments, help will return a corresponding message");
            Console.WriteLine("The application supports local variables to assign a value to a variable: $name1=value1 $name2=value2 .. $nameN=valueN\nAlso to use them: $name");
            Console.WriteLine("Also supported a command pipeline: [cmd1] | [cmd2] | .. | [cmdN], outputs of cmd<i> will become inputs of cmd<i+1>.");
            Console.WriteLine("Also, if the command was not recognized by bash, then the system process starts,\nand tries to launch the application with the entered command, in case of failure it will return <Application not found>\n");
            if (args.Length > 1)
            {
                return new string[1] { "Too many arguments in the\"help\" command" };
            }

            return PrintInfo(args.Length == 1 ? args[0] : null);
        }

        private string[]? PrintInfo(string? command)
        {
            string[] info = new string[]
            {
                "clear",
                "The <clear> command clears the console output\n",
                "cd",
                "<cd [directory]> - changes the directory to absolute or relative directory specified\n<cd ~> or <cd> - returns the directory to the home directory (the directory where the project dll is located)\n<cd ~user> - changes the directory to another user\n<cd ..\\somepath> - in this case, the directory changes to a directory level higher and falls into the somepath path\nreturns nothing if successful\nelse returns  <Failed to put the directory, the reasons may be: too many arguments or the wrong path>\n",
                "cat",
                "<cat [file1] [file2] .. [fileN]> - returns contents of file specified\nif file does not exist returns <Filename: \"file\" does not exist...>\n",
                "echo",
                "<echo [arg1] [arg2] .. [argN]> - return null\nprints arguments to standart output\n",
                "exit",
                "<exit [code]> - exits the application with code specified\nif many parameters were set or not a numeric code then exits the application with \"-1\"\nif without parameters then exits the application with \"0\"\n",
                "ls",
                "<ls> - returns all the files in the current directory \nif an error occurs, returns <No files in this directory.>\n",
                "pwd",
                "<pwd> - returns name of the current directory\n",
                "wc",
                "<wc [file1] [file2] .. [fileN]> - returns lines, words and byte length of the files specified\nif file does not exist returns <Filename: \"file\" does not exist...>\n"
            };

            if (command is null)
            {
                for (int i = 1; i < info.Length; i += 2)
                {
                    Console.WriteLine(info[i]);
                }
            }
            else
            {
                string result = "the command was not recognized";
                for (int i = 0; i < info.Length; i += 2)
                {
                    if (command == info[i])
                    {
                        result = info[i + 1];
                    }
                }

                if (result == "the command was not recognized")
                {
                    return new string[1] { "the command was not recognized" };
                }
                Console.WriteLine(result);
            }

            return null;

        }

    }
}