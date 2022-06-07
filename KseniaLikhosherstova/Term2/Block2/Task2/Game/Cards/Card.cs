namespace Game.Cards
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }


        public Card(Rank rank, Suit suit)
        {
            if (!Enum.IsDefined(rank) || !Enum.IsDefined(suit))
                throw new ArgumentException($"Rank {rank} or/and suit {suit} are not valid");

            Rank = rank;
            Suit = suit;
        }



        public int GetValueOfCard(int score)
        {
            if (Rank == Rank.King || Rank == Rank.Queen || Rank == Rank.Jack)
                return 10;

            if (Rank == Rank.Ace)
            {
                if ((score + 11) > 21)
                    return 1;
                else
                    return 11;
            }

            return (int)Rank;
        }

        public override string ToString()
        {
            int d = (int)Rank;
            string name = d switch
            {
                (<= 10) => d.ToString(),
                11 => "Jack",
                12 => "Queen",
                13 => "King",
                14 => "Ace",
                _ => "",
            };

            string suit = Suit switch
            {
                Suit.Clubs => "of clubs",
                Suit.Diamonds => "of diamonds",
                Suit.Hearts => "of hearts",
                Suit.Spades => "of spades",
                _ => "",
            };

            return $"{name} {suit}";
        }
    }
}