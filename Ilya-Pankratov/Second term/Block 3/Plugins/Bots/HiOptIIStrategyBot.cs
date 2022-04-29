using GameTools;

namespace Bots
{
    public class HiOptIIStrategyBot : ABaseCountingBot
    {
        public HiOptIIStrategyBot(int cash = 1000, int numberOfDecks = 8, int sizeOfDeck = 52) : base(cash, numberOfDecks, sizeOfDeck)
        {
            Name = "HiOptIIStrategyBot";
        }

        public HiOptIIStrategyBot(Func<int, bool> leaveFunction, int cash = 1000) : base(leaveFunction, cash)
        {
            Name = "HiOptIIStrategyBot";
        }

        public override void MakeBet(int minBet) // bet spread of 1-12 units 
        {
            base.MakeBet(minBet);
            int betUnit = 2 * minBet;
            int bet;

            if (realScore < 0)
            {
                bet = betUnit / 2;
            }
            else if (realScore < 1)
            {
                bet = betUnit;
            }
            else if (realScore < 2)
            {
                bet = betUnit * 2;
            }
            else if (realScore < 3)
            {
                bet = betUnit * 4;
            }
            else if (realScore < 4)
            {
                bet = betUnit * 6;
            }
            else if (realScore < 5)
            {
                bet = betUnit * 8;
            }
            else if (realScore < 6)
            {
                bet = betUnit * 10;
            }
            else
            {
                bet = betUnit * 12;
            }

            Hands[0].Bet = bet;
            Cash -= bet;
        }

        protected override int CountCardValue(Card card)
        {
            if (card.Rank == CardRank.Ace || card.Rank == CardRank.Eight || card.Rank == CardRank.Nine) // A/8/9
            {
                return 0;
            }
            else if (card.Rank == CardRank.Four || card.Rank ==  CardRank.Five ) // 4/5
            {
                return 2;
            }
            else if ((int)card.Rank >= 10) // Ten/Jack/Queen/King
            {
                return -2;
            }
            else // 2/3/6/7
            {
                return 1;
            }
        }
    }
}