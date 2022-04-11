using System;
using DecksLibrary;
using BotsLibrary;

namespace BlackjackLibrary
{
    public class Dealer
    {
        public Decks PlayingDecks { get; private set; }
        public byte[] DealerCards { get; private set; } // dealers cards
        public byte BjFlag; // if player has blackjack

        public Dealer(Decks Cards, byte bjFlag = 0)
        {
            PlayingDecks = new Decks();
            PlayingDecks = Cards;
            DealerCards = new byte[21];
            this.BjFlag = bjFlag;
        }

        public void SetFirstCard()
        {
            for (int i = 0; i < 21; i++)
            {
                DealerCards[i] = 0;
            }
            DealerCards[0] = PlayingDecks.GetCard();
        }

        public byte DealersTurn(uint playerScore)
        {
            uint sum = 0;
            uint position = 0;
            byte flag = 0;
            for (uint i = 0; i < 21; i++)
            {
                sum += DealerCards[i];
            }
            while (sum < 17)
            {
                for (uint i = 0; i < 21; i++)
                {
                    if (DealerCards[i] == 1)
                    {
                        flag = 1;
                    }
                    else if (DealerCards[i] == 0)
                    {
                        position = i;
                        break;
                    }
                }
                if (flag != 0 && sum < 17)
                {
                    if (sum + 10 == 21 && BjFlag != 0)
                    {
                        if (BjFlag == 2)
                            BjFlag = 0;
                        else
                            BjFlag -= 1;
                        if (DealerCards[2] == 0)
                        {
                            return 2;
                        }
                        else
                            return 0;
                    }
                    else if (sum + 10 == 21 && BjFlag == 0 && playerScore == 21)
                    {
                        if (DealerCards[2] == 0)
                            return 1;
                        else
                            return 2;
                    }
                    if (sum + 10 >= playerScore && sum + 10 <= 21)
                    {
                        sum += 10;
                        if (sum > 17)
                            break;
                    }
                }
                Hit();
                sum += DealerCards[position];
            }
            if (sum < playerScore || (sum > 21 && playerScore != 0))
            {
                return 0;
            }
            else if (sum > playerScore)
            {
                return 1;
            }
            return 2;
        }

        private void Hit()
        {
            int i = 0;
            while (DealerCards[i] != 0)
                i++;
            DealerCards[i] = PlayingDecks.GetCard();
        }
    }
}
