using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Shoes
    {
        private List<Card> Queue = new List<Card>();
        private int Current = 0;


        public void Fill(int numberOfDecks)
        {
            this.Queue.Clear();
            this.Current = 0;
            for (int i = 0; i < numberOfDecks; i++)
            {
                Deck deck = new Deck();
                deck.Mix();
                this.Queue.AddRange(deck.Cards);
            }
        }
        public Card Withdraw()
        {
            this.Current++;
            return this.Queue[Current - 1];
        }

        public void Check()
        {
            if (this.Current > 300)
                this.Fill(8);
        }
    }

}
