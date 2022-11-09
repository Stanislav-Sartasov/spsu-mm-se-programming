using System.IO;
using Tools;

namespace Commands
{
	public class Cat : ICommand
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			string? result = "";

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "-")
				{
					result += input;
					continue;
				}

				try
				{
					if (result != null)
						result += File.ReadAllText(args[i]);
				}
				catch
				{
					errorOutput.Write($"Cat: cannot read file {args[i]}");
					result = null;
				}
			}

			if (args.Length == 0)
				return input;
			return result ?? string.Empty;
		}
	}
}
