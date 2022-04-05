using System;
using DecksLibrary;
using BotsLibrary;

namespace BlackjackLibrary
{
    public class Dealer
    {
        public Decks PlayingDecks { get; private set; }
        public byte[] DealerCards { get; private set; } // dealers cards
        public byte[] DealerCardsCopy { get; private set; } // for correct testing
        private uint InitialMoney; // initial balance of player
        private Bots PlayerStrategy; // what strategy player uses
        private byte BjFlag; // if player has blackjack
        private byte SurrenderFlag;
        private uint FirstAmount; // players score in first hand
        private uint SecondAmount; // players score in secon hand
        private int FirstWager; // players wager in first hand after his turn
        private int SecondWager; // players wager in second hand after his turn

        public Dealer(Decks Cards, uint initialMoney, Bots strategy = 0, byte bjFlag = 0)
        {
            PlayingDecks = new Decks();
            PlayingDecks = Cards;
            DealerCards = new byte[21];
            PlayerStrategy = strategy;
            InitialMoney = initialMoney;
            BjFlag = bjFlag;
        }

        public int Game()
        {
            DealerCards[0] = PlayingDecks.GetCard();
            byte firstResult, secondResult;
            int sum = 0; 
            uint wager = 16; // we choose the initial wager for player
            if (PlayerStrategy == Bots.BasicStrategy)
            {
                Player player = new Player(DealerCards[0], InitialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                FirstAmount = player.FirstSum;
                SecondAmount = player.SecondSum;
                SurrenderFlag = player.SurrFlag;
                BjFlag = player.BjFlag;
                player.GetWagers(ref FirstWager, ref SecondWager);
            }
            else if (PlayerStrategy == Bots.CardsCounterStrategy)
            {
                CardsCounterStrategy player = new CardsCounterStrategy(DealerCards[0], InitialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                FirstAmount = player.FirstSum;
                SecondAmount = player.SecondSum;
                SurrenderFlag = player.SurrFlag;
                BjFlag = player.BjFlag;
                player.GetWagers(ref FirstWager, ref SecondWager);
            }
            else
            {
                SimpleStrategy player = new SimpleStrategy(DealerCards[0], InitialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                FirstAmount = player.FirstSum;
                SecondAmount = player.SecondSum;
                SurrenderFlag = player.SurrFlag;
                BjFlag = player.BjFlag;
                player.GetWagers(ref FirstWager, ref SecondWager);
            }
            // Console.Write("Players result - {0} {1} ", first_amount, second_amount);
            InitialMoney = (uint)(InitialMoney - (int)(FirstWager + SecondWager)); // that is players balance after his turn
            DealerCardsCopy = new byte[21];
            if (SurrenderFlag == 1)
            {
                for (int i = 0; i < 21; i++)
                {
                    DealerCardsCopy[i] = DealerCards[i];
                }
                InitialMoney += (uint)FirstWager;
                return FirstWager;
            }
            firstResult = DealersTurn(FirstAmount);
            if (SecondAmount != 0)
            {
                secondResult = DealersTurn(SecondAmount);
                if (secondResult == 1) // If dealer wins
                {
                    SecondWager = (-1) * SecondWager;
                }
                else if (secondResult == 0) // If player wins
                {
                    SecondWager = SecondWager * 2;
                }
                sum += SecondWager; // If we have draw than we just add player wagers to sum
            }
            // Console.WriteLine();
            if (firstResult == 1)
            {
                FirstWager = (-1) * FirstWager;
            }
            else if (firstResult == 0)
            {
                FirstWager = FirstWager * 2;
            }
            sum += FirstWager;
            InitialMoney = (uint)((int)InitialMoney + sum); // that is players balance after one game
            for (int i = 0; i < 21; i++)
            {
                DealerCardsCopy[i] = DealerCards[i];
                DealerCards[i] = 0;
            }
            return sum;
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
