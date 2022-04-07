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

        public Bot(int str)
        {
            Strategy = str;
        }
        public override PlayerMove Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
        {

            ResourceManager rm = new ResourceManager("BotLibrary.resources.strategyPack.strategy" + Strategy.ToString(),
                Assembly.GetExecutingAssembly());

            int dealerSum = dealerHand[0].GetCardValue();
            int sum = this.sum[hand];
            int i = -1;
            List<Card> botHand = this[hand];
            if (botHand.Count == 2 && botHand[0].GetCardValue() == botHand[1].GetCardValue() && this[3].Count == 0)
            {
                if (botHand.Exists(x => x.GetCardValue() == 1)) i = 10;
                else i = 20 - 2 + botHand[0].GetCardValue();
            }

            else if (botHand.Exists(x => x.GetCardValue() == 1))
            {
                i = 10 - 2 + this.sum[hand];
                if (i >= 19) i = 19;
            }
            else if (sum <= 8) i = 0;
            else if (sum > 8 && sum < 17) i = -8 + this.sum[hand];
            else i = 9;

            char[] h = rm.GetString("String" + i.ToString()).ToCharArray();

            int result = Convert.ToInt32(h[-1 + dealerHand[0].GetCardValue()]) - 48;

            if (result == 3 && this[3].Count != 0) return (PlayerMove)1;
            return (PlayerMove)result;
        }
        public int GiveResponce()
        {
            return this.bank;
        }
    }
}