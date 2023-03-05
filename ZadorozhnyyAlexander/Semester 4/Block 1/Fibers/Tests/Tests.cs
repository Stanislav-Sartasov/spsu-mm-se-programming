using NUnit.Framework;
using Fibers;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Timeout(60000)]
        public void ProcessesWithNonPriorityTest()
        {
            var pm = new ProcessManager(Priorities.NonPriority);
            for (int i = 0; i < 10; i++)
            {
                Process process = new Process(pm);
                pm.AddProcess(process);
            }

            pm.RunProcess();
            pm.Dispose();

            Assert.Pass();
        }

        [Test, Timeout(60000)]
        public void ProcessesWithPriorityTest()
        {
            var pm = new ProcessManager(Priorities.Priority);
            for (int i = 0; i < 10; i++)
            {
                Process process = new Process(pm);
                pm.AddProcess(process);
            }

            pm.RunProcess();
            pm.Dispose();

            Assert.Pass();
        }
    }
}