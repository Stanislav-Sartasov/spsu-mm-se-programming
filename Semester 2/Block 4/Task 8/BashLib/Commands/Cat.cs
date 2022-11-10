using System.Collections.Specialized;
using BashLib.IO;
using System.IO;

namespace BashLib.Commands
{
	public class Cat : ICommand
	{
		private IWriter errorWriter;
		
		public Cat(IWriter errorWriter)
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
					output += File.ReadAllText(argument) + "\n";
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