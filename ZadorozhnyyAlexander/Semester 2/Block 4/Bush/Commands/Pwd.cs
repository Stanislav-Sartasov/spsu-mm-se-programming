using AbstractOperators;
using FileExplorer;


namespace Commands
{
    public class Pwd : ACommand
    {
        public override List<string> Execute(List<String> args, List<String> input)
        {
            List<string> result = new List<string>();
            var path = FileManager.GetCurrentDirectory();
            result.Add(path);
            result.AddRange(FileManager.GetDirectoryFiles(path));

            return result;
        }
    }
}
