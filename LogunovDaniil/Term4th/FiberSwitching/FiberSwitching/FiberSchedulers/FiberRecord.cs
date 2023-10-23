using Fibers;

namespace FiberSchedulers
{
	public class FiberRecord
	{
		public Fiber Fiber { get; set; }
		public int Priority { get; set; }

		public FiberRecord(Fiber fiber, int priority)
		{
			Fiber = fiber;
			Priority = priority;
		}
	}
}
