using Tools;

namespace Commands
{
	public interface ICommand
	{
		public string Run(string[] args, string input, Writer errorOutput);
	}
}
