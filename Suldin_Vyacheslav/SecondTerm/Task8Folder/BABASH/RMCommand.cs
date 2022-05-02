using System.IO;

namespace BABASH
{
    public class RMCommand : Command
    {
        public RMCommand(string[] args, Session comandSession)
        {
            Name = "rm";
            parametres = args;
            session = comandSession;
        }

        public override void Execute()
        {
            foreach (string delFile in parametres)
            {
                string absolutePath = Path.GetFullPath(delFile, session.GetCurrentDirectory());
                if (!Directory.Exists(absolutePath))
                {
                    if (!File.Exists(absolutePath))
                    {
                        error.Message += $"\n{Name}: cannot remove \'{delFile}\': No such file or directory";
                        error.StdErr = 1;
                    }
                    else
                    {
                        File.Delete(absolutePath);
                    }
                }
                else
                {
                    error.Message += $"\n{Name}: cannot remove \'{delFile}\': Is a directory";
                    error.StdErr = 1;
                }
            }
            stdOut = string.Empty;
            if (error.StdErr != 0) error.Message = error.Message[1..];
        }
    }
}