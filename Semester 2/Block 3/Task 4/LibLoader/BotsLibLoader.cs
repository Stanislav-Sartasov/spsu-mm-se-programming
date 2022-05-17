using System;
using System.Reflection;
using System.Collections.Generic;
using BotsLib;
using RouletteLib;

namespace LibLoader
{
	public class BotsLibLoader
	{
		public List<Bot> Bots { get; private set; }

		public bool Load(string path, BetEssence bet, int startCash)
		{
			Bots = new List<Bot>();

			Assembly assembly = default;
			try
			{
				assembly = Assembly.LoadFrom(path);
			}
			catch (System.IO.FileNotFoundException)
			{
				Console.WriteLine("File not found");
				return false;
			}

			foreach (Type type in assembly.GetTypes())
			{
				if (type.BaseType == typeof(Bot))
					Bots.Add((Bot)Activator.CreateInstance(type, new object[] { bet, startCash }));
			}

			return true;
		}
	}
}
