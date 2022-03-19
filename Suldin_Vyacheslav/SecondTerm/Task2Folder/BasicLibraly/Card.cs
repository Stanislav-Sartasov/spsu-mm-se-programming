using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibraly
{
    public class Card
    {
        public string Suit;
        public string Rank;
        public int Value;

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
            Value = CalculateValue(rank);
        }
        public int CalculateValue(string rank)
        {
            switch (rank.Length)
            {
                case 1:
                    {
                        return rank[0] - '0';
                    }
                case 3: 
                    {
                        return 1;
                    }
                default:
                    {
                        return 10;
                    }   
            }

        }
        public static bool operator ==(Card firstCard,Card secondCard)
        {
            if (!String.Equals(firstCard.Rank, secondCard.Rank) ||
                !String.Equals(firstCard.Suit, secondCard.Suit) ||
                firstCard.Value != secondCard.Value)
                return false;
            else return true;


        }
        public static bool operator !=(Card firstCard, Card secondCard)
        {
            return !(firstCard == secondCard);
        }
    }
}
