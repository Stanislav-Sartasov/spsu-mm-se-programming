using System.Reflection;
using Blackjack.PlayerLibrary;

namespace Blackjack.PlayerLoaderLibrary
{
	public static class PlayerLoader
	{
		public static AbstractPlayer[]? LoadFromDLL(string path)
		{
			if (!Directory.Exists(path))
				return null;

			AbstractPlayer[] players;

			try
			{
				players = Directory.GetFiles(path, "*.dll")
					.Select(file => Assembly.LoadFile(Path.GetFullPath(file)).GetTypes())
					.Aggregate((first, second) => Enumerable.Concat(first, second).ToArray())
					.Where(type => type.IsSubclassOf(typeof(AbstractPlayer)))
					.Select(type => (AbstractPlayer?)Activator.CreateInstance(type))
					.Where(type => type != null)
					.ToArray();
			}
			catch (Exception ex)
			{
				return null;
			}

			return players;
		}
	}
}