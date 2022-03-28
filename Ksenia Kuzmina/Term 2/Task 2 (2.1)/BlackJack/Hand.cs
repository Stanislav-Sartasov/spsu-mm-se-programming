using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino;

namespace Casino
{
	public class Hand
	{
		public int Score;
		public List<Card> Cards;
		public int Bet;
		public int Number;

		public Hand(int number)
		{
			Bet = 0;
			Score = 0;
			Cards = new List<Card>();
			Number = number;
		}

		public void Hit(Card card)
		{
			Cards.Add(card);
			Score += card.ConvertCardToNumber(Score);
		}

		public void Double(Deck deck)
		{
			Card card = deck.GetCard();
			Hit(card);
			Bet *= 2;
		}
	}
}
