using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Card
    {
        private int Suit;
        private int Rank;
        private int Value;

        public Card(int suit, int rank)
        {
            Suit = suit;
            Rank = rank;
            Value = CalculateValue(rank);
        }
        public int CalculateValue(int rank)
        {
            if (rank > 10) return 10;
            else return rank;
                 
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

        public int[] GetCardInfo()
        {
            int[] cardInfo = new int[3] {this.Rank, this.Suit, this.Value };
            return cardInfo;
        }
    }

    //1 - Ase
    //11 - Jack
    //12 - Queen
    //13 - King
}
