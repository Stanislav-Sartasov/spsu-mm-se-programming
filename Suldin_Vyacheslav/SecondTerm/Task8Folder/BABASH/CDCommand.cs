using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BABASH
{
    public class CDCommand : Command
    {
        public CDCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "cd";
            parametres = args;
        }
        public override void Execute()
        {
            if (parametres.Length > 1)
            {
                error.StdErr = 1;
                error.Message = $"{Name}: too many arguments";
                return;
            }
            else if (parametres.Length == 0)
            {
                Confirm(session.GetLocalVar("$HOME"), session.GetLocalVar("$HOME"));
                return;
            }
            var arg = parametres[0];

            string abcolutePath = Path.GetFullPath(arg, session.GetCurrentDirectory());

            Confirm(abcolutePath, arg);
            
            stdOut = "";
            return;
        }

        public void Confirm(string path, string obj)
        {
            if (File.Exists(path))
            {
                error.StdErr = 1;
                error.Message = $"{Name}: \'{obj}\': Not a directory";
            }
            else if (!Directory.Exists(path))
            {
                error.StdErr = 1;
                error.Message = $"{Name}: \'{obj}\': No such directory";
            }
            else
            {
                session.SetCurrentDirectory(path);
            }
        }
    }
}
