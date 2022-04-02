namespace AbstractClasses
{
    public abstract class ABot : AParticipant
    {
        protected double StartRate;
        protected bool IsWonLastGame = false;

        public double Money { get; private set; }
        public double Multiplier { get; private set; }
        public double Rate { get; protected set; }

        public bool IsWantNextGame { get; private set; }
        public bool IsWantNextCard { get; protected set; }
        public bool IsStandAfterFirstBlackjack { get; protected set; }

        public ABot(double money, double startRate)
        {
            Money = money;
            StartRate = startRate;
            Rate = StartRate;
            Multiplier = 1;
            CardsInHand = new List<ACard>();
            CheckIsWantNextGame();
        }

        public abstract PlayerTurn GetNextTurn(ACard dealerCard);

        protected abstract void PrepareToNextGame();

        public void DoubleRate()
        {
            Rate *= 2;
        }

        public void MakeBlackjackMultiplayer()
        {
            Multiplier = 1.5;
        }

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
            Multiplier = 1;
            CountGames++;
            CountWinGames++;
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
