using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace BABASH
{
    public class EXPORTCommand : Command
    {
        public EXPORTCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "export";
            parametres = args;
        }

        public override void Execute()
        {
            foreach (string asd in parametres)
            {
                string qwe = Regex.Match(asd, @"[+]+").Value;
                string key = Regex.Match(asd, @"[^=]+").Value;
                string value = Regex.Match(asd, @"(?<==).+").Value;
                session.SetLocalVar(key, value);
            }
            stdOut = "";
        }
    }
}