using System;
using System.Collections.Generic;

namespace BasicLibraly
{

    public class Game
    {
        public List<Gamester> Gamesters;
        public Diler TheDiler;
        public Shoes TheShoes;

        
        public Game(List<Gamester> Gamesters)
        {
            this.Gamesters = Gamesters;
            TheShoes = new Shoes();
            TheDiler = new Diler();
            TheDiler.FillShoes(TheShoes, 8);
            
        }
        public void Start(int shuffle)
        {
            while (shuffle != 0)
            {
                shuffle--;

                if (this.TheShoes.Current > 300)
                    TheDiler.FillShoes(TheShoes, 8);

                bool stop = true;

                for (int i = 0; i < Gamesters.Count; i++)
                    Gamesters[i].MakeBet(0);

                for (int i = 0; i < Gamesters.Count; i++)
                    for (int j = 0; j < Gamesters[i].Hands.Length; j++)
                        if (Gamesters[i].Bets[j] != 0) stop = false;

                if (stop) break;



                int[,] condition = new int[9, 4];

                TheDiler.InitialDistribution(Gamesters, TheShoes, condition);

                while (TheDiler.Ask(Gamesters, TheShoes, condition))
                {
                    //for (int i = 0; i < 9; i++)
                    //{
                    //    for (int j = 0; j < 4; j++)
                    //    {
                    //        if (condition[i, j] == 3)
                    //            this.ShowBroke(i);
                    //    }
                    //}

                }


                while (TheDiler.Sum[0] < 17 && !TheDiler.IsBlackJack(0))
                    TheDiler.GiveCard(TheDiler, 0, TheShoes);


                TheDiler.CalculateBets(Gamesters);

                TheDiler.GetCardsBack(Gamesters);

            }
        }

        private void ShowBroke(int i)
        {
            for (int j = 0; j < 4 && Gamesters[i].Sum[j] != 0; j++)
            {
                Console.Write("\n");
                
                Console.Write($"{j}-hand: [");
                for (int k = 0; k < Gamesters[i].Hands[j].Count; k++)
                {
                    Console.Write($"{Gamesters[i].Hands[j][k].Value} ");
                }
                Console.Write("]  bet:");
                Console.Write($"{Gamesters[i].Bets[j]} ");
                Console.Write($"{Gamesters[i].Bank} ");
            }
            Console.Write("\n\n");
        }

        public static int GetCoorectAnswer(int bottom, int top)
        {
            int answer;
            while (!int.TryParse(Console.ReadLine(), out answer) || answer > top || answer < bottom)
                Console.WriteLine($"Error, enter {bottom}-{top}");
            return answer;
        }
    }
}
