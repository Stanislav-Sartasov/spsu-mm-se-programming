using NUnit.Framework;
using Enumerations;
using System.Collections.Generic;

namespace Task_1.UnitTests
{
    public class ProcessStartCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash("../../../TestFolder/").WithCurrentArguments(new List<string> { "somethimg.exe" });
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.ProcessStart, bash);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);

            Assert.Pass();
        }
    }
}
