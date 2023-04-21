using AbstractClasses;

namespace PrimitiveManchetanStrategyBotLibrary
{
    public class PrimitiveManchetanStrategyBot : ABot
    {
        private bool IsDoubledRate = false;

        public PrimitiveManchetanStrategyBot(double money, double startRate) : base(money, startRate)
        {
            IsStandAfterFirstBlackjack = true;
        }

        protected override void PrepareToNextGame()
        {
            if (IsWonLastGame)
            {
                Rate = IsDoubledRate ? StartRate : StartRate * 2;
                IsDoubledRate = !IsDoubledRate;
            }
            else
            {
                Rate = StartRate;
                IsDoubledRate = false;
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
