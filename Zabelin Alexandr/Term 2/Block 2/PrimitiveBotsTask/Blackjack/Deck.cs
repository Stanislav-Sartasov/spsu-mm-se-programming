using System;
using System.Collections.Generic;

namespace Blackjack
{
    internal class Deck
    {
        private List<Card> deck;

        internal int Length { get; private set; }

        internal Deck()
        {
            this.Length = 52;
            this.deck = new List<Card>(this.Length);
            int j;

            for (int i = 0; i < 4; i++)
            {
                for (j = i * 13; j < (i + 1) * 13; j++)
                {
                    this.deck.Add(new Card((byte)((j % 13) + 2)));
                }
            }
        }

        internal Card TakeCard()
        {
            {
                int index = RandomInt.RandInt(this.Length);
                Card card = this.deck[index];

                this.deck.RemoveAt(index);
                this.Length--;

                return card;
            }
        }
    }
}

