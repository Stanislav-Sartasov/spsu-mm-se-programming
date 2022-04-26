using System.Reflection;
using Plugins;
using BotInterface;

namespace BotManagementPlugin
{
    public class BotManager : IPlugin
    {
        public string Title => "BotManagement";
        public string Description => "Finds bots along their path and organizes competitions between them.";

        private List<IBot> bots = null;
        private List<string> botNames = null;
        private int botCash = 1000;
        private int roundPlayed = 50;

        public void Do()
        {
            Console.WriteLine("Enter the path to the bot library: ");
            string path = Console.ReadLine();

            if (AskForPath(path) == false)
            {
                return;
            }

            while (FindBots(path) == false)
            {
                if (AskForPath("") == false)
                {
                    return;
                }
            }

            BotsPlay();
        }

        private bool FindBots(string path)
        {
            while (path[path.Length - 1] == ' ')
            {
                path = path.Remove(path.LastIndexOf(" "), 1);
            }

            var botsList = new List<IBot>();
            var files = Directory.GetFiles(path, "*.dll"); 

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));
                var pluginTypes = assembly.GetTypes().Where(t => typeof(IBot).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .ToArray();

                foreach (var plugin in pluginTypes)
                {
                    var constructors = plugin.GetConstructors();
                    ConstructorInfo activator = null;

                    foreach (var constructor in constructors)
                    {
                        var paramInfo = constructor.GetParameters();

                        foreach (var param in paramInfo)
                        {
                            if (param.Name == "leaveFunction")
                            {
                                activator = constructor;
                                break;
                            }
                        }

                        if (activator != null)
                        {
                            break;
                        }
                    }

                    Func<int, bool> leaveFunc = (x) => x > roundPlayed;
                    Object[] parameters = new Object[]{leaveFunc, botCash};
                    var pluginInstance = Activator.CreateInstance(plugin, parameters) as IBot;
                    botsList.Add(pluginInstance);
                }
            }

            if (botsList.Count == 0)
            {
                return false;
            }

            bots = botsList;
            return true;
        }

        private void BotsPlay()
        {

            foreach (var bot in bots)
            {
                var gameTable = new Blackjack.GameTable(bot);
                gameTable.Play();
            }

            bots = bots.OrderByDescending(x => x.Cash).ToList();
            
            Console.WriteLine($"\nLet's sum up the results of the bot competition. The initial cash amount is {botCash}. And the bots played {roundPlayed} rounds in BlackJack.\n" +
                              $" - The winner of competition is {bots[0].Name}! He has {bots[0].Cash}\n" +
                              $" - The second place has {bots[1].Name}! He has {bots[1].Cash}!\n" +
                              $" - And the last is {bots[2].Name}. He has {bots[2].Cash}.\n");
        }

        private bool AskForPath(string path)
        {
            while (path == "" || !Directory.Exists(path))
            {
                Console.WriteLine("The path does not exist or there is no any bot libraries. Please, try again or write 'Exit' to leave");
                path = Console.ReadLine();

                if (path == "Exit")
                {
                    return false;
                }
            }

            return true;
        }
    }
}