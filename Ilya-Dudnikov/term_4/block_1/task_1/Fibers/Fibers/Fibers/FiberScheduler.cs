using Fibers.ProcessManager;

namespace Fibers.Fibers;

public interface FiberScheduler : IDisposable
{
    void ScheduleProcess(Process process);

    void RemoveRunningFiber();

    void Execute();

    void RunNextFiber();
}