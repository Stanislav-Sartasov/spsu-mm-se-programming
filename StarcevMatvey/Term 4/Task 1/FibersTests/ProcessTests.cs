using System.Runtime.InteropServices;
using FiberLib;
using NUnit.Framework;

namespace FibersTests
{
    public class ProcessTests
    {
        private Process testProcess;

        [SetUp]
        public void Init()
        {
            testProcess = new Process();
        }

        [Test]
        public void GetTottalDurationTest()
        {
            Assert.NotNull(testProcess.TotalDuration);
        }

        [Test]
        public void GetTottalActiveTest()
        {
            Assert.NotNull(testProcess.ActiveDuration);
        }
    }
}
