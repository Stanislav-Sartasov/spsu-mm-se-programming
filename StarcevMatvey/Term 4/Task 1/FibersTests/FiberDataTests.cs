using System.Security.Cryptography;
using FiberLib;
using NUnit.Framework;

namespace FibersTests
{
    public class FiberDataTests
    {
        private Process testProcess;
        private FiberData testFiberData;

        [SetUp]
        public void Init()
        {
            testProcess = new Process();
            testFiberData = new FiberData(testProcess.Priority, 0, new Fiber(testProcess.Run));
        }

        [Test]
        public void GetIdTest()
        {
            Assert.AreEqual(0, testFiberData.Id);
        }

        [Test]
        public void GetPrioTest()
        {
            Assert.AreEqual(testProcess.Priority, testFiberData.Prio);
        }

        [Test]
        public void GetFiberTest()
        {
            Assert.NotNull(testFiberData.Fiber);
        }
    }
}
