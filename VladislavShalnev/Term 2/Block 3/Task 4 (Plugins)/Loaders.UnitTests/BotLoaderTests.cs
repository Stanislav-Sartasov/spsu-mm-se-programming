using NUnit.Framework;
using System.Collections.Generic;
using Blackjack.Players;
using Bots;

namespace Loaders.UnitTests;

public class BotLoaderTests
{
	[Test]
	public void LoadBotsTest()
	{
		List<Player> bots = BotLoader.Load("../../../Dlls/");
		
		Assert.AreEqual(3, bots.Count);
		
		Assert.AreEqual(typeof(MadBot), bots[0].GetType());
		Assert.AreEqual(typeof(RiskyBot), bots[1].GetType());
		Assert.AreEqual(typeof(SillyBot), bots[2].GetType());
	}
}