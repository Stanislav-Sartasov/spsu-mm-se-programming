using Fibers.ProcessManager;

namespace FibersTests
{
    internal class DynamicPrioritySchedulerTests
    {
        IScheduler scheduler;

        [SetUp]
        public void Setup()
        {
            scheduler = new DynamicPriorityScheduler();
            ProcessManager.ChangeScheduler(scheduler);
        }

        [Test]
        public void RunEmptyTest()
        {
            Assert.Catch(scheduler.Run);
        }

        [Test]
        public void RunIndividualProcessTest()
        {
            scheduler.Add(new Process());
            Assert.DoesNotThrow(ProcessManager.Run);
        }

        [Test]
        public void RunManyProcessTest()
        {
            var random = new Random();
            var processNumber = random.Next(2, 5);
            for (var i = 0; i < processNumber; i++)
                scheduler.Add(new Process());
            Assert.DoesNotThrow(ProcessManager.Run);
        }
    }
}
