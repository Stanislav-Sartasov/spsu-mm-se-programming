using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class FaceCard : ACard
    {
        public FaceCard(CardNames name, CardSuits suit)
        {
            CardName = name;
            CardSuit = suit;
            CardNumber = 10;
        }

    }
}
