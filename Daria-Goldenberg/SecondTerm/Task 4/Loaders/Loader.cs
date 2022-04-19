using System.Reflection;
using BlackJack;

namespace LibraryLoader
{
	public static class Loader
	{
		public static List<Player> LoadBots(string path, Game game)
		{
			Assembly botDll = Assembly.LoadFrom(path);
			List<Player> bots = new List<Player>();

			foreach (var botType in botDll.GetExportedTypes())
				bots.Add((Player)botType.GetConstructors()[0].Invoke(new object[] { game, 1000 }));

			return bots;
		}
	}
}