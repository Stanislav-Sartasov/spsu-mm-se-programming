using System;
using BashLib.Bash;
using BashLib.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BashTests
{
	[TestClass]
	public class BashTests
	{
		[TestMethod]
		public void BashSessionTest()
		{

			var reader = new Mock<IReader>();
			reader.SetupSequence(r => r.ReadLine()).Returns("echo echooo").Returns("unknowncommand").Returns("exit");

			var writer = new Mock<IWriter>();

			var exiter = new Mock<IExiter>();
			exiter.Setup(e => e.Exit()).Throws(new Exception());

			try
			{
				new BashSession(reader.Object, writer.Object, exiter.Object).Start();
			}
			catch
			{
				writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Exactly(3));
			}
		}

		[TestMethod]
		public void ResolvedCommandTest()
		{
			var name = "echo";
			var arguments = new string[] { "argument1", "argument2" };


			var resolvedCommand = new ResolvedCommand(name, arguments);
			
			Assert.AreEqual(name, resolvedCommand.Name);
			Assert.AreEqual(arguments, resolvedCommand.Args);
		}

		[TestMethod]
		public void SessionManagerTest()
		{
			var name = "echo";
			var arguments = new string[] { "resolve me", "totally" };

			var manager = new SessionManager();
			var resolvedCommand = manager.ResolveCommand($" {name} $a=\"{arguments[0]}\" {arguments[1]}");

			Assert.AreEqual(name, resolvedCommand.Name);
			Assert.AreEqual(arguments[0], resolvedCommand.Args[0]);
			Assert.AreEqual(arguments[1], resolvedCommand.Args[1]);
		}
	}
}