using System.Text.RegularExpressions;

namespace Bash.Commands;

public class WcCommand : ICommand
{
	public string Trigger => "wc";
	
	public string[] Run(string[] args)
	{
		var result = new List<string> { "filename\tlines\twords\tsize" };

		foreach (var arg in args)
		{
			string text = File.ReadAllText(arg);
			int lines = text.Split(Environment.NewLine).Length;
			int words = Regex.Split(text, $"[ {Environment.NewLine}]+").Length; // Splitting with spaces, \n or \r\n
			int bytes = File.ReadAllBytes(arg).Length;
			
			result.Add($"{arg}\t{lines}\t{words}\t{bytes} bytes");
		}
			

		return result.ToArray();
	}
}