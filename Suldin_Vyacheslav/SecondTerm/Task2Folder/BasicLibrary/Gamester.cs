using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Gamester : Player
    {
        protected int Bank = 0;
        protected int[] Bets = new int[4] { 0, 0, 0, 0 }; 
        public virtual int Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
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

        public void ChangeBank(int money)
        {
            Bank += money;
        }
        public void SplitCards(int from, int to)
        {
            this.Hands[to].Add(this.Hands[from][0]);
            this.Hands[from].Remove(this.Hands[from][0]);
            this.Sum[from] /= 2;
            this.Sum[to] += this.Sum[from];
            this.Bets[to] = this.Bets[from];
            this.Bank -= this.Bets[from];
        }

        public int GetBet(int hand)
        {
            return this.Bets[hand]; 
        }
        public void SetBet(int hand, int bet)   
        {
            this.Bets[hand] = bet;
        }
        public void ReturnBet(int hand)
        {
            this.Bets[hand] = 0;
        }
    }
}
