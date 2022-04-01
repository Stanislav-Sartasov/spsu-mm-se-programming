namespace BlackJack
{
	public class Card
	{
		public readonly Rank Rank;
		public readonly Suit Suit;

		public Card(Rank rank, Suit suit)
		{
			Rank = rank;
			Suit = suit;
		}

		public int GetValue(int score)
		{
			if (Rank == Rank.Two)
				return 2;
			else if (Rank == Rank.Three)
				return 3;
			else if(Rank == Rank.Four)
				return 4;
			else if(Rank == Rank.Five)
				return 5;
			else if(Rank == Rank.Six)
				return 6;
			else if(Rank == Rank.Seven)
				return 7;
			else if(Rank == Rank.Eight)
				return 8;
			else if(Rank == Rank.Nine)
				return 9;
			else if(Rank == Rank.Ten)
				return 10;
			else if(Rank == Rank.Jack)
				return 10;
			else if(Rank == Rank.Queen)
				return 10;
			else if(Rank == Rank.King)
				return 10;
			else if (Rank == Rank.Ace && score <= 10)
				return 11;
			else if (Rank == Rank.Ace && score > 10)
				return 1;
			return 0;
		}

		public string GetName()
		{
			string result = Suit switch
			{
				Suit.Diamonds => "Diamonds-",
				Suit.Hearts => "Hearts-",
				Suit.Clubs => "Clubs-",
				Suit.Spades => "Spades-",
				_ => "",
			};

			result += Rank switch
			{
				Rank.Ace => "Ace",
				Rank.Two => "Two",
				Rank.Three => "Three",
				Rank.Four => "Four",
				Rank.Five => "Five",
				Rank.Six => "Six",
				Rank.Seven => "Seven",
				Rank.Eight => "Eight",
				Rank.Nine => "Nine",
				Rank.Ten => "Ten",
				Rank.Jack => "Jack",
				Rank.Queen => "Queen",
				Rank.King => "King",
				_ => "",
			};

			return result;
		}
	}
}
