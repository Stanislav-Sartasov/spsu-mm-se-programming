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

            Bot[] set = new Bot[] { new Counter(1), new OneThreeTwoSix(1), new Martingale(1),
            new Counter(2), new OneThreeTwoSix(2), new Martingale(2),
            new Counter(3), new OneThreeTwoSix(3), new Martingale(3)};

            Gambler man = new Gambler(100000);
            List<Gamester> players = new List<Gamester>(10);



            switch (Game.GetCoorectAnswer(1, 3))
            {
                case 1:
                    {
                        int[] taskAnswer = new int[9];
                        players.AddRange(set);

                        Game JackBlack = new Game(players);

                        for (int j = 0; j < 100; j++)
                        {
                            for (int i = 0; i < set.Length; i++)
                                set[i].Bank = 10000;
                            JackBlack.Start(40);

                            for (int i = 0; i < set.Length; i++)
                                taskAnswer[i] += set[i].Bank - 10000;
                        }

                        for (int i = 0; i < set.Length; i++)
                            Console.WriteLine(taskAnswer[i] / 100);

                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < set.Length; i++)
                            set[i].Bank = 100000;

                        players.Add(man);
                        players.AddRange(set);
                        Game JackBlack = new Game(players);
                        JackBlack.Start(10000);
                        break;
                    }
                default:
                    {
                        players.Add(man);
                        Game JackBlack = new Game(players);
                        JackBlack.Start(10000);
                        break;
                    }
            }


            Console.WriteLine();
        }


    }
}
