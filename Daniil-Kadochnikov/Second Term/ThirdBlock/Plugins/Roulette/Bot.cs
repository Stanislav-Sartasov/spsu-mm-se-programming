using Roulette.Bets;
using Roulette.BetBuilderPattern;
using System;
using System.Collections.Generic;

namespace Roulette
{
	public abstract class Bot : Player
	{
		protected BetDirector betDirector; //Black = 0, Red = 1, Even = 2, Odd = 3 
		protected int wins;
		protected int money;

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