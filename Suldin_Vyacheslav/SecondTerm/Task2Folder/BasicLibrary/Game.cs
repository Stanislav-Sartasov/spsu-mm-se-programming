using System;
using System.Collections.Generic;

namespace BasicLibrary
{

    public class Game
    {
        public List<Gamester> Gamesters;
        private Dealer gameDealer;
        private Shoes gameShoes;

        
        public Game(List<Gamester> gamesters)
        {
            this.Gamesters = gamesters;
            gameShoes = new Shoes();
            gameDealer = new Dealer();
            gameShoes.Fill(8);
            
        }
        public void Start(int shuffle)
        {
            while (shuffle != 0)
            {
                shuffle--;


                gameShoes.Check();

                bool stop = true;

                for (int i = 0; i < Gamesters.Count; i++)
                    Gamesters[i].MakeBet(0);

                for (int i = 0; i < Gamesters.Count; i++)
                    for (int j = 0; j < Gamesters[i].GetHandsLenght(); j++)
                        if (Gamesters[i].GetBet(j) != 0) stop = false;

                if (stop) break;



                int[,] condition = new int[10, 4];

                gameDealer.InitialDistribution(Gamesters, gameShoes, condition);

                while (gameDealer.Ask(Gamesters, gameShoes, condition))
                {
                }


                while (gameDealer.GetSum(0) < 17 && !gameDealer.IsBlackJack(0))
                    gameDealer.GiveCard(gameDealer, 0, gameShoes);


                gameDealer.CalculateBets(Gamesters);

                gameDealer.GetCardsBack(Gamesters);

            }
        }

        public static int GetCoorectAnswer(int bottom, int top)
        {
            int answer;
            while (!int.TryParse(Console.ReadLine(), out answer) || answer > top || answer < bottom)
                Console.WriteLine($"Error, enter {bottom}-{top}");
            return answer;
        }
        public static void ShowTable(List<Card> dealerHand, List<Gamester> gamesters)
        {
            Console.Write("          ");
            for (int i = 0; i < dealerHand.Count; i++)
            {
                Console.Write($"{dealerHand[i].GetCardInfo()[2]} ");
            }
            Console.Write("\n\n");
            for (int i = 0; i < gamesters.Count; i++)
            {
                Console.Write($"{i}-player: ");
                for (int j = 0; j < 4 && gamesters[i].GetSum(j) != 0; j++)
                {
                    Console.Write("\n");
                    Console.Write($"{j}-hand: [");
                    for (int k = 0; k < gamesters[i].ScanHand(j).Count; k++)
                    {
                        Console.Write($"{gamesters[i].ScanHand(j)[k].GetCardInfo()[2]} ");
                    }
                    Console.Write("]  bet:");
                    Console.Write($"{gamesters[i].GetBet(j)} ");
                }
                Console.Write("\n\n");
            }
        }
    }
}
