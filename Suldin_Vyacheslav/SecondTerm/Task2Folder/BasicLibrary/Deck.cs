using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Deck
    {
        public Card[] Cards = new Card[52];

        public Deck()
        {
            for (int i = 0; i < 52; i++)
            {
                Cards[i] = new Card(i % 4 + 1, i % 13 + 1);
            }
        }
        
        public void Mix()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = this.Cards.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                Card tmp = this.Cards[j];
                this.Cards[j] = this.Cards[i];
                this.Cards[i] = tmp;
            }
        }
    }
}
