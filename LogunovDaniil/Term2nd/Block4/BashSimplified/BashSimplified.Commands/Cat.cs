using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public class Cat : IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			string? result = string.Empty;

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
					errorOutput.WriteLine($"Cat: cannot read file {args[i]}");
					result = null;
				}
			}

			if (args.Length == 0)
				return input;
			return result != null ? result : string.Empty;
		}
	}
}
