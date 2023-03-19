using ProcessManager;

namespace Fibers;

internal class Program
{
    public static void Main()
    {
        var processes = new List<Process>();

        for (int i = 0; i < 4; i++)
        {
            processes.Add(new Process());
        }

        ProcessManager.ProcessManager.Execute(processes, SchedulerStrategy.WithPriority);
    }
}