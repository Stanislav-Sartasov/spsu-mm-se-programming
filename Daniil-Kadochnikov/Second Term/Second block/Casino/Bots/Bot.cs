using Roulette;
using Roulette.Bets;
using Roulette.Bets.PossibleBets;
using System;
using System.Collections.Generic;

namespace Bots
{
	public abstract class Bot : Player
	{
		internal protected readonly int betCell; //Black = 0, Red = 1, Even = 2, Odd = 3
		internal protected int wins;
		internal protected int money;

		public Bot(string name, int deposit) : base(name, deposit) => betCell = new Random().Next(4);

		public abstract override List<Bet> MakeBet(int player);

		private protected Bet CreateBet(int player, int money, int betCell)
		{
			return betCell switch
			{
				0 => new ColourBet(player, money, PossibleColour.Black),
				1 => new ColourBet(player, money, PossibleColour.Red),
				2 => new ParityBet(player, money, PossibleParity.Even),
				3 => new ParityBet(player, money, PossibleParity.Odd),
				_ => throw new System.NotSupportedException(),
			};
		}
	}
}