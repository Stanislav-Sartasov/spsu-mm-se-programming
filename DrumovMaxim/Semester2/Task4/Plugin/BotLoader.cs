using GameTable.BotStructure;
using System.Reflection;

namespace Plugin
{
    public class BotLoader
    {
        public List<IBot>? BotsLoader (string folderPath, object[] launchArgs)
        {
            if(!Directory.Exists(folderPath))
            {
                Console.WriteLine("The folder was not found at the specified path");
                return null;
            }

            var bots = new List<IBot>();
            var plugins = Directory.GetFiles(folderPath, "*.dll").ToList(); 
            
            foreach(string plugin in plugins)
            {
                if(!File.Exists(plugin))
                {
                    continue;
                }

                try
                {
                    Assembly assembly = Assembly.LoadFrom(plugin);
                    Type[] types = assembly.GetTypes().Where(type => typeof(IBot).IsAssignableFrom(type) && !type.IsAbstract).ToArray();
                    int i = 0, found = 0;

                    foreach (Type type in types)
                    {
                        found = types[i].ToString().IndexOf(".");
                        launchArgs[0] = types[i].ToString().Substring(found + 1);
                        bots.Add((IBot)Activator.CreateInstance(type, launchArgs));                        
                        i++;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error loading plugin from path {plugin}");
                }
            }

            return bots;
        }
    }
}
