using System;
using Roulette;

namespace Bots
{
	public class RandomBot : APlayer
	{
		public RandomBot()
		{
			name = "RandomBot";
			cash = 100000;
		}

		public override void Bet()
		{
			Random random = new();
			if (cash < 100)
				Console.WriteLine("LightBot is bankrupt");
			else
			{
				currentBet = random.Next(100, 500);
				typeOfBet = (TypeOfBet)random.Next(1,11);
			}
		}
	}
}
