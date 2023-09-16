using Fibers;
using ProcessManager;

namespace FiberSchedulers
{
	public class PrioritisedScheduler : AFiberScheduler
	{
		private Queue<FiberRecord>[] fiberQueues = new Queue<FiberRecord>[Process.PriorityLevelsNumber];
        private Random rnd = new Random();

        public PrioritisedScheduler()
		{
            for (int i = 0; i < fiberQueues.Length; i++) fiberQueues[i] = new Queue<FiberRecord>();
		}

		public override void QueueFiber(FiberRecord fiber)
		{
			fiberQueues[fiber.Priority].Enqueue(fiber);
		}

		public override FiberRecord? SelectNextFiber()
        {
            var chosenQueue = -1;
            var lastNonEmptyQueue = -1;
            for (int i = Process.PriorityLevelsNumber - 1; i >= 0; i--)
            {
                if (fiberQueues[i].Count > 0)
                {
                    lastNonEmptyQueue = i;
                    if (rnd.NextDouble() > 0.1)
                    {
                        chosenQueue = i;
                        break;
                    }
                }
            }

            if (lastNonEmptyQueue == -1) return null;

            if (chosenQueue == -1) chosenQueue = lastNonEmptyQueue;

            return fiberQueues[chosenQueue].Dequeue();
        }
	}
}
