using System.Collections.Generic;

namespace Blackjack
{
    public class Deck
    {
        private List<Card> deck = new List<Card>();

        public byte Length { get; private set; }

        public Deck(uint decksNumber)
        {
            for (byte i = 2; i < 12; i++)
            {
                if (i == 10)
                {
                    this.deck.Add(new Card(3 * decksNumber, 10));
                }
                else
                {
                    this.deck.Add(new Card(decksNumber, i));
                }
            }

            this.Length = (byte)deck.Count;
        }

        public byte TakeCard()
        {
            var random = new Random();
            int randomInt = random.Next(deck.Count);
            Card card = deck[randomInt];
            byte cardWeight = card.Weight;

            card.Number--;

            if (card.Number == 0)
            {
                deck.RemoveAt(randomInt);
                this.Length--;
            }

            return cardWeight;
        }
    }
}
