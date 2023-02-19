using NUnit.Framework;
using Enumerations;
using System.Collections.Generic;

namespace Task_1.UnitTests
{
    public class EchoCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash();
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Echo, bash);
            Assert.AreEqual("", bash.Output);
            bash = new MyBash().WithCurrentArguments(new List<string> { "argument1", "argument2", "argument3" });
            bash = executor.ExecuteCommand(Command.Echo, bash);
            Assert.AreEqual("argument1\nargument2\nargument3\n", bash.Output);

            Assert.Pass();
        }
    }
}
