using AbstractOperators;
using FileExplorer;


namespace Commands
{
    public class Cat : ACommand
    {
        public override List<string> Execute(List<String> args, List<String> input)
        {
			List<string> result = input;

			for (int i = 0; i < args.Count(); i++)
            {
                if (FileManager.IsFileExist(args[i]))
                    result.Add(File.ReadAllText(args[i]));
                else
                    result.Add("This file didn't exist!");
            }

			return new List<String>() { String.Join("", result) };
		}
    }
}
