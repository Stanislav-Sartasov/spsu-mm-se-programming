using System.IO;
using System.Linq;

namespace BABASH
{
    public class CATCommand : Command
    {
        public CATCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "cat";
            parametres = args;
        }

        public override void Execute()
        {
            if (parametres.Length == 0)
            {
                stdOut = stdIn;
                return;
            }

            foreach (string pathFile in parametres)
            {
                string abcolutePath = Path.GetFullPath(pathFile, session.GetCurrentDirectory());

                if (Directory.Exists(abcolutePath))
                {
                    error.StdErr = 1;
                    error.Message += $"\n{Name}: {pathFile}: Is a directory";
                }
                else if (!File.Exists(abcolutePath))
                {
                    error.StdErr = 1;
                    error.Message += $"\n{Name}: {pathFile}: No such file or directory";
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