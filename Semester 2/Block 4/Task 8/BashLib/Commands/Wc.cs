using BashLib.IO;
using System.IO;

namespace BashLib.Commands
{
	public class Wc : ICommand
	{
		private IWriter errorWriter;

		public Wc(IWriter errorWriter)
		{
			this.errorWriter = errorWriter;
		}

		public string Run(string[] arguments)
		{
			string output = string.Empty;

			foreach (var argument in arguments)
			{
				try
				{
					var lines = File.ReadAllText(argument).Split("\n").Length;
					var words = File.ReadAllText(argument).Split(" ").Length;
					var bytes = new FileInfo(argument).Length;

					output += $"{argument}:\nlines - {lines}\nwords - {words}\nbytes - {bytes}";
				}
				catch
				{
					errorWriter.WriteLine($"Unable to read file {argument}");
				}
			}

			return output;
		}
	}
}