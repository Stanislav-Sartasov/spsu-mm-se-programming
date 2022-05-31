namespace Bash.Utils;

public class IOManager : IIOManager
{
	public string Read()
	{
		Console.Write("> ");
		return Console.ReadLine() ?? "";
	}

	public void Write(string message) =>
		Console.WriteLine(message);
}