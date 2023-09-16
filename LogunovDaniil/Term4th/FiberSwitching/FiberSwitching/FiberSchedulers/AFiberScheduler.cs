using Fibers;

namespace FiberSchedulers
{
	public abstract class AFiberScheduler
	{
		public abstract void QueueFiber(FiberRecord fiber);

		public abstract FiberRecord? SelectNextFiber();
	}
}
