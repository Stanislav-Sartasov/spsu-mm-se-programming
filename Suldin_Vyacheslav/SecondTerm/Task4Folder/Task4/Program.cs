using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Task4.Sdk;
using BasicLibrary;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose program mode: 'Task', 'WithBots', 'Solo'\n" +
                "if !Task mode: 'Call', 'Double', 'Split', 'Surrender', 'Show'" +
                "\nIs need result: 'No', 'Yes'" +
                "\n'Exit' on bet - game is end.");

            Gambler man = new Gambler(100000);
            List<Gamester> players = new List<Gamester>(10);

            Console.WriteLine("Add extensios? ('Yes', 'No'");
            switch (man.confirmer.GetCorectAnswer<AnswerType>())
            {
                case AnswerType.Yes:
                    {
                        var loader = new PluginLoader();
                        var plugins = loader.GetPlugins(SetPaths());
                        if (plugins.Count == 0)
                        {
                            Console.WriteLine("Sorry, no bots have been loaded\n");
                            return;
                        }
                        var plugin = Find(plugins);
                        

                        if (plugin == default(IPluginBot))
                        {
                            Console.WriteLine("Sorry, no bots have been loaded\n");
                            return;
                        }

                        players.AddRange(plugin.LoadBots(loader.GetAsm(plugin.GetType().Name)));

                        Console.WriteLine("Whats your game mode?");
                        switch (man.confirmer.GetCorectAnswer<GameMode>())
                        {
                            case GameMode.Task:
                                {
                                    for (int j = 0; j < 30; j++)
                                    {
                                        foreach (var player in players)
                                            player.ChangeBank(-player.GiveResponce() + 10000);
                                        Game blackJack = new Game(players);
                                        blackJack.Start(40);

                                        foreach (var player in players)
                                            player.Difference += (player.GiveResponce() - 10000) / 30;
                                    }
                                    foreach (var player in players)
                                        Console.WriteLine(player.Difference);
                                    break;
                                }

                            case GameMode.WithBots:
                                {
                                    players.AddRange(plugin.LoadBots(loader.GetAsm(plugin.GetType().Name)));
                                    foreach (var player in players)
                                        player.ChangeBank(-player.GiveResponce() + 10000);

                                    Game jackBlack = new Game(players);
                                    jackBlack.Start(10000);
                                    break;
                                }
                            case GameMode.Solo:
                                {
                                    Console.WriteLine("Solo mode requires no extensions\n");
                                    break;
                                }
                        }

                        break;
                    }
                case AnswerType.No:
                    {
                        Console.WriteLine("So when its solo mode\n");
                        players.Add(man);
                        Game jackBlack = new Game(players);
                        jackBlack.Start(10000);
                        break;
                    }
            }
        }
        static public string[] SetPaths()
        {
            var paths = new List<string>();
            Console.WriteLine("Enter paths of libraries");
            var libPath = Console.ReadLine();
            while (libPath != "stop")
            {
                
                if (!Directory.Exists(libPath))
                {
                    Console.WriteLine("No such directory, try again (or 'stop')");
                }
                else
                {
                    paths.Add(libPath);
                    Console.WriteLine($"{libPath} added, more? (or 'stop')");
                }
                libPath = Console.ReadLine();
            }
            return paths.ToArray();
        }
        static public T Find<T>(List<T> collection)
        {
            var itemName = Console.ReadLine();
            while (itemName != "stop")
            {
                foreach (var item in collection)
                {
                    if (itemName == item.GetType().Name)
                    {
                        return item;
                    }
                }
                Console.WriteLine("Cannot find, try again");
                itemName = Console.ReadLine();
            }
            return default(T);
        }
    }
}
