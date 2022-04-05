using System;
using System.Collections.Generic;

namespace Blackjack
{
    internal class ShufflingMachine
    {
        internal int Length { get; private set; }
        internal uint WhenToShuffle { get; private set; }   // When numOfCards <= WhenToShuffle at the end of the game, the Shuffle method will be called
        
        private List<Deck> shoe = new List<Deck>();
        private int numOfCards;
        private int numOfDecks;

        internal ShufflingMachine(int numOfDecks, uint whenToShuffle)
        {
            if (numOfDecks <= 0)
            {
                Console.WriteLine("The number of decks must be positive integer. One deck will be loaded in shuffling machine");

                numOfDecks = 1;
            }
            else if (whenToShuffle >= numOfDecks * 52)
            {
                Console.WriteLine($"The number of cards need to left to shuffle must be less than the number of cards in shuffling machine. numOfCardsToShuffle will be set to {whenToShuffle / 3}");

                whenToShuffle = whenToShuffle / 3;
            }

            this.numOfDecks = numOfDecks;
            this.WhenToShuffle = whenToShuffle;

            Update();
        }

        internal byte GetBlackjackCardWeight()
        {
            return ConvertCardWeightForBlackjack(TakeCard());
        }

        internal Card TakeCard()
        {
            int index = RandomInt.RandInt(this.Length);

            Card card = this.shoe[index].TakeCard();
            this.numOfCards--;

            if (this.shoe[index].Length == 0)
            {
                this.shoe.RemoveAt(index);
                this.Length--;
            }

            return card;
        }

        internal void Shuffle()
        {
            if (this.numOfCards <= this.WhenToShuffle)
            {
                Update();
            }
        }

        private void Update()
        {
            this.numOfCards = 52 * numOfDecks;
            this.Length = this.numOfDecks;
            this.shoe = new List<Deck>(this.Length);

            for (int i = 0; i < this.Length; i++)
            {
                this.shoe.Add(new Deck());
            }
        }

        private byte ConvertCardWeightForBlackjack(Card card)
        {
            byte weight = card.Weight;

            if (weight < 11)
            {
                return weight;
            }
            else if (weight < 14)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }
    }
}

