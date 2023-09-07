using FiberLib;
using ProcessManagerFrameworkLib.Strategies;

namespace ProcessManagerFrameworkLib
{
	public static class ProcessManager
	{
		private static List<Fiber> fibers = new List<Fiber>();
		private static Fiber current;
		private static IStrategy strategy;

		public static void Start(List<Process> processes, IStrategy str)
		{
			strategy = str;
			
			foreach (var process in processes)
				fibers.Add(new Fiber(process.Run, process.TotalDuration, process.Priority));

			current = fibers.First();
			Fiber.Switch(current.Id);
		}

		public static void Switch(bool fiberFinished)
		{
			if (fiberFinished)
				fibers.Remove(current);

			if (fibers.Count == 0)
			{
				Fiber.Switch(Fiber.PrimaryId);
				return;
			}
			
			current = strategy.GetNextFiber(fibers);
			Fiber.Switch(current.Id);
		}
	}
}
