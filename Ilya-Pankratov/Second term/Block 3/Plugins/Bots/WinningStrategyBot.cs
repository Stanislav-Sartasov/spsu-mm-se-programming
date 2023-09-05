using GameTools;
using Player;

namespace Bots
{
    public class WinningStrategyBot : ABaseBot
    {
        private int winCounter;
        private int oldCash;
        
        public WinningStrategyBot(int botCash = 1000) : base(botCash)
        {
            oldCash = Cash;
            winCounter = 0;
            Name = "WinningStrategyBot";
        }

        public WinningStrategyBot(Func<int, bool> leaveFunction, int botCash = 1000) : base(leaveFunction, botCash)
        {
            Name = "WinningStrategyBot";
        }

        public override HandState Play(Hand hand, List<Card> dealerCards)
        {
            if (Flag == PlayerState.DeckReset)
            {
                winCounter = 0;
                return HandState.Done;
            }

            return base.Play(hand, dealerCards);
        }

        public override void MakeBet(int minBet)
        {
            base.MakeBet(minBet);
            int betUnit = 2 * minBet;
            int bet;

            if (oldCash < Cash)
            {
                winCounter = (winCounter + 1) % 5;
            }
            else
            {
                winCounter = 0;
            }

            oldCash = Cash;

            if(winCounter == 0 || winCounter == 1)
            {
                bet = betUnit;
            }
            else if(winCounter == 2)
            {
                bet = 3 *  betUnit;
            }
            else if (winCounter == 3)
            {
                bet = 2 * betUnit;
            }
            else
            {
                bet = 6 * betUnit;
            }

            Hands[0].Bet = bet;
            Cash -= bet;
        }
    }
}