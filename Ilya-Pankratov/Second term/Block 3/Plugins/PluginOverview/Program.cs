using Plugins;

namespace PluginOverview
{ 
    public static class Program
    {

        static void Main(string[] agrs)
        {
            Console.WriteLine("The program demonstrates the ability to connect plugins to blackjack.\n" +
                              "The plugins will be searched as a .dll file in the 'Extensions' folder.\nNow one plugin 'BotManagementPlugin' is connected to the program.\n" +
                              "For convenience, the library with bots can be found in the 'Extensions' folder too.");

            PluginManager.LoadExtensions("..\\..\\..\\..\\Extensions");

            if (PluginManager.Plugins != null && PluginManager.Plugins.Count != 0)
            {
                foreach (var plugin in PluginManager.Plugins)
                {
                    Console.WriteLine($"Plugin title: {plugin.Title}\nPlugin description: {plugin.Description}\n");
                    plugin.Do();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Plugins were not found!");
            }

            Console.WriteLine("That's all! Thank you!");
            return;
        }
    }
}