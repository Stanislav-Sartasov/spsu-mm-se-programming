using Fibers.Fibers;

namespace Fibers.ProcessManager;

public static class ProcessManager
{
    private static IScheduler scheduler = new DefaultScheduler();

    public static void Run(List<Process> processes, IScheduler? iScheduler = null)
    {
        scheduler = iScheduler ?? scheduler;
        /*foreach (var process in processes) scheduler.ScheduleProcess(process);*/
        processes.ForEach(process => scheduler.ScheduleProcess(process));
        scheduler.RunFiber();
        scheduler.Dispose();
    }

    public static void Switch(bool fiberFinished)
    {
        if (fiberFinished) scheduler.RemoveFiber();
        scheduler.RunFiber();
    }
}

