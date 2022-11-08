using NUnit.Framework;
using Commands;

namespace ParserTests
{
	public class ParserTests
	{
		private string testCommand = "echo";
		private string[] testArgs = { "test", "args" };

		[Test]
		public void ParseTest()
		{
			var data = new Data(testCommand, testArgs);
			Assert.AreEqual(testCommand, data.Command);
			Assert.AreEqual(testArgs, data.Args);

			Parser.Parser parser = new();
			var cmd = parser.ParseCommands("echo test1 test2");
			Assert.AreEqual("echo", cmd.Command);
			Assert.AreEqual(2, cmd.Args.Length);
			Assert.AreEqual("test1", cmd.Args[0]);
			Assert.AreEqual("test2", cmd.Args[1]);

			parser.ParseCommands("$a=b");
			var testcmd = parser.ParseCommands("echo $a");

			Assert.IsNotNull(testcmd);
			Assert.AreEqual(1, testcmd.Args.Length);
			Assert.AreEqual("b", testcmd.Args[0]);

			testcmd = parser.ParseCommands("echo $$");
			Assert.AreEqual(0, testcmd.Args.Length);

			Assert.Pass();
		}
	}
}