namespace Commands
{
	public class PwdCommand : ICommand
	{
		public string Name { get; } = "pwd";

		public string[]? Execute(string[] arguments)
		{
			List<string> result = new List<string>();
			string directory = Directory.GetCurrentDirectory();
			result.Add(directory);
			FileInfo[] fileNames = new DirectoryInfo(directory).GetFiles();
			foreach (FileInfo file in fileNames)
			{
				result.Add(file.Name);
			}

			return result.ToArray();
		}
	}
}