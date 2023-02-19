using NUnit.Framework;
using Enumerations;
using Task_1.Commands;

namespace Task_1.UnitTests
{
    public class ExecuterTests
    {
        [Test]
        public void GetCommandTest()
        {
            var executor = new Executer();
            Assert.AreEqual("Task_1.Commands.ExitCommand", executor.GetCommand(Command.Exit).ToString());
            Assert.AreEqual("Task_1.Commands.PwdCommand", executor.GetCommand(Command.Pwd).ToString());
            Assert.AreEqual("Task_1.Commands.EchoCommand", executor.GetCommand(Command.Echo).ToString());
            Assert.AreEqual("Task_1.Commands.CatCommand", executor.GetCommand(Command.Cat).ToString());
            Assert.AreEqual("Task_1.Commands.WcCommand", executor.GetCommand(Command.Wc).ToString());
            Assert.AreEqual("Task_1.Commands.WhoamiCommand", executor.GetCommand(Command.Whoami).ToString());
            Assert.AreEqual("Task_1.Commands.CdCommand", executor.GetCommand(Command.Cd).ToString());
            Assert.AreEqual("Task_1.Commands.PipelineCommand", executor.GetCommand(Command.Pipeline).ToString());
            Assert.AreEqual("Task_1.Commands.SetLocalVarCommand", executor.GetCommand(Command.SetLocalVar).ToString());
            Assert.AreEqual("Task_1.Commands.ClearCommand", executor.GetCommand(Command.Clear).ToString());
            Assert.AreEqual("Task_1.Commands.SetArgumentCommand", executor.GetCommand(Command.SetArgument).ToString());
            Assert.AreEqual("Task_1.Commands.ProcessStartCommand", executor.GetCommand(Command.ProcessStart).ToString());
            Assert.AreEqual("Task_1.Commands.ExitCommand", executor.GetCommand(Command.Exit).ToString());

            Assert.Pass();
        }
    }
}