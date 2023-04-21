using System.Diagnostics;
using Bash.Utils;

namespace Bash;

public class Program
{
	public static void Main(string[] args)
	{
		new Core.Bash(
			new IOManager(),
			Environment.Exit,
			Process.Start
		).Run();
	}
}