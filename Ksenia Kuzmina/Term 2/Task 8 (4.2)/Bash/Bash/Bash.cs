using Bash.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash
{
	public class Bash
	{
		private VariableManager variableManager = new VariableManager();
		private ILogger logger;
		private IExiter exiter;

		public Bash(ILogger logger, IExiter exiter)
		{
			this.logger = logger;
			this.exiter = exiter;
		}

		public void Run()
		{
			while (true)
			{
				RunCommand();
			}
		}

		private void RunCommand()
		{
			string[] commands = logger.Input().Split("|");
			string[] previousArgs = { };

			foreach (string command in commands)
			{
				var commandCopy = variableManager.ProcessCommand(command);
				string[] args = GetCommandArgs(commandCopy.Trim());
				switch (args[0])
				{
					case ("cat"): previousArgs = new Cat().RunCommand(args[1..].Concat(previousArgs).ToArray()); break;
					case ("wc"): previousArgs = new Wc().RunCommand(args[1..].Concat(previousArgs).ToArray()); break;
					case ("exit"): previousArgs = new Exit(exiter).RunCommand(args[1..].Concat(previousArgs).ToArray()); break;
					case ("pwd"): previousArgs = new Pwd().RunCommand(args[1..].Concat(previousArgs).ToArray()); break;
					case ("echo"): previousArgs = new Echo(logger).RunCommand(args[1..].Concat(previousArgs).ToArray()); break;
					default: StartProcess(args[0], string.Join(" ", args[1..].Concat(previousArgs))); break;
				}
			}

			foreach (string argument in previousArgs)
			{
				logger.Write(argument + Environment.NewLine);
			}
		}

		private string[] GetCommandArgs(string command)
		{
			bool insideQuotation = false;
			List<string> result = new List<string> { "" };

			foreach (char c in command)
			{
				if (c == '"')
				{
					insideQuotation = !insideQuotation;
					continue;
				}

				if (c == ' ' && !insideQuotation)
				{
					result.Add("");
					continue;
				}

				result[^1] += c;
			}

			return result.ToArray();
		}

		private void StartProcess(string appName, string args)
		{
			if (appName.StartsWith("$"))
			{
				return;
			}

			try
			{
				Process.Start(appName, args);
			}
			catch
			{
				logger.Write("Error starting the app" + Environment.NewLine);
			}
		}
	}
}
