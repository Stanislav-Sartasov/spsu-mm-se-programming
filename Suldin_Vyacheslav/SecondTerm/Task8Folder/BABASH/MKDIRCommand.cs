using System;
using System.IO;

namespace BABASH
{
    public class MKDIRCommand : Command
    {
        public MKDIRCommand(string[] args, Session commandSession)
        {
            session = commandSession; 
            Name = "mkdir";
            parametres = args;
        }

        public override void Execute()
        {
            foreach (string newDirectory in parametres)
            {
                string absolutePath = Path.GetFullPath(newDirectory, session.GetCurrentDirectory());
                if (!Directory.Exists(absolutePath))
                {
                    try
                    {
                        Directory.CreateDirectory(absolutePath);
                    }
                    catch (IOException)
                    {
                        error.Message += $"\n{Name}: cannot create directory ‘{newDirectory}’: Not supported name";
                        error.StdErr = 1;
                    }
                }
                else
                {
                    error.Message += $"\n{Name}: cannot create directory ‘{newDirectory}’: File exists";
                    error.StdErr = 1;
                }
            }
            stdOut = string.Empty;
            if (error.StdErr != 0) error.Message = error.Message[1..];
        }
    }
}