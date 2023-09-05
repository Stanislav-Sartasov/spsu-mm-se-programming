using static System.Console;
using System.Reflection;
using BlackJack;

namespace BotLoader
{
    public class Loader
    {
        public Loader()
        {

        }

        public List<Player> LoadAllBotsFromDirectory(string filePath)
        {
            List<string> fileNames = new List<string>();
            if (!Directory.Exists(filePath))
            {
                WriteLine("Can't find directory here. Perhaps the path was specified incorrectly.");
                return null;
            }

            fileNames = Directory.GetFiles(filePath, "*.dll").ToList();

            if (fileNames.Count == 0)
            {
                WriteLine("Can't find any .dll file here.");
                return null;
            }

            List<Player> bots = new List<Player>();

            foreach (string file in fileNames)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                var types = assembly.GetTypes().Where(x => x.BaseType == typeof(Player));
                foreach (var type in types)
                {
                    var constructor = type.GetConstructor(new Type[] { typeof(string), typeof(int) });
                    Player bot = (Player)constructor.Invoke(new object[] { type.Name, 1000 });
                    bots.Add(bot);
                }
            }

            return bots;
        }

        public List<Player> LoadBot(string filePath)
        {
            if (!File.Exists(filePath))
            {
                WriteLine("Can't find file here. Perhaps the path was specified incorrectly.");
                return null;
            }

            if (!(new string(filePath.TakeLast(4).ToArray()).Contains(".dll")))
            {
                WriteLine("It isn't a .dll file, I guess.");
                return null;
            }

            Assembly assembly = Assembly.LoadFrom(filePath);
            List<Player> bots = new List<Player>();

            var types = assembly.GetTypes().Where(x => x.BaseType == typeof(Player));
            foreach (var type in types)
            {
                var constructor = type.GetConstructor(new Type[] { typeof(string), typeof(int)});
                Player bot = (Player)constructor.Invoke(new object[] {type.Name, 1000});
                bots.Add(bot);
            }

            return bots;
        }
    }
}