using System.IO;

namespace CommandLib
{
    public class CDCommand : ACommand
    {
        public CDCommand(string[] args)
        {
            name = CommandName.cd;
            parametres = args;
        }
        public override void Run()
        {
            if (parametres.Length > 1)
            {
                error.StdErr = 1;
                error.Message = $"{name}: too many arguments\n";
                return;
            }
            else if (parametres.Length == 0)
            {
                Confirm(Environ.GetLocalVar("$HOME"), Environ.GetLocalVar("$HOME"));
                return;
            }
            var arg = parametres[0];

            string abcolutePath = Path.GetFullPath(arg, Environ.GetCurrentDirectory());

            Confirm(abcolutePath, arg);

            stdOut = stdOut == null ? null : stdOut[..^1];
            return;
        }

        public void Confirm(string path, string obj)
        {
            if (File.Exists(path))
            {
                error.StdErr = 1;
                error.Message = $"{name}: \'{obj}\': Not a directory\n";
            }
            else if (!Directory.Exists(path))
            {
                error.StdErr = 1;
                error.Message = $"{name}: \'{obj}\': No such directory\n";
            }
            else
            {
                Environ.SetCurrentDirectory(path);
            }
        }
    }
}
