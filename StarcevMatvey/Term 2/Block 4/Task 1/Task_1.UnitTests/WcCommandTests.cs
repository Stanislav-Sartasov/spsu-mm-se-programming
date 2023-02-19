using System.Collections.Generic;
using Enumerations;
using NUnit.Framework;

namespace Task_1.UnitTests
{
    public class WcCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash();
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Wc, bash);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "" });
            bash = executor.ExecuteCommand(Command.Wc, bash);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../TestFolder/test1.txt" });
            bash = executor.ExecuteCommand(Command.Wc, bash);
            Assert.AreEqual(new List<string> { "4 4 10 ../../../TestFolder/test1.txt" }, bash.CurrentArguments);

            Assert.Pass();
        }
    }
}
