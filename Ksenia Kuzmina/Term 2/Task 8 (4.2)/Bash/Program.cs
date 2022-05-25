using Bash.Commands;
using System;

namespace Bash
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new Bash(new Logger(), new Exiter()).Run();
		}
	}
}