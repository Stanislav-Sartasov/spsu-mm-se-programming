using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Player
    {
        protected List<Card>[] Hands = new List<Card>[4];
        protected int[] Sum = new int[4] { 0, 0, 0, 0 };


        public Player()
        {
            for (int i = 0; i < 4;  i++ )
            {
                Hands[i] = new List<Card>();
            }
        }

        public int GetSum(int hand)
        {
            return Sum[hand];
        }
        public List<Card> ScanHand(int hand)
        {
            List<Card> handCopy = new List<Card>();
            handCopy.AddRange(Hands[hand]);
            return handCopy;
        }
        public int GetHandsLenght()
        {
            return Hands.Length;
        }
        public void ReceiveCard(Card card, int hand)
        {
            this.Hands[hand].Add(card);
            this.Sum[hand] += card.GetCardInfo()[2];
        }

        public bool IsBlackJack(int hand)
        {
            if (Sum[hand] == 11 && Hands[hand].Exists(x => x.GetCardInfo()[0] == 1) && Hands[hand].Count == 2)
            {
                return true;
            }
                
            return false;
        }
        public void ConfirmBlackJack(int hand)
        {
            Sum[hand] += 10;
        }

        public void Discard()
        {
            for (int j = 0; j < 4; j++)
            {
                this.Hands[j].Clear();
                this.Sum[j] = 0;
            }
        }
    }
}
