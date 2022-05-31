namespace Bash.Commands;

public class PwdCommand : ICommand
{
	public string Trigger => "pwd";
	
	public string[] Run(string[]? args = null)
	{
		string currentDirectory = Directory.GetCurrentDirectory();
		
		List<string> result = new() { currentDirectory };
		result.AddRange(Directory
			.GetFiles(currentDirectory)
			.Select(Path.GetFileName)!
		);

		return result.ToArray();
	}
}