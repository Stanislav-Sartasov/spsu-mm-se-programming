using AbstractOperators;
using FileExplorer;

namespace Commands
{
    public class Cd : ACommand
    {
        public override List<string> Execute(List<String> args, List<String> input)
        {
            return FileManager.SetDirectory(args[0]) ? new List<string>() : new List<string>() { "This path doesn't exist!" };
        }
    }
}
