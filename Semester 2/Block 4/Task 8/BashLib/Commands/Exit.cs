using BashLib.IO;

namespace BashLib.Commands
{
	public class Exit : ICommand
	{
		private IExiter exiter;
		
		public Exit(IExiter exiter)
		{
			this.exiter = exiter;
		}
		
		public string Run(string[] arguments)
		{
			exiter.Exit();
			return string.Empty;
		}
	}
}