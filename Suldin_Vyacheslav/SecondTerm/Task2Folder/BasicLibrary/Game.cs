using System;
using System.Collections.Generic;

namespace BasicLibrary
{

    public class Game
    {
        public List<Gamester> Gamesters;
        public Dealer TheDealer;
        public Shoes TheShoes;

        
        public Game(List<Gamester> Gamesters)
        {
            this.Gamesters = Gamesters;
            TheShoes = new Shoes();
            TheDealer = new Dealer();
            TheDealer.FillShoe(TheShoes, 8);
            
        }
        public void Start(int shuffle)
        {
            while (shuffle != 0)
            {
                shuffle--;

                if (this.TheShoes.Current > 300)
                    TheDealer.FillShoe(TheShoes, 8);

                bool stop = true;

                for (int i = 0; i < Gamesters.Count; i++)
                    Gamesters[i].MakeBet(0);

                for (int i = 0; i < Gamesters.Count; i++)
                    for (int j = 0; j < Gamesters[i].Hands.Length; j++)
                        if (Gamesters[i].Bets[j] != 0) stop = false;

                if (stop) break;



                int[,] condition = new int[9, 4];

                TheDealer.InitialDistribution(Gamesters, TheShoes, condition);

                while (TheDealer.Ask(Gamesters, TheShoes, condition))
                {
                }


                while (TheDealer.Sum[0] < 17 && !TheDealer.IsBlackJack(0))
                    TheDealer.GiveCard(TheDealer, 0, TheShoes);


                TheDealer.CalculateBets(Gamesters);

                TheDealer.GetCardsBack(Gamesters);

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
