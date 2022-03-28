using AbstractClasses;

namespace BlackjackMechanics.Cards
{
    public class AceCard : ACard
    {
        public AceCard(string suit)
        {
            CardName = "Ace";
            CardSuit = suit;
            CardNumber = 11;
        }

        public void CheckIsMoreThenTwentyOne(List<ACard> allCards)
        {
            int sum = 0;
            foreach (var card in allCards)
                sum += card.CardNumber;

            foreach (ACard card in allCards)
            {
                if (card.CardName == "Ace" || sum + this.CardNumber > 21)
                {
                    this.CardNumber = 1;
                    break;
                }
            }
        }
    }
}
