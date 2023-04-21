using System.Collections.Generic;
using Enumerations;
using NUnit.Framework;

namespace Task_1.UnitTests
{
    public class SetArgumentCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var args = new Queue<string>();
            args.Enqueue("argument1");
            args.Enqueue("argument2");
            args.Enqueue("argument3");
            var bash = new MyBash().WithArguments(args);
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.SetArgument, bash);
            Assert.AreEqual(new List<string> { "argument1" }, bash.CurrentArguments);
            bash = executor.ExecuteCommand(Command.SetArgument, bash);
            bash = executor.ExecuteCommand(Command.SetArgument, bash);
            Assert.AreEqual(new List<string> { "argument3" }, bash.CurrentArguments);

            Assert.Pass();
        }
    }
}
