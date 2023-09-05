using System;
using Blackjack;
using BotsPluginManagement;

namespace PrimitiveBotsTask
{
    class Program
    {
        static void Main(string[]? args)
        {
            //if (args.Length != 1)
            //{
            //    throw new ArgumentException($"There must be only one argument - path to the library folder, but {args.Length} were given");
            //}

            // For demonstration purposes. Delete this and next 6 lines to use your path

            args = new string[] { @"..\..\..\..\Bots" };
            Console.WriteLine($"Attention! For demonstration purposes the path was changed to '{args[0]}'");
            Console.WriteLine("Delete lines 16 - 22 in Program.cs file to use your path\n\n");

            // ----------------------

            Game game = new Game();
            float startingSum = 1000F;
            BotsManager botsManager = new BotsManager(args[0], startingSum);
            float[] averageBalance = new float[botsManager.BotsCount];

            Console.WriteLine($"Starting sum = {startingSum}\n");

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < botsManager.BotsCount; j++)
                {
                    game.Play(botsManager.Bots[j], 50, 40);
                    averageBalance[j] += botsManager.Bots[j].Balance / 1000F;
                }

                botsManager.UpdateBots(startingSum);
            }

            for (int i = 0; i < averageBalance.Length; i++)
            {
                Console.WriteLine($"The balance of {botsManager.Bots[i].GetType().Name} = {averageBalance[i]}");
            }
        }
    }
}