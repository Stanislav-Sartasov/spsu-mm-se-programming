using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandResolverLib
{
    public static class Analyser
    {
        static public string[] MySplit(string line, char str)
        {
            // А если нечетное?)
            bool inQuotes = false;
            var list = new List<string>();
            string asd = "";
            foreach (char ch in line)
            {
                if (inQuotes)
                {
                    if (ch == '\"')
                    {
                        asd += ch;
                        inQuotes = false;
                    }
                    else
                    {
                        asd += ch;
                    }
                }
                else
                {
                    if (ch == '\"')
                    {
                        asd += ch;
                        inQuotes = true;
                    }
                    else if (ch == str)
                    {
                        if (asd != string.Empty) list.Add(asd);
                        asd = string.Empty;
                    }
                    else
                    {
                        asd += ch;
                    }
                }
            }
            if (asd != string.Empty) list.Add(asd);
            return list.ToArray();
        }
        static public string Substitution(string argLine, IReadOnlyDictionary<string, string> dict)
        {
            string[] asdasd = Regex.Matches(argLine, @"\${[^}]+\}|\$[^\W]+").OfType<Match>().Select(m => m.Value).ToArray();
            foreach (string replace in asdasd)
            {
                try
                {
                    argLine = argLine.Replace(replace, dict[Regex.Replace(replace, @"\{|\}", String.Empty)]);
                }
                catch (KeyNotFoundException)
                {
                    argLine = argLine.Replace(replace, string.Empty);
                }

            }
            return argLine;
        }
    }

}
