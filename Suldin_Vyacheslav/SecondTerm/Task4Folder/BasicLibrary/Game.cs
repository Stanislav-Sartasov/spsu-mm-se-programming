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
            gameDealer = new Dealer(17);
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
                    {
                        if (Gamesters[i].GetBet(j) != 0) stop = false;
                        if (Gamesters[i].GetBet(j) == -1) return;
                    }
                        

                if (stop) break;



                PlayerMove[,] condition = new PlayerMove[10, 4];

                gameDealer.InitialDistribution(Gamesters, gameShoes, condition);

                while (gameDealer.Ask(Gamesters, gameShoes, condition))
                {
                }

                gameDealer.TakeCards(gameShoes);
                
                gameDealer.CalculateBets(Gamesters);

                gameDealer.GetCardsBack(Gamesters);

            }
        }
        public static void ShowTable(List<Card> dealerHand, List<Gamester> gamesters)
        {
            Console.Write("          ");
            for (int i = 0; i < dealerHand.Count; i++)
            {
                Console.Write($"{dealerHand[i].GetCardValue()} ");
            }
            Console.Write("\n\n");
            for (int i = 0; i < gamesters.Count; i++)
            {
                Console.Write($"{i}-player: ");
                for (int j = 0; j < 4 && gamesters[i].GetSum(j) != 0; j++)
                {
                    Console.Write("\n");
                    Console.Write($"{j}-hand: [");
                    for (int k = 0; k < gamesters[i][j].Count; k++)
                    {
                        Console.Write($"{gamesters[i][j][k].GetCardValue()} ");
                    }
                    Console.Write("]  bet:");
                    Console.Write($"{gamesters[i].GetBet(j)} ");
                }
                Console.Write("\n\n");
            }
        }
    }
}
