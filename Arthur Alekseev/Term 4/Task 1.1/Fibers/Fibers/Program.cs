using Fibers.ProcessManager;

namespace Fibers;

public static class Program
{
	public static void Main()
	{
		List<Process> processes = new();
		for (var i = 0; i < 10; i++) processes.Add(new Process());
		ProcessManager.ProcessManager.Run(processes, AlgorithmType.WithPriority);
	}
}