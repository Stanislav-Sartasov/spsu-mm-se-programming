using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace Bots
{
	public class BotTwo : Player
	{
		public BotTwo(int money) : base(money)
		{
			Money = money;
			Name = "Bot two";
		}

		public override void MakeBet()
		{
			if (Money > 400)
			{
				Hands[0].Bet = 100;
				Money -= 100;
			}
			else if (Money <= 400 && Money >= 100)
			{
				Hands[0].Bet = 50;
				Money -= 50;
			}
			else if (Money < 100 && Money >= 10)
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
			else if (Hands[0].Bet > 100)
			{
				insuranceBet = 1;
			}
			else
			{
				insuranceBet = 0;
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
					else
					{
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