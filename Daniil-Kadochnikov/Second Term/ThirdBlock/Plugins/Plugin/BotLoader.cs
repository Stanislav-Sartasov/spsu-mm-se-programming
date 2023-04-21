using System.Reflection;

namespace Plugin
{
	public class BotLoader
	{
		public List<Type>? FindBots(string path, Type player)
		{
			try
			{
				List<Type>? bots = new List<Type>();
				IEnumerable<string>? paths = Directory.EnumerateFiles(path, "*.dll");

				foreach (string botPath in paths)
				{
					Assembly assembly = Assembly.LoadFrom(botPath);
					foreach (Type type in assembly.GetTypes())
						if (type.IsSubclassOf(player))
							if (Check(bots, type))
								bots.Add(type);
				}
				return bots;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		private bool Check(List<Type> bots, Type type)
		{
			for (int x = 0; x < bots.Count; x++)
				if (bots[x] == type)
					return false;
			return true;
		}
	}
}