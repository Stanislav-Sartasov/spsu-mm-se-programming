using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public class Wc : IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			int lineCounter = input.Count(x => x == '\n') + (input.Length == 0 ? 0 : 1);
			int wordCounter = input.Count(x => x == ' ' || x == '\n') + (input.Length == 0 ? 0 : 1);
			int byteCounter = input.Length;

			for (int i = 0; i < args.Length; i++)
			{
				try
				{
					var file = File.ReadAllText(args[i]);
					lineCounter += file.Count(x => x == '\n') + 1;
					wordCounter += file.Count(x => x == ' ' || x == '\n') + 1;
					byteCounter += file.Length;
				}
				catch
				{
					for (;i < args.Length; i++)
						if (!File.Exists(args[i]))
							errorOutput.WriteLine($"Wc: cannot read file {args[i]}");
					return string.Empty;
				}
			}

			return $"   {lineCounter}   {wordCounter}   {byteCounter}";
		}
	}
}
