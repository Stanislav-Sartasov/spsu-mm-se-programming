using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace Bots
{
	public class Bot : Player
	{
		public Bot(int money) : base(money)
		{
			Money = money;
			Name = "Bot";
		}

		public override void MakeBet()
		{
			if (Money > 10)
			{
				Hands[0].Bet = 10;
				Money -= 10;
			}
			else
			{
				Hands[0].Bet = 1;
				Money -= 1;
			}
		}

		public override void GetInsurance(Dealer dealer)
		{
			int insuranceBet;

			if (Hands[0].Score == 21)
			{
				insuranceBet = 0;
			}
			else
			{
				insuranceBet = 1;
			}

			if (insuranceBet == 1)
			{
				Console.WriteLine(Name + " got an insurance.");
			}
			else
			{
				Console.WriteLine(Name + " refused insurance");
			}

			if (insuranceBet == 0)
			{
				if (dealer.Score == 21)
				{
					if (Hands[0].Score == 21)
					{
						Money += Hands[0].Bet;
						Hands[0].Bet = 0;
					}
				}
			}
			else
			{
				if (dealer.Score == 21)
				{
					Money += Hands[0].Bet;
					Hands[0].Bet = 0;
				}
				else
				{
					Hands[0].Bet = Hands[0].Bet / 2;
				}
			}
		}
	}
}