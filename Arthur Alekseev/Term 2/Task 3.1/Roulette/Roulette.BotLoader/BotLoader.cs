using System.Reflection;
using Roulette.Common;

namespace Roulette.BotLoader
{
	public class BotLoader
	{
		private List<Type> types;
		public readonly List<string> BotNames;

		public BotLoader(string botLibraryLocation)
		{
			types = new List<Type>();
			BotNames = new List<string>();
			Assembly botLibrary = Assembly.LoadFrom(botLibraryLocation);
			foreach (Type type in botLibrary.GetExportedTypes())
			{
				if (!type.IsAbstract)
				{
					types.Add(type);
					BotNames.Add(type.Name);
				}
			}
		}

		public IPlayer? GetBot(string name, int startingMoney)
		{
			foreach (var type in types)
			{
				if (type.Name == name)
				{
					object foundBot = type.GetConstructors()[0].Invoke(new object[] { startingMoney });

					if (foundBot is IPlayer)
					{
						return (IPlayer)foundBot;
					}
					else
					{
						Console.WriteLine("One bot was not added. " + type.Name + " is not inheirited from the IPlayer interface");
					}
				}
			}

			return null;
		}
	}
}