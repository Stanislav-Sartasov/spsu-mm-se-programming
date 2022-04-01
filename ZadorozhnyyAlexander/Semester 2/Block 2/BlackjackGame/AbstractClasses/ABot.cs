namespace AbstractClasses
{
    public abstract class ABot : AParticipant
    {
        public double Money;
        public double Multiplier = 1;
        public double Rate;

        public bool IsWantNextCard;
        public bool IsWantNextGame = true;
        public bool IsStandAfterFirstBlackjack;

        protected double StartRate;
        protected bool IsWonLastGame = false;

        public ABot(double money, double startRate)
        {
            Money = money;
            StartRate = startRate;
            Rate = StartRate;
            CardsInHand = new List<ACard>();
        }

        public abstract PlayerTurn GetNextTurn(ACard dealerCard);

        protected abstract void PrepareToNextGame();

        private void CheckIsWantNextGame()
        {
            IsWantNextGame = CountGames < 40 && Money > 0;
        }

        protected bool IsAceInCardHand()
        {
            foreach (var card in CardsInHand)
            {
                if (card.CardName == CardNames.Ace)
                    return true;
            }
            return false;
        }

        protected int GetSumOfCardWithoutAce()
        {
            int sumCards = 0;
            foreach (var card in CardsInHand)
                sumCards = card.CardName != CardNames.Ace ? sumCards + card.CardNumber : sumCards;
            return sumCards;
        }

        protected int GetIndexOfTurnListFromDealerCard(ACard dealerCard)
        {
            switch (dealerCard.CardNumber)
            {
                case 1:
                case 11: return 9;
                default: return dealerCard.CardNumber - 2;
            }
        }

        public override bool GetNextCard()
        {
            IsWantNextCard = !IsWantNextCard;
            return !IsWantNextCard;
        }

        public override void Win()
        {
            Money += Rate * Multiplier;
            CountGames++;
            CountWinGames++;
            Multiplier = 1;
            IsWonLastGame = true;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }

        public override void Lose()
        {
            Money -= Rate;
            Multiplier = 1;
            CountGames++;
            IsWonLastGame = false;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }
    }
}
