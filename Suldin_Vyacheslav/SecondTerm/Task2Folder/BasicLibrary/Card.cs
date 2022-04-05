using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BasicLibrary
{
    
    
    public class Card
    {
        private int suit;
        private int rank;
        private int value;

        public Card(CardSuit suit, CardRank rank)
        {
            this.suit = (int)suit + 1;
            this.rank = (int)rank + 1;
            value = CalculateValue(this.rank);
        }
        public int CalculateValue(int rank)
        {
            if (rank > 10) return 10;
            else return rank;

        }
        public static bool operator ==(Card firstCard, Card secondCard)
        {
            if (firstCard.rank != secondCard.rank ||
                firstCard.suit != secondCard.suit ||
                firstCard.value != secondCard.value)
                return false;
            else return true;


        }
        public static bool operator !=(Card firstCard, Card secondCard)
        {
            return !(firstCard == secondCard);
        }

        public int[] GetCardInfo()
        {
            int[] cardInfo = new int[3] { this.rank, this.suit, this.value };
            return cardInfo;
        }
    }
}
