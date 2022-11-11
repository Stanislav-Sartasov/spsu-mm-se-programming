namespace Commands
{
	public class CatCommand : ICommand
	{
		public string Name { get; } = "cat";

		public string[]? Execute(string[] arguments)
		{
			List<string> result = new List<string>();
			foreach (string file in arguments)
			{
				if (File.Exists(file))
				{
					try
					{
						result.Add($"File name: {file}");
						foreach (string line in File.ReadAllLines(file))
							result.Add(line);
					}
					catch (Exception ex)
					{
						throw new Exception($"The file \"{file}\" was not found.");
					}
				}

			}
			return result.ToArray();
		}
	}
}