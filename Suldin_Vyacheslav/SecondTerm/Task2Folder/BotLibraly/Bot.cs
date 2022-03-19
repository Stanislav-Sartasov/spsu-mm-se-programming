using System;
using BasicLibraly;
using System.Collections.Generic;
using System.IO;

namespace BotLibraly
{
    public class Bot : Gamester
    {
        public int Strategy;
        static public int Cycle = 100;
        public int LastBank = 0;
        // 1 - medium, 2 - risky, 3 - save
        public Bot(int str)
        {
            Strategy = str;
        }
        public override int Answer(int hand, List<Card> dilerHand, List<Gamester> gamesters, Shoes shoes)
        {
            string path = "..\\..\\..\\..\\BotLibraly\\strategy"+Strategy.ToString()+".txt";
            string[] strategyTable = File.ReadAllLines(path);
            int dilerSum = dilerHand[0].Value;
            int sum = this.Sum[hand];
            int i = -1;

            if (this.Hands[hand].Count == 2 && this.Hands[hand][0].Value == this.Hands[hand][1].Value && this.Hands[3].Count == 0)
            {
                if (this.Hands[hand].Exists(x => x.Rank == "Ace")) i = 10;
                else i = 20 - 2 + this.Hands[hand][0].Value;
            }

            else if (this.Hands[hand].Exists(x => x.Rank == "Ace") )
            {
                i = 10 - 2 + this.Sum[hand];
                if (i >= 19) i = 19;
            }
            else if (sum <= 8) i = 0;
            else if (sum > 8 && sum < 17) i = -8 + this.Sum[hand];
            else i = 9;

            char[] h = strategyTable[i].ToCharArray();

            int result = Convert.ToInt32(h[-1 + dilerHand[0].Value]) - 48;

            if (result == 3 && this.Hands[3].Count != 0) return 1;
            return result;

        }

        
    }
}
