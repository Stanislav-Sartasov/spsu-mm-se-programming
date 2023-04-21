using AbstractClasses;

namespace BlackjackBots
{
    public class UsualBaseStrategyBot : ABot
    {
        private PlayerTurn[,] hardTotals =
        {
            {PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand},
            {PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double},
            {PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit}
        };
        private PlayerTurn[,] softTotals =
        {
            {PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand},
            {PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Double, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Stand},
            {PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Stand, PlayerTurn.Stand, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Hit,  PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit},
            {PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Double, PlayerTurn.Double, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit, PlayerTurn.Hit}
        };

        public UsualBaseStrategyBot(double money, double startRate) : base(money, startRate)
        {
            IsStandAfterFirstBlackjack = true;
        }

        private PlayerTurn GetTurnHardHand(ACard dealerCard, int sumCards)
        {
            int dealerIndex = GetIndexOfTurnListFromDealerCard(dealerCard);

            int hardTotalIndex = sumCards switch
            {
                < 9 => 6,
                9 => 5,
                10 => 4,
                11 => 3,
                12 => 2,
                < 17 => 1,
                _ => 0
            };

            PlayerTurn turn = hardTotals[hardTotalIndex, dealerIndex];
            IsWantNextCard = turn == PlayerTurn.Hit || turn == PlayerTurn.Double;

            return turn;
        }

        private PlayerTurn GetTurnSoftHand(ACard dealerCard, int sumCards)
        {
            int dealerIndex = GetIndexOfTurnListFromDealerCard(dealerCard);
            int sumCardsWithoutAce = GetSumOfCardWithoutAce();

            int softTotalIndex = sumCardsWithoutAce switch
            {
                < 4 => 5,
                < 6 => 4,
                6 => 3,
                7 => 2,
                8 => 1,
                9 => 0,
                _ => -1
            };

            if (softTotalIndex != -1)
            {
                PlayerTurn turn = softTotals[softTotalIndex, dealerIndex];
                IsWantNextCard = turn == PlayerTurn.Hit || turn == PlayerTurn.Double;
                return turn;
            }
                
            return GetTurnHardHand(dealerCard, sumCards);
        }

        protected override void PrepareToNextGame()
        {
            Rate = startRate;
        }

        public override PlayerTurn GetNextTurn(ACard dealerCard)
        {
            int sumCards = this.GetSumOfCards();
            if (sumCards == 21)
                return PlayerTurn.Blackjack;
            if (sumCards > 21)
                return PlayerTurn.Stand;

            if (IsAceInCardHand())
                return GetTurnSoftHand(dealerCard, sumCards);
            return GetTurnHardHand(dealerCard, sumCards);
        }
    }
}
