using System.Reflection;
using AbstractClasses;
using UsualBaseStrategyBotLibrary;

namespace PlaginLoaderLibrary
{
    public class PlaginLoader
    {
        public List<ABot> AllBots { get; private set; }

        public PlaginLoader(string path, int startBalance, int startRate)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("The folder does not exist or the path specified is incorrect.");
                return;
            }
            Console.WriteLine("The folder was found...");

            var dllFiles = Directory.EnumerateFiles(path).Where(s => s.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)).ToList();

            if (dllFiles.Count() == 0)
            {
                Console.WriteLine("There are no plugins with the extension in this folder .dll, bots have not been added.");
                return;
            }

            Console.WriteLine($"{dllFiles.Count()} dll files was founded...");

            AllBots = new List<ABot>();
            object[] args = { startBalance, startRate };
            foreach (string dllFile in dllFiles)
            {
                var type = Assembly.LoadFrom(dllFile).GetTypes()
                    .Where(s => s.BaseType == typeof(UsualBaseStrategyBot) || s.BaseType == typeof(ABot)).ToList();
                if (type.Count() != 0)
                    AllBots.Add((ABot)Activator.CreateInstance(type[0], args));
            }

            Console.WriteLine($"All {dllFiles.Count()} dll files have been successfully uploaded.");
        }
    }
}