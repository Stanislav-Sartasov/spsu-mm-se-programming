using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
	public class Gambler : Gamester
	{
		public Gambler(int bank)
		{
			this.Bank = bank;
		}
		public override int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters, Shoes shoes)
		{

			int answer = Game.GetCoorectAnswer(0,5);

			if (answer == 5)
			{
				Console.WriteLine($"Current hand is {hand}");
				Game.ShowTable(dealerHand, gamesters);
				return this.Answer(hand, dealerHand, gamesters, shoes);
			}
			if ((answer == 2 && this.Bank < this.Bets[hand]) ||
				(answer == 3 && 
				(this.Hands[hand].Count != 2 || this.Hands[hand][0].Value != this.Hands[hand][1].Value)))
				{
					Console.WriteLine("Not available answer!");
					return this.Answer(hand, dealerHand, gamesters, shoes);
				}
			else
			{
				Console.WriteLine("Answer taken	");
				return answer;

			}
		}
        public override bool IsNeedResult()
        {
			Console.WriteLine("Need result?");
			if (Game.GetCoorectAnswer(0, 1) == 1)
				return true;
			else return false;

        }
        public override void MakeBet(int hand)
		{
			Console.WriteLine($"Enter your bet. Bank : {this.Bank}");
			int bet = Game.GetCoorectAnswer(0,this.Bank);

			Bets[hand] = bet;
			this.Bank -= bet;

		}
	}
}
