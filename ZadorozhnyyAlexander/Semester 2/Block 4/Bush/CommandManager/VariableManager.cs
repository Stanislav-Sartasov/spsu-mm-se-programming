using System.Text.RegularExpressions;

namespace CommandManager
{
    public class VariableManager
    {
        private Dictionary<String, String> variableStorage = new Dictionary<String, String>();
		private char[] alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890_-".ToCharArray();

		public List<String> ReplaceVariables(List<String> splittedCommand)
		{
			return splittedCommand.Select(x => ReplaceAllVariables(x)).ToList();
		}

		public void TryToAssignmentVariable(List<String> splittedCommand)
		{
			if (splittedCommand.Count(x => x != "") != 2 || !splittedCommand.First().StartsWith("$"))
				throw new InvalidOperationException("Uncorrect assightment. It should be: $variable=value");

			if (splittedCommand[1].StartsWith('"') && splittedCommand[1].EndsWith('"'))
				splittedCommand[1] = splittedCommand[1].Substring(1, splittedCommand[1].Length - 2);

			AddVariable(splittedCommand[0], splittedCommand[1]);
		}

		private void AddVariable(String name, String value)
        {
            if (!variableStorage.ContainsKey(name))
                variableStorage.Add(name, value);
        }

		private string TryReplace(String stroke, String possibleVariable)
		{
			var regex = new Regex(Regex.Escape(possibleVariable));
			if (variableStorage.ContainsKey(possibleVariable))
				return regex.Replace(stroke, variableStorage[possibleVariable], 1);

			throw new KeyNotFoundException($"{possibleVariable} variable not found");
		}

		private string ReplaceAllVariables(String stroke)
		{
			string possibleVariable = "$";
			int index = stroke.IndexOf('$');

			while (index != -1)
            {
				for (int symbol = index + 1; symbol < stroke.Length + 1; symbol++)
					if (symbol != stroke.Length && alphabet.Contains(stroke[symbol]))
						possibleVariable += stroke[symbol];
					else
					{
						stroke = TryReplace(stroke, possibleVariable);
						possibleVariable = "$";
						break;
					}
				index = stroke.IndexOf('$');
			}

			return stroke;
		}
	}
}