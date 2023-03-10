using Fibers.Fibers;

namespace Fibers.ProcessManager;

public static class ProcessManager
{
    private static FiberScheduler scheduler = new DefaultScheduler();

    public static void Run(List<Process> processes, FiberScheduler? fiberScheduler = null)
    {
        scheduler = fiberScheduler ?? scheduler;
        processes.ForEach(process => scheduler.ScheduleFiber(new Fiber(process.Run)));
        scheduler.Execute();
        scheduler.Dispose();
    }

    public static void Switch(bool fiberFinished)
    {
        if (fiberFinished) scheduler.RemoveRunningFiber();

        scheduler.RunNextFiber();
    }
}