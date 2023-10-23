using FiberSchedulers;
using ProcessManager;

public static class Program
{
	public static void Main()
	{
		List<Process> processes = new List<Process>();
		for (int i = 0; i < 10; i++) processes.Add(new Process());

		Console.WriteLine($"Running {processes.Count} processes with priority enabled");
		ProcessManager.ProcessManager.RunProcesses(processes, new PrioritisedScheduler());

		Console.WriteLine();

		Console.WriteLine($"Running {processes.Count} processes with priority disabled");
		ProcessManager.ProcessManager.RunProcesses(processes, new NonPrioritisedScheduler());
	}
}