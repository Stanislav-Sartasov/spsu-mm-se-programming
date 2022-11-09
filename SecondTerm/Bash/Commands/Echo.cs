using Tools;

namespace Commands
{
	public class Echo : ICommand
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			return string.Join(" ", args);
		}
	}
}
