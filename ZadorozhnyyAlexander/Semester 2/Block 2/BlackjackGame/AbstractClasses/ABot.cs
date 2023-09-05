namespace AbstractClasses
{
    public abstract class ABot : AParticipant
    {
        protected double startRate;
        protected bool isWonLastGame = false;
        protected int numberOfMoves = 0;

        public double Money { get; private set; }
        public double Multiplier { get; private set; }
        public double Rate { get; protected set; }

        public bool IsWantNextGame { get; private set; }
        public bool IsWantNextCard { get; protected set; }
        public bool IsStandAfterFirstBlackjack { get; protected set; }

        public ABot(double money, double startRate)
        {
            Money = money;
            this.startRate = startRate;
            Rate = startRate;
            Multiplier = 1;
            CardsInHand = new List<ACard>();
            CheckIsWantNextGame();
        }

        public void NextGameRound()
        {
            numberOfMoves++;
        }

        public abstract PlayerTurn GetNextTurn(ACard dealerCard);

        protected abstract void PrepareToNextGame();

        private PlayerTurn GetAnswerAfterFirstBlackjack()
        {
            return IsStandAfterFirstBlackjack ? PlayerTurn.Stand : PlayerTurn.Take;
        }

        public void MakeNextPlayerTurn(ACard dealerCard)
        {
            if (!(numberOfMoves == 0 && GetSumOfCards() == 21))
                PlayerTurnNow = GetNextTurn(dealerCard);

            else if (dealerCard.CardName == CardNames.Ace)
            {
                PlayerTurn answer = GetAnswerAfterFirstBlackjack();
                if (answer == PlayerTurn.Take)
                    PlayerTurnNow = PlayerTurn.Take;
                else
                {
                    MakeBlackjackMultiplayer();
                    PlayerTurnNow = PlayerTurn.Stand;
                }
            }

            else
            {
                MakeBlackjackMultiplayer();
                PlayerTurnNow = dealerCard.CardNumber == 10 ? PlayerTurn.Stand : PlayerTurn.Blackjack;
            }
        }

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

        public override bool IsNeedNextCard()
        {
            IsWantNextCard = !IsWantNextCard;
            return !IsWantNextCard;
        }

        public override void Win()
        {
            base.Win();
            Money += Rate * Multiplier;
            Multiplier = 1;
            isWonLastGame = true;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }

        public override void Lose()
        {
            base.Lose();
            Money -= Rate;
            Multiplier = 1;
            isWonLastGame = false;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }

        public override void Push()
        {
            base.Push();
            Multiplier = 1;
        }
    }
}
