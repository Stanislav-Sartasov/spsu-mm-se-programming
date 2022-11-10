using Commands;
using System.Diagnostics;
using System.Reflection;

namespace Bash
{
	public class MyBash
	{
		private VariableManager variableManager;
		private readonly Dictionary<string, ICommand> commands;

		public MyBash()
		{
			variableManager = new VariableManager();

			commands = new Dictionary<string, ICommand>();
			Assembly asm = Assembly.LoadFrom("Commands.dll");
			foreach (Type type in asm.GetTypes())
				if (type.GetInterface("ICommand") != null)
				{
					ICommand command = (ICommand)Activator.CreateInstance(type);
					commands.Add(command.Name, command);
				}
		}

		public void Run()
		{
			string? input;

			while (true)
			{
				Console.Write(">>>");
				input = Console.ReadLine();
				if (input != null)
					Console.WriteLine(ParceInput(input));
				Console.WriteLine("");
			}
		}

		public string ParceInput(string input)
		{
			string[]? returnValue = new string[] { };
			foreach (string command in input.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
			{
				try
				{
					if (command[0] == '$')
						variableManager.AssignVariable(command);
					else
					{
						string[] arguments = command.Split(" ");
						variableManager.ReplaceVariables(ref arguments);

						ICommand? commandClass;
						if (!commands.TryGetValue(arguments[0], out commandClass))
							StartProcess(arguments.Concat(returnValue).ToArray());

						returnValue = commandClass.Execute(arguments[1..].Concat(returnValue).ToArray());
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					break;
				}

			}
			return String.Join('\n', returnValue);
		}

		private void StartProcess(string[] arguments)
		{
			try
			{
				Process.Start(arguments[0], arguments[1..]);
			}
			catch
			{
				throw new Exception("Unknown app \"" + arguments[0] + "\".");
			}
		}
	}
}
