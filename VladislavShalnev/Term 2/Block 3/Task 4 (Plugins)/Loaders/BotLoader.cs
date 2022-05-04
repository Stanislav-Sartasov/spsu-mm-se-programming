using System.Reflection;
using Blackjack.Players;

namespace Loaders;

public static class BotLoader
{
	public static List<Player> Load(string path)
	{
		string[] fileNames = Directory.GetFiles(path, "*.dll");
		
		List<Player> bots = new List<Player>();
		
		foreach (string fileName in fileNames)
		{
			Assembly asm = Assembly.LoadFrom(fileName);

			List<Type> botTypes = asm
				.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(Player)))
				.ToList();
		
			foreach (Type type in botTypes)
				if (Activator.CreateInstance(type, type.Name, 0) is Player bot)
					bots.Add(bot);
		}

		return bots;
	}
}