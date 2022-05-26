using System.Text.RegularExpressions;


namespace CommandManager
{
    public class Splitter
    {
        public Tuple<bool, List<String>> ParseToSingleCommands(string command)
        {
			if (command.Contains("="))
            {
				return Tuple.Create(false, Regex.Split(command, "=").ToList());
            }

			List<String> result = new List<String>() { "" };
			bool flag = true; 

			foreach (var symbol in command)
            {
				switch (symbol)
                {
					case '"':
						flag = !flag;
						result[0] += symbol;
						break;
					case '|' when flag:
						result = result.Prepend("").ToList();
						break;
					default:
						result[0] += symbol;
						break;
				}
            }

			result.Reverse();
			return Tuple.Create(true, result);
		}

        public Tuple<String, List<String>> ParseSubCommand(string command)
        {
			string pattern = @"\s+(?=([^""]*""[^""]*"")*[^""]*$)";
			command = Regex.Replace(command.Trim(), pattern, " ");

			List<String> result = new List<String>() { "" };
			bool flag = true;

			foreach (var symbol in command)
            {
				switch (symbol)
                {
					case '"':
						flag = !flag;
						break;
					case ' ' when (flag && result[0] != ""):
						result = result.Prepend("").ToList();
						break;
					default:
						result[0] += symbol;
						break;
				}
            }

			result.Reverse();
			return Tuple.Create(result[0], result.GetRange(1, result.Count - 1));
		}
    }
}
