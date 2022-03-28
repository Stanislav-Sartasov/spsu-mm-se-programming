using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino;

namespace Casino
{
	public class Deck
	{
		public List<Card> Cards;
		public Random Rnd = new Random();

		public Deck()
		{
			Cards = new List<Card>();

			for (int i = 1; i < 5; i++)
				for (int j = 2; j < 15; j++)
					for (int k = 0; k < 8; k++)
						Cards.Add(new Card((CardSuit)i, (CardNumber)j));
		}

		public void Shuffle()
		{
			Cards = Cards.OrderBy(a => Rnd.Next()).ToList();
		}

		public Card GetCard()
		{
			Card card = Cards[0];
			Cards.RemoveAt(0);
			return card;
		}
	}
}
