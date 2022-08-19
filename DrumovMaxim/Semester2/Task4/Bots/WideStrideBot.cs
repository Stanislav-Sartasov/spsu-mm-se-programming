using GameTable.BetsType;
using GameTable.BotStructure;

namespace Bots
{
    public class WideStrideBot : ChoosingBot
    {
        public WideStrideBot(string name, int balance, int counter) : base(name, balance, counter)
        {
            /*
             * This bot plays a tactic called Wide Stride.
             * After each win, the player places the initial bet. 
             * After each loss, the bet doubles and increases by one. 
             * The player always bets on equal chances.
             */
        }

        public bool CheckBet(int bet)
        {
            if (Balance <= bet || GameCounter == 0 || Balance == 0)
            {
                State = BotState.Stop;
                return true;
            }
            return false;
        }

        public override List<Bet> NewBet(int bet)
        {
            if (CheckBet(LastBet))
            {
                return null;
            }

            List<Bet> playersBets = new List<Bet>();

            if (LastGame)
            {
                LastBet = bet;

                if (CheckBet(LastBet))
                {
                    return null;
                }

                playersBets.Add(betHeader.CreateBet(LastBet));
                Balance -= LastBet;
            }
            else
            {
                LastBet *= 2;

                if(LastBet > Balance && Balance != 0)
                {
                    LastBet = bet;
                }

                if (CheckBet(LastBet))
                {
                    return null;
                }

                playersBets.Add(betHeader.CreateBet(LastBet));
                Balance -= LastBet;
            }

            return playersBets;
        }
    }
}
