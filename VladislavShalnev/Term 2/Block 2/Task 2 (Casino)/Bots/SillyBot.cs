﻿using Blackjack.Players;

namespace Bots;

public class SillyBot : Player
{
	public SillyBot(string name, int money)
	{
		Name = name;
		Money = money;
	}

	public override void MakeBet()
	{
		Bet = Money < 500 ? 1 : 10;
		base.MakeBet();
	}
}