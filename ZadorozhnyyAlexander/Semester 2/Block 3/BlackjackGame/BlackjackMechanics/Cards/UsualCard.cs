using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class UsualCard : ACard
    {
        public UsualCard(CardNames name, CardSuits suit)
        {
            CardName = name;
            CardSuit = suit;
            CardNumber = GetCardNumber();
        }

        private int GetCardNumber()
        {
            return (int)CardName < 9 ? (int)CardName + 2 : 10;
        }
    }
}
