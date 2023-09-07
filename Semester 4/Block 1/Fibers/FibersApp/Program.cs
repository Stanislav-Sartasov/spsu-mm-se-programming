using ProcessManagerFrameworkLib;
using ProcessManagerFrameworkLib.Strategies;

namespace FibersApp
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("This program implements non-priority and priority scheduling using fibers.");

			var processes = new List<Process>();

			for (int i = 0; i < 8; i++)
				processes.Add(new Process());

			ProcessManager.Start(processes, new DefaultStrategy());
			Console.WriteLine("Manager with non-priority strategy successfully finished.");

			ProcessManager.Start(processes, new PriorityStrategy());
			Console.WriteLine("Manager with priority strategy successfully finished.");
		}
	}
}
