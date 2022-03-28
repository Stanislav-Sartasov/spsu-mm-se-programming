using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino;

namespace Casino
{
	public class Dealer
	{
		public int Score;
		public List<Card> Cards;

		public Dealer()
		{
			Cards = new List<Card>();
			Score = 0;
		}

		public void Begin(Deck deck)
		{
			Card card;

			for (int i = 0; i < 2; i++)
			{
				card = deck.GetCard();
				Cards.Add(card);
				Score += card.ConvertCardToNumber(Score);
			}

			Console.WriteLine("Dealer's first card is " + Cards[0].FindOutTheNameOfTheCard() + "\n");
		}

		public void Play(Deck deck)
		{
			while (Score < 17)
			{
				Card card = deck.GetCard();
				Cards.Add(card);
				Score += card.ConvertCardToNumber(Score);
			}
		}

		public void Finish()
		{
			Cards.Clear();
			Score = 0;
		}
	}
}
