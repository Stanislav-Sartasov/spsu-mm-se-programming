using NUnit.Framework;
using Fibers;
using ProcessManager;

namespace ProcessManagerTests
{
    public class ProcessManagerTests
    {
        [Test]
        public void PriorityTest()
        {
            var processes = new List<Process>();
            for (int i = 0; i < 4; i++)
            {
                processes.Add(new Process());
            }
            ProcessManager.ProcessManager.Execute(processes, SchedulerStrategy.WithPriority);

            Assert.Pass();
        }

        [Test]
        public void NoPriorityTest()
        {
            var processes = new List<Process>();
            for (int i = 0; i < 4; i++)
            {
                processes.Add(new Process());
            }
            ProcessManager.ProcessManager.Execute(processes, SchedulerStrategy.NoPriority);

            Assert.Pass();
        }
    }
}