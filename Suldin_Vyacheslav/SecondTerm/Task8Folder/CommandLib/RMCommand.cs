using System.IO;

namespace CommandLib
{
    public class RMCommand : ACommand
    {
        public RMCommand(string[] args)
        {
            name = CommandName.rm;
            parametres = args;
        }

        public override void Run()
        {
            foreach (string delFile in parametres)
            {
                string absolutePath = Path.GetFullPath(delFile, Environ.GetCurrentDirectory());
                if (!Directory.Exists(absolutePath))
                {
                    if (!File.Exists(absolutePath))
                    {
                        error.Message += $"{name}: cannot remove \'{delFile}\': No such file or directory\n";
                        error.StdErr = 1;
                    }
                    else
                    {
                        File.Delete(absolutePath);
                    }
                }
                else
                {
                    error.Message += $"{name}: cannot remove \'{delFile}\': Is a directory\n";
                    error.StdErr = 1;
                }
            }
            stdOut = string.Empty;
        }
    }
}