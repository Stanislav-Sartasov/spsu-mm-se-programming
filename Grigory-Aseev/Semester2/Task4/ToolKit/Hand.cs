namespace ToolKit
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Bet { get; set; }
        public int Points { get; private set; }
        public GamingState State { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
            Bet = 0;
            Points = 0;
            State = GamingState.Playing;
        }

        public Hand(int bet) : this()
        {
            Bet = bet;
        }

        public void UpdateScore()
        {
            Points = 0;
            int countAces = 0;
            foreach (var item in Cards)
            {
                if (item.CardPoint == CardPoints.Ace)
                {
                    countAces++;
                }
                else
                {
                    Points += item.GetPoints();
                }
            }

            while (countAces-- > 0)
            {
                Points += new Card(Suits.Heart, CardPoints.Ace).GetPoints(Points);
            }
        }
    }
}
