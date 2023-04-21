using FiberLib;
using NUnit.Framework;

namespace FibersTests
{
    public class FiberManagerTests
    {
        private FiberManager manager;
        private Process testProcess;
        private FiberData testFiberData;

        [SetUp]
        public void Init()
        {
            manager = new FiberManager();
            testProcess = new Process();
            testFiberData = new FiberData(testProcess.Priority, 0, new Fiber(testProcess.Run));
        }

        [Test]
        public void AddTest()
        {
            manager.Add(testProcess);

            Assert.IsFalse(manager.IsEmpty);
        }

        [Test]
        public void RemoveTest()
        {
            manager.Add(testProcess);
            manager.Remove(testFiberData);
            
            Assert.IsTrue(manager.IsEmpty);
        }

        [Test]
        public void GetNextFiberTest()
        {
            manager.Add(testProcess);

            var data = manager.GetNextFiber(SchedulerPriority.NonePrio);
            Assert.NotNull(data);

            data = manager.GetNextFiber(SchedulerPriority.LevelPrio);
            Assert.NotNull(data);

            manager.Remove(testFiberData);

            data = manager.GetNextFiber(SchedulerPriority.NonePrio);
            Assert.Null(data);

        }
    }
}