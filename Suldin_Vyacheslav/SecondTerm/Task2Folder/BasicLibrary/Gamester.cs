using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Gamester : Player
    {
        public int Bank;
        public int[] Bets = new int[4] { 0 ,0 ,0 ,0 }; 
        public virtual int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters, Shoes shoes)
        {
            return Bets[0]/100;
        }
        public virtual bool IsNeedResult()
        {
            return false;
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
