using NUnit.Framework;
using Enumerations;
using System.Collections.Generic;

namespace Task_1.UnitTests
{
    public class CdCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash().WithCurrentArguments(new List<string> { "not a file" });
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Cd, bash);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../TestFolder" });
            bash = executor.ExecuteCommand(Command.Cd, bash);
            Assert.AreEqual("../../../TestFolder/", bash.Path);
            bash = new MyBash("../../../").WithCurrentArguments(new List<string> { "TestFolder" });
            bash = executor.ExecuteCommand(Command.Cd, bash);
            Assert.AreEqual("../../../TestFolder/", bash.Path);

            Assert.Pass();

        }
    }
}
