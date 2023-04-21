using ProcessManager;

namespace Task1.UnitTests
{
    public class FibersTest
    {
        [Test]
        public void PriorityTest()
        {
            var processes = new List<Process>();
            for (int i = 0; i < 4; i++)
            {
                processes.Add(new Process());
            }
            ProcessManager.ProcessManager.Execute(processes, Priority.Classic);
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
            ProcessManager.ProcessManager.Execute(processes, Priority.None);
            Assert.Pass();
        }
    }
}