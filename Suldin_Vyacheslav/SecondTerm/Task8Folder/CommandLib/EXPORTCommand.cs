using System.Text.RegularExpressions;

namespace CommandLib
{
    public class EXPORTCommand : ACommand
    {
        public EXPORTCommand(string[] args)
        {
            name = CommandName.export;
            parametres = args;
        }

        public override void Run()
        {
            foreach (string substitution in parametres)
            {
                string key = Regex.Match(substitution, @"[^=]+").Value;
                string value = Regex.Match(substitution, @"(?<==).+").Value;
                Environ.SetLocalVar(key, value);
            }
            stdOut = "";
        }
    }
}