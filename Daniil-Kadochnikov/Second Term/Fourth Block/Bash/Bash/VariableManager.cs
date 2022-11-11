namespace Bash
{
	public class VariableManager
	{
		private char[] forbiddenSymbols = { '$', ' ' };
		private Dictionary<string, string> variables = new Dictionary<string, string>();

		public void AssignVariable(string command)
		{
			string[] arguments = command.Substring(1).Split("=", 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			if (arguments.Length != 2)
				throw new Exception("Mistake in syntax. Variable \"" + arguments[0] + "\" was not defined.\nOne or both of the argumnets were not provided.");

			foreach (char symbol in forbiddenSymbols)
				if (arguments[0].Contains(symbol))
					throw new Exception("Mistake in syntax. Variable \"" + arguments[0] + "\" was not defined.\nThe name of the variable can't contain \"" + symbol + "\".");

			variables[arguments[0]] = arguments[1];
		}

		public void ReplaceVariables(ref string[] arguments)
		{
			for (int i = 1; i < arguments.Length; i++)
				if (arguments[i][0] == '$')
				{
					string variable = arguments[i].Substring(1);
					string? value;
					if (!variables.TryGetValue(variable, out value))
						throw new Exception("Unknown variable \"" + variable + "\".");
					arguments[i] = value;
				}
		}
	}
}
