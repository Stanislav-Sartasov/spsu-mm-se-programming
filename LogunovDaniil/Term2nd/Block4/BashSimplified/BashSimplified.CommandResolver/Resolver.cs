namespace BashSimplified.CommandResolver
{
	public class Resolver
	{
		private Dictionary<string, string> variables = new Dictionary<string, string>();
		private string forbiddenSymbols = "\\|$?<>:;=";

		public CommandData? ResolveCommand(string cmdLine)
		{
			var quotes = cmdLine.Split('"');
			bool isInQuotes = false;
			var args = new List<string>();
			foreach (var quote in quotes)
			{
				if (quote.Length == 0)
					continue;

				var tokens = quote.Split(' ')
					.Select(s => ResolveToken(s));

				if (isInQuotes)
					args.Add(string.Join(" ", tokens));
				else
					foreach (var token in tokens.Where(s => !string.IsNullOrWhiteSpace(s)))
						args.Add(token);

				isInQuotes = !isInQuotes;
			}
			if (args.Count == 0)
				return null;
			return new CommandData(args[0], args.ToArray()[1..]);
		}

		private string ResolveToken(string token)
		{
			if (token.Length == 0 || token[0] != '$')
				return token;

			token = token[1..];

			if (token.Contains('='))
			{
				var assign = token.Split('=', 2);
				for (int i = 0; i < assign.Length; i++)
				{
					for (int j= 0; j < assign[i].Length; j++)
					{
						if (forbiddenSymbols.Contains(assign[i][j]))
							return string.Empty;
					}
				}
				if (assign[0].Length == 0)
					return string.Empty;
				SetVariable(assign[0], assign[1]);
				return assign[1];
			}

			if (!variables.ContainsKey(token))
			{
				var env = Environment.GetEnvironmentVariable(token);
				return env != null ? env : string.Empty;
			}

			return variables[token];
		}

		private void SetVariable(string name, string value)
		{
			if (variables.ContainsKey(name))
				variables[name] = value;
			else
				variables.Add(name, value);
		}
	}
}