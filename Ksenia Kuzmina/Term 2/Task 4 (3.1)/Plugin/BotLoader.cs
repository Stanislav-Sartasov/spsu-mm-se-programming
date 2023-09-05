using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using System.Reflection;

namespace Plugin
{
	public static class BotLoader
	{
		public static List<Player> Bots = new List<Player>();

		public static List<Player> LoadBots(string libraryPath, int sum)
		{
			Assembly library = null;

			try
			{
				library = Assembly.LoadFrom(libraryPath);
			}
			catch
			{
				Console.WriteLine("Something wrong with the file. Check the library.");
				return Bots;
			}

			foreach (Type type in library.GetExportedTypes())
			{
				try
				{
					Bots.Add((Player)type.GetConstructors()[0].Invoke(new object[] { sum }));
				}
				catch
				{
					continue;
				}
			}

			return Bots;
		}
	}
}