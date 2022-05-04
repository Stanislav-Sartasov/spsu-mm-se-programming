using System.IO;

namespace CommandLib
{
    public class MKDIRCommand : ACommand
    {
        public MKDIRCommand(string[] args)
        {
            name = CommandName.mkdir;
            parametres = args;
        }

        public override void Run()
        {
            foreach (string newDirectory in parametres)
            {
                string absolutePath = Path.GetFullPath(newDirectory, Environ.GetCurrentDirectory());
                if (!Directory.Exists(absolutePath))
                {
                    try
                    {
                        Directory.CreateDirectory(absolutePath);
                    }
                    catch (IOException)
                    {
                        error.Message += $"\n{name}: cannot create directory ‘{newDirectory}’: Not supported name";
                        error.StdErr = 1;
                    }
                }
                else
                {
                    error.Message += $"\n{name}: cannot create directory ‘{newDirectory}’: File exists";
                    error.StdErr = 1;
                }
            }
            stdOut = string.Empty;
            if (error.StdErr != 0) error.Message = error.Message[1..];
        }
    }
}