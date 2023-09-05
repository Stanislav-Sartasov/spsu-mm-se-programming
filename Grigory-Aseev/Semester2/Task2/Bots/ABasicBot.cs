using PlayerStructure;
using ToolKit;

namespace Bots
{
    public abstract class ABasicBot : IPlayer
    {
        public List<Hand> Hands { get; set; }
        private protected int balance;
        public int Balance { get => balance; }
        public int GameCounter { get; set; }
        public PlayerState State { get; set; }
        public ShoeState StateOfShoe { get; set; }

        protected ABasicBot(int money = 10000, int games = 40)
        {
            Hands = new List<Hand>();
            Hands.Add(new Hand());
            balance = money;
            GameCounter = games;
            State = PlayerState.Playing;
        }

        public void ChangeBalance(int money)
        {
            if (balance + money < 0)
            {
                State = PlayerState.Stop;
            }
            else
            {
                balance += money;
            }
        }

        public void ResetBalance(int money)
        {
            balance = Math.Abs(money);
        }

        public virtual void MakeBet(int bet)
        {
            GameCounter--;
            State = IsLeave() ? PlayerState.Stop : PlayerState.Playing;
            if (State == PlayerState.Playing && bet > 0)
            {
                ChangeBalance(-bet);
                Hands[0].Bet = bet;
            }
            else
            {
                Hands[0].State = GamingState.Lose;
            }
        }

        public GamingState TakeMove(Hand playerHand, Card dealerCard)
        {
            if (State == PlayerState.Stop)
            {
                return GamingState.Stand;
            }

            int dealerPoints = dealerCard.GetPoints();
            playerHand.UpdateScore();

            if (IsSplit(playerHand.Cards))
            {
                return ChooseSplitOrAnother(playerHand.Cards[0].GetPoints(), dealerPoints);
            }

            if (IsAceInHand(playerHand.Cards))
            {
                return ActSoftHand(playerHand.Points, dealerPoints);
            }

            return ActHardHand(playerHand, dealerPoints);
        }

        private bool IsLeave() => GameCounter < 0;

        private bool IsSplit(List<Card> cards)
        {
            return cards.Count == 2 && (cards[0].CardPoint == cards[1].CardPoint || (int)cards[0].CardPoint >= 10 && (int)cards[1].CardPoint >= 10);
        }

        private bool IsAceInHand(List<Card> cards)
        {
            foreach (var card in cards)
            {
                if (card.CardPoint == CardPoints.Ace)
                {
                    return true;
                }
            }

            return false;
        }

        private GamingState ChooseSplitOrAnother(int points, int dealerPoints)
        {
            switch (points)
            {
                case 5:
                    return GamingState.Double;
                case 4:
                    return dealerPoints == 5 || dealerPoints == 6 ? GamingState.Split : GamingState.Hit;
                case 9:
                    return dealerPoints == 7 || dealerPoints == 10 || dealerPoints == 11 ? GamingState.Stand : GamingState.Split;
                case 10:
                    return GamingState.Stand;
                case >= 8:
                    return dealerPoints == 11 ? GamingState.Hit : GamingState.Split; // here points equal 8 | 11
                case <= 7:
                    return dealerPoints >= 8 || (dealerPoints == 7 && points == 6) ? GamingState.Hit : GamingState.Split; // here points equal 2 | 3 | 6 | 7
            }
        }

        private GamingState ActSoftHand(int points, int dealerPoints)
        {
            switch (points)
            {
                case <= 17:
                    return dealerPoints == 5 || dealerPoints == 6 ? GamingState.Double : GamingState.Hit;
                case 18:
                    return dealerPoints >= 9 ? GamingState.Hit : GamingState.Stand;
                case >= 19:
                    return GamingState.Stand;
            }
        }

        private GamingState ActHardHand(Hand playerHand, int dealerPoints)
        {
            int points = playerHand.Points;
            switch (points)
            {
                case <= 8:
                    return GamingState.Hit;
                case 9:
                    return dealerPoints <= 6 && dealerPoints >= 3 ? GamingState.Double : GamingState.Hit;
                case <= 11:
                    return dealerPoints == 11 || dealerPoints == 10 && points == 10 ? GamingState.Hit : GamingState.Double;
                case 12:
                    return dealerPoints <= 6 && dealerPoints >= 4 ? GamingState.Stand : GamingState.Hit;
                case <= 16:
                    if (dealerPoints >= 10 && (points == 15 || points == 16) && playerHand.Cards.Count == 2)
                    {
                        return GamingState.Surrender;
                    }
                    return dealerPoints <= 6 && dealerPoints >= 2 ? GamingState.Stand : GamingState.Hit;
                case >= 17:
                    return GamingState.Stand;
            }
        }
    }
}