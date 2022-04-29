namespace GameTools
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Points { get; private set; }
        public int Bet { get; set; }
        public HandState Flag { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
            Points = 0;
            Bet = 0;
            Flag = HandState.Playing;
        }

        public void RecountPoints()
        {
            int points = 0;
            int aceCounter = 0;

            foreach (var card in Cards)
            {


                if (card.Rank == CardRank.Ace)
                {
                    aceCounter++;
                }
                else
                {
                    points += card.GetPoints(0);
                }
            }

            while (points + aceCounter * 11 <= 21 && aceCounter != 0)
            {
                aceCounter--;
                points += 11;
            }

            Points = points + aceCounter;
        }
    }
}