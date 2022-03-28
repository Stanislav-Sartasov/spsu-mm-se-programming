using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino;

namespace Casino
{
	public class Card
	{
		public CardSuit Suit;
		public CardNumber Number;

		public Card(CardSuit suit, CardNumber number)
		{
			Suit = suit;
			Number = number;
		}

		public int ConvertCardToNumber(int score)
		{
			if (Number == CardNumber.Two)
				return 2;
			else if (Number == CardNumber.Three)
				return 3;
			else if (Number == CardNumber.Four)
				return 4;
			else if (Number == CardNumber.Five)
				return 5;
			else if (Number == CardNumber.Six)
				return 6;
			else if (Number == CardNumber.Seven)
				return 7;
			else if (Number == CardNumber.Eight)
				return 8;
			else if (Number == CardNumber.Nine)
				return 9;
			else if (Number == CardNumber.Ten)
				return 10;
			else if (Number == CardNumber.Jack)
				return 10;
			else if (Number == CardNumber.Queen)
				return 10;
			else if (Number == CardNumber.King)
				return 10;

			if (Number == CardNumber.Ace && score + 11 <= 21)
				return 11;
			else
				return 1;
		}

		public string FindOutTheNameOfTheCard()
		{
			string name = "";

			if (Number == CardNumber.Two)
				name = "Two ";
			else if (Number == CardNumber.Three)
				name = "Three ";
			else if (Number == CardNumber.Four)
				name = "Four ";
			else if (Number == CardNumber.Five)
				name = "Five ";
			else if (Number == CardNumber.Six)
				name = "Six ";
			else if (Number == CardNumber.Seven)
				name = "Seven ";
			else if (Number == CardNumber.Eight)
				name = "Eight ";
			else if (Number == CardNumber.Nine)
				name = "Nine ";
			else if (Number == CardNumber.Ten)
				name = "Ten ";
			else if (Number == CardNumber.Jack)
				name = "Jack ";
			else if (Number == CardNumber.Queen)
				name = "Queen ";
			else if (Number == CardNumber.King)
				name = "King ";
			else if (Number == CardNumber.Ace)
				name = "Ace ";

			if (Suit == CardSuit.Diamonds)
				name += "diamonds";
			else if (Suit == CardSuit.Hearts)
				name += "hearts";
			else if (Suit == CardSuit.Clubs)
				name += "clubs";
			else if (Suit == CardSuit.Spades)
				name += "spades";

			return name;
		}
	}
}
