using System;
using Roulette;

namespace Bots
{
	public class MartingaleBot : APlayer
	{
		public MartingaleBot()
		{
			name = "MartingaleBot";
			cash = 100000;
			oldCash = 100000;
			currentBet = 100;
			typeOfBet = TypeOfBet.Even;
		}

		public override void Bet()
		{
			if (oldCash < cash)
				currentBet = 100;
			else
				currentBet = oldCash - cash + 100;
			if (cash < currentBet)
			{
				while (cash < currentBet && currentBet > 100)
					currentBet /= 2;
			}
			else if (cash < 100)
			{
				Console.WriteLine("Martingale is bankrupt");
			}
		}
	}
}
