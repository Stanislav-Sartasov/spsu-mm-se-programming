using NUnit.Framework;
using System.Collections.Generic;
using Enumerations;

namespace Task_1.UnitTests
{
    public class PipelineTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash().WithCurrentArguments(new List<string> { "argument1\nargument2\nargument3" });
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Pipeline, bash);
            Assert.AreEqual(new List<string> { "argument1", "argument2", "argument3" }, bash.CurrentArguments);
            bash = executor.ExecuteCommand(Command.Pipeline, bash);
            Assert.AreEqual(new List<string> { "argument1", "argument2", "argument3" }, bash.CurrentArguments);

            Assert.Pass();
        }
    }
}
