using Fibers.ProcessManager;

namespace Fibers.Fibers;

public interface IScheduler : IDisposable
{
    void ScheduleProcess(Process process);

    void RemoveFiber();

    void RunFiber();
}

