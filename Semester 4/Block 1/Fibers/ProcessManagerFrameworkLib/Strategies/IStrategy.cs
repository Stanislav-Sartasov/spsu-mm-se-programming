using FiberLib;

namespace ProcessManagerFrameworkLib.Strategies
{
	public interface IStrategy
	{
		public Fiber GetNextFiber(List<Fiber> fibers);
	}
}
