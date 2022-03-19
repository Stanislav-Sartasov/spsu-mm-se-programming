using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibraly
{
    public class Deck
    {
        public Card[] Cards = new Card[52];

        public Deck()
        {
            for (int i = 0; i<52; i++)
            {
                Cards[i] = new Card("","");
                Cards[i].Suit = SuitFromInt(i % 4);
                Cards[i].Rank = RankFromInt(i % 13);
                Cards[i].Value = Cards[i].CalculateValue(Cards[i].Rank);
            }
        }
        
        private string SuitFromInt(int suit)
        {
            if (suit == 0) return "Clubs";
            else if (suit == 1) return "Diamonds";
            else if (suit == 2) return "Hearts";
            else return "Spades";
        }
        private string RankFromInt(int rank)
        {
            if (rank <= 8) return (rank + 2).ToString();
            else if (rank == 9) return "Jack";
            else if (rank == 10) return "Queen";
            else if (rank == 11) return "King";
            else return "Ace";
        }

        
    }
}
