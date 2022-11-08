using System;
using Roulette;

namespace Bots
{
	public class RandomBot : Player
	{
		private Random random;
		public RandomBot(string name, int balance) : base(name, balance) 
		{
			random = new Random(DateTime.Now.Second);
		}

		public override void Bet()
		{
			if (cash < 100)
				Console.WriteLine("RandomBot is bankrupt");
			else
			{
				currentBet = random.Next(100, 500);
				typeOfBet = (TypeOfBet)random.Next(1,11);
			}
		}
	}
}
