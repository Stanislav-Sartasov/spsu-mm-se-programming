using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibrary;

namespace BotLibrary
{
    public class Oscar : Bot
    {

        public int Wins;
        public Oscar(int str)
            : base(str)
        {
        }
        public override void MakeBet(int hand)
        {

            if (LastBank < Bank)
                Wins++;
            int bet = 100 + Wins*3;

            if (bet < Bank)
            {
            }
            else bet = Bank;

            Bets[hand] = bet;
            LastBank = Bank;
            Bank -= bet;

        }
    }
}
