using PlayerStructure;
using System.Reflection;

namespace Plugins
{
    public class BotsLoader
    {
        private List<IPlayer> bots { get; set; }
        private List<string> botsNames { get; set; }
        private List<string> plugins { get; set; }

        public int NumberOfBots
        {
            get => bots.Count;
        }

        public int SuccessfullyLoadedBots { get; private set; }

        public BotsLoader()
        {
            bots = new List<IPlayer>();
            botsNames = new List<string>();
            plugins = new List<string>();
            SuccessfullyLoadedBots = 0;
        }

        public int Load(string? path, int money = 10000, int games = 40)
        {
            if (!SavePath(path))
            {
                return 0;
            }

            return LoadBots(money, games);
        }

        public (List<IPlayer>, List<string>) TakeBots()
        {
            List<IPlayer> takenBots = new List<IPlayer>(bots);
            List<string> takenBotsNames = new List<string>(botsNames);
            bots.Clear();
            botsNames.Clear();

            return (takenBots, takenBotsNames);
        }

        private bool SavePath(string? path)
        {
            try
            {
                if (!Directory.Exists(path) && !File.Exists(path))
                {
                    Console.WriteLine("The path or file with this path does not exist.");
                    return false;
                }

                if (Directory.Exists(path))
                {
                    plugins = Directory.GetFiles(path, "*.dll").ToList();
                    return true;
                }
                else
                {
                    if (!Equals(Path.GetExtension(path), ".dll"))
                    {
                        throw new Exception();
                    }

                    plugins.Add(path);
                    return true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The bot plugin must have a dll extension.");
                return false;
            }
        }

        private int LoadBots(int money, int games)
        {
            int countBots = 0;

            foreach (var plugin in plugins)
            {
                if (!File.Exists(plugin))
                {
                    continue;
                }

                try
                {
                    Assembly loader = Assembly.LoadFrom(plugin);
                    Type[] types = loader.GetTypes().Where(x => typeof(IPlayer).IsAssignableFrom(x) && x.IsSubclassOf(loader.GetType("Bots.ABasicBot")) && !x.IsAbstract).ToArray();
                    foreach (Type type in types)
                    {
                        var pluginBot = type.GetConstructors()[0].Invoke(new object[] { money, games });
                        if (pluginBot is IPlayer player)
                        {
                            bots.Add(player);
                            botsNames.Add(type.Name);
                            countBots++;
                            SuccessfullyLoadedBots++;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to load plugin with path: {plugin}");
                }
            }

            return countBots;
        }
    }
}