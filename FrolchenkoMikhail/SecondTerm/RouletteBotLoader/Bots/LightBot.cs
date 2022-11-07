using System;
using Roulette;

namespace Bots
{
	public class LightBot:Player
	{
		public LightBot(string name, int balance) : base(name, balance)
		{
			typeOfBet = TypeOfBet.Single;
			cells = 0;
		}
		public override void Bet()
		{
			currentBet = 100;
			while (cash < currentBet && cash > 100)
			{
				currentBet /= 2;
			}
			if (currentBet < 100)
				Console.WriteLine("LightBot is bankrupt");
		}
	}
}
