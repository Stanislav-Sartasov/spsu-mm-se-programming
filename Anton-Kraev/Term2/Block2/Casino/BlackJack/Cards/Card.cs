namespace Cards
{
    public class Card
    {
        public CardRank Rank { get; }

        public Card(CardRank rank)
        {
            Rank = rank;
        }

        public int GetCardRank()
        {
            if (Rank == CardRank.Jack || Rank == CardRank.Queen || Rank == CardRank.King)
                return 10;
            return (int)Rank;
        }
    }
}