using System.IO;
using System.Linq;
using Tools;

namespace Commands
{
	public class Wc : ICommand
	{
		public string Run(string[] args, string input, Writer errorOutput)
		{
			int countOfLines = input.Count(x => x == '\n') + (input.Length == 0 ? 0 : 1);
			int countOfWords = input.Count(x => x == ' ' || x == '\n') + (input.Length == 0 ? 0 : 1);
			int countOfBytes = input.Length;

			for (int i = 0; i < args.Length; i++)
			{
				try
				{
					var file = File.ReadAllText(args[i]);
					countOfLines += file.Count(x => x == '\n') + 1;
					countOfWords += file.Count(x => x == ' ' || x == '\n') + 1;
					countOfBytes += file.Length;
				}
				catch
				{
					for (; i < args.Length; i++)
						if (!File.Exists(args[i]))
							errorOutput.Write($"Wc: cannot read file {args[i]}");
					return "";
				}
			}
			return $"{countOfLines} {countOfWords} {countOfBytes}";
		}
	}
}
