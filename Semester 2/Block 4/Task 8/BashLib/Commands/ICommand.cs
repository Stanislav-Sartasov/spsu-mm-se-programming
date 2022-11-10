using BashLib.IO;

namespace BashLib.Commands
{
	public interface ICommand
	{
		public string Run(string[] arguments);
	}
}