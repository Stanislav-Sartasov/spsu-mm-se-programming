using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class FaceCard : ACard
    {
        public FaceCard(int data, string suit)
        {
            string[] cardNames = { "Jack", "Queen", "King" };
            CardName = cardNames[data];
            CardSuit = suit;
            CardNumber = 10;
        }
    }
}
