namespace BlackJack
{
	public class Card
	{
		private readonly CardRank rank;
		private readonly CardSuit suit;

		public Card(CardRank rank, CardSuit suit)
		{
			this.rank = rank;
			this.suit = suit;
		}

		public int GetValue(int score)
		{
			if (rank == CardRank.Two)
				return 2;
			else if (rank == CardRank.Three)
				return 3;
			else if (rank == CardRank.Four)
				return 4;
			else if (rank == CardRank.Five)
				return 5;
			else if (rank == CardRank.Six)
				return 6;
			else if (rank == CardRank.Seven)
				return 7;
			else if (rank == CardRank.Eight)
				return 8;
			else if (rank == CardRank.Nine)
				return 9;
			else if (rank == CardRank.Ten)
				return 10;
			else if (rank == CardRank.Jack)
				return 10;
			else if (rank == CardRank.Queen)
				return 10;
			else if (rank == CardRank.King)
				return 10;
			else if (rank == CardRank.Ace && score <= 10)
				return 11;
			else if (rank == CardRank.Ace && score > 10)
				return 1;
			return 0;
		}

		public string GetName()
		{
			string result = suit switch
			{
				CardSuit.Diamonds => "Diamonds-",
				CardSuit.Hearts => "Hearts-",
				CardSuit.Clubs => "Clubs-",
				CardSuit.Spades => "Spades-",
				_ => "",
			};

			result += rank switch
			{
				CardRank.Ace => "Ace",
				CardRank.Two => "Two",
				CardRank.Three => "Three",
				CardRank.Four => "Four",
				CardRank.Five => "Five",
				CardRank.Six => "Six",
				CardRank.Seven => "Seven",
				CardRank.Eight => "Eight",
				CardRank.Nine => "Nine",
				CardRank.Ten => "Ten",
				CardRank.Jack => "Jack",
				CardRank.Queen => "Queen",
				CardRank.King => "King",
				_ => "",
			};

			return result;
		}
	}
}
