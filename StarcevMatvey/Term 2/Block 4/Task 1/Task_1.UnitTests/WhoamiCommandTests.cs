using System.Collections.Generic;
using Enumerations;
using NUnit.Framework;
using System;

namespace Task_1.UnitTests
{
    public class WhoamiCommandTests
    {
        [Test]
        public void ExecuteTest()
        {
            var bash = new MyBash();
            var executor = new Executer();
            bash = executor.ExecuteCommand(Command.Whoami, bash);
            Assert.AreEqual(new List<string> { Environment.UserName }, bash.CurrentArguments);

            Assert.Pass();
        }
    }
}
