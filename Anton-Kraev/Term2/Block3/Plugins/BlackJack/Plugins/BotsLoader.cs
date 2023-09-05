using System.Reflection;
using BotStructure;

namespace Plugins
{
    public static class BotsLoader
    {
        public static List<IBot> LoadBots(string path)
        {
            var bots = new List<IBot>();
            var dlls = new List<string>();

            try
            {
                dlls = Directory.GetFiles(path, "*.dll").ToList();
            }
            catch (Exception)
            {
                Console.WriteLine($"path '{path}' was incorrect\n");
                return bots;
            }

            foreach (var dll in dlls)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    
                    var type = assembly
                        .GetTypes()
                        .Where(x => x.BaseType == typeof(Bot) && x.GetInterfaces().Contains(typeof(IBot)))
                        .First();

                    var bot = type
                        .GetConstructor(new[] { typeof(int) })
                        .Invoke(new object[] { 1000 });

                    bots.Add((IBot)bot);
                }
                catch (Exception)
                {
                    Console.WriteLine("failed to load the library with bot");
                }
            }

            if (bots.Count == 0)
                Console.WriteLine($"path '{path}' does not contain libraries with bots\n");
                
            return bots;
        }
    }
}