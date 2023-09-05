using GameTable.BetsType;
using GameTable.BotStructure;

namespace Bots
{
    public class ThomasDonaldBot : ChoosingBot 
    {
        public ThomasDonaldBot(string name, int balance, int counter) : base(name, balance, counter)
		{
			/*
			 * This bot plays a tactic called Thomas Donald.
			 * Sets the minimum bid. If you lose, then the minimum bet is added to the last bet. 
			 * If won and the bet is equal to the minimum bet, then it does nothing. 
			 * If not equal to the minimum bet, then subtract 1 minimum bet from it.
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

			if(State == BotState.Stop)
            {
				return null;
            }
			if(LastGame)
            {
				if(LastBet > bet)
                {
                    LastBet -= bet;
                }

				if(CheckBet(LastBet))
                {
					return null;
                }

				playersBets.Add(betHeader.CreateBet(LastBet));
				Balance -= LastBet;
			}
            else
            {
                LastBet += bet;

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
