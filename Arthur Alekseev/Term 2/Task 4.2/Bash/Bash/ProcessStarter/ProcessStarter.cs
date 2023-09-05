using Bash.App;
using Bash.App.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.ProcessStarter
{
	public class ProcessStarter : IProcessStarter
	{
		ILogger logger;

		public ProcessStarter(ILogger logger)
		{
			this.logger = logger;
		}

		public void StartProcess(string processName, string processArgs)
		{
			try
			{
				Process.Start(processName, processArgs);
			}
			catch
			{
				try
				{
					Process.Start(processName + ".exe", processArgs);
				}
				catch
				{
					logger.Log("Error starting the app");
				}
			}
		}
	}
}
