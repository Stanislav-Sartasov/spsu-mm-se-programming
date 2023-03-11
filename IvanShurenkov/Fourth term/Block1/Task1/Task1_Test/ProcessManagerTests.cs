using NUnit.Framework;
using ProcessManager;

namespace Task1_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(0, ProcessManagerStrategy.Trivial), Timeout(60000)]
        [TestCase(1, ProcessManagerStrategy.Trivial)]
        [TestCase(3, ProcessManagerStrategy.Trivial)]
        [TestCase(0, ProcessManagerStrategy.Priority)]
        [TestCase(1, ProcessManagerStrategy.Priority)]
        [TestCase(3, ProcessManagerStrategy.Priority)]
        public void TesProcessManager(int cnt_of_processes, ProcessManagerStrategy strategy)
        {
            List<Process> processes = new List<Process>();
            for (int i = 0; i < cnt_of_processes; i++)
            {
                processes.Add(new Process());
            }

            ProcessManager.ProcessManager.Run(processes, strategy);
            ProcessManager.ProcessManager.ClearFibers();

            Assert.Pass();
        }
    }
}