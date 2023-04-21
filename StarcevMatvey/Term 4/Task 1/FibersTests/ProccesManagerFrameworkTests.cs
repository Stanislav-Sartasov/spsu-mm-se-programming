using System.Security.Cryptography;
using FiberLib;
using NUnit.Framework;

namespace FibersTests
{
    public class ProccesManagerFrameworkTests
    {
        private Process testProcess;

        [SetUp]
        public void Init()
        {
            testProcess = new Process();
        }

        [Test]
        public void AddTest()
        {
            Assert.DoesNotThrow(() => ProcessManager.Add(testProcess));
        }

        [Test]
        public void DisposeTest()
        {
            Assert.DoesNotThrow(() => ProcessManager.Dispose());
        }

        [Test]
        public void ExecTest()
        {
            Assert.Catch(() => ProcessManager.Exec(SchedulerPriority.NonePrio));
        }

        [Test]
        public void SwitchTest()
        {
            Assert.Catch(() => ProcessManager.Switch(true));
        }
    }
}
