using FiberLib;

namespace ProcessManagerFrameworkLib.Strategies
{
	public class PriorityStrategy : IStrategy
	{
		private Random random = new Random();
		public Fiber GetNextFiber(List<Fiber> fibers)
		{
			if (random.Next(100) > 20) // Get fiber with max priority and min duration.
			{
				var maxPriority = fibers.Aggregate(fibers[0].Priority, (acc, c) => Math.Max(acc, c.Priority));

				var prioFibers = fibers.FindAll(fiber => fiber.Priority == maxPriority);

				return prioFibers.Aggregate(prioFibers.First(),
					(acc, c) => c.TotalDuration < acc.TotalDuration ? c : acc);
			}
			else // Get random fiber. This branch prevents starvation. It is executed with a lower probability.
			{
				return fibers[random.Next(fibers.Count)];
			}
		}
	}
}
