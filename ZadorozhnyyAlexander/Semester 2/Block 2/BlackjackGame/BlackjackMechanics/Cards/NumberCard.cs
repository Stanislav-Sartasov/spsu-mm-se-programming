using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class NumberCard : ACard
    {
        public NumberCard(CardNames name, CardSuits suit)
        {
            CardName = name;
            CardSuit = suit;
            CardNumber = (int)name + 2;
        }
    }
}
