using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Deck
    {
        private Card[] Cards = new Card[52];

        public Deck()
        {
            for (int i = 0; i < 52; i++)
            {
                Cards[i] = new Card((CardSuit)(i % 4),(CardRank)(i % 13));
            }
        }
        
        public Card[] GetCards()
        {
            Card[] cardsCopy = new Card[Cards.Length];
            Array.Copy(Cards,cardsCopy, Cards.Length);
            return cardsCopy;
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
