using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLib
{
    public class HELPCommand : ACommand
    {
        public HELPCommand(string[] args)
        {
            parametres = args;
            name = CommandName.help;
        }
        public override void Run()
        {
            if (parametres.Length == 0)
            {
                parametres = new string[] { "echo", "cat", "export", "cd", "pwd", "ls", "exit", "touch", "mkdir", "rm",
                "rmdir", "wc", "help"};
            }
            foreach (string name in parametres)
            {
                switch (name)
                {
                    case "echo":
                        {
                            stdOut += "echo [arg1] [arg2] ... - Display a line of text\n";
                            break;
                        }
                    case "help":
                        {
                            stdOut += "help [commandName1] [commandName2]... - Shows info for commands\n" +
                                "\t\talternative : help - shows info for all commands\n";
                            break;
                        }
                    case "exit":
                        {
                            stdOut += "exit [errCode] - Exit the shell with status errCode\n";
                            break;
                        }
                    case "export":
                        {
                            stdOut += "export [var1=value1] [var2=value2] ... - Variable declaration\n" +
                                "\t\tcall variable: $var; \"${var}\"\n";
                            break;
                        }
                    case "pwd":
                        {
                            stdOut += "pwd - Shows current working directory\n";
                            break;
                        }
                    case "ls":
                        {
                            stdOut += "ls - [dir1] [dir2] ... - Shows all files and catalogs in directories by path\n" +
                                 "\t\talternative : ls - shows files and catalog in current directory\n";
                            break;
                        }
                    case "cat":
                        {
                            stdOut += "cat [arg1] [arg2] ... - Concatenate files and print on the standard output\n";
                            break;
                        }
                    case "wc":
                        {
                            stdOut += "wc [file1path] [file2path] ... - Print newline, word, and byte counts for each file\n";
                            break;
                        }

                    case "cd":
                        {
                            stdOut += "cd [directory] - Change to directory by absolute or relative path \n";
                            break;
                        }
                    case "touch":
                        {
                            stdOut += "touch [file1path] [file2path] ... - Creates files if they don't exists \n";
                            break;
                        }
                    case "mkdir":
                        {
                            stdOut += "mkdir [dir1path] [dir2path] ... - Creates directories if they don't exists\n";
                            break;
                        }
                    case "rmdir":
                        {
                            stdOut += "rmdir [dir1path] [dir2path] ... - Removes existing directories\n";
                            break;
                        }
                    case "rm":
                        {
                            stdOut += "rm [file1path] [file2path] ... - Removes existing files\n";
                            break;
                        }
                    default:
                        {
                            error.Message += $"-bash: {this.name}: no help topics match `{name}'\n";
                            error.StdErr = 1;
                            break;
                        }
                }
            }
            stdOut = stdOut == null ? null : stdOut[..^1];
        }
    }
}
