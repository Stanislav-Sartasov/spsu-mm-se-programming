namespace Fibers.ProcessManager;

public static class ProcessManager
{
    private static IScheduler processScheduler = new DynamicPriorityScheduler();
    public static void Run(IScheduler? scheduler = null)
    {
        processScheduler = scheduler ?? processScheduler;
        processScheduler.Run();
        processScheduler.Dispose();
    }

    public static void Run()
    {
        Run(null);
    }

    public static void ChangeScheduler(IScheduler scheduler)
    {
        processScheduler = scheduler ?? throw new NullReferenceException();
    }

    public static void Switch(bool fiberFinished)
    {
        Thread.Sleep(10);

        if (fiberFinished)
        {
            processScheduler.RemoveFinished();
        }

        processScheduler.Run();
    }
}
