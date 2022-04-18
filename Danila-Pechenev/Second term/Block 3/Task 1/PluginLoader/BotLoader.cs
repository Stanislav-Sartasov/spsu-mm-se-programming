namespace PluginLoader;
using System.Reflection;
using Roulette;

public static class BotLoader
{
    public static APlayer[]? LoadBots(string pathToFolder, object[] parameters)
    {
        if (!Directory.Exists(pathToFolder))
        {
            Console.WriteLine("The folder is not found.");
            return null;
        }

        var fileNames = Directory.GetFiles(pathToFolder, "*.dll");
        if (fileNames.Length == 0)
        {
            Console.WriteLine("There are no .dll files in the folder.");
            return null;
        }

        var bots = new List<APlayer>();
        foreach (string fileName in fileNames)
        {
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(fileName));
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(APlayer)))
                {
                    bots.Add((APlayer)Activator.CreateInstance(type, parameters));
                }
            }
        }

        return bots.ToArray();
    }
}
