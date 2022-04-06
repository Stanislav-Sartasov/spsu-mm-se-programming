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
        public override int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
        {

            //string userCulture = Thread.CurrentThread.CurrentUICulture.Name;

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB"); 

            ResourceManager rm = new ResourceManager("BotLibrary.resources.strategyPack.strategy" + Strategy.ToString(),
                Assembly.GetExecutingAssembly());

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(userCulture);

            int dealerSum = dealerHand[0].GetCardInfo()[2];
            int sum = this.sum[hand];
            int i = -1;
            List<Card> botHand = this.ScanHand(hand);
            if (botHand.Count == 2 && botHand[0].GetCardInfo()[2] == botHand[1].GetCardInfo()[2] && this.ScanHand(3).Count == 0)
            {
                if (botHand.Exists(x => x.GetCardInfo()[0] == 1)) i = 10;
                else i = 20 - 2 + botHand[0].GetCardInfo()[2];
            }

            else if (botHand.Exists(x => x.GetCardInfo()[0] == 1))
            {
                i = 10 - 2 + this.sum[hand];
                if (i >= 19) i = 19;
            }
            else if (sum <= 8) i = 0;
            else if (sum > 8 && sum < 17) i = -8 + this.sum[hand];
            else i = 9;

            char[] h = rm.GetString("String" + i.ToString()).ToCharArray();

            int result = Convert.ToInt32(h[-1 + dealerHand[0].GetCardInfo()[2]]) - 48;

            if (result == 3 && this.ScanHand(3).Count != 0) return 1;
            return result;



        }
        public int GiveResponce()
        {
            return this.bank;
        }
    }
}