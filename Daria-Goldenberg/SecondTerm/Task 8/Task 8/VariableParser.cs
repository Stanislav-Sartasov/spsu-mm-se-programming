namespace Task_8
{
	public static class VariableParser
	{
		private static string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		public static void SetVariable(ref string command, Dictionary<string, string> variables)
		{
			string commandNoFirstArg = string.Join(" ", command.Split(" ")[1..]) + " ";
			int index = 0;
			string foundVar = "";

			while (index < commandNoFirstArg.Length)
			{
				if (foundVar != "")
				{
					if (allowedChars.Contains(commandNoFirstArg[index]))
						foundVar += commandNoFirstArg[index];
					else
					{
						var stringBefore = commandNoFirstArg.Substring(0, index - foundVar.Length);
						var stringAfter = commandNoFirstArg.Substring(index, commandNoFirstArg.Length - index);

						if (!variables.ContainsKey(foundVar))
							throw new Exception("Variable " + foundVar + " does not exist.");

						commandNoFirstArg = stringBefore + variables[foundVar] + stringAfter;
						index += variables[foundVar].Length - foundVar.Length;

						foundVar = "";
					}
				}

				if (commandNoFirstArg[index] == '$')
					foundVar = "$";

				index++;
			}

			command = command.Split(" ")[0] + " " + commandNoFirstArg;
		}
	}
}