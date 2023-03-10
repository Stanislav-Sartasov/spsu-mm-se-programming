namespace Fibers.Fibers;

public interface FiberScheduler : IDisposable
{
    void ScheduleFiber(Fiber fiber);

    void RemoveRunningFiber();

    void Execute();

    void RunNextFiber();
}