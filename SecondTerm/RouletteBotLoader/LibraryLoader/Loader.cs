using System;
using System.Linq;
using Roulette;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace LibraryLoader
{
	public class Loader
	{
		public List<Player> LoadBots(string path)
		{
			List<string> files = new();
			if (!Directory.Exists(path))
			{
				Console.WriteLine("Can't find directory here");
				return null;
			}

			files = Directory.GetFiles(path, "*.dll").ToList();

			if (files.Count == 0)
			{
				Console.WriteLine("Can't find any .dll file here.");
				return null;
			}

			List<Player> bots = new List<Player>();

			foreach (string file in files)
			{
				Assembly assembly = Assembly.LoadFrom(file);

				var types = assembly.GetTypes().Where(x => x.BaseType == typeof(Player));
				foreach (var type in types)
				{
					var constructor = type.GetConstructor(new Type[] { typeof(string), typeof(int) });
					Player bot = (Player)constructor.Invoke(new object[] { type.Name, 100000 });
					bots.Add(bot);
				}
				if (bots.Count == 0)
				{
					Console.WriteLine("Can't find bots here.");
					return null;
				}
			}
			return bots;
		}
	}
}
