using System.IO;

namespace CommandLib
{
    public class RMDIRCommand : ACommand
    {
        public RMDIRCommand(string[] args)
        {
            Name = "rmdir";
            parametres = args;
        }

        public override void Run()
        {
            foreach (string delDirectory in parametres)
            {
                string absolutePath = Path.GetFullPath(delDirectory, Environ.GetCurrentDirectory());
                if (!File.Exists(absolutePath))
                {
                    if (!Directory.Exists(absolutePath))
                    {
                        error.Message += $"\n{Name}: failed to remove \'{delDirectory}\': No such file or directory";
                        error.StdErr = 1;
                    }
                    else
                    {
                        if (Directory.GetFiles(absolutePath).Length == 0 + Directory.GetDirectories(absolutePath).Length)
                        {
                            Directory.Delete(absolutePath);
                        }
                        else
                        {
                            error.Message += $"\n{Name}: failed to remove \'{delDirectory}\': Directory not empty";
                            error.StdErr = 1;
                        }
                    }
                }
                else
                {
                    error.Message += $"\n{Name}: failed to remove \'{delDirectory}\': Not a directory";
                    error.StdErr = 1;
                }
            }
            stdOut = string.Empty;
            if (error.StdErr != 0) error.Message = error.Message[1..];
        }
    }
}