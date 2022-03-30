namespace BlackJack
{
	public class Card
	{
		public readonly string Rank;
		public readonly string Name;

		public Card(string rank, string name)
		{
			Rank = rank;
			Name = name;
		}

		public static int GetValue(string rank, int score)
		{
			if (int.TryParse(rank, out _))
				return Convert.ToInt32(rank);
			else if (rank.Length == 3 && score <= 10)
				return 11;
			else if (rank.Length == 3 && score > 10)
				return 1;
			return 10;
		}
	}
}
