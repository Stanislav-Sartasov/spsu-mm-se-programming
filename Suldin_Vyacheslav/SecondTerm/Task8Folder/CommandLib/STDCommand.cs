using System;
using System.IO;
using System.Linq;
using CommandResolverLib;

namespace CommandLib
{
    public class STDCommand : ACommand
    {
        private CommandCreator creator;
        public STDCommand(string[] args, CommandCreator cc)
        {
            creator = cc;
            parametres = args;
        }
        public override void Run()
        {
            if (parametres.Length == 0)
            {
                return;
            }
            ICommand initialCommand = creator.Create(parametres[0]);
            initialCommand.SetStdIn(stdIn);
            initialCommand.Run();
            stdIn = initialCommand.GetStdOut();


            string lastAccess = string.Empty;

            foreach (var directory in parametres.Skip(1))
            {
                string[] sumArgs = Analyser.MySplit(directory, ' ');

                if (lastAccess != string.Empty)
                {
                    stdIn = File.ReadAllText(lastAccess);
                    File.WriteAllText(lastAccess, string.Empty);
                }

                for (int k = 0; k < sumArgs.Length; k++)
                    sumArgs[k] = sumArgs[k].Replace("\"", string.Empty);

                foreach (string value in sumArgs.Skip(1))
                {
                    stdIn += value + " ";
                }

                string absolutePath = Path.GetFullPath(sumArgs[0], Environ.GetCurrentDirectory());
                try
                {
                    if (!File.Exists(absolutePath))
                    {
                        File.Create(absolutePath).Close();
                    }
                    File.WriteAllText(absolutePath, stdIn);
                    lastAccess = absolutePath;
                }
                catch (DirectoryNotFoundException)
                {
                    error.Message = $"-mybash: {sumArgs[0]}: No sush file or directory\n";
                    error.StdErr = 1;
                }
                catch (UnauthorizedAccessException)
                {
                    error.Message = $"-mybash: {sumArgs[0]}: Is a directory\n";
                    error.StdErr = 1;
                }
            }
            stdOut = stdOut == null ? null : stdOut[..^1];
        }
    }
}