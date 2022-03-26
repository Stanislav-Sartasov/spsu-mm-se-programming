namespace Roulette.UnitTests;
using NUnit.Framework;

public class CasinoTests
{
    [Test]
    public void InitTest()
    {
        Casino casino = new Casino(50, 10000);
        Assert.AreEqual(50, casino.MinBetAmount);
        Assert.AreEqual(10000, casino.MaxBetAmount);
    }
}
