using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibraly
{
    public class Gamester : Player
    {
        public int Bank;
        public int[] Bets = new int[4] { 0 ,0 ,0 ,0 }; 
        public virtual int Answer(int hand, List<Card> dilerHand, List<Gamester> players, Shoes shoes)
        {
            return Bets[0]/100;
        }

        public virtual void MakeBet(int hand)
        {
            int bet = 100;
            if (bet <= Bank)
            {
            }
            else
            {
                bet = Bank;
            }
            Bets[hand] = bet;
            Bank -= bet;
        }
    }
}
