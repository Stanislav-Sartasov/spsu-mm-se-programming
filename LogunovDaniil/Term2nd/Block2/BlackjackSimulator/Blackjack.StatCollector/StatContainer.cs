using Blackjack.GeneralInfo;
using System.Text;

namespace Blackjack.StatCollector
{
	public class StatContainer
	{
		private readonly RoundMemo[][] rounds;
		private readonly string testName;
		public readonly int StartingStack;
		public double AverageReturn;
		public double AverageWins;
		
		public StatContainer(RoundMemo[][] rounds, string testName, int startingStack, double averageWinning)
		{
			this.testName = testName;
			this.rounds = rounds;
			StartingStack = startingStack;
			AverageReturn = averageWinning;

			// calculating average wins
			int wins = 0;
			for (int i = 0; i < rounds.Length; i++)
			{
				wins += rounds[i].Count(r => r.Result.Equals(RoundResult.PlayerWon));
			}
			AverageWins = (double)wins / rounds.Length;
		}

		public int PrintMainData(Stream output)
		{
			string str = "";
			str += $"{testName} run for {rounds.Length} times\n";
			str += $"with the initial stack of {StartingStack} resulting in:\n";
			str += $"    {AverageReturn:F2} return and\n";
			str += $"    {AverageWins:F2} winning rounds\n";
			str += $"in average per each game cycle.\n";
			try
			{
				output.Write(Encoding.Default.GetBytes(str));
			}
			catch
			{
				return -1;
			}
			return 0;
		}
	}
}