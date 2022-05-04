using Blackjack.Players;

namespace Bots;

public class MadBot : Player
{
	public MadBot(string name, int money) : base(name, money) {}
	public override void MakeBet()
	{
		Bet = new Random(DateTime.Now.Millisecond).Next(1, Money / 4);
		base.MakeBet();
	}
}