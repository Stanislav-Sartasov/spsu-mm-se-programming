using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibrary;

namespace BotLibrary
{
    public class Martingale : Bot
    {
        public Martingale(int str)
            : base(str)
        {
        }

        public int LoseStreak = 0;
        public override void MakeBet(int hand)
        {

            if (LastBank > bank)
                LoseStreak++;
            else LoseStreak = 0;
            int bet = Cycle;
            for (int i = 0; i < LoseStreak; i++)
            {
                bet *= 2;
            }

            if (bet <= bank)
            {
            }
            else
            {
                bet = bank;
            }
            bets[hand] = bet;
            LastBank = bank;
            bank -= bet;
        }
    }
}
