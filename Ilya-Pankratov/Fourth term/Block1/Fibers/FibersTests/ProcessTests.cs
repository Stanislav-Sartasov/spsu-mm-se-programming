using Fibers.ProcessManager;

namespace FibersTests
{
    public class ProcessTests
    {
        private Process testedProcess;

        [SetUp]
        public void Init()
        {
            testedProcess = new Process();
        }

        [Test]
        public void AssignmentTest()
        {
            Assert.IsNotNull(testedProcess);
            Assert.GreaterOrEqual(testedProcess.Priority, 0);
            Assert.GreaterOrEqual(testedProcess.ActiveDuration, 0);
            Assert.GreaterOrEqual(testedProcess.TotalDuration, 0);
        }

    }
}
