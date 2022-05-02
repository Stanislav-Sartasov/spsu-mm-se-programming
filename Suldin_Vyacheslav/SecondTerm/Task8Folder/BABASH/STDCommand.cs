using System;
using System.IO;
using System.Linq;

namespace BABASH
{
    public class STDCommand : Command
    {
        public STDCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            parametres = args;
        }
        public override void Execute()
        {
            Command initialCommand = CommandCreator.Create(parametres[0], session);
            initialCommand.SetStdIn(stdIn);
            initialCommand.Execute();
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

                string absolutePath = Path.GetFullPath(sumArgs[0], session.GetCurrentDirectory());
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
                    error.Message = $"-babach: {sumArgs[0]}: No sush file or directory";
                    error.StdErr = 1;
                }
                catch (UnauthorizedAccessException)
                {
                    error.Message = $"-babach: {sumArgs[0]}: Is a directory";
                    error.StdErr = 1;
                }
            }
            stdOut = "";
        }
    }
}