using System;
using Roulette;

namespace Bots
{
	public class LightBot:APlayer
	{
		public LightBot()
		{
			name = "LightBot";
			cash = 100000;
			typeOfBet = TypeOfBet.Single;
			cells = 0;
			currentBet = 100;
		}
		public override void Bet()
		{
			while (cash < currentBet && cash > 100)
			{
				currentBet /= 2;
			}
			if (currentBet < 100)
				Console.WriteLine("LightBot is bankrupt");
		}
	}
}
