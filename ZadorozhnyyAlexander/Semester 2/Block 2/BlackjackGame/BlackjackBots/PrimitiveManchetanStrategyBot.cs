using AbstractClasses;

namespace BlackjackBots
{
    public class PrimitiveManchetanStrategyBot : ABot
    {
        private bool isDoubledRate = false;

        public PrimitiveManchetanStrategyBot(double money, double startRate) : base(money, startRate)
        {
            IsStandAfterFirstBlackjack = true;
        }

        protected override void PrepareToNextGame()
        {
            if (isWonLastGame)
            {
                Rate = isDoubledRate ? startRate : startRate * 2;
                isDoubledRate = !isDoubledRate;
            }
            else
            {
                Rate = startRate;
                isDoubledRate = false;
            }
        }

        public override PlayerTurn GetNextTurn(ACard dealerCard)
        {
            int sumCards = this.GetSumOfCards();
            if (sumCards == 21)
                return PlayerTurn.Blackjack;
            if (sumCards >= 17)
                return PlayerTurn.Stand;
            if (sumCards > 10)
                return PlayerTurn.Double;
            return PlayerTurn.Hit;
        }
    }
}
