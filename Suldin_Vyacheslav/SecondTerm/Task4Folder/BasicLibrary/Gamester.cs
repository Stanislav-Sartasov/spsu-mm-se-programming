using System.Collections.Generic;

namespace BasicLibrary
{
    public class Gamester : Player
    {
        protected int bank = 0;
        protected int[] bets = new int[4] { 0, 0, 0, 0 };
        public int Difference { get; set; }

        public virtual PlayerMove Answer(int hand, List<Card> dealerHand, List<Gamester> gamesters)
        {
            return (PlayerMove)(bets[0]/100);
        }   
        public virtual bool IsNeedResult()
        {
            return false;
        }
        public virtual void MakeBet(int hand)
        {
            int bet = 100;
            if (bet <= bank)
            {
            }
            else
            {
                bet = bank;
            }
            bets[hand] = bet;
            bank -= bet;
        }

        public void ChangeBank(int money)
        {
            bank += money;
        }
        public void SplitCards(int from, int to)
        {
            this.hands[to].Add(this.hands[from][0]);
            this.hands[from].Remove(this.hands[from][0]);
            this.sum[from] /= 2;
            this.sum[to] += this.sum[from];
            this.bets[to] = this.bets[from];
            this.bank -= this.bets[from];
        }

        public int GetBet(int hand)
        {
            return this.bets[hand]; 
        }
        public void SetBet(int hand, int bet)   
        {
            this.bets[hand] = bet;
        }
        public void ReturnBet(int hand)
        {
            this.bets[hand] = 0;
        }
        public int GiveResponce()
        {
            return this.bank;
        }
    }
}
