using NUnit.Framework;
using BashSimplified.CommandResolver;

namespace BashSimplified.CommandResolver.UnitTests
{
	public class CommandResolverTests
	{
		private string someCmd = "echo";
		private string[] someArgs = { "hello", "it's me" };

		[Test]
		public void CommandDataValueRetentionTest()
		{
			var data = new CommandData(someCmd, someArgs);
			Assert.AreEqual(someCmd, data.Command);
			Assert.AreEqual(someArgs, data.Args);
		}

		[Test]
		public void ResolverCommandSplitTest()
		{
			var resolver = new Resolver();
			var data = resolver.ResolveCommand("echo b c");
			Assert.AreEqual("echo", data.Command);
			Assert.AreEqual(2, data.Args.Length);
			Assert.AreEqual("b", data.Args[0]);
			Assert.AreEqual("c", data.Args[1]);
		}

		[Test]
		public void ResolverCommandSplitQuotesTest()
		{
			var resolver = new Resolver();
			var data = resolver.ResolveCommand("echo \"b c\"");
			Assert.AreEqual("echo", data.Command);
			Assert.AreEqual(1, data.Args.Length);
			Assert.AreEqual("b c", data.Args[0]);
		}

		[Test]
		public void ResolverVariableSetTest()
		{
			var resolver = new Resolver();
			resolver.ResolveCommand("$a=b");
			var cmd = resolver.ResolveCommand("echo $a");

			Assert.IsNotNull(cmd);
			Assert.AreEqual(1, cmd.Args.Length);
			Assert.AreEqual("b", cmd.Args[0]);
		}

		[Test]
		public void ResolverForbiddenSymbolsTest()
		{
			var resolver = new Resolver();
			var data = resolver.ResolveCommand("echo $b=$a");
			Assert.AreEqual(0, data.Args.Length);
		}
	}
}