using System.Collections.Generic;
using Enumerations;
using NUnit.Framework;

namespace Task_1.UnitTests
{
    public class PwdCommandTests
    {
        [Test]
        public void ExecuteCommandTest()
        {
            var bash = new MyBash("../../../TestFolder/TestFolder/");
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Pwd, bash);
            Assert.AreEqual(new List<string> { "../../../TestFolder/TestFolder/", "test.txt", "test1.txt", "test2.txt" }, bash.CurrentArguments);

            Assert.Pass();
        }
    }
}
