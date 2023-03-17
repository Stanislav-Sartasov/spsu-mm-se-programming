using Fibers.ProcessManager;

namespace Fibers.Fibers;

public class DefaultScheduler : FiberScheduler
{
    private readonly Dictionary<long, bool> fiberIsFinished = new();
    private readonly Queue<Fiber> queue = new();
    private readonly List<Fiber> terminatedFibers = new();

    public void Dispose()
    {
        foreach (var fiber in queue) Fiber.Delete(fiber.Id);
        terminatedFibers.ForEach(fiber => Fiber.Delete(fiber.Id));
        fiberIsFinished.Clear();
        queue.Clear();
        terminatedFibers.Clear();
    }

    public void ScheduleProcess(Process process)
    {
        var fiber = new Fiber(process.Run);
        queue.Enqueue(fiber);
        fiberIsFinished[fiber.Id] = false;
    }

    public void Execute()
    {
        if (!queue.Any()) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        RunNextFiber();
    }

    public void RunNextFiber()
    {
        if (!queue.Any()) throw new InvalidOperationException("No fibers scheduled, cannot execute anything");

        var currentFiber = queue.Dequeue();
        if (!fiberIsFinished[currentFiber.Id]) queue.Enqueue(currentFiber);

        if (!queue.Any())
        {
            Fiber.Switch(Fiber.PrimaryId);
            return;
        }

        Fiber.Switch(queue.Peek().Id);
    }

    public void RemoveRunningFiber()
    {
        if (!queue.Any())
            throw new InvalidOperationException("There are no fibers in the execution queue, cannot remove anything");

        fiberIsFinished[queue.Peek().Id] = true;
        terminatedFibers.Add(queue.Peek());
    }
}