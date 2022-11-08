using System;

namespace Bash
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program runs a simplified version of bash");
			var bash = Bash.BashInit();
			bash.Start();
		}
	}
}