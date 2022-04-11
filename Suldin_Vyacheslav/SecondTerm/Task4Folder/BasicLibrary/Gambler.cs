using System;
using System.Collections.Generic;

namespace BasicLibrary
{
	public class Gambler : Gamester
	{
		public Confirmer confirmer = new Confirmer();
		public Gambler(int bank)
		{
			this.bank = bank;
		}
		public override PlayerMove Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
		{
			Console.WriteLine("Make your move");
			PlayerMove answer = confirmer.GetCorectAnswer<PlayerMove>();

			if (answer == PlayerMove.Show)
			{
				Console.WriteLine($"Current hand is {hand}");
				Game.ShowTable(dealerHand, gamesters);
				return this.Answer(hand, dealerHand, gamesters);
			}

			if ((answer == PlayerMove.Double && this.bank < this.bets[hand]) ||
				(answer == PlayerMove.Split && 
				(this[hand].Count != 2 || this[hand][0].GetCardValue() != this[hand][1].GetCardValue())))
				{
					Console.WriteLine("Not available answer!");
					return this.Answer(hand, dealerHand, gamesters);
				}
			else
			{
				Console.WriteLine("Answer taken	");
				return answer;
			}
		}
		public override bool IsNeedResult()
		{
			Console.WriteLine($"Your current bank: {this.bank}");
			Console.WriteLine("Need result?");
			if (confirmer.GetCorectAnswer<AnswerType>() == AnswerType.Yes)
				return true;
			else return false;

		}
		public override void MakeBet(int hand)
		{
			Console.WriteLine($"Enter your bet. Bank : {this.bank}");
			int bet = confirmer.GetCorectInt(0,this.bank);

			bets[hand] = bet;
			this.bank -= bet;

		}

	}
}
