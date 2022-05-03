using System.Text.RegularExpressions;

namespace CommandLib
{
    public class EXPORTCommand : ACommand
    {
        public EXPORTCommand(string[] args)
        {
            Name = "export";
            parametres = args;
        }

        public override void Run()
        {
            foreach (string asd in parametres)
            {
                string qwe = Regex.Match(asd, @"[+]+").Value;
                string key = Regex.Match(asd, @"[^=]+").Value;
                string value = Regex.Match(asd, @"(?<==).+").Value;
                Environ.SetLocalVar(key, value);
            }
            stdOut = "";
        }
    }
}