using Fibers.ProcessManager;

namespace Fibers.Fibers;

public class PriorityScheduler : FiberScheduler
{
    private readonly Dictionary<long, bool> fiberIsFinished = new();
    private readonly Dictionary<long, int> priority = new();

    private readonly PriorityQueue<Fiber, Tuple<int, DateTime>> queue =
        new(Comparer<Tuple<int, DateTime>>.Create((x, y) =>
            y.Item1 - x.Item1 + ((DateTime.Now - y.Item2).Seconds - (DateTime.Now - x.Item2).Seconds) / 10));

    private readonly List<Fiber> terminatedFibers = new();

    public void Dispose()
    {
        while (queue.Count > 0)
        {
            var fiber = queue.Dequeue();
            Fiber.Delete(fiber.Id);
        }

        terminatedFibers.ForEach(fiber => Fiber.Delete(fiber.Id));
        terminatedFibers.Clear();
        fiberIsFinished.Clear();
        priority.Clear();
    }

    public void ScheduleProcess(Process process)
    {
        var fiber = new Fiber(process.Run);
        queue.Enqueue(fiber, new Tuple<int, DateTime>(process.Priority, DateTime.Now));
        fiberIsFinished[fiber.Id] = false;
        priority[fiber.Id] = process.Priority;
    }

    public void RemoveRunningFiber()
    {
        if (queue.Count == 0) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        var currentFiber = queue.Peek();
        fiberIsFinished[currentFiber.Id] = true;
        terminatedFibers.Add(currentFiber);
    }

    public void Execute()
    {
        if (queue.Count == 0) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        RunNextFiber();
    }

    public void RunNextFiber()
    {
        if (queue.Count == 0) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        var currentFiber = queue.Dequeue();

        if (!fiberIsFinished[currentFiber.Id])
            queue.Enqueue(currentFiber, new Tuple<int, DateTime>(priority[currentFiber.Id], DateTime.Now));

        if (queue.Count == 0)
        {
            Fiber.Switch(Fiber.PrimaryId);
            return;
        }

        Fiber.Switch(queue.Peek().Id);
    }
}