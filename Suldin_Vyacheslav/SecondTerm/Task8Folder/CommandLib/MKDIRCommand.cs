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
                        error.Message += $"{name}: cannot create directory ‘{newDirectory}’: Not supported name\n";
                        error.StdErr = 1;
                    }
                }
                else
                {
                    error.Message += $"{name}: cannot create directory ‘{newDirectory}’: File exists\n";
                    error.StdErr = 1;
                }
            }
            stdOut = stdOut == null ? null : stdOut[..^1];
        }
    }
}