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
            Console.WriteLine("Choose program mode: 'Task', 'WithBots', 'Solo'\n" +
                "if !Task mode: 'Call', 'Double', 'Split', 'Surrender', 'Show'" +
                "\nIs need result: 'No', 'Yes'" +
                "\n'Exit' on bet - game is end.");

            Bot[] set = new Bot[] { new Oscar(1), new OneThreeTwoSix(1), new Martingale(1),
            new Oscar(2), new OneThreeTwoSix(2), new Martingale(2),
            new Oscar(3), new OneThreeTwoSix(3), new Martingale(3)};

            Gambler man = new Gambler(100000);
            List<Gamester> players = new List<Gamester>(10);



            switch (Gambler.GetCorectAnswer<GameMode>())
            {
                case GameMode.Task:
                    {
                        int[] taskAnswer = new int[9];
                        players.AddRange(set);

                        Game jackBlack = new Game(players);
                        int j;
                        for (j = 0; j < 30; j++)
                        {
                            for (int i = 0; i < set.Length; i++)
                            {
                                set[i].ChangeBank(-set[i].GiveResponce() + 10000);
                            }

                            jackBlack.Start(40);

                            for (int i = 0; i < set.Length; i++)
                                set[i].Difference += (set[i].GiveResponce() - 10000) / 30;
                        }

                        for (int i = 0; i < set.Length; i++)
                            Console.WriteLine(set[i].Difference);

                        break;
                    }
                case GameMode.WithBots:
                    {
                        for (int i = 0; i < set.Length; i++)
                            set[i].ChangeBank(100000);

                        players.Add(man);
                        players.AddRange(set);
                        Game jackBlack = new Game(players);
                        jackBlack.Start(10000);
                        break;
                    }
                case GameMode.Solo:
                    {
                        players.Add(man);
                        Game jackBlack = new Game(players);
                        jackBlack.Start(10000);
                        break;
                    }
            }
        }
        public static Type GetCorectMode(Type enumType)
        {
            while (true)
            {
                string answer = Console.ReadLine();

                foreach (Type mode in Enum.GetValues(enumType) )
                {
                    if (String.Equals(answer, mode))
                    {
                        return mode;
                    }
                }
                Console.WriteLine("Wrong input");
            }
        }
    }
}
