using BashLib.Bash;
using BashLib.IO;
using System;

namespace BashApp
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("This app starts simplified bash session\n");
			
			new BashSession(new ConsoleReader(), new ConsoleWriter(), new EnvironmentalExiter()).Start();

			Console.WriteLine("Thanks for using the app!\n");
		}
	}
}