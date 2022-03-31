namespace Roulette.UnitTests;
using NUnit.Framework;

public class BetTests
{
    [Test]
    public void InitTest()
    {
        Bet bet = new Bet(BetType.Number, 20, 1000);

        Assert.AreEqual(BetType.Number, bet.Type);
        Assert.AreEqual(20, bet.Number);
        Assert.AreEqual(1000, bet.Sum);
    }
}
