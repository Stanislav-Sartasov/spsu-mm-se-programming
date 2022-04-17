using GameTools;

namespace Bots
{
    public class HiLowStrategyBot : ABaseCountingBot // HiLow
    {
        public HiLowStrategyBot(int cash = 1000, int numberOfDecks = 8, int sizeOfDeck = 52) : base(cash, numberOfDecks, sizeOfDeck)
        {
            Name = "HiLowStrategyBot";
        }

        public HiLowStrategyBot(Func<int, bool> leaveFunction, int cash = 1000) : base(leaveFunction, cash)
        {
            Name = "HiLowStrategyBot";
        }

        public override void MakeBet(int minBet) // bet spread of 1-8 units
        {
            base.MakeBet(minBet);
            int betUnit = 2 * minBet;
            int bet;

            if (realScore < 0)
            {
                bet  = betUnit / 2;
            }
            else if (realScore < 2)
            {
                bet = betUnit;
            }
            else if (realScore < 3)
            {
                bet = betUnit * 2;
            }
            else if (realScore < 4)
            {
                bet = betUnit * 4;
            }
            else
            {
                bet = betUnit * 8;
            }

            Hands[0].Bet = bet;
            Cash -= bet;
        }

        protected override int CountCardValue(Card card)
        {
            if ((int)card.Rank > 9 || card.Rank == CardRank.Ace)
            {
                return -1;
            }
            else if ((int)card.Rank < 7)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}