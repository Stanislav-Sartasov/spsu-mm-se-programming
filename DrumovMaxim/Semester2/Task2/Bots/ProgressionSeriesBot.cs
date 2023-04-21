using GameTable.BetsType;
using GameTable.BotStructure;

namespace Bots
{
	public class ProgressionSeriesBot : ChoosingBot
	{
		public ProgressionSeriesBot(string name, int balance, int counter) : base(name, balance, counter)
		{
			LastGame = false;
			/*
			 *   This bot plays a series of tactic called 1-3-2-4.
			 *	 Sets the minimum bid. After the first win, it multiplies the minimum bet by 3; 
			 *	 after the second win, it multiplies the minimum bet by 2. 
			 *	 After the fourth win, it multiplies the minimum bet by 4. 
			 *	 Even if the bet 4 wins, we return to the first step.
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

			if (State == BotState.Stop)
			{
				return null;
			}
			
			if(LastGame)
            {
				if (LastBet == bet)
				{
					LastBet = bet * 3;
				}
				else if (LastBet == bet * 3)
				{
					LastBet = bet * 2;
				}
				else if (LastBet == bet * 2)
				{
					LastBet = bet * 4;
				}
				else
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
            else
            {
				LastBet = bet;

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