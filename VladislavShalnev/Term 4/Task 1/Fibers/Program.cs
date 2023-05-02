using System;

namespace Fibers
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This program is a process manager with priority and non-priority modes.");

			List<Process> processes = new List<Process>();

			for (int i = 0; i < 4; i++)
				processes.Add(new Process());

			Console.WriteLine("Starting manager with non-priority mode");
			ProcessManager.Start(processes, false);
			
			Console.WriteLine("Starting manager with priority mode");
			ProcessManager.Start(processes, true);
		}
	}
}