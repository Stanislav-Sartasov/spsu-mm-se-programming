using System.IO;

namespace BABASH
{
    public class TOUCHCommand : Command
    {
        public TOUCHCommand(string[] args, Session comandSession)
        {
            session = comandSession;
            Name = "touch";
            parametres = args;
        }

        public override void Execute()
        {
            foreach (string newFile in parametres)
            {
                string absolutePath = Path.GetFullPath(newFile, session.GetCurrentDirectory());
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