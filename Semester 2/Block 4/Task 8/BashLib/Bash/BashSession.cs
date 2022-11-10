using BashLib.IO;
using BashLib.Commands;
using System.Linq;
using System.Diagnostics;

namespace BashLib.Bash
{
	public class BashSession
	{
		private IReader reader;
		private IWriter writer;
		private IExiter exiter;
		private SessionManager manager;

		public BashSession(IReader reader, IWriter writer, IExiter exiter)
		{
			this.reader = reader;
			this.writer = writer;
			this.exiter = exiter;
			manager = new SessionManager();
		}

		public void Start()
		{
			while (true)
			{
				var commands = reader.ReadLine().Split('|');

				string output = string.Empty;
				foreach (var cmd in commands)
				{
					var command = manager.ResolveCommand(cmd);
					if (command != null)
					{
						output = ExecuteCommand(command, output);
					}
					else
					{
						output = string.Empty;
					}
				}

				writer.WriteLine(output);
			}
		}

		private string ExecuteCommand(ResolvedCommand command, string lastResult)
		{
			switch (command.Name)
			{
				case "cat": return new Cat(writer).Run(JoinArgumentsAndLastResult(command.Args, lastResult));
				case "wc": return new Wc(writer).Run(JoinArgumentsAndLastResult(command.Args, lastResult));
				case "exit": return new Exit(exiter).Run(JoinArgumentsAndLastResult(command.Args, lastResult));
				case "pwd": return new Pwd().Run(JoinArgumentsAndLastResult(command.Args, lastResult));
				case "echo": return new Echo().Run(JoinArgumentsAndLastResult(command.Args, lastResult));
				default:
					try
					{
						Process.Start(command.Name, command.Args);
					}
					catch
					{
						writer.WriteLine($"{command.Name}: command not found");
					}
					return string.Empty;	
			}
		}

		private string[] JoinArgumentsAndLastResult(string[] arguments, string lastResult)
		{
			var joined = arguments.ToList();
			
			if (lastResult != string.Empty)
				joined.Add(lastResult);
			
			return joined.ToArray();
		}
	}
}