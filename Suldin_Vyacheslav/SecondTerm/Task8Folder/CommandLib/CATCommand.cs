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
                    error.Message += $"\n{name}: {pathFile}: Is a directory";
                }
                else if (!File.Exists(abcolutePath))
                {
                    error.StdErr = 1;
                    error.Message += $"\n{name}: {pathFile}: No such file or directory";
                }
                else
                {
                    string text = File.ReadAllText(abcolutePath);
                    stdOut += text;
                    error.Message += "\n" + text;
                }
            }
            if (error.StdErr != 0) error.Message = error.Message.Substring(1);
        }
    }
}