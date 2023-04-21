namespace Bash.Commands;

public class CatCommand : ICommand
{
	public string Trigger => "cat";
	
	public string[] Run(string[] args)
	{
		var result = new List<string>();

		foreach (var arg in args)
			result.Add(File.ReadAllText(arg));

		return result.ToArray();
	}
}