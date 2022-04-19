using System.Collections.Generic;

namespace Roulette.Common
{
	public interface IPlayer
	{
		public int Money { get; }
		public string Description { get; }
		public string Name { get; }

		public List<Bet.Bet> MakeBets();
		public void GiveMoney(int amount);
		public int TakeMoney(int requestedAmount);
	}
}