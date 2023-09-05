using NUnit.Framework;
using System.Collections.Generic;
using Enumerations;

namespace Task_1.UnitTests
{
    public class CatCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash();
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Cat, bash);
            Assert.AreEqual(0, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "" });
            bash = executor.ExecuteCommand(Command.Cat, bash);
            Assert.AreEqual(2, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../TestFolder/test1.txt" });
            bash = executor.ExecuteCommand(Command.Cat, bash);
            Assert.AreEqual(new List<string> { "1\r\n2\r\n3\r\n4" }, bash.CurrentArguments);
            bash = new MyBash("../../../TestFolder/").WithCurrentArguments(new List<string> { "test1.txt", "test2.txt" });
            bash = executor.ExecuteCommand(Command.Cat, bash);
            Assert.AreEqual(new List<string> { "1\r\n2\r\n3\r\n4", "5\r\n6\r\n7\r\n8" }, bash.CurrentArguments);
            
            Assert.Pass();
        }
    }
}
