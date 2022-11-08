using System.Collections.Generic;
using Tools;
using Commands;
using System.Diagnostics;

namespace Bash
{
	public class Bash
	{
		private Parser.Parser parser = new();
		public Dictionary<string, ICommand> commands = new();
		private Writer writer;
		private Reader reader;

		public Bash(Reader reader, Writer writer)
		{
			this.reader = reader;
			this.writer = writer;
		}

		public void AddCommand(string str, ICommand command)
		{
			if (commands.ContainsKey(str))
				commands[str] = command;
			else
				commands.Add(str, command);
		}

		public static Bash BashInit()
		{
			Bash bash = new(new Reader(), new Writer());
			bash.AddCommand("cat", new Cat());
			bash.AddCommand("pwd", new Pwd());
			bash.AddCommand("echo", new Echo());
			bash.AddCommand("exit", new Exit());
			bash.AddCommand("wc", new Wc());
			return bash;
		}

		public void Start()
		{
			for(; ; )
			{
				var cmds = reader.Read().Split('|');

				string output = "";
				foreach (var call in cmds)
				{
					var cmd = parser.ParseCommands(call);

					if (cmd == null)
					{
						output = "";
						continue;
					}

					if (cmd.Command == "exit")
						return;

					if (commands.ContainsKey(cmd.Command))
					{
						output = commands[cmd.Command].Run(cmd.Args, output, writer);
					}
					else
					{
						try
						{
							Process.Start(cmd.Command, cmd.Args);
						}
						catch
						{
							writer.Write($"{cmd.Command}: command not found");
						}
						output = "";
					}
				}
				writer.Write(output);
			}
		}
	}
}
