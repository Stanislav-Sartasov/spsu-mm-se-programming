using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibrary;

namespace BotLibrary
{
    public class OneThreeTwoSix : Bot
    {
        public OneThreeTwoSix(int str)
            : base(str)
        {
        }

        public int Step = 0;
        public override void MakeBet(int hand)
        {

            bool win = LastBank < Bank;
            int bet;

            if (!win || Step == 4)
                Step = 0;

            if (win && LastBank != 0)
                Step++;


            switch (Step)
            {
                case 0:
                    {
                        bet = Cycle / 2;
                        break;
                    }
                case 1:
                    {
                        bet = Cycle * 3 / 2;
                        break;
                    }
                case 2:
                    {
                        bet = Cycle;
                        break;
                    }
                case 3:
                    {
                        bet = 3 * Cycle;
                        break;
                    }
                default:
                    {
                        bet = Cycle;
                        break;
                    }
            }
            
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
