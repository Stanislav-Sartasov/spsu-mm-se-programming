using System;
using BasicLibrary;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace BotLibrary
{
    public class Bot : Gamester
    {
        public int Strategy;
        static public int Cycle = 100;
        public int LastBank = 0;

        public double Difference = 0;

        // 1 - medium, 2 - risky, 3 - save
        public Bot(int str)
        {
            Strategy = str;
        }
        public override int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US"); 
            ResourceManager rm = new ResourceManager("BotLibrary.resources.strategyPack.strategy" + Strategy.ToString(),
                Assembly.GetExecutingAssembly());

            int dealerSum = dealerHand[0].GetCardInfo()[2];
            int sum = this.Sum[hand];
            int i = -1;

            if (this.Hands[hand].Count == 2 && this.Hands[hand][0].GetCardInfo()[2] == this.Hands[hand][1].GetCardInfo()[2] && this.Hands[3].Count == 0)
            {
                if (this.Hands[hand].Exists(x => x.GetCardInfo()[0] == 1)) i = 10;
                else i = 20 - 2 + this.Hands[hand][0].GetCardInfo()[2];
            }

            else if (this.Hands[hand].Exists(x => x.GetCardInfo()[0] == 1))
            {
                i = 10 - 2 + this.Sum[hand];
                if (i >= 19) i = 19;
            }
            else if (sum <= 8) i = 0;
            else if (sum > 8 && sum < 17) i = -8 + this.Sum[hand];
            else i = 9;

            char[] h = rm.GetString("String" + i.ToString()).ToCharArray();

            int result = Convert.ToInt32(h[-1 + dealerHand[0].GetCardInfo()[2]]) - 48;

            if (result == 3 && this.Hands[3].Count != 0) return 1;
            return result;

        }


    }
}