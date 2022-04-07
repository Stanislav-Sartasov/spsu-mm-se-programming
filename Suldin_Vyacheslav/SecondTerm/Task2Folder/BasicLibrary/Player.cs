using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Player
    {
        protected List<Card>[] hands = new List<Card>[4];
        protected int[] sum = new int[4] { 0, 0, 0, 0 };


        public Player()
        {
            for (int i = 0; i < 4;  i++ )
            {
                hands[i] = new List<Card>();
            }
        }

        public int GetSum(int hand)
        {
            return sum[hand];
        }

        public List<Card> this[int index]
        {
            get => hands[index];
        }
        public int GetHandsLenght()
        {
            return hands.Length;
        }
        public void ReceiveCard(Card card, int hand)
        {
            this.hands[hand].Add(card);
            this.sum[hand] += card.GetCardValue();
        }

        public bool IsBlackJack(int hand)
        {
            if (sum[hand] == 11 && hands[hand].Exists(x => x.GetCardValue() == 1) && hands[hand].Count == 2)
            {
                return true;
            }
                
            return false;
        }
        public void ConfirmBlackJack(int hand)
        {
            sum[hand] += 10;
        }

        public void Discard()
        {
            for (int j = 0; j < 4; j++)
            {
                this.hands[j].Clear();
                this.sum[j] = 0;
            }
        }
    }
}
