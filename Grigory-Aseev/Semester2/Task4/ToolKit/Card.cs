namespace ToolKit
{
    public class Card
    {
        public Suits Suit { get; private set; }
        public CardPoints CardPoint { get; private set; }

        public Card(Suits suit, CardPoints point)
        {
            Suit = suit;
            CardPoint = point;
        }

        public int GetPoints(int points = 0)
        {
            if (points + 11 <= 21 && CardPoint == CardPoints.Ace)
            {
                return 11;
            }

            if ((int)CardPoint >= 10)
            {
                return 10;
            }

            return (int)CardPoint;
        }
    }
}
