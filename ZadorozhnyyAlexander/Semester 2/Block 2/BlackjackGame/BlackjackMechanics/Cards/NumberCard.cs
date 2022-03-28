using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class NumberCard : ACard
    {
        public NumberCard(int data, string suit)
        {
            CardName = data.ToString();
            CardSuit = suit;
            CardNumber = data;
        }
    }
}
