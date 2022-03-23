using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Player
    {
        public List<Card>[] Hands = new List<Card>[4];
        public int[] Sum = new int[4] { 0, 0, 0, 0};
        public bool[] BlackJack = new bool[] { };
            
        public Player()
        {
            for (int i = 0; i < 4;  i++ )
            {
                Hands[i] = new List<Card>();
            }
        }
        public void ReceiveCard(Card card, int hand)
        {
            this.Hands[hand].Add(card);
            this.Sum[hand] += card.Value;
        }

        public bool IsBlackJack(int hand)
        {
            if (Sum[hand] == 11 && Hands[hand].Exists(x => x.Rank == "Ace") && Hands[hand].Count == 2)
            {
                return true;
            }
                
            return false;
        }
    }
}
