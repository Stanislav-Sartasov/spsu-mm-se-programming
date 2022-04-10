using System.Collections.Generic;

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
                for (int j = 0; j < 52; j++)
                    this.queue.Add(deck[j]);

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
