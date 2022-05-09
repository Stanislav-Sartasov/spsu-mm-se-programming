using Bash.App.BashComponents.Exceptions;
using System;

namespace Bash
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				var logger = new App.Output.Logger();
				new App.Bash(logger, new ProcessStarter.ProcessStarter(logger)).Run();
			}
			catch (ExitException ex)
			{
				Environment.Exit(ex.ExitCode);
			}
		}
	}
}