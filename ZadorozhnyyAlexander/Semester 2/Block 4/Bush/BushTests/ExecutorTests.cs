using AbstractOperators;
using CommandManager;
using Commands;
using NUnit.Framework;
using System.Collections.Generic;

namespace BushTests
{
    public class ExecutorTests
    {
        private Dictionary<string, ACommand> commands = new Dictionary<string, ACommand>();
        private Executor executor;

        [Test]
        public void ExecutorFunctionalityTest()
        {
            commands.Add("echo", new Echo());
            
            executor = new Executor(commands, new RunnerForTesting(new LoggerForTesting(new List<string>(), new List<List<string>>())));

            Assert.AreEqual(executor.Execute("", new List<string>(), new List<string>()), new List<string>());

            Assert.AreEqual(executor.Execute("echo", new List<string>() { "123", "456", "789" }, new List<string>()), new List<string>() { "123 456 789" });

            Assert.AreEqual(executor.Execute("not exist", new List<string>() { "123" }, new List<string>() { "123" }), new List<string>() { "Process was complited!" });
        }
    }
}