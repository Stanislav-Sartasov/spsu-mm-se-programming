using Fibers.ProcessManager;

namespace Fibers.Fibers;

public class PriorityScheduler : IScheduler
{
    private const int constant = 10;
    private readonly Dictionary<long, bool> isFinished = new();
    private readonly Dictionary<long, int> priority = new();

    private readonly PriorityQueue<Fiber, Tuple<int, DateTime>> queue =
        new(Comparer<Tuple<int, DateTime>>.Create((x, y) =>
            y.Item1 - x.Item1 + ((DateTime.Now - y.Item2).Seconds - (DateTime.Now - x.Item2).Seconds) / constant));

    private readonly List<Fiber> depricatedFibers = new();

    public void ScheduleProcess(Process process)
    {
        var fiber = new Fiber(process.Run);
        queue.Enqueue(fiber, new Tuple<int, DateTime>(process.Priority, DateTime.Now));
        isFinished[fiber.Id] = false;
        priority[fiber.Id] = process.Priority;
    }

    public void RemoveFiber()
    {
        if (queue.Count == 0) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        var currentFiber = queue.Peek();
        isFinished[currentFiber.Id] = true;
        depricatedFibers.Add(currentFiber);
    }

    public void RunFiber()
    {
        if (queue.Count == 0) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        var currentFiber = queue.Dequeue();

        if (!isFinished[currentFiber.Id])
            queue.Enqueue(currentFiber, new Tuple<int, DateTime>(priority[currentFiber.Id], DateTime.Now));

        if (queue.Count == 0)
        {
            Fiber.Switch(Fiber.PrimaryId);
            return;
        }

        Fiber.Switch(queue.Peek().Id);
    }

    public void Dispose()
    {
        while (queue.Count > 0)
        {
            var fiber = queue.Dequeue();
            Fiber.Delete(fiber.Id);
        }

        foreach (var fiber in depricatedFibers) Fiber.Delete(fiber.Id);
        depricatedFibers.Clear();
        isFinished.Clear();
        priority.Clear();
    }

}