using AbstractOperators;
using FileExplorer;
using System.Text;
using System.Text.RegularExpressions;

namespace Commands
{
    public class Wc : ACommand
    {
        public override List<string> Execute(List<String> args, List<String> input)
        {
            List<string> result = new List<string>();

            if (input.Count() != 0)
            {
                result.Add(String.Join("\t",
                    "Input stream",
                    input.Select(x => x.Count(y => y == '\n')).Sum() + 1,
                    input.Select(x => Regex.Split(Regex.Replace(x, @"[\s*]|[\t*]|[\n*]", " "), " ").Length).Sum(),
                    input.Select(x => Encoding.ASCII.GetBytes(x).Length).Sum())
                    );
            }   

            for (int i = 0; i < args.Count(); i++)
            {
                if (FileManager.IsFileExist(args[i]))
                {
                    var file = File.ReadAllLines(args[i]);
                    result.Add(String.Join("\t",
                        args[i],
                        file.Count(), 
                        file.Select(x => x.Split(" ").Length).Sum(), 
                        new FileInfo(args[i]).Length));
                }
                else
                    result.Add("This file or directory didn't exist!");
            }

            return result;
        }
    }
}
