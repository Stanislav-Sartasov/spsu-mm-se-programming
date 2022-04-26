using BotInterface;
using GameTools;
using Player;

namespace Bots
{
    public abstract class ABaseBot : IBot
    {
        public string Name { get; protected set; }
        public List<Hand> Hands { get; set; }
        public PlayerState Flag { get; set; }
        public int Cash { get; set; }
        public int GamePlayed { get;  set; }
        private readonly Func<int, bool> leaveCondtiionFunction;
        private string _name;

        protected ABaseBot(int cash = 1000)
        {
            Hands = new List<Hand>
            {
                new Hand()
            };

            leaveCondtiionFunction = i => false;
            Flag = PlayerState.Play;
            Cash = cash;
        }

        protected ABaseBot(Func<int, bool> leaveFunction, int cash = 1000) : this(cash)
        {
            leaveCondtiionFunction = leaveFunction;
        }

        public virtual void MakeBet(int minBet)
        {
            GamePlayed++;
        }

        public bool IsLeave()
        {
            return leaveCondtiionFunction(GamePlayed);
        }

        public virtual HandState Play(Hand hand, List<Card> dealerCards)
        {
            int dealerPoints = 0;
            hand.RecountPoints();

            if (Flag == PlayerState.Stop)
            {
                return HandState.Stand;
            }

            foreach (Card card in dealerCards)
            {
                if (card.Flag == Visibility.Visible)
                {
                    dealerPoints += card.GetPoints(0);
                }
            }

            HandState flag;

            if (hand.Cards.Count == 2)
            {
                if (IsTwoSameCards(hand))
                {
                    flag = PlaySameCards(hand, dealerPoints);
                }
                else if (IsAceInHand(hand))
                {
                    flag = PlaySoftTotals(hand, dealerPoints);
                }
                else
                {
                    flag = PlayHardTotals(hand, dealerPoints);
                }

            }
            else if (IsAceInHand(hand))
            {
                flag = PlaySoftTotals(hand, dealerPoints);
            }
            else
            {
                flag = PlayHardTotals(hand, dealerPoints);
            }

            return flag;
        }

        static private HandState PlaySameCards(Hand hand, int dealerPoints)
        {
            CardRank rank = hand.Cards[0].Rank;

            if (rank == CardRank.Ace || rank == CardRank.Eight) // A/A 8/8
            {
                if (dealerPoints == 11)
                {
                    return HandState.Hit;
                }
                else
                {
                    return HandState.Split;
                }
            }
            else if (rank == CardRank.Ten) // 10/10
            {
                return HandState.Stand;
            }
            else if (rank == CardRank.Five) // 5/5
            {
                return HandState.Double;
            }
            else if (rank == CardRank.Four) // 4/4
            {
                if (dealerPoints == 6 || dealerPoints == 5)
                {
                    return HandState.Split;
                }
                else
                {
                    return HandState.Hit;
                }
            }
            else if (rank == CardRank.Nine) // 9/9
            {
                if (dealerPoints == 7 || dealerPoints == 10 || dealerPoints == 11)
                {
                    return HandState.Stand;
                }
                else
                {
                    return HandState.Split;
                }
            }
            else // 2/2 3/3 6/6 7/7
            {
                if (dealerPoints >= 8)
                {
                    return HandState.Hit;
                }
                else if (rank == CardRank.Six && dealerPoints == 7)
                {
                    return HandState.Hit;
                }
                else
                {
                    return HandState.Split;
                }
            }
        }

        private static HandState PlayHardTotals(Hand hand, int dealerPoints)
        {
            if (4 <= hand.Points && hand.Points <= 8) // 4-8
            {
                return HandState.Hit;
            }
            else if (hand.Points == 9) // 9
            {
                if (dealerPoints <= 6)
                {
                    return HandState.Hit;
                }
                else
                {
                    return HandState.Double;
                }
            }
            else if (hand.Points <= 11) // 10-11
            {
                if (hand.Points == 11 && dealerPoints == 11)
                {
                    return HandState.Hit;
                }
                else if (dealerPoints >= 10)
                {
                    return HandState.Hit;
                }
                else
                {
                    return HandState.Double;
                }
            }
            else if (hand.Points == 12) // 12
            {
                if (4 <= dealerPoints && dealerPoints <= 6)
                {
                    return HandState.Stand;
                }
                else
                {
                    return HandState.Hit;
                }
            }
            else if ((hand.Points == 15 || hand.Points == 16) && (dealerPoints == 10 || dealerPoints == 11))
            {
                return HandState.Surrender;
            }
            else if (13 <= hand.Points && hand.Points <= 16) // 13-16
            {
                if (2 <= dealerPoints && dealerPoints <= 6)
                {
                    return HandState.Stand;
                }
                else
                {
                    return HandState.Hit;
                }
            }
            else
            {
                return HandState.Stand;
            }
        }

        private static HandState PlaySoftTotals(Hand hand, int dealerPoints)
        {
            if (hand.Points == 11)
            {
                return HandState.Double;
            }
            else if (13 <= hand.Points && hand.Points <= 17) // 13-17 (3-7)
            {
                if (dealerPoints <= 6)
                {
                    return HandState.Double;
                }
                else
                {
                    return HandState.Hit;
                }
            }
            else if (hand.Points == 18)
            {
                if (dealerPoints >= 9)
                {
                    return HandState.Hit;
                }
                else
                {
                    return HandState.Stand;
                }
            }
            else
            {
                return HandState.Stand;
            }
        }

        private static bool IsTwoSameCards(Hand hand)
        {
            return hand.Cards[0].Rank == hand.Cards[1].Rank;
        }
        private static bool IsAceInHand(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (card.Rank == CardRank.Ace)
                    return true;
            }

            return false;
        }
    }
}