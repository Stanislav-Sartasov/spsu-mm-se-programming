using System.Collections.Generic;

namespace Roulette.Common
{
	public interface IPlayer
	{
		public List<Bet.Bet> MakeBets();
		public void GiveMoney(int amount);
		public int TakeMoney(int requestedAmount);
	}
}