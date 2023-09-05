using BashSimplified.IOLibrary;
using BashSimplified.Commands;
using BashSimplified.CommandResolver;
using System.Diagnostics;

namespace BashSimplified.BashLibrary
{
	public class Bash
	{
		private Resolver resolver = new Resolver();
		private IWriter writer;
		private IReader reader;
		private Dictionary<string, IExecutable> executables = new Dictionary<string, IExecutable>();

		public Bash(IReader reader, IWriter writer)
		{
			this.reader = reader;
			this.writer = writer;
		}

		public void AddCommand(string alias, IExecutable command)
		{
			if (executables.ContainsKey(alias))
				executables[alias] = command;
			else
				executables.Add(alias, command);
		}

		public void StartMainLoop()
		{
			while (true)
			{
				var cmds = reader.GetLine().Split('|');

				string output = string.Empty;
				foreach (var call in cmds)
				{
					var cmd = resolver.ResolveCommand(call);

					if (cmd == null)
					{
						output = string.Empty;
						continue;
					}

					if (cmd.Command == "exit")
						return;

					if (executables.ContainsKey(cmd.Command))
					{
						output = executables[cmd.Command].Run(cmd.Args, output, writer);
					}
					else
					{
						try
						{
							Process.Start(cmd.Command, cmd.Args);
						}
						catch
						{
							writer.WriteLine($"{cmd.Command}: command not found");
						}

						output = string.Empty;
					}
				}

				writer.WriteLine(output);
			}
		}
	}
}