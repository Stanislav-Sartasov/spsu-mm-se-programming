using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecksLibrary
{
    public class Decks
    {
        public byte[] Cards { get; private set; }
        public double CountOfCards { get; private set; }

        public void FillCards()
        {
            Random random = new Random();
            Cards = new byte[416];
            for (int k = 0; k < 32; k++)
            {
                for (int i = 0; i < 13; i++)
                {
                    Cards[i + k * 13] = (byte)(i + 1);
                    if (i > 9)
                    {
                        Cards[i + k * 13] = 10;
                    }
                }
            }
            // I make the cards go in random order in the array
            for (int i = 416 - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = Cards[j];
                Cards[j] = Cards[i];
                Cards[i] = temp;
            }
            CountOfCards = 0;
        }

        public double CountCards(byte value)
        {
            // I use Halves strategy of counting cards from wikipedia "Card counting" article
            // I make the oldValue variable and double return for the testing
            double oldValue = CountOfCards;
            if (value == 2 || value == 7)
            {
                CountOfCards += 0.5;
            }
            else if (value == 3 || value == 4 || value == 6)
            {
                CountOfCards += 1;
            }
            else if (value == 5)
            {
                CountOfCards += 1.5;
            }
            else if (value == 10 || value == 1)
            {
                CountOfCards -= 1;
            }
            else if (value == 9)
            {
                CountOfCards -= 0.5;
            }
            return CountOfCards - oldValue;
        }

        public byte GetCard()
        {
            int index;
            byte value;
            index = 0;
            while (Cards[index] == 0)
            {
                index += 1;
                if (index == 416)
                {
                    FillCards();
                    index = 0;
                }
            }
            value = Cards[index];
            Cards[index] = 0;
            CountCards(value);
            return value;
        }
    }
}
