using System.IO;

namespace CommandLib
{
    public class TOUCHCommand : ACommand
    {
        public TOUCHCommand(string[] args)
        {
            Name = "touch";
            parametres = args;
        }

        public override void Run()
        {
            foreach (string newFile in parametres)
            {
                string absolutePath = Path.GetFullPath(newFile, Environ.GetCurrentDirectory());
                if (!File.Exists(absolutePath))
                {
                    try
                    {
                        File.Create(absolutePath).Close();
                    }
                    catch (DirectoryNotFoundException)
                    {
                        error.Message += $"\n{Name}: cannot touch {newFile}: No such file or directory";
                        error.StdErr = 1;
                    }
                }
            }
            stdOut = string.Empty;
            if (error.StdErr != 0) error.Message = error.Message[1..];
        }
    }
}