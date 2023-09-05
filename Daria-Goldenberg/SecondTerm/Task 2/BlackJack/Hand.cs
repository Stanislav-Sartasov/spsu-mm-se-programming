﻿namespace BlackJack
{
	public class Hand
	{
		public List<Card> Cards { get; private set; }

		public Hand()
		{
			Cards = new List<Card>();
		}

		public void TakeCard(Deck deck)
		{
			Cards.Add(deck.TakeCard());
		}

		public void Clear()
		{
			Cards.Clear();
		}

		public int CountPoints()
		{
			int score = 0;
			foreach (Card card in Cards)
				score += card.GetValue(score);

			return score;
		}
	}
}
