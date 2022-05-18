using System;
using System.Reflection;
using System.Collections.Generic;
using BotsLib;
using RouletteLib;

namespace LibLoader
{
	public class BotsLibLoader
	{

		private List<Bot> PrivateBots;
		public IReadOnlyList<Bot> Bots { get { return PrivateBots; } }

		public bool Load(string path, BetEssence bet, int startCash)
		{
			PrivateBots = new List<Bot>();

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
					PrivateBots.Add((Bot)Activator.CreateInstance(type, new object[] { bet, startCash }));
			}

			return true;
		}
	}
}
