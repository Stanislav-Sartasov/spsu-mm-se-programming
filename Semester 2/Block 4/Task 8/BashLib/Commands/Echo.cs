using BashLib.IO;

namespace BashLib.Commands
{
	public class Echo : ICommand
	{
		public string Run(string[] arguments)
		{
			return string.Join(" ", arguments);
		}
	}
}