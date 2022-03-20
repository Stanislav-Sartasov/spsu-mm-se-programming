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
            TheDiler.FillShoe(TheShoes, 8);
            
        }
        public void Start(int shuffle)
        {
            while (shuffle != 0)
            {
                shuffle--;

                if (this.TheShoes.Current > 300)
                    TheDiler.FillShoe(TheShoes, 8);

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
                }


                while (TheDiler.Sum[0] < 17 && !TheDiler.IsBlackJack(0))
                    TheDiler.GiveCard(TheDiler, 0, TheShoes);


                TheDiler.CalculateBets(Gamesters);

                TheDiler.GetCardsBack(Gamesters);

            }
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
