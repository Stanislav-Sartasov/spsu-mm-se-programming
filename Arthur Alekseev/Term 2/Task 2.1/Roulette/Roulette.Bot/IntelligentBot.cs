using System.Collections.Generic;
using Roulette.Common.Bet;

namespace Roulette.Bot
{
	public class IntelligentBot : Bot
	{
		public IntelligentBot(int startingMoney) : base("Intelligent Bot", startingMoney)
		{
			Description =
				"This bot is mathematician, and it studied statistics and probability and stuff. \nIt does not play at all because probabilities to win are less than the amount of money it will get";
		}

		public override List<Bet> MakeBets()
		{
			return new List<Bet>();
		}
	}
}