using Bots.BetBuilderPattern;
using Roulette;
using Roulette.Bets;
using System;
using System.Collections.Generic;

namespace Bots
{
	public abstract class Bot : Player
	{
		internal BetDirector betDirector; //Black = 0, Red = 1, Even = 2, Odd = 3 
		private protected int wins;
		private protected int money;

		public Bot(string name, int deposit) : base(name, deposit)
		{
			IBetBuilder builder = ChooseBet(new Random().Next() % 4);
			betDirector = new BetDirector(builder);
		}

		public abstract override List<Bet> MakeBet(int player);

		private IBetBuilder ChooseBet(int number)
		{
			return number switch
			{
				0 => new ColourBlackBetBuilder(),
				1 => new ColourRedBetBuilder(),
				2 => new ParityEvenBetBuilder(),
				3 => new ParityOddBetBuilder(),
				_ => throw new NotSupportedException(),
			};
		}
	}
}