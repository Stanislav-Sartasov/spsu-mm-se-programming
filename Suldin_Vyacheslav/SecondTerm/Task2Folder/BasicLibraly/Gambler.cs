using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibraly
{
	public class Gambler : Gamester
	{
		public Gambler(int bank)
		{
			this.Bank = bank;
		}
		public override int Answer(int hand, List<Card> dilerHand, List<Gamester> gamesters, Shoes shoes)
		{

			int answer = Game.GetCoorectAnswer(0,5);

			if (answer == 5)
			{
				this.OpenEyes(hand,dilerHand, gamesters);
				return this.Answer(hand, dilerHand, gamesters, shoes);
			}
			if ((answer == 2 && this.Bank < this.Bets[hand]) ||
				(answer == 3 && 
				(this.Hands[hand].Count != 2 || this.Hands[hand][0].Value != this.Hands[hand][1].Value)))
				{
					Console.WriteLine("Not available answer!");
					return this.Answer(hand, dilerHand, gamesters, shoes);
				}
			else
			{
				Console.WriteLine("Answer taken	");
				return answer;

			}
		}

		public void OpenEyes(int hand, List<Card> dilerHand, List<Gamester> gamesters)
		{
			Console.Write("          ");
			for (int i = 0; i < dilerHand.Count; i++)
			{
				Console.Write($"{dilerHand[i].Value} ");
			}
			Console.Write("\n\n");
			for (int i = 0; i < gamesters.Count; i++)
			{
				Console.Write($"{i}-player: ");
				for (int j = 0; j < 4 && gamesters[i].Sum[j] != 0; j++)
				{
					Console.Write("\n");
					if (hand == j) Console.Write(">>>");
					Console.Write($"{j}-hand: [");
					for (int k = 0; k < gamesters[i].Hands[j].Count; k++)
					{
						Console.Write($"{gamesters[i].Hands[j][k].Value} ");
					}
					Console.Write("]  bet:");
					Console.Write($"{gamesters[i].Bets[j]} ");
					Console.Write($"{gamesters[i].Bank} ");
				}
				Console.Write("\n\n");
			}
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
