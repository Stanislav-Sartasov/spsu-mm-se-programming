using System.IO;

namespace CommandLib
{
    public class CATCommand : ACommand
    {
        public CATCommand(string[] args)
        {
            name = CommandName.cat;
            parametres = args;
        }

        public override void Run()
        {
            if (parametres.Length == 0)
            {
                stdOut = stdIn;
                return;
            }

            foreach (string pathFile in parametres)
            {
                string abcolutePath = Path.GetFullPath(pathFile, Environ.GetCurrentDirectory());

                if (Directory.Exists(abcolutePath))
                {
                    error.StdErr = 1;
                    error.Message += $"{name}: {pathFile}: Is a directory\n";
                }
                else if (!File.Exists(abcolutePath))
                {
                    error.StdErr = 1;
                    error.Message += $"{name}: {pathFile}: No such file or directory\n";
                }
                else
                {
                    string text = File.ReadAllText(abcolutePath);
                    stdOut += text;
                }
            }
        }
    }
}