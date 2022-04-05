using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Shoes
    {
        private List<Card> queue = new List<Card>();
        private int current = 0;


        public void Fill(int numberOfDecks)
        {
            this.queue.Clear();
            this.current = 0;
            for (int i = 0; i < numberOfDecks; i++)
            {
                Deck deck = new Deck();
                deck.Mix();
                this.queue.AddRange(deck.GetCards());
            }
        }
        public Card Withdraw()
        {
            this.current++;
            return this.queue[current - 1];
        }

        public void Check()
        {
            if (this.current > 300)
                this.Fill(8);
        }
    }

}
