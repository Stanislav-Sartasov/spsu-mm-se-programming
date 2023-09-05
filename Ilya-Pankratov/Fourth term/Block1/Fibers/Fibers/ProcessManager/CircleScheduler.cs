using Fibers.Fibers;

namespace Fibers.ProcessManager
{
    public class CircleScheduler : IScheduler
    {
        private readonly Queue<TaskInfo> taskQueue = new();
        private uint delayedDisposing;
        private bool initialRound = true;
        private bool notDeleted = true;

        public void Add(Process process)
        {
            var fiber = new Fiber(process.Run);
            taskQueue.Enqueue(new TaskInfo(fiber.Id, process.Priority, DateTime.Now));
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
                var task = taskQueue.Peek();
                Fiber.Switch(task.Id);
                return;
            }

            if (notDeleted)
                taskQueue.Enqueue(taskQueue.Dequeue());
            else
                notDeleted = true;

            Fiber.Switch(taskQueue.Peek().Id); 
        }

        public void RemoveFinished()
        {
            if (delayedDisposing != 0)
                Fiber.Delete(delayedDisposing);

            delayedDisposing = taskQueue.Peek().Id;
            taskQueue.Dequeue();
        }

        public void Dispose()
        {
            if (delayedDisposing != 0)
                Fiber.Delete(delayedDisposing);
            delayedDisposing = 0;
        }
    }
}
