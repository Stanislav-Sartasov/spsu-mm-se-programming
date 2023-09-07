using FiberLib;

namespace ProcessManagerFrameworkLib.Strategies
{
	public class DefaultStrategy : IStrategy
	{
		private Random random = new Random();
		public Fiber GetNextFiber(List<Fiber> fibers)
		{
			return fibers[random.Next(fibers.Count)];
		}
	}
}
