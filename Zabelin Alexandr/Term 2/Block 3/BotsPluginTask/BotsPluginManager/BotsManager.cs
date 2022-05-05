using Blackjack;
using System.IO;
using System.Reflection;

namespace BotsPluginManagement
{
    public class BotsManager
    {
        private List<APlayer> bots = new List<APlayer>();
        private List<Type> botTypes = new List<Type>();


        public int BotsCount { get; private set; } = 0;
        public IReadOnlyList<APlayer> Bots
        {
            get
            {
                return bots.AsReadOnly();
            }
        }


        public BotsManager(string path, float startingSum)
        {
            Load(path);
            AddBotsToList(startingSum);
        }

        public void UpdateBots(float startingSum)
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                bots[i] = (APlayer)Activator.CreateInstance(botTypes[i], startingSum);
            }
        }

        public APlayer? GetBotByTypeName(string typeName)
        {
            foreach (APlayer bot in bots)
            {
                if (bot.GetType().Name == typeName)
                {
                    return (APlayer)bot;
                }
            }

            throw new KeyNotFoundException($"Bot with type name {typeName} was not found");
        }

        private void Load(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.dll");

                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        Assembly assembly = Assembly.LoadFile(Path.GetFullPath(file));

                        AddBotTypesToList(assembly);
                    }

                    if (!botTypes.Any())
                    {
                        throw new FileNotFoundException($"There are no bots inherited from APlayer in {path}");
                    }
                }
                else
                {
                    throw new FileNotFoundException($"There are no .dll files in {path}");
                }
            }
            else
            {
                throw new DirectoryNotFoundException($"The path '{path}' is invalid");
            }
        }

        private void AddBotsToList(float startingSum)
        {
            foreach (Type botType in botTypes)
            {
                bots.Add((APlayer)Activator.CreateInstance(botType, startingSum));
            }

            BotsCount = bots.Count;
        }

        private void AddBotTypesToList(Assembly assembly)
        {
            foreach(Type type in assembly.GetTypes())
            { 
                if (typeof(APlayer).IsAssignableFrom(type))
                {
                    botTypes.Add(type);
                }
            }
        }
    }
}