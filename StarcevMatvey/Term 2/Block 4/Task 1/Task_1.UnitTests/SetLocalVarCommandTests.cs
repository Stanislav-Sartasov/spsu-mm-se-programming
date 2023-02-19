using System.Collections.Generic;
using Enumerations;
using NUnit.Framework;

namespace Task_1.UnitTests
{
    public class SetLocalVarCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var args = new Queue<(string name, string value)>();
            args.Enqueue(("a", "aa"));
            args.Enqueue(("a", "aa"));
            var bash = new MyBash().WithArgumentsForSetLocalVar(args);
            var executor = new Executer();
            var d = new Dictionary<string, string>
            {
                { "a", "aa" }
            };
            bash = executor.ExecuteCommand(Command.SetLocalVar, bash);
            Assert.AreEqual(d, bash.LocalVar);
            bash = executor.ExecuteCommand(Command.SetLocalVar, bash);
            Assert.AreEqual(d, bash.LocalVar);

            Assert.Pass();
        }
    }
}
