namespace Commands
{
	public class WcCommand : ICommand
	{
		public string Name { get; } = "wc";

		public string[]? Execute(string[] arguments)
		{
			List<string> result = new List<string>();

			foreach (string file in arguments)
			{
				if (File.Exists(file))
				{
					List<string> counts = new List<string>();
					try
					{
						result.Add($"File name: {file}");
						counts.Add($"Lines: {File.ReadAllLines(file).Length.ToString()},");
						counts.Add($"Words: {File.ReadAllText(file).Split(" ").Length.ToString()},");
						counts.Add($"Bytes: {new FileInfo(file).Length.ToString()}");
						result.Add(String.Join(' ', counts));
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