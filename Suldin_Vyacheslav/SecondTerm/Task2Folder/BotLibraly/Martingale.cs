using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibraly;

namespace BotLibraly
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

            if (LastBank > Bank)
                LoseStreak++;
            else LoseStreak = 0;
            int bet = Cycle;
            for (int i = 0; i< LoseStreak; i++)
            {
                bet *= 2;
            }

            if (bet <= Bank)
            {
                
            }
            else
            {
                bet = Bank;
            }
            Bets[hand] = bet;
            LastBank = Bank;
            Bank -= bet;
        }
        public override int Answer(int hand, List<Card> dilerHand, List<Gamester> gamesters, Shoes shoes)
        {
            return base.Answer(hand, dilerHand, gamesters, shoes);
        }
    }
}
