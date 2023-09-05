using Fibers.Fibers;

namespace Fibers.ProcessManager
{
    public class DynamicPriorityScheduler : IScheduler
    {
        private List<TaskInfo> taskQueue = new();
        private uint delayedDisposing;
        private bool initialRound = true;
        private bool notDeleted = true;

        public void Add(Process process)
        {
            var fiber = new Fiber(process.Run);
            taskQueue.Add(new TaskInfo(fiber.Id, process.Priority, DateTime.Now));
        }

        public void Run()
        {
            if (!taskQueue.Any())
            {
                if (initialRound)
                    throw new InvalidOperationException("You must add at least one process");
                Fiber.Switch(Fiber.PrimaryId);
            }

            if (initialRound)
            {
                initialRound = false;
                var fiberId = taskQueue.First().Id;
                Fiber.Switch(fiberId);
                return;
            }

            if (notDeleted)
            {
                var addToEnd = taskQueue.First();
                taskQueue.RemoveAt(0);
                addToEnd.Paused = true;
                taskQueue.Add(addToEnd);
            }
            else
            {
                notDeleted = true;
            }

            var nextTask = taskQueue.First();

            if (nextTask.Paused)
            {
                if (taskQueue.Count > 1)
                {
                    var time = DateTime.Now;
                    taskQueue = taskQueue.OrderBy(x => CountPriority(x.Priority, x.Time, time, taskQueue.Count))
                        .ToList();
                    taskQueue.ForEach(x => x.Paused = false);
                    nextTask = taskQueue.First();
                }
            }

            Fiber.Switch(nextTask.Id);
        }

        public void RemoveFinished()
        {
            if (delayedDisposing != 0)
                Fiber.Delete(delayedDisposing);

            delayedDisposing = taskQueue.First().Id;
            taskQueue.RemoveAt(0);
            notDeleted = false;
        }

        private static int CountPriority(int priority, DateTime initTime, DateTime finishedTime, int constant)
        {
            return priority + (finishedTime - initTime).Milliseconds / constant;
        }

        public void Dispose()
        {
            if (delayedDisposing != 0)
                Fiber.Delete(delayedDisposing);
            delayedDisposing = 0;
        }
    }
}
