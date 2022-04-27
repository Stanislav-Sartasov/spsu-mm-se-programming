using System;
using System.Collections.Generic;
using RouletteLib;

namespace BotsLib
{
	public class BotLaboucherer : Bot
	{
		private readonly int  StartSequenceLength = 5;

		public BotLaboucherer(string betEssence, int startCash) : base(betEssence, startCash) { }

		public override int Play(int numberOfBets)
		{
			int cashInPlay = (int)(0.1 * StartCash);
			int sequenceMember = (int)(cashInPlay / StartSequenceLength);

			List<int> sequence = new List<int>();
			for (int i = 0; i < StartSequenceLength - 1; i++)
			{
				sequence.Add(sequenceMember);
			}
			sequence.Add(cashInPlay - (StartSequenceLength - 1) * sequenceMember);

			Roulette roulette = new Roulette();
			int cash = StartCash;
			int betAmount;

			for (int i = 0; i < numberOfBets; i++)
			{
				betAmount = sequence[0] + sequence[^1];

				if (roulette.Spin(BetEssence))
				{
					cash += betAmount;

					sequence.RemoveAt(0);
					sequence.RemoveAt(sequence.Count - 1);

					if (sequence.Count < 2)
					{
						Bot newBot = new BotLaboucherer(BetEssence, cash);
						return newBot.Play(numberOfBets - i - 1);
					}
				}
				else
				{
					cash -= betAmount;

					if (cash > sequence[0] + betAmount)
					{
						sequence.Add(betAmount);
					}
					else
					{
						return cash;
					}
				}
			}

			return cash;
		}
	}
}
