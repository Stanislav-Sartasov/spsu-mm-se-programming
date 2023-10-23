using ProcessManager;
using FiberSchedulers;

namespace FiberSwitchingTests
{
	public class ProcessManagerTests
	{
		[Test]
		public void NoProcessesTest()
		{
			ProcessManager.ProcessManager.RunProcesses(new List<Process>(), new NonPrioritisedScheduler());

			Assert.Pass();
		}

		[Test]
		public void AllProcessesRunTest()
		{
			List<Process> processes = new List<Process>();
			for (int i = 0; i < 10; i++)
			{
				processes.Add(new Process());
			}

			var scheduler = new NonPrioritisedScheduler();

			ProcessManager.ProcessManager.RunProcesses(processes, scheduler);

			Assert.Null(scheduler.SelectNextFiber());
		}
	}
}
