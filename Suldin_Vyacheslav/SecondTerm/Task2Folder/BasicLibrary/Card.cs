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
        private int Suit;
        private int Rank;
        private int Value;

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = (int)suit + 1;
            Rank = (int)rank + 1;
            Value = CalculateValue(Rank);
        }
        public int CalculateValue(int rank)
        {
            if (rank > 10) return 10;
            else return rank;

        }
        public static bool operator ==(Card firstCard, Card secondCard)
        {
            if (firstCard.Rank != secondCard.Rank ||
                firstCard.Suit != secondCard.Suit ||
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
            int[] cardInfo = new int[3] { this.Rank, this.Suit, this.Value };
            return cardInfo;
        }
    }
}
