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
			this.bank = bank;
		}
		public override PlayerMove Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
		{
			Console.WriteLine("Make your move");
			PlayerMove answer = GetCorectAnswer<PlayerMove>();

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
			if (GetCorectAnswer<AnswerType>() == AnswerType.Yes)
				return true;
			else return false;

		}

		public static enumType GetCorectAnswer<enumType>()
			where enumType : Enum
		{
			while (true)
			{
				string answer = Console.ReadLine();

				foreach (enumType key in Enum.GetValues(typeof(enumType)))
				{
					if (String.Equals(answer, key.ToString()))
					{
						return key;
					}
				}
				Console.WriteLine("Wrong input");
			}
		}

		public static int GetCorectInt(int bottom, int top)
        {
            int answer;
			string input = Console.ReadLine();

			if (String.Equals(input, "Exit"))
				return -1;

			while (!int.TryParse(input, out answer) || answer > top || answer < bottom)
            {
				Console.WriteLine($"Error, enter {bottom}-{top}");
				input = Console.ReadLine();
			}    
			return answer;
		}

		public override void MakeBet(int hand)
		{
			Console.WriteLine($"Enter your bet. Bank : {this.bank}");
			int bet = GetCorectInt(0,this.bank);

			bets[hand] = bet;
			this.bank -= bet;

		}

	}
}
