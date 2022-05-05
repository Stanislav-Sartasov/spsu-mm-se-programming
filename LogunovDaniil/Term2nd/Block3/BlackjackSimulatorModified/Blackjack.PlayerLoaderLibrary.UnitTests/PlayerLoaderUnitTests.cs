using NUnit.Framework;

namespace Blackjack.PlayerLoaderLibrary.UnitTests
{
	public class PlayerLoaderUnitTests
	{
		private static string TestingDLLs = "..\\..\\..\\TestingDLLs\\";

		[Test]
		public void CorrectPlayerLoadingTest()
		{
			var res = PlayerLoader.LoadFromDLL(TestingDLLs + "ExistingPlayersDLL");
			Assert.NotNull(res);
			Assert.AreEqual(3, res.Length);
		}

		[Test]
		public void NoPlayersLoadingTest()
		{
			var res = PlayerLoader.LoadFromDLL(TestingDLLs + "NoPlayersDLL");
			Assert.NotNull(res);
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void NoDirectoryLoadingTest()
		{
			var res = PlayerLoader.LoadFromDLL(TestingDLLs + "randomdirectory");
			Assert.Null(res);
		}
	}
}