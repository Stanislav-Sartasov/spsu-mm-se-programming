namespace Roulette
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var statisticCollector = new StatisticCollector();
			statisticCollector.CollectStatistics();
			statisticCollector.LogStatistics();
		}
	}
}