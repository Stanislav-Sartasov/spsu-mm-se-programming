using Blackjack.Players;

namespace Bots;

public class RiskyBot : Player
{
	public RiskyBot(string name, int money) : base(name, money) {}

	public override void MakeBet()
	{
		Bet = Money < 500 ? 10 : Money / 3;
		base.MakeBet();
	}
}