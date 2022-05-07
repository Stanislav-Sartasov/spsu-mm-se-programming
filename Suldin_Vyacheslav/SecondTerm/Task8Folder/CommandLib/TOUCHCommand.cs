using System.IO;

namespace CommandLib
{
    public class TOUCHCommand : ACommand
    {
        public TOUCHCommand(string[] args)
        {
            name = CommandName.touch;
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
                        error.Message += $"{name}: cannot touch {newFile}: No such file or directory\n";
                        error.StdErr = 1;
                    }
                }
            }
            stdOut = stdOut == null ? null : stdOut[..^1];
        }
    }
}