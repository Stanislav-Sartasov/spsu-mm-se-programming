using System.Runtime.InteropServices;
using FiberLib;
using NUnit.Framework;

namespace FibersTests
{
    public class FiberTests
    {
        private Process testProcess;
        private Fiber testFiber;

        [SetUp]
        public void Init()
        {
            testProcess = new Process();
            testFiber = new Fiber(testProcess.Run);
        }

        [Test]
        public void GetIdTest()
        {
            Assert.NotNull(testFiber.Id);
        }

        [Test]
        public void GetIsPrimaryTest()
        {
            Assert.True(testFiber.IsPrimary);
        }
    }
}
