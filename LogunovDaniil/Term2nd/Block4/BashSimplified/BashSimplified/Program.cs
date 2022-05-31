using BashSimplified.BashLibrary;
using BashSimplified.Commands;
using BashSimplified.IOLibrary;

namespace BashSimplified
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to BashSimplified!");

			var bash = new Bash(new ConsoleReader(), new ConsoleWriter());
			bash.AddCommand("wc", new Wc());
			bash.AddCommand("cat", new Cat());
			bash.AddCommand("pwd", new Pwd());
			bash.AddCommand("echo", new Echo());
			bash.AddCommand("quit", new Exit());
			bash.StartMainLoop();

			Console.WriteLine("Goodbye!");
		}
	}
}