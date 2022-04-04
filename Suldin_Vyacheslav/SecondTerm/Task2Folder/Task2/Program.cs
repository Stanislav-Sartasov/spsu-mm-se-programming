using System;
using System.Collections.Generic;
using BasicLibrary;
using BotLibrary;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
       {
            Console.WriteLine("Choose program mode: 1 = task mode,  2 = with bots mode, 3 = solo mode\n" +
                "if 3: 1 - call, 2 - double, 3 - split, 4 - surrender, 5 - show your cards" +
                "\n0 on bet - game is end.");

            Bot[] set = new Bot[] { new Oscar(1), new OneThreeTwoSix(1), new Martingale(1),
            new Oscar(2), new OneThreeTwoSix(2), new Martingale(2),
            new Oscar(3), new OneThreeTwoSix(3), new Martingale(3)};

            Gambler man = new Gambler(100000);
            List<Gamester> players = new List<Gamester>(10);



            switch (Game.GetCoorectAnswer(1, 3))
            {
                case 1:
                    {
                        int[] taskAnswer = new int[9];
                        players.AddRange(set);

                        Game jackBlack = new Game(players);
                        int j;
                        for ( j = 0; j < 30; j++)
                        {
                            for (int i = 0; i < set.Length; i++)
                                set[i].Bank = 10000;
                            jackBlack.Start(40);

                            for (int i = 0; i < set.Length; i++)
                                set[i].Difference += (set[i].Bank - 10000)/30;
                        }

                        for (int i = 0; i < set.Length; i++)
                            Console.WriteLine(set[i].Difference);

                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < set.Length; i++)
                            set[i].Bank = 100000;

                        players.Add(man);
                        players.AddRange(set);
                        Game jackBlack = new Game(players);
                        jackBlack.Start(10000);
                        break;
                    }
                default:
                    {
                        players.Add(man);
                        Game jackBlack = new Game(players);
                        jackBlack.Start(10000);
                        break;
                    }
            }
        }
    }
}
