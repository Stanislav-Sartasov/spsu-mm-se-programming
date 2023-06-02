using Fibers.ProcessManager;

namespace Fibers.Fibers;

public class DefaultScheduler : IScheduler
{
    private readonly Queue<Fiber> queue = new();
    private readonly Dictionary<long, bool> isFinished = new();
    private readonly List<Fiber> depricatedFibers = new();

    public void ScheduleProcess(Process process)
    {
        var fiber = new Fiber(process.Run);
        queue.Enqueue(fiber);
        isFinished[fiber.Id] = false;
    }

    public void RunFiber()
    {
        if (!queue.Any()) throw new InvalidOperationException("No fibers scheduled, cannot run anything");

        var currentFiber = queue.Dequeue();
        if (!isFinished[currentFiber.Id]) queue.Enqueue(currentFiber);

        if (!queue.Any())
        {
            Fiber.Switch(Fiber.PrimaryId);
            return;
        }

        Fiber.Switch(queue.Peek().Id);
    }

    public void RemoveFiber()
    {
        isFinished[queue.Peek().Id] = true;
        depricatedFibers.Add(queue.Peek());
    }

    public void Dispose()
    {
        foreach (var fiber in queue) Fiber.Delete(fiber.Id);
        foreach (var fiber in depricatedFibers) Fiber.Delete(fiber.Id);
        isFinished.Clear();
        queue.Clear();
        depricatedFibers.Clear();
    }

}