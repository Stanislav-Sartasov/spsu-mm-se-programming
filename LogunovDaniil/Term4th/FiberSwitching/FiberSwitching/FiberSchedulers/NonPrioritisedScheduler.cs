using Fibers;

namespace FiberSchedulers
{
	public class NonPrioritisedScheduler : AFiberScheduler
	{
		private Queue<FiberRecord> fibers = new Queue<FiberRecord>();

		public override void QueueFiber(FiberRecord fiber)
		{
			fibers.Enqueue(fiber);
		}

		public override FiberRecord? SelectNextFiber()
		{
			if (fibers.Count == 0) return null;
			return fibers.Dequeue();
		}
	}
}
