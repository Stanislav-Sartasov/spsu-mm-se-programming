using System;
using System.Collections.Generic;
using Roulette.Common;
using Roulette.Common.Bet;

namespace Roulette.Bot
{
	public abstract class Bot : IPlayer
	{
		public readonly string Name;
		public string Description;
		public int Money { get; private set; }
		protected const int MaxTurns = 40;
		protected readonly int InitialMoney;
		protected int BetsPlayed;
		protected bool IsPlaying;

		protected Bot(string name, int startingMoney)
		{
			BetsPlayed = 0;
			IsPlaying = true;
			Name = name;
			InitialMoney = startingMoney;
			Money = startingMoney;
		}

		public abstract List<Bet> MakeBets();

		public void GiveMoney(int amount)
		{
			Money += amount;
		}

		public int TakeMoney(int requestedAmount)
		{
			var amountTaken = Math.Min(requestedAmount, Money);
			Money -= amountTaken;
			return amountTaken;
		}

		public override string ToString()
		{
			return Name + " " + Convert.ToString(Money);
		}
	}
}